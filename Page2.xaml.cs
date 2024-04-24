using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp
{
    public partial class Page2 : Page
    {
        public static MainProgram Program = new MainProgram();//main- program, data,etc
        int nextTimes = 0;
        readonly DateTime today = DateTime.MinValue;
        static int TeachersId = 1;
        static int CourseId = 1;
        bool isDayOfTheWeek = false;
        int MinValue = 0;
        int MaxValueLimit = 500;    
        int MaxValue = 500;
        bool SubmitTeacher = false; //if false then it will submit the MeetingTimes

        Dictionary<int, string> DaysOfWeek = new Dictionary<int, string>()
        {
            { 1, "Sunday" },
            { 2, "Monday" },
            { 3, "Tuesday" },
            { 4, "Wednesday" },
            { 5, "Thursday" },
            { 6, "Friday" },
            { 7, "Saturday" }
        };

        Dictionary<int, int> daysFix = new Dictionary<int, int>()
        {
            { 1, 7 },
            { 2, 1 },
            { 3, 2 },
            { 4, 3 },
            { 5, 4 },
            { 6, 5 },
            { 7, 6 }
        };

        private void UpdateDayLabel(int currentValue)
        {
            if (DaysOfWeek.TryGetValue(currentValue, out var day))
            {
                dayLabel.Text = day;
            }
        }

        public Page2()
        {
            InitializeComponent();
            Program.data = new Data(); // this initializes your data correctly         
        }

        private void IncreaseSeats_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = int.TryParse(seatsInput.Text, out int value) ? value : 0;
            MaxValue = isDayOfTheWeek ? 7 : MaxValueLimit;

            if (currentValue < MaxValue)
            {
                seatsInput.Text = (currentValue + 1).ToString();
                if (isDayOfTheWeek)
                {
                    UpdateDayLabel(currentValue + 1);
                }
            }
        }

        private void DecreaseSeats_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = int.TryParse(seatsInput.Text, out int value) ? value : 0;
            MinValue = isDayOfTheWeek ? 1 : 0;

            if (currentValue > MinValue)
            {
                seatsInput.Text = (currentValue - 1).ToString();
                if (isDayOfTheWeek)
                {
                    UpdateDayLabel(currentValue - 1);
                }
            }
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e) // reset and move the data to !Data!
        {
            
            // Example: Fetch and use the input data from TextBoxes
            int seats;
            isDayOfTheWeek = false;
            switch (nextTimes)
            {
                case 0:
                    if (stringInput1.Text.Length > 1 && int.TryParse(seatsInput.Text, out seats) && seats > 0)
                    {
                        ChangeColorS(sender, e);
                        var temp = new Classrooms(stringInput1.Text, seats);//add input exampele
                        Program.data.Rooms.Add(temp);
                        seatsInput.Text = "0";
                    }
                    else ChangeColorF(sender, e);
                    break;

                case 1:
                    if (int.TryParse(seatsInput.Text, out seats) && stringInput1.Text.Length > 0 && seats > 0)
                    {
                        ChangeColorS(sender, e);
                        // Splitting the time range into start and end times
                        string[] times = stringInput1.Text.Split(new string[] { "-" }, StringSplitOptions.None);
                        string startTime = times[0];
                        string endTime = times[1];

                        // Splitting the start time into hours and minutes
                        string[] startTimeParts = startTime.Split(':');
                        int startHours = int.Parse(startTimeParts[0]);
                        int startMinutes = int.Parse(startTimeParts[1]);

                        // Splitting the end time into hours and minutes
                        string[] endTimeParts = endTime.Split(':');
                        int endHours = int.Parse(endTimeParts[0]);
                        int endMinutes = int.Parse(endTimeParts[1]);

                        if (seats > 7 || seats < 1) seats = 1; // if error goes to default
                        seats = daysFix[seats];
                        var range1Start = new DateTime(today.Year, today.Month, seats, startHours, startMinutes, 0);
                        var range1End = new DateTime(today.Year, today.Month, seats, endHours, endMinutes, 0);


                        //add insertion 
                        Program.data.MeetingTimes.Add(new DateRange(range1Start, range1End, 1));

                        dayLabel.Visibility = Visibility.Visible;
                        seatsInput.Text = "1";
                        isDayOfTheWeek = true;
                    }
                    else ChangeColorF(sender, e);
                    break;

                case 2:
                    //Teachers insertion
                    // adds the meeting time per teacher
                    ShowSelectionBox();
                    
                    if (stringInput1.Text.Length > 0 && !Program.data.MeetingTimesPerTeacher.IsEmpty)
                    {
                        ChangeColorS(sender, e);
                        Program.data.Instructors.Add(new Teachers(TeachersId.ToString(), stringInput1.Text, Program.data.MeetingTimesPerTeacher.Copy()));
                        TeachersId++;
                        Program.data.MeetingTimesPerTeacher.Clear();
                    }
                    else ChangeColorF(sender, e);
                    break;

                case 3:
                    //Courses insertion
                    if (stringInput1.Text.Length > 0 && int.TryParse(seatsInput.Text, out seats) && !Program.data.InstructorsPerCourse.IsEmpty)
                    {
                        ChangeColorS(sender, e);

                        Program.data.Courses1.Add(new Courses(CourseId.ToString(),
                        stringInput1.Text, Program.data.InstructorsPerCourse.Copy(), seats));
                        seatsInput.Text = "0";
                        Program.data.InstructorsPerCourse.Clear();
                        CourseId++;
                    }
                    else ChangeColorF(sender, e);
                    break;
            }
            MainWindow.showData.UpdateData(Program.data);// update desplay data in Info
            stringInput1.Text = "";
        }

        private void Next_Click(object sender, RoutedEventArgs e) // move to next input
        {
            dayLabel.Visibility = Visibility.Hidden;
            if (nextTimes < 4)
            nextTimes++;

            // Example: Fetch and use the input data from TextBoxes
            stringInput1.Visibility = Visibility.Visible;
            isDayOfTheWeek = false;
            switch (nextTimes)
            {
                case 1:
                    ShowCounter();
                    HideSelectionBox();
                    SeatsLabel.Text = "Enter the day of the week:";
                    instructionLabel.Text = "Enter time ranges example: 9:00 - 12:00, 13:00 - 16:00";
                    dayLabel.Visibility = Visibility.Visible;
                    seatsInput.Text = "1";
                    isDayOfTheWeek = true;

                    break;
                case 2:
                    SubmitListButton.Content = "Submit Times";
                    SubmitTeacher = false;//thats for the meeting times
                    HideCounter();
                    ShowSelectionBox();
                    instructionLabel.Text = "Enter Instructor Name example: John Doe";
                    //then the teacher chooses the times he is available

                    break;
                case 3:
                    SubmitListButton.Content = "Submit Teacher";
                    SubmitTeacher = true;
                    ShowCounter();
                    ShowSelectionBox();
                    instructionLabel.Text = "Enter Course Name example: Math 101";
                    SeatsLabel.Text = "Enter the max number of seats per course";
                    seatsInput.Text = "0";
                    break;

                case 4:

                    HideCounter();
                    HideSelectionBox();
                    stringInput1.Visibility = Visibility.Hidden;
                    instructionLabel.Text = "Done!";

                    MainWindow.showScheduleLive.LoadScheduleData(Program);
                    NavigationService.Navigate(MainWindow.showScheduleLive); // make the schedule
                    break;
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            dayLabel.Visibility = Visibility.Hidden;
            stringInput1.Visibility = Visibility.Visible;
            isDayOfTheWeek = false;
            if (nextTimes > 0)
            nextTimes--;
            
            switch (nextTimes)
            {
                case 0:
                    ShowCounter();
                    HideSelectionBox();
                    SeatsLabel.Text = "Number of Seats per Class:";
                    instructionLabel.Text = "Enter Classroom name";
                    seatsInput.Text = "0";

                    break;

                case 1:
                    ShowCounter();
                    HideSelectionBox();
                    SeatsLabel.Text = "Enter the day of the week:";
                    instructionLabel.Text = "Enter time ranges example: 9:00 - 12:00, 13:00 - 16:00";
                    dayLabel.Visibility = Visibility.Visible;
                    seatsInput.Text = "1";
                    isDayOfTheWeek = true;

                    break;
                case 2:
                    SubmitListButton.Content = "Submit Times";
                    SubmitTeacher = false;//thats for the meeting times
                    HideCounter();
                    ShowSelectionBox();
                    instructionLabel.Text = "Enter Instructor Name example: John Doe";

                    break;
                case 3:
                    SubmitListButton.Content = "Submit Teacher";
                    SubmitTeacher = true;
                    ShowCounter();
                    ShowSelectionBox();
                    SeatsLabel.Text = "Enter the max number of seats per course";
                    seatsInput.Text = "0";
                    instructionLabel.Text = "Enter Course Name example: Math 101";
                    break;
            }
        }

        // If setting the data context in code-behind or performing additional initialization
        
        private void ShowSelectionBox()
        {
            comboBox1.Visibility = Visibility.Visible;
            SubmitListButton.Visibility = Visibility.Visible;
        }

        private void HideSelectionBox()
        {
            comboBox1.Visibility = Visibility.Hidden;
            SubmitListButton.Visibility = Visibility.Hidden;
        }

        private void ShowCounter()
        { 
            SeatsLabel.Visibility = Visibility.Visible;
            seatsInput.Visibility = Visibility.Visible;
            plusB.Visibility = Visibility.Visible;
            minusB.Visibility = Visibility.Visible;
        }

        private void HideCounter()
        {
            SeatsLabel.Visibility = Visibility.Hidden;
            seatsInput.Visibility = Visibility.Hidden;
            plusB.Visibility = Visibility.Hidden;
            minusB.Visibility = Visibility.Hidden;
        }

       
        private void SubmitList(object sender, RoutedEventArgs e)
        {
            if (SubmitTeacher) {                 // check if SubmitMeetingTimes / SubmitInstructors
                int selectedIndex = comboBox1.SelectedIndex;
                if (selectedIndex == -1)
                {
                    ChangeColorF(sender, e);
                    return;
                }
                if (!Program.data.InstructorsPerCourse.Contains(Program.data.Instructors.GetAt(selectedIndex)))
                {
                    Program.data.InstructorsPerCourse.Add(Program.data.Instructors.GetAt(selectedIndex));
                    ChangeColorS(sender, e);
                }
                else
                {
                    ChangeColorF(sender, e);
                }
            }else
            {
                SubmitMeetingTimesClick(sender, e);
            }
        }

        private void SubmitMeetingTimesClick(object sender, RoutedEventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                ChangeColorF(sender, e);
                return;
            }
            if (!Program.data.MeetingTimesPerTeacher.Contains(Program.data.MeetingTimes.GetAt(selectedIndex)))
            {
                Program.data.MeetingTimesPerTeacher.Add(Program.data.MeetingTimes.GetAt(selectedIndex));
                ChangeColorS(sender, e);
            }
            else
            {
                ChangeColorF(sender, e);
            }
        }

        private void MenuOpenedInstructors(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (SubmitTeacher)
            {
                comboBox1.Items.Clear(); // Clear existing items
                foreach (var instructor in Program.data.Instructors)
                {
                    comboBox1.Items.Add(instructor); // Add items one by one
                }
            }
            else
            {
                MenuOpenedMeetingTimes(sender, e);
            }
        }

        private void MenuOpenedMeetingTimes(object sender, System.Windows.Input.MouseEventArgs e)
        {
            comboBox1.Items.Clear(); // Clear existing items
            foreach (var Time in Program.data.MeetingTimes)
            {
                comboBox1.Items.Add(Time); // Add items one by one
            }
        }

        
        public void ChangeColorS(object sender, RoutedEventArgs e) // Change the color of the button is Seccssesful
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Change the button's background color to indicate it's been clicked
                button.Background = new SolidColorBrush(Colors.Green); // Temporary color

                // Create and start a timer to revert the color back
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(0.5); // Set the time after which the color should revert
                timer.Tick += (s, args) =>
                {
                    button.Background = new SolidColorBrush(Colors.DodgerBlue); // Revert to original color
                    timer.Stop(); // Stop the timer to prevent it from ticking again
                };
                timer.Start();
            }
        }

        public void ChangeColorF(object sender, RoutedEventArgs e) // Change the color of the button is Fail
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Change the button's background color to indicate it's been clicked
                button.Background = new SolidColorBrush(Colors.Red); // Temporary color

                // Create and start a timer to revert the color back
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(0.5); // Set the time after which the color should revert
                timer.Tick += (s, args) =>
                {
                    button.Background = new SolidColorBrush(Colors.DodgerBlue); // Revert to original color
                    timer.Stop(); // Stop the timer to prevent it from ticking again
                };
                timer.Start();
            }
        }

        public void updateDataSQL(Data newData)
        {
            Program.data = newData;
            MainWindow.showData.UpdateData(Program.data);// update desplay data in Info
        }
    }
}