using Mysqlx.Crud;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp
{
    /// <summary>
    /// This class represents the data in the system, that is used by the algorithms and shown in the UI.
    /// </summary>
    ///  <DataGridTextColumn Header="Available Times" Binding="{Binding MeetingTimesPerTeacher}"/>
    public partial class Info : Page
    {
        private ICollectionView view1 = null;
        private ICollectionView view2 = null;
        private ICollectionView view3 = null;
        private ICollectionView view4 = null;
        private ICollectionView view5 = null;
        public ObservableCollection<string> InstructorsPerCourseString { get; set; }


        public Info()
        {
            InitializeComponent();
        }

        //turns each data.Courses1 instructor into a row in the DataGrid in the UI - Info           

        //Refreshes the data in the DataGrid in the UI - Info 
        public void UpdateData(Data data)
        {
            MainProgram.data = data;
            view1 = CollectionViewSource.GetDefaultView(data.Courses1);
            view2 = CollectionViewSource.GetDefaultView(data.Rooms);
            view3 = CollectionViewSource.GetDefaultView(data.Instructors);
            view4 = CollectionViewSource.GetDefaultView(data.MeetingTimes);
            InstructorsPerCourseString = new ObservableCollection<string>();

            InsertInstructorsPerCourse(data);
            view5 = CollectionViewSource.GetDefaultView(InstructorsPerCourseString);

            DataGrid1.ItemsSource = data.Courses1;
            DataGrid2.ItemsSource = data.Rooms;
           
            DataGrid3.ItemsSource = data.Instructors;
            DataGrid4.ItemsSource = data.MeetingTimes;

            DataGrid5.ItemsSource = InstructorsPerCourseString;

            view1.Refresh();
            view2.Refresh();
            view3.Refresh();
            view4.Refresh();
            view5.Refresh();
        }

        private void InsertInstructorsPerCourse(Data data)
        {
            foreach (var item in data.Courses1)
            {
                var temp = "{Course: " + item.name + "}\n";
                foreach (var item2 in item.GetInstructors())
                {
                    temp += item2.ToString();
                    InstructorsPerCourseString.Add(temp);
                    temp = "{Course: " + item.name + "}\n";
                }
            }
        }

        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class
        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Courses1.Delete(item as Courses);
            DataGrid1.ItemsSource = MainProgram.data.Courses1;
            view1.Refresh();
        }

        private void DeleteRoom(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Rooms.Delete(item as Classrooms);
            DataGrid2.ItemsSource = MainProgram.data.Rooms;
            view2.Refresh();
        }

        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class and all of its references in the Courses that use it
        private void DeleteInstructor(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Instructors.Delete(item as Teachers);
            DataGrid3.ItemsSource = MainProgram.data.Instructors;
            view3.Refresh();
        }
        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class and all of its references in the Instructors that use it
        private void DeleteMeetingTime(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.MeetingTimes.Delete(item as DateRange);
            DataGrid4.ItemsSource = MainProgram.data.MeetingTimes;
            view4.Refresh();
        }

        //not a good way to do this, but it works
        private void DeleteInstructorPerCourse(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext;
            var fullString = item.ToString().Split('\n');
            var InstructorString = fullString[1];
            var CourseString = fullString[0].Split(':')[1];
            CourseString = CourseString.Substring(1, CourseString.Length - 2);
            foreach (var item2 in MainProgram.data.Courses1)
            {
                if (item2.name == CourseString)
                {
                    foreach (var item3 in item2.GetInstructors())
                    {
                        if (item3.ToString() == InstructorString)
                        {
                            MainProgram.data.Courses1.GetAt(MainProgram.data.Courses1.IndexOf(item2)).GetInstructors().Delete(item3);
                        }
                    }
                }
            }
            InstructorsPerCourseString.Remove(item as string);
            DataGrid5.ItemsSource = InstructorsPerCourseString;
            view5.Refresh();
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
