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

namespace WpfCrud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private wpfCrudEntities _db = new wpfCrudEntities();
        public static DataGrid dataGrid;
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            myDataGrid.ItemsSource = _db.members.ToList();
            dataGrid = myDataGrid;
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            var memberId = GetSelectedMemberId();
            if (memberId != 0)
            {
                MessageBox.Show("Can't click Insert button here");
                return;
            }

            InsertPage IPage = new InsertPage();
            IPage.ShowDialog();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            var memberId = GetSelectedMemberId();

            UpdatePage updatePage = new UpdatePage(memberId);
            if (memberId == 0)
            {
                return;
            }
            updatePage.ShowDialog();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var memberId = GetSelectedMemberId();

            var member = _db.members.FirstOrDefault(m => m.id == memberId);
            if (member is null)
            {
                MessageBox.Show($"member with id {memberId} is not found");
                return;
            }

            _db.members.Remove(member);
            _db.SaveChanges();

            MainWindow.dataGrid.ItemsSource = _db.members.ToList();
        }

        private int GetSelectedMemberId()
        {
            var selectedMember = myDataGrid.SelectedItem as member;

            if (selectedMember != null)
            {
                var memberId = selectedMember.id;

                return memberId;
            }

            return default;
        }
    }
}
