using CurshachCTO1;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace CurshachCTO1
{
    public partial class AddEditOrderWindow : Window
    {
        private CTOVEREntities context;
        public Orders Order { get; private set; }

        public AddEditOrderWindow(Orders order = null)
        {
            InitializeComponent();
            context = CTOVEREntities.GetContext();

            if (order == null)
            {
                Order = new Orders();
                context.Orders.Add(Order);
            }
            else
            {
                Order = order;
                context.Orders.Attach(Order);
            }

            DataContext = Order;
            LoadData();
        }

        private void LoadData()
        {
            ServiceComboBox.ItemsSource = context.Services.ToList();
            EmployeeComboBox.ItemsSource = context.Employees.ToList();
            StatusComboBox.ItemsSource = context.RequestStatus.ToList();
            ServiceComboBox.SelectionChanged += ServiceComboBox_SelectionChanged;

            ApplicationNumberTextBox.Text = Order.ApplicationNumber.ToString();
            ClientTextBox.Text = Order.Clients?.Fullname;
            CarTextBox.Text = Order.Cars?.Brand;
            CarModelTextBox.Text = Order.Cars?.Model;
            CarYearTextBox.Text = Order.Cars?.YearOfManufacture.ToString();
        }

        private void ServiceComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem is Services selectedService)
            {
                ServicePriceTextBox.Text = selectedService.Price.ToString();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Преобразование номера заявки из текста в число
                if (!int.TryParse(ApplicationNumberTextBox.Text, out int applicationNumber))
                {
                    MessageBox.Show("Номер заявки должен быть числом.");
                    return; // Прерываем выполнение, если номер заявки неверный
                }
                Order.ApplicationNumber = applicationNumber;

                // Устанавливаем текущую дату для заказа, если это новый заказ
                if (Order.RequestDate == null)
                {
                    Order.RequestDate = DateTime.Now; // Устанавливаем текущую дату и время
                }

                // Проверяем и создаем новый объект Cars, если его нет
                if (Order.Cars == null)
                {
                    Order.Cars = new Cars(); // Инициализируем Cars, если его нет
                }

                // Заполнение информации о машине
                if (string.IsNullOrWhiteSpace(Order.Cars.Brand))
                {
                    Order.Cars.Brand = CarTextBox.Text; // Присваиваем марку из текстового поля, если она не указана
                }

                if (string.IsNullOrWhiteSpace(Order.Cars.Model))
                {
                    Order.Cars.Model = CarModelTextBox.Text; // Присваиваем модель из текстового поля, если она не указана
                }

                // Преобразование года производства машины из текста в число
                if (int.TryParse(CarYearTextBox.Text, out int year))
                {
                    Order.Cars.YearOfManufacture = year;
                }
                else
                {
                    Order.Cars.YearOfManufacture = 0;  // Или можете оставить пустым, если поддерживаете nullable тип
                }

                // Проверяем и создаем нового клиента, если его нет
                if (Order.Clients == null)
                {
                    Order.Clients = new Clients
                    {
                        Fullname = ClientTextBox.Text,  // Присваиваем имя
                        Phone = PhoneTextBox.Text        // Присваиваем телефон
                    };
                    context.Clients.Add(Order.Clients); // Добавляем нового клиента в контекст
                }
                else
                {
                    Order.Clients.Fullname = ClientTextBox.Text;
                    Order.Clients.Phone = PhoneTextBox.Text;
                }

                // Заполнение других полей заказа
                Order.Services = ServiceComboBox.SelectedItem as Services;
                Order.Employees = EmployeeComboBox.SelectedItem as Employees;
                Order.RequestStatus = StatusComboBox.SelectedItem as RequestStatus;

                // Сохраняем изменения в базе данных
                context.SaveChanges();
                DialogResult = true;
            }
            catch (DbEntityValidationException ex)
            {
                // Обработка ошибки валидации
                string errorMessages = string.Join(Environment.NewLine, ex.EntityValidationErrors
                    .SelectMany(eve => eve.ValidationErrors)
                    .Select(ve => $"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}"));

                MessageBox.Show($"Ошибка валидации: {errorMessages}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }





        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
