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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PochinkaFilsovWorldSkills.Classhelper;
using PochinkaFilsovWorldSkills.EF;
using static PochinkaFilsovWorldSkills.Classhelper.AttachBDClass;

namespace PochinkaFilsovWorldSkills
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int maxNotes;
        int currentNotes;
        int pageNumber = 0;
        List<string> countNotesLIST = new List<string>
        {
            "Все",
            "По 10",
            "По 50",
            "По 200"
        };
        List<Client> clientsList = new List<Client>();
        List<string> genderList = new List<string>
        {
            "Все",
            "Мужчина",
            "Женщина"
        };
        public MainWindow()
        {
            InitializeComponent();
            mainListView.ItemsSource = BDContent.Client.ToList();
            countNotesCMB.ItemsSource = countNotesLIST;
            countNotesCMB.SelectedIndex = 0;

            genderCMB.ItemsSource = genderList;
            genderCMB.SelectedIndex = 0;
          
        }

        public void Filter(){
            clientsList = BDContent.Client.ToList();
            clientsList = clientsList.Where(i => i.FirstName.Contains(searchTB.Text)).ToList();

            switch (genderCMB.SelectedIndex)
            {
                case 1:
                    clientsList = BDContent.Client.Where(i => i.GenderCode == "м").ToList();
                    break;
                case 2:
                    clientsList = BDContent.Client.Where(i => i.GenderCode == "ж").ToList();
                    break;
                default:
                    break;
            }

            switch (countNotesCMB.SelectedIndex)
            {
                case 1:
                    clientsList = clientsList.OrderBy(i => i.ID).Skip(pageNumber * 10).Take(10).ToList();
                    maxNotes = BDContent.Client.Count();
                    currentNotes = clientsList.Count();
                    break;
                case 2:
                    clientsList = clientsList.OrderBy(i => i.ID).Skip(pageNumber * 50).Take(50).ToList();
                    maxNotes = BDContent.Client.Count();
                    currentNotes = clientsList.Count();
                    break;
                case 3:
                    clientsList = clientsList.OrderBy(i => i.ID).Skip(pageNumber * 200).Take(200).ToList();
                    maxNotes = BDContent.Client.Count();
                    currentNotes = clientsList.Count();
                    break;
                default:
                    break;
                
            }

          

            currentNotesOnDisplayLABEL.Text = currentNotes.ToString();
            maxNotesOnDateLABEL.Text = maxNotes.ToString();

            mainListView.ItemsSource = clientsList;
            }

        private void countNotesCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void nextPageBTN_Click(object sender, RoutedEventArgs e)
        {
            pageNumber += 1;
            Filter();
        }

        private void prevPageBTN_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber > 0)
            {
                pageNumber -= 1;
                Filter();
            }
            else
            {
                return;
            }
        }

        private void genderCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
    }
}
