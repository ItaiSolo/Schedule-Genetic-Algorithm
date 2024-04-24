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
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //Dont forget to delete all the unused libraries!
        bool opened1 = false;// one for each one
        bool opened2 = false;
        bool opened3 = false;
        bool opened4 = false;

        public static Page1 SignInWindow =  new Page1();// create the page1 window and every thing else
        public static Page2 CreateWindow = new Page2();
        public static Page3 showScheduleLive = new Page3();
        public static Page4 showData = new Page4();
        public static Page5 showScheduleDays = new Page5();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(SignInWindow);
        }
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Initiate the dragging of the window
            DragMove();
        }

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

        //change color of the selected button
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
                    if (!opened1)
                    {
                        opened1 = true;
                        MainFrame.Navigate(showScheduleDays);
                    }
                    opened2 = false;
                    opened3 = false;
                    opened4 = false;
                    break;
                case "Sign-In":
                    if (!opened2)
                    {
                        opened2 = true;

                        MainFrame.Navigate(SignInWindow);
                    }
                    
                    opened1 = false;
                    opened3 = false;
                    opened4 = false;
                   

                    break;
                case "Create":

                    if (!opened3)
                    {
                        opened3 = true;

                        MainFrame.Navigate(CreateWindow);
                    }
                    opened1 = false;
                    opened2 = false;
                    opened4 = false;

                    break;
                case "Info":
                    MainFrame.Navigate(showData); // can also be MainFrame.Navigate(new otherPage1());
                    opened1 = false;
                    opened2 = false;
                    opened3 = false;
                    

                    break;
                default:
                    MainFrame.Content = null;
                    break;
            }
        }



    }
}
