using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CurshachCTO1
{
    public partial class EditOrder : Window
    {
        private CTOVEREntities context;
        private Orders Order;

        public EditOrder(Orders order)
        {
            InitializeComponent();
            context = CTOVEREntities.GetContext();
            Order = order;
          

            // Заполняем поля данными из выбранной заявки
            LoadData();
        }


        private void LoadData()
        {
            ApplicationNumberTextBox.Text = Order.ApplicationNumber.ToString();
            RequestDateTextBox.Text = Order.RequestDate.HasValue
    ? Order.RequestDate.Value.ToString("dd.MM.yyyy")
    : string.Empty;
            ClientTextBox.Text = Order.Clients.Fullname;
            PhoneTextBox.Text = Order.Clients.Phone;
            CarBrandTextBox.Text = Order.Cars.Brand;
            CarModelTextBox.Text = Order.Cars.Model;
            CarYearTextBox.Text = Order.Cars.YearOfManufacture.ToString();
            ServiceComboBox.ItemsSource = context.Services.ToList(); // Загружаем услуги
            EmployeeComboBox.ItemsSource = context.Employees.ToList(); // Загружаем работников
            StatusComboBox.ItemsSource = context.RequestStatus.ToList();



            ServiceComboBox.SelectedItem = Order.Services;
            EmployeeComboBox.SelectedItem = Order.Employees;
            StatusComboBox.SelectedItem = Order.RequestStatus;
        }

        private void BtnEdit1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка и обновление данных заказа
                if (int.TryParse(ApplicationNumberTextBox.Text, out int applicationNumber))
                {
                    Order.ApplicationNumber = applicationNumber;
                }
                else
                {
                    MessageBox.Show("Неверный формат номера заявки.");
                    return;
                }

                if (DateTime.TryParse(RequestDateTextBox.Text, out DateTime requestDate))
                {
                    Order.RequestDate = requestDate;
                }
                else
                {
                    MessageBox.Show("Неверный формат даты заявки.");
                    return;
                }

                // Обновление данных клиента
                if (Order.Clients != null)
                {
                    Order.Clients.Fullname = ClientTextBox.Text;
                    Order.Clients.Phone = PhoneTextBox.Text;
                }

                // Обновление данных машины
                if (Order.Cars != null)
                {
                    Order.Cars.Brand = CarBrandTextBox.Text;
                    Order.Cars.Model = CarModelTextBox.Text;
                    if (int.TryParse(CarYearTextBox.Text, out int carYear))
                    {
                        Order.Cars.YearOfManufacture = carYear;
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат года выпуска.");
                        return;
                    }
                }

                // Проверка на выбор услуги, работника и статуса
                if (ServiceComboBox.SelectedItem != null)
                {
                    Order.Services = ServiceComboBox.SelectedItem as Services;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите услугу.");
                    return;
                }

                if (EmployeeComboBox.SelectedItem != null)
                {
                    Order.Employees = EmployeeComboBox.SelectedItem as Employees;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите работника.");
                    return;
                }

                if (StatusComboBox.SelectedItem != null)
                {
                    Order.RequestStatus = StatusComboBox.SelectedItem as RequestStatus;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите статус заявки.");
                    return;
                }

                // Сохраняем изменения в базе данных
                context.SaveChanges();

                // Закрываем окно и уведомляем пользователя
                MessageBox.Show("Заявка успешно обновлена.");
                DialogResult = true;  // Закрываем окно с результатом "true"
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие окна без изменений
            DialogResult = false;
        }
    }
}
