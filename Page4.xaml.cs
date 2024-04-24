using Genetic;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        private ICollectionView view1 = null;
        private ICollectionView view2 = null;
        private ICollectionView view3 = null;
        private ICollectionView view4 = null;
        Data Data;
        public Page4()
        {
            InitializeComponent();
        }

        public void UpdateData(Data data)
        {
            Data = data;
            view1 = CollectionViewSource.GetDefaultView(data.Courses1);
            view2 = CollectionViewSource.GetDefaultView(data.Rooms);
            view3 = CollectionViewSource.GetDefaultView(data.Instructors);
            view4 = CollectionViewSource.GetDefaultView(data.MeetingTimes);

            DataGrid1.ItemsSource = data.Courses1;
            DataGrid2.ItemsSource = data.Rooms;
            DataGrid3.ItemsSource = data.Instructors;
            DataGrid4.ItemsSource = data.MeetingTimes;

            view1.Refresh();
            view2.Refresh();
            view3.Refresh();
            view4.Refresh();
        }

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            Data.Courses1.Delete(item as Courses);
            DataGrid1.ItemsSource = Data.Courses1;
            view1.Refresh();
        }

        private void DeleteRoom(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            Data.Rooms.Delete(item as Classrooms);
            DataGrid2.ItemsSource = Data.Rooms;
            view2.Refresh();
        }
        
        private void DeleteInstructor(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            Data.Instructors.Delete(item as Teachers);
            DataGrid3.ItemsSource = Data.Instructors;
            view3.Refresh();
        }
        private void DeleteMeetingTime(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            Data.MeetingTimes.Delete(item as DateRange);
            DataGrid4.ItemsSource = Data.MeetingTimes;
            view4.Refresh();
        }   
    }
}
