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

    public partial class Info : Page
    {
        private ICollectionView view1, view2, view3, view4, view5, view6 = null;
        public ObservableCollection<string> InstructorsPerCourseString { get; set; }
        public ObservableCollection<string> HoursPerTeacherString { get; set; }

        public Info()
        {
            InitializeComponent();
        }        

        //Refreshes the data in the DataGrid in the UI - Info 
        public void UpdateData(Data data)
        {
            MainProgram.data = data;
            view1 = CollectionViewSource.GetDefaultView(data.Courses1);
            view2 = CollectionViewSource.GetDefaultView(data.Rooms);
            view3 = CollectionViewSource.GetDefaultView(data.Instructors);
            view4 = CollectionViewSource.GetDefaultView(data.MeetingTimes);
            InstructorsPerCourseString = new ObservableCollection<string>();
            HoursPerTeacherString = new ObservableCollection<string>();

            InsertInstructorsPerCourse(data);
            view5 = CollectionViewSource.GetDefaultView(InstructorsPerCourseString);

            InsertHoursPerTeacher(data);
            view6 = CollectionViewSource.GetDefaultView(HoursPerTeacherString);

            UpdateDataGrid(data);
        }

        private void UpdateDataGrid(Data data)
        {
            DataGrid1.ItemsSource = data.Courses1;
            DataGrid2.ItemsSource = data.Rooms;

            DataGrid3.ItemsSource = data.Instructors;
            DataGrid4.ItemsSource = data.MeetingTimes;

            DataGrid5.ItemsSource = InstructorsPerCourseString;
            DataGrid6.ItemsSource = HoursPerTeacherString;

            view1.Refresh();
            view2.Refresh();
            view3.Refresh();
            view4.Refresh();
            view5.Refresh();
            view6.Refresh();
        }

        //not a good way to do this, but it works :)
        private void InsertHoursPerTeacher(Data data)
        {
            foreach (var Course in data.Courses1)
            {            
                foreach (var Instructor in Course.GetInstructors())
                {
                    foreach (var MeetingTimes in Instructor.GetMeetingTimesPerTeacher())
                    {
                        var temp = "{Teacher: " + Instructor.Name + "}\n";
                        temp += MeetingTimes.ToString();
                        if(!HoursPerTeacherString.Contains(temp))
                            HoursPerTeacherString.Add(temp);
                    }
                }
            }
        }

        private void InsertInstructorsPerCourse(Data data)
        {
            foreach (var Course in data.Courses1)
            {
                foreach (var Instructor in Course.GetInstructors())
                {
                    var temp = "{Course: " + Course.name + "}\n";
                    temp += Instructor.ToString();
                    InstructorsPerCourseString.Add(temp);
                }
            }
        }

        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class
        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Courses1.Delete(item as Courses);
            UpdateDataGrid(MainProgram.data);
        }

        private void DeleteRoom(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Rooms.Delete(item as Classrooms);
            UpdateDataGrid(MainProgram.data);
        }

        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class and all of its references in the Courses that use it
        private void DeleteInstructor(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.Instructors.Delete(item as Teachers);
            UpdateDataGrid(MainProgram.data);
        }
        //Deletes a row from the DataGrid in the UI - Info and the corresponding data from the Data class and all of its references in the Instructors that use it
        private void DeleteMeetingTime(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            MainProgram.data.MeetingTimes.Delete(item as DateRange);
            UpdateDataGrid(MainProgram.data);
        }

        //not a good way to do this, but it works :)
        private void DeleteInstructorPerCourse(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            var fullString = item.ToString().Split('\n');
            var InstructorString = fullString[1];
            var CourseString = fullString[0].Split(':')[1];
            CourseString = CourseString.Substring(1, CourseString.Length - 2);
            foreach (var Course in MainProgram.data.Courses1)
            {
                if (Course.name == CourseString)
                {
                    foreach (var Instructor in Course.GetInstructors())
                    {
                        if (Instructor.ToString() == InstructorString)
                        {
                            Course.GetInstructors().Delete(Instructor);
                        }
                    }
                }
            }
            InstructorsPerCourseString.Remove(item as string);
            UpdateDataGrid(MainProgram.data);
        }

        //not a good/efficient way to do this, but it works ;)
        private void DeleteHoursPerTeacher(object sender, RoutedEventArgs e)
        {
            MainProgram.SetDataChanged();
            var button = sender as Button;
            var item = button.DataContext;
            var fullString = item.ToString().Split('\n');
            var InstructorString = fullString[0].Split(':')[1];
            var HoursString = fullString[1];
            InstructorString = InstructorString.Substring(1, InstructorString.Length - 2);
            foreach (var Course in MainProgram.data.Courses1)
            {
                foreach (var Instructor in Course.GetInstructors())
                {
                    if (Instructor.Name == InstructorString)
                    {
                        foreach (var MeetingTimes in Instructor.GetMeetingTimesPerTeacher())
                        {
                            if (MeetingTimes.ToString() == HoursString)
                            {
                                Instructor.GetMeetingTimesPerTeacher().Delete(MeetingTimes);
                            }
                        }
                        
                    }
                }
            }
            HoursPerTeacherString.Remove(item as string);
            UpdateDataGrid(MainProgram.data);
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
