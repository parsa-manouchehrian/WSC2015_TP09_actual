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
    /// Interaction logic for WinRunnerRegister.xaml
    /// </summary>
    public partial class WinRunnerRegister : Window
    {
        public WinRunnerRegister()
        {
            InitializeComponent();
            updateDate();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            using (var db = new MarathonDBEntities())
            {
                comboGender.ItemsSource = db.Genders.ToList();
                comboCountry.ItemsSource = db.Countries.ToList();
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
            WinCheck main = new WinCheck();
            main.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MarathonDBEntities())
            {
                try
                {
                    string email = txtEmail.Text.Trim();
                    
                    string pass = password.Password.Trim();
                    string rePass = rePassword.Password.Trim();
                    string firstname = txtFirstname.Text.Trim();
                    string lastname = txtLastname.Text.Trim();
                    if (birth.SelectedDate == null
                       || string.IsNullOrEmpty(email)
                       || string.IsNullOrEmpty(pass)
                       || string.IsNullOrEmpty(rePass)
                       || string.IsNullOrEmpty(firstname)
                       || string.IsNullOrEmpty(lastname)
                       || comboGender.SelectedItem == null
                       || comboCountry.SelectedItem == null)
                    {
                        throw new Exception("All the feild are required");
                    }

                    if(!email.Contains("@")
                        || !email.Contains(".")
                        || email.Length < 6)
                    {
                        throw new Exception("Email is invalid");
                    }

                    if (!pass.Equals(rePass))
                    {
                        throw new Exception("Password and again password should be same");
                    }

                    Gender gen = comboGender.SelectedItem as Gender;
                    Country country = comboCountry.SelectedItem as Country;

                   
                    DateTime dtBirth = birth.SelectedDate.Value;
                    if(dtBirth.AddYears(10) > DateTime.Now)
                    {
                        throw new Exception("Runner should be at least 10 years old");
                    }

                    if (pass.Length < 6
                        || !pass.Any(s => char.IsUpper(s))
                        || !pass.Any(s => char.IsNumber(s))
                        || !pass.Any(s => "!@#$%^".Contains(s)))
                    {
                        throw new Exception("The password is invalid");
                    }

                    User us = new User()
                    {
                        Email = email,
                        FirstName = firstname,
                        LastName = lastname,
                        Password = pass,
                        RoleId = "R"
                    };

                    us.Runners.Add(new Runner() {
                        Email = email,
                        Gender = gen.Gender1,
                        DateOfBirth = dtBirth,
                        CountryCode = country.CountryCode
                    });

                    db.Users.Add(us);
                    db.SaveChanges();
                    MessageBox.Show("Submitted Successfully");
                    WinLogin login = new WinLogin();
                    login.Show();
                    Close();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
    }
}
