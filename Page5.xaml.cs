using Genetic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Page5.xaml
    /// </summary>
    /// 
    //subclass ScheduleItem
    public class ScheduleItem
    {
        public string Time { get; set; }
        public string Activity { get; set; }

        public ScheduleItem(string time, string activity)
        {
            Time = time;
            Activity = activity;
        }
    }
    public partial class Page5 : Page
    {
        public ObservableCollection<ScheduleItem> SundaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> MondaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> TuesdaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> WednesdaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> ThursdaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> FridaySchedule { get; set; }  = new ObservableCollection<ScheduleItem>();
        public ObservableCollection<ScheduleItem> SaturdaySchedule { get; set; } = new ObservableCollection<ScheduleItem>();

        public Page5()
        {
            InitializeComponent();  
        }

        public void ShowFull(MyList<ScheduleResult> scheduleResult)
        {
            SundaySchedule.Clear();
            MondaySchedule.Clear();
            TuesdaySchedule.Clear();
            WednesdaySchedule.Clear();
            ThursdaySchedule.Clear();
            FridaySchedule.Clear();
            SaturdaySchedule.Clear();

            foreach (var item in scheduleResult)
            {
                var times = item.MeetingTimes.Split(' ');
                var day = times[0];
                switch (day)
                {
                    case "Sunday":
                        SundaySchedule.Add(AddItem(times, item));

                        break;
                    case "Monday":
                        MondaySchedule.Add(AddItem(times, item));
                        break;
                    case "Tuesday":

                        TuesdaySchedule.Add(AddItem(times, item));
                        break;

                    case "Wednesday":

                        WednesdaySchedule.Add(AddItem(times, item));
                        break;

                    case "Thursday":

                        ThursdaySchedule.Add(AddItem(times, item));
                        break;

                    case "Friday":

                        FridaySchedule.Add(AddItem(times, item));
                        break;

                    case "Saturday":
                        
                        SaturdaySchedule.Add(AddItem(times, item));
                        break;
                }
            }
            DataContext = this;
        }

        private ScheduleItem AddItem(string[] times,ScheduleResult item)
        {
            return new ScheduleItem(string.Join(" ", times.Skip(1)),
                " Course: " + item.Courses + " Room: " + item.Rooms + " Instructor: " + item.Instructors);

        }
    }
}
