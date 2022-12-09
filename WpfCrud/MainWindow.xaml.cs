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
            InsertPage IPage = new InsertPage();
            IPage.ShowDialog();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            int Id = (myDataGrid.SelectedItems as member).id;

            UpdatePage updatePage = new UpdatePage(Id);
            updatePage.ShowDialog();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var memberId = (myDataGrid.SelectedItems as member).id;

            var member = _db.members.FirstOrDefault(m => m.id == memberId);
            if (member is null)
            {
                MessageBox.Show($"member with id {memberId} is not found");
            }

            _db.members.Remove(member);
            _db.SaveChanges();

            MainWindow.dataGrid.ItemsSource = _db.members.ToList();
        }
    }
}
