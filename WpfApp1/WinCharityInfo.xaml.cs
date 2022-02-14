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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WinCharityInfo.xaml
    /// </summary>
    public partial class WinCharityInfo : Window
    {
        public WinCharityInfo(int charityID)
        {
            InitializeComponent();

            using (var db = new MarathonDBEntities())
            {
                var ch = db.Charities.FirstOrDefault(s => s.CharityId == charityID);
                lblName.Content = ch.CharityName;
                lblDescription.Text = ch.CharityDescription;
            }
        }
    }
}
