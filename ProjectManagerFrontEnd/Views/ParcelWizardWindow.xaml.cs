using ProjectManagerFrontEnd.Enums;
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
using System.Windows.Shapes;

namespace ProjectManagerFrontEnd.Views
{
    /// <summary>
    /// Interaction logic for ParcelWizardWindow.xaml
    /// </summary>
    public partial class ParcelWizardWindow : Window
    {
        public event Action<long> Id;

        private WindowEnum windowType;
        private SQLiteQueries sqliteQueries;

        public ParcelWizardWindow(WindowEnum windowType)
        {
            InitializeComponent();

            this.windowType = windowType;

            sqliteQueries = new SQLiteQueries();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            long parcelId = -1;

            switch (windowType)
            {
                case WindowEnum.Project:
                    parcelId = sqliteQueries.InsertIntoProject(null, this.TitleBox.Text, this.DescriptionBox.Text);
                    break;
                case WindowEnum.Epic:
                    break;
                case WindowEnum.Story:
                    break;
                case WindowEnum.Task:
                    parcelId = sqliteQueries.InsertIntoTask(null, this.TitleBox.Text, this.DescriptionBox.Text, -1, -1);
                    break;
                case WindowEnum.SubTask:
                    break;

            }

            if (Id != null)
                Id(parcelId);
            
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
