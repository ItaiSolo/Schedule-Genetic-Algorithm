using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Newtonsoft.Json;
using System.IO;
using System.Data;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        // Connection string includes the server information, database name, user name, and password.
        string connectionString = "server=database-3f.cbacig0a47uz.eu-north-1.rds.amazonaws.com;" +
                                  "user=admin;password=asdQWE123!^&*; database=try1;";
        MySqlConnection connection;
        private string UserName;
        private string Password;
        private int userId;

        public SignIn()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnLogin.Content = "Successfully connected to the database.";
                userId = SearchSQL(txtUser.Text, txtPass.Password, connection);
                if (userId != -1)
                {
                    UserName = txtUser.Text;
                    Password = txtPass.Password;
                    btnLogin.Content = "Found";
                    StackPanel1.Visibility = Visibility.Hidden;
                    inputStackPanel.Visibility = Visibility.Visible;
                    ScheduleLabel.Visibility = Visibility.Visible;
                    SignedText.Visibility = Visibility.Visible;
                }
                else
                {
                    btnLogin.Content = "Not Found";
                }
            }
            catch (MySqlException ex)
            {
                btnLogin.Content = $"An error occurred: {ex.Message}";
            }
        }


        public int SearchSQL(string username, string password, MySqlConnection connection)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Change the query to select the user's ID
                string commandText = "SELECT id FROM TempUsers WHERE name = @name AND password = @password;";

                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Execute the query and obtain the result
                    object result = command.ExecuteScalar();

                    // Check if the result is not null (user found)
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            // Return a specific value that indicates "user not found", commonly -1 is used for this purpose
            return -1;
        }

        private void SignUp(object sender, MouseButtonEventArgs e)
        {
            { 
                try
                {
                    SignUpSQL(txtUser.Text, txtPass.Password, connection);
                    btnLogin.Content = "Successfully connected to the database.";
                }
                catch (MySqlException ex)
                {
                    btnLogin.Content = $"An error occurred: {ex.Message}";
                }
            }
        }

        private void SignUpSQL(string username, string password, MySqlConnection connection)
        {
            if (txtUser.Text != null && txtPass.Password != null && txtUser.Text != "" && 
                txtPass.Password != "" && txtUser.Text.Length > 1 && txtPass.Password.Length > 1)
            {
                string commandText = "INSERT INTO TempUsers (name, password) VALUES (@name, @password);";

                MySqlCommand command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@name", txtUser.Text);
                command.Parameters.AddWithValue("@password", txtPass.Password); // Consider hashing

                command.ExecuteNonQuery();
                BtnLogin_Click(null, null);//call for login
            }
        }

        private void IncreaseSeats_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(seatsInput.Text, out int currentValue))
            {
                seatsInput.Text = (currentValue + 1).ToString();
                if (LoadSchedule(currentValue + 1))
                    MainWindow.CreateWindow.ChangeColorS(sender, e);
                else MainWindow.CreateWindow.ChangeColorF(sender, e);

            }
        }

        private void DecreaseSeats_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(seatsInput.Text, out int currentValue) && currentValue > 0)
            {
                seatsInput.Text = (currentValue - 1).ToString();
                if(LoadSchedule(currentValue - 1))
                    MainWindow.CreateWindow.ChangeColorS(sender, e);
                else MainWindow.CreateWindow.ChangeColorF(sender, e);
            }
        }

        public bool InsertNewSchedule(MyList<ScheduleResult> result, Data data)
        {
            using (var transaction = connection.BeginTransaction())
            {
                transaction.Commit();
                try
                {
                    SaveDataSchedule(result, data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool LoadSchedule(int currentScheduleId)
        {
            var query = @"
                SELECT save, save2,dateCreated FROM TempInfo
                WHERE InfoID = @InfoId;";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@InfoId", currentScheduleId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string jsonData = reader.GetString("save");
                        Data data = JsonConvert.DeserializeObject<Data>(jsonData);
                        MainWindow.CreateWindow.updateDataSQL(data);
                        string jsonResultData = reader.GetString("save2");
                        MyList<ScheduleResult> result = JsonConvert.DeserializeObject<MyList<ScheduleResult>>(jsonResultData);
                        DateTime? dateCreated = reader.IsDBNull(reader.GetOrdinal("dateCreated")) ? null : (DateTime?)reader.GetDateTime("dateCreated");
                        MainWindow.showScheduleLive.ShowResult(result,false, dateCreated.ToString());
                        NavigationService.Navigate(MainWindow.showScheduleLive); // make the schedule
                        return true;
                    }
                    else return false;
                }
            }
        }



        public void SaveDataSchedule(MyList<ScheduleResult> result, Data data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            string jsonResultData = JsonConvert.SerializeObject(result);
            var query = @"
                INSERT INTO TempInfo (createdByUserId, dateCreated, temp1, save, save2)
                VALUES (@CreatedByUserId, NOW(), @Temp1, @Save, @Save2);
                SELECT LAST_INSERT_ID();";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CreatedByUserId", userId);
                command.Parameters.AddWithValue("@Temp1", DBNull.Value);
                command.Parameters.AddWithValue("@Save", jsonData);
                command.Parameters.AddWithValue("@Save2", jsonResultData);

                int infoId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

    }
}
