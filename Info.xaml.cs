using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Page
    {
        private ICollectionView view1 = null;
        private ICollectionView view2 = null;
        private ICollectionView view3 = null;
        private ICollectionView view4 = null;
        Data Data;
        public Info()
        {
            InitializeComponent();
        }

        //Refreshes the data in the DataGrid in the UI - Info 
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

        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class
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

        private void InfoClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("In this window you can see/delete all the information about the current schedule." +
                " click Yes to see the full documentation", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("https://docs.google.com/document/d/1-gEMkTd8JQxsS0qG9tEmxP3307A95BpPU-DL_9jQS70/view?pli=1#heading=h.nv0s9cpso0p5");
            }
        }
    }
}
