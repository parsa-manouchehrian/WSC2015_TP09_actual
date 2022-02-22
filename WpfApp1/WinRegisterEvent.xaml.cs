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

    public class EventDS {

        public string Name{ get; set; }
        public string EventId { get; set; }
        public EventDS(Event s)
        {
            Name = string.Format("{0}(${1})", s.EventType.EventTypeName, s.Cost);
            EventId = s.EventId;
        }
    }

    public class OptionDS
    {

        public string Name { get; set; }
        public string optionId { get; set; }
        public OptionDS(RaceKitOption s)
        {
            Name = string.Format("Option {0} (${1}): {2}",
                s.RaceKitOptionId, s.Cost, s.RaceKitOption1);
            optionId = s.RaceKitOptionId;
        }
    }


    /// <summary>
    /// Interaction logic for WinRegisterEvent.xaml
    /// </summary>
    public partial class WinRegisterEvent : Window
    {
        public string Email { get; set; }

        public WinRegisterEvent(string email)
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Email = email;

            using (var db = new MarathonDBEntities())
            {
                var list = db.Events.ToList();
                var list2 = db.RaceKitOptions.ToList();
                comboChar.ItemsSource = db.Charities.ToList();
                List<EventDS> final = new List<EventDS>();
                List<OptionDS> final2 = new List<OptionDS>();

                foreach (var item in list)
                {
                    final.Add(new EventDS(item));   
                }
                dgEvents.ItemsSource = final;

                foreach (var item in list2)
                {
                    final2.Add(new OptionDS(item));
                }
                dfOptions.ItemsSource = final2;
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
            WinRunner main = new WinRunner(Email);
            main.Show();
            Close();
        }
    }
}
