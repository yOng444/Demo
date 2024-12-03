using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CurshachCTO1
{
    public partial class AuthorizationWindow : Window
    {
        public string UserRole { get; private set; }

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            
            var context = CTOVEREntities.GetContext();

            
            var user = context.Users
                              .FirstOrDefault(u => u.Username == username && u.Password == password); 

            if (user != null)
            {
                
                UserRole = user.Roles.RoleName;  
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }
    }
}