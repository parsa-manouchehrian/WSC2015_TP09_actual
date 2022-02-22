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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinSponsor main = new WinSponsor();
            main.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            winFindOutMore main = new winFindOutMore();
            main.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WinLogin main = new WinLogin();
            main.Show();
            Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WinCheck main = new WinCheck();
            main.Show();
            Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var db = new MarathonDBEntities())
        //    {
        //        var fakes = db.FakeStaffs.ToList();

        //        foreach (var item in fakes)
        //        {
        //            string[] names = new string[2];
        //            if (item.Fullname.Contains("*"))
        //            {
        //                names = item.Fullname.Split('*');

        //            }
        //            else if (item.Fullname.Contains("#"))
        //            {
        //                names = item.Fullname.Split('#');

        //            }
        //            else
        //            {
        //                names = item.Fullname.Split('|');

        //            }

        //            Staff s = new Staff()
        //            {
        //                StaffId = item.ID,
        //                Firstname = names[0].Trim(),
        //                LastName = names[1].Trim(),
        //                DateOfBirth = item.DateOfBirth.Value,
        //                Email = item.EmailAddress,
        //                Gender = item.Gender.Contains("F") ? "Female" : "Male",
        //                PositionId = (short)item.PositionID.Value
        //            };

        //            db.Staffs.Add(s);

        //        }
        //            db.SaveChanges();

        //    }
        //}
    }
}
