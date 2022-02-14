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
    /// Interaction logic for WinSponsor.xaml
    /// </summary>
    ///

    public class RunnerDS 
    {
        public string Firstname { get; set; }
        public string Lastname{ get; set; }
        public Nullable<short> Bib { get; set; }
        public string CountryCode { get; set; }
        public override string ToString()
        {

            return string.Format("{0}, {1} - {2} ({3})", Firstname, Lastname,
               Bib,
                CountryCode);
        }
    }
    public partial class WinSponsor : Window
    {
        public WinSponsor()
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            using (var db = new MarathonDBEntities())
            {
                runner.ItemsSource = db.Runners.Select(s => new RunnerDS() { 
                    Bib = s.Registrations.FirstOrDefault().RegistrationEvents.FirstOrDefault().BibNumber ?? null,
                    Firstname =s.User.FirstName, Lastname =s.User.LastName,
                    CountryCode = s.CountryCode
                }).ToList();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFullName.Text.Trim();
            string cardNumber = txtCreditCard.Text.Trim();
            string nameOnCard = txtNameOnCard.Text.Trim();
            string expiryM = exDateM.Text.Trim();
            string expiryY = exDateY.Text.Trim();
            string amount = txtAmount.Text.Trim();
            string cvc = txtCVC.Text.Trim();



            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(cardNumber)
                || string.IsNullOrEmpty(expiryM)
                || string.IsNullOrEmpty(nameOnCard)
                || string.IsNullOrEmpty(expiryY)
                || string.IsNullOrEmpty(cvc)
                || string.IsNullOrEmpty(amount))
            {
                MessageBox.Show("All the fields are required");
                return;
            }

            RunnerDS runnerDS = null;
            try
            {
                runnerDS = (RunnerDS)runner.SelectedItem;
                if (runnerDS == null)
                {
                    throw new Exception("All the fields are required");
                }

                if(cvc.Length != 3)
                {
                    throw new Exception("CSV must be three digitss");

                }

                double a = double.Parse(amount);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            if(cardNumber.Length != 16)
            {
                MessageBox.Show("Credit card length should be 16");
                return;
            }

            int month = 0;
            int year = 0;
            try
            {
                month = int.Parse(expiryM);
                year = int.Parse(expiryY);

                if(new DateTime(year,month,DateTime.Today.Day) < DateTime.Today)
                {
                    throw new Exception("Invalid Date");
                }
            }
            catch
            {
                MessageBox.Show("Invalid Date");
                return;
            }

            Charity ch = null;
            using (var db = new MarathonDBEntities())
            {
                ch = db.Charities.FirstOrDefault(s=>s.CharityId == 13);
            }
            WinConfirm winConfirm = new WinConfirm(
                String.Format("{0} {1} ({2}) from {3}", runnerDS.Firstname,
                runnerDS.Lastname, runnerDS.Bib, runnerDS.CountryCode),
                ch.CharityName,
                "$" + amount
                );
            winConfirm.Show();
            Hide();
        }

        private void txtAmount_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(txtAmount.Text);
                amount += 10;
                txtAmount.Text = amount + "";

            }
            catch
            {

            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(txtAmount.Text);
                amount -= 10;
                if(amount < 0)
                {
                    amount = 0;
                }
                txtAmount.Text = amount + "";

            }
            catch
            {

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WinCharityInfo win = new WinCharityInfo(13);
            win.ShowDialog();
        }
    }
}
