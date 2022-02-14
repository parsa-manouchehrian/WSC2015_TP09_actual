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
    /// Interaction logic for winFindOutMore.xaml
    /// </summary>
    public partial class winFindOutMore : Window
    {
        public winFindOutMore()
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            winChari main = new winChari();
            main.Show();
            Close();
        }
    }
}
