using CurshachCTO1;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CurshachCTO1
{
    public partial class MainWindow : Window
    {
        private string userRole;
        private CTOVEREntities context;

        public MainWindow()
        {
            InitializeComponent();
            context = CTOVEREntities.GetContext();
            LoadData();
            UpdateCompletedOrdersCount();
            LoadStatusFilter();
            AuthorizeUser();
        }

        private void OnOrderUpdated()
        {
            UpdateCompletedOrdersCount();
        }
        private void BtnOut_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, был ли выбран элемент в ComboBox
            if (ComboStatus.SelectedIndex != -1)
            {
                ComboStatus_SelectionChanged(sender, null); // Вызов фильтрации, если выбран статус
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите статус для фильтрации.");
            }
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            var filteredOrders = context.Orders
                .Include("Clients")
                .Include("Cars")
                .Include("Services")
                .Include("Employees")
                .Include("RequestStatus")
                .Where(o => o.Clients.Fullname.ToLower().Contains(searchText) ||
                            o.Cars.Brand.ToLower().Contains(searchText) ||
                            o.Cars.Model.ToLower().Contains(searchText))
                .ToList();

            CTO.ItemsSource = filteredOrders;
        }

        private void AuthorizeUser()
        {
            var authWindow = new AuthorizationWindow();
            if (authWindow.ShowDialog() == true)
            {
                userRole = authWindow.UserRole;
                SetPermissions(userRole);
            }
            else
            {
                Close();
            }
        }
        private void SetPermissions(string role)
        {
            BtnAdd.IsEnabled = role == "Admin" || role == "User";
            BtnDelet.IsEnabled = role == "Admin" || role == "Worker";
            foreach (var item in CTO.Items)
            {
                var row = (DataGridRow)CTO.ItemContainerGenerator.ContainerFromItem(item);
                if (row != null)
                {
                    var editButton = (Button)CTO.Columns[10].GetCellContent(row);
                    if (editButton != null)
                    {
                        editButton.IsEnabled = role == "Admin" || role == "Worker";
                    }
                }
            }
        }

        private Button GetButtonFromRow(DataGridRow row)
        {
           
            var buttonCell = CTO.Columns[10].GetCellContent(row) as Button;
            return buttonCell;
        }


        private void BtnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            var authWindow = new AuthorizationWindow();
            authWindow.Show();
            this.Close();
            AuthorizeUser();
        }


        // Загрузка данных заказов в DataGrid
        private void LoadData()
        {
            var orders = context.Orders
                .Include("Clients")
                .Include("Cars")
                .Include("Services")
                .Include("Employees")
                .Include("RequestStatus")
                .ToList();
            CTO.ItemsSource = orders;
        }

        // Загрузка статусов в фильтр
        private void LoadStatusFilter()
        {
            ComboStatus.ItemsSource = context.RequestStatus.ToList();
            ComboStatus.SelectedIndex = -1;
        }

        // Обработчик для кнопки добавления
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddEditOrderWindow();
            if (addOrderWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        // Обработчик для кнопки обновления
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            
        }

        // Обработчик для кнопки удаления
        private void BtnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (CTO.SelectedItem is Orders selectedOrder)
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранный заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    context.Orders.Remove(selectedOrder);
                    context.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления.");
            }
        }

        // Обработчик для фильтрации по статусу
        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный статус из ComboBox
            string selectedStatusName = ComboStatus.SelectedValue as string;

            if (string.IsNullOrEmpty(selectedStatusName))
            {
                LoadData(); // Показать все заказы, если статус не выбран
            }
            else
            {
                var filteredOrders = context.Orders
                    .Include("Clients")
                    .Include("Cars")
                    .Include("Services")
                    .Include("Employees")
                    .Include("RequestStatus")
                    .Where(o => o.RequestStatus.StatusName == selectedStatusName)
                    .ToList();

                CTO.ItemsSource = filteredOrders;
            }
        }

        private void UpdateCompletedOrdersCount()
        {
            // Получаем контекст данных
            var context = CTOVEREntities.GetContext();

            // Считаем количество завершенных заказов
            int completedOrdersCount = context.Orders
                .Where(order => order.RequestStatus.StatusName == "Завершено")
                .Count();

            // Устанавливаем значение в TextBox
            Box.Text = completedOrdersCount.ToString();
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            ComboStatus.SelectedIndex = -1; // Сбрасываем выбранный элемент в ComboBox
            LoadData(); // Загружаем все данные без фильтрации
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CTO.SelectedItem is Orders selectedOrder)
            {
                // Открываем окно редактирования, передаем выбранный заказ для редактирования
                var editOrderWindow = new EditOrder(selectedOrder);

                // Ожидаем, что окно редактирования вернёт DialogResult в случае сохранения
                if (editOrderWindow.ShowDialog() == true)
                {
                    // После сохранения в окне редактирования, обновляем DataGrid
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для редактирования.");
            }
        }

    }

}






