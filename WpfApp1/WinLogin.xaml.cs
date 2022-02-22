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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MarathonDBEntities())
            {
                var user = db.Users.FirstOrDefault(s => s.Email.Equals(txtEmail.Text.Trim())
                && s.Password.Equals(txtPassword.Password.Trim()));

                switch (user.RoleId)
                {
                    case "A":
                        WinAdmin winAdmin = new WinAdmin();
                        winAdmin.Show();
                        Close();
                        break;
                    case "C":
                        WinCoordinator winCoordinator = new WinCoordinator();
                        winCoordinator.Show();
                        Close();
                        break;
                    case "R":
                        WinRunner winRunner = new WinRunner(user.Email);
                        winRunner.Show();
                        Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void updateDate()
        {
            DateTime dt = new DateTime(2022, 9, 5, 6, 0, 0, 0);
            var differ = dt.Subtract(DateTime.Now);
            lblTimer.Content = string.Format("{0} days {1} hours and {2} minutes until the race starts",
                differ.Days, differ.Hours, differ.Minutes);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            updateDate();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
