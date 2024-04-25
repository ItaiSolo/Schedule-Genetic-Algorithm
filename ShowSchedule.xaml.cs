﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp
{
    public partial class ShowSchedule : Page
    {
        MainProgram Program;
        int Generation = 0;
        double Fitness = 0;
        int Conflicts = 0;

        MyList<ScheduleResult> scheduleResult;
        DateTime startTime;

        public ShowSchedule()
        {
            InitializeComponent();
        }

        //Gets data from main program and calls the main algorithm then displays the results
        public void LoadScheduleData(MainProgram Program)
        {
            startTime = DateTime.Now;

            this.Program = Program;
            // Load data into the DataGrid
            scheduleResult = Program.MainRun(Program);
            if (scheduleResult != null)
            {
                ShowResult(scheduleResult, true);
            }
            else
            {
                MessageBox.Show("Error Loading Data");
            }
        }

        //Saves the data to the database
        private void SaveData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.SignInWindow.InsertNewSchedule(scheduleResult, Program.data)) // send to database/signIn window
            {
                MessageBox.Show("Saved Successfully");
            }
            else
            {
                MessageBox.Show("Error Saving Data");
            }
        }

        //Displays the results in the UI Grid 
        public void ShowResult(MyList<ScheduleResult> scheduleResult, bool newSchedule, string dateCreated = "")
        {
            if (newSchedule)
            {
                SaveDataButton.Visibility = Visibility.Visible;
                ScheduleTitel.Text = "Schedule Information[View By Days -> In Home]";
                CurrentDateLabel.Content = DateTime.Now.ToString();
            }
            else
            {
                CurrentDateLabel.Content = dateCreated;
                SaveDataButton.Visibility = Visibility.Collapsed;
                startTime = DateTime.Now;
                ScheduleTitel.Text = "Schedule Information[Loaded From Database]";
            }
            Dispatcher.Invoke(() =>
            {
                Generation = scheduleResult.GetAt(0).Generations;
                Fitness = Math.Round(scheduleResult.GetAt(0).Fitness, 4);
                Conflicts = scheduleResult.GetAt(0).Conflicts;

                ConflictsValue.Text = Conflicts.ToString();
                FitnessValue.Text = Fitness.ToString();
                GenerationValue.Text = Generation.ToString();

                ScheduleDataGrid.ItemsSource = scheduleResult.AsEnumerable(); // is doing the list in excel
                ScheduleDataGrid2.ItemsSource = scheduleResult.GetAt(0).Errors.AsEnumerable();
                MainWindow.showScheduleDays.ShowFull(scheduleResult);
                TimeSpan elapsedTime = DateTime.Now - startTime;
                timerLabel.Content = elapsedTime.ToString(@"mm\:ss");
            });
        }
    }
}
