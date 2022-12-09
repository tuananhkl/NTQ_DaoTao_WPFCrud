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

namespace WpfCrud
{
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        private wpfCrudEntities _db = new wpfCrudEntities();
        public InsertPage()
        {
            InitializeComponent();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametextBox.Text))
            {
                MessageBox.Show("You have to enter value for member's name");
                return;
            }

            if (string.IsNullOrWhiteSpace(gendercomboBox.Text))
            {
                MessageBox.Show("You have to select value for gender");
                return;
            }

            var member = new member
            {
                name = nametextBox.Text,
                gender = gendercomboBox.Text
            };

            _db.members.Add(member);
            _db.SaveChanges();
            MainWindow.dataGrid.ItemsSource = _db.members.ToList();
            this.Hide();
        }
    }
}
