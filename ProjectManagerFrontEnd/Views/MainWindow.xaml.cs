using ProjectManagerFrontEnd;
using ProjectManagerFrontEnd.Enums;
using ProjectManagerFrontEnd.Views;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManagerFrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SQLiteQueries sqliteQueries;
        long projectId;
        List<long> taskIdList;

        public MainWindow()
        {
            InitializeComponent();

            sqliteQueries = new SQLiteQueries();
            sqliteQueries.InitializeDatabase();

            // We do not have an active project
            projectId = -1;

            taskIdList = new List<long>();
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            if(projectId > 0)
            {
                // Set up the window
                var taskWindow = new ParcelWizardWindow(WindowEnum.Task);
                taskWindow.Owner = this;
                taskWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                // Grab the new task id from the parcelWizard and add it to list of tasks
                taskWindow.Id += value => taskIdList.Add(value);

                taskWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please open an existing project or create a new one");
            }
        }

        private void MoveBacklog_Click(object sender, RoutedEventArgs e)
        {
            // Move ListItem to Backlog 
        }

        private void MoveTodo_Click(object sender, RoutedEventArgs e)
        {
            // Move ListItem to Todo
        }

        private void MoveInProgress_Click(object sender, RoutedEventArgs e)
        {
            // Move ListItem to In Progress 
        }

        private void MoveFinished_Click(object sender, RoutedEventArgs e)
        {
            // Move ListItem to Finished
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            if (projectId < 0)
            {
                // Set up the window
                var projectWindow = new ParcelWizardWindow(WindowEnum.Project);
                projectWindow.Owner = this;
                projectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                // Grab the new project id from the parcelWizard
                projectWindow.Id += value => projectId = value;

                projectWindow.ShowDialog();

                // TODO: Change this to print the name of the project, not the id (since the id is only used in the backend)
                this.Title = this.Title + " - " + projectId.ToString();
            }
            else
            {
                MessageBox.Show("Please close the current project before creating a new one (I am too lazy to implement a better way to handle this!)");
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
