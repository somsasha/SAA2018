using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SAA2018.Models;
using System.Data.Entity;

namespace SAA2018
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DBL dbl;
        public LoginWindow(DBL d)
        {
            InitializeComponent();
            dbl = d;
            this.Closed += LogInWindow_Closed;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Show();
            NewUniversity_Event();
            this.Close();
        }

        public delegate void EventHandler();
        public event EventHandler NewUniversity_Event;

        private void Login()
        {
            if (dbl.LogIn(LoginBox.Text, PasswordBox.Password))
            {
                App.Current.MainWindow.Show();
                this.Close();
            }
            else
            {
                PasswordBox.Clear();
                LogInError.Visibility = Visibility.Visible;
            }
        }

        private void LogInWindow_Closed(object sender, EventArgs e)
        {
            if (App.Current.MainWindow.Visibility == Visibility.Hidden)
                App.Current.MainWindow.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }
    }
}