using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp.MVM.View;

namespace WpfApp
{
    /// create the main window and pages and initialize
    /// 

    public partial class MainWindow : Window
    {
        public static SignIn SignInWindow =  new SignIn();// create the page1 window and every thing else
        public static CreateSchedule CreateWindow = new CreateSchedule();
        public static ShowSchedule showScheduleLive = new ShowSchedule();
        public static Info showData = new Info();
        public static ScheduleDays showScheduleDays = new ScheduleDays();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(SignInWindow);
        }

        // Close the application
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Move the window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Initiate the dragging of the window
            DragMove();
        }

        // Minimize the window
        private void Minimize(object sender, MouseButtonEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //change color of the selected button and show the selected page
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have a reference to your original style and the highlighted style
            Style originalStyle = FindResource("CustomRadioButtonStyle") as Style;
            Style highlightedStyle = FindResource("CustomRadioButtonStyleHighlighted") as Style;

            // Reset all RadioButtons to original style
            foreach (var child in MyStackPanel.Children) // MyStackPanel is the x:Name of your StackPanel
            {
                if (child is RadioButton rb)
                {
                    rb.Style = originalStyle;
                }
            }

            // Apply the highlighted style to the clicked RadioButton
            RadioButton clickedRadioButton = sender as RadioButton;
            if (clickedRadioButton != null)
            {
                clickedRadioButton.Style = highlightedStyle;
            }


            RadioButton radioButton = (RadioButton)sender;

            switch (radioButton.Content.ToString())
            {
                case "Home":                   
                        MainFrame.Navigate(showScheduleDays);

                    break;
                case "Sign-In":
                        MainFrame.Navigate(SignInWindow);
                   
                    break;
                case "Create":
                        MainFrame.Navigate(CreateWindow);
                    
                    break;
                case "Info":
                    MainFrame.Navigate(showData); // can also be MainFrame.Navigate(new otherPage1());
                   
                    break;
                default:
                    MainFrame.Content = SignInWindow;
                    break;
            }
        }



    }
}
