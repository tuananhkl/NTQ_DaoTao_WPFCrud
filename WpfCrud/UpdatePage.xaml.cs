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
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        private wpfCrudEntities _db = new wpfCrudEntities();
        private int Id;

        public UpdatePage(int memberId)
        {
            InitializeComponent();

            if (memberId == 0)
            {
                MessageBox.Show("You can't update member doesn't exist");
                return;
            }
            else
            {
                Id = memberId;
                PopulateData();
            }
        }

        private void PopulateData()
        {
            var updatedMember = _db.members.FirstOrDefault(m => m.id == Id);
            // if (updatedMember is null)
            // {
            //     MessageBox.Show($"member with id {Id} is not found.");
            //     return;
            // }

            nametextBox.Text = updatedMember.name;
            gendercomboBox.Text = updatedMember.gender;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametextBox.Text))
            {
                MessageBox.Show($"You have to enter member name");
                return;
            }

            var updatedMember = _db.members.FirstOrDefault(m => m.id == Id);
            if (updatedMember is null)
            {
                MessageBox.Show($"member with id {Id} is not found.");
                return;
            }

            // var memberInput = new member
            // {
            //     name = nametextBox.Text,
            //     gender = gendercomboBox.Text
            // };

            updatedMember.name = nametextBox.Text;
            updatedMember.gender = gendercomboBox.Text;

            _db.SaveChanges();
            MainWindow.dataGrid.ItemsSource = _db.members.ToList();
            this.Hide();
        }
    }
}
