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
    /// Interaction logic for WinRunner.xaml
    /// </summary>
    public partial class WinRunner : Window
    {
        public string Email { get; set; }
        public WinRunner(string email)
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Email = email;
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void contact_Click(object sender, RoutedEventArgs e)
        {
            WinContactInfo win = new WinContactInfo();
            win.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinEditProfile profile = new WinEditProfile(Email);
            profile.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            WinRegisterEvent winRegisterEvent = new WinRegisterEvent(Email);
            winRegisterEvent.Show();
            Close();
        }
    }
}
