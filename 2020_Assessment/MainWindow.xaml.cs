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

namespace _2020_Assessment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Player> allPlayers = new List<Player>();
        List<Player> selectedPlayers = new List<Player>();
        int selectedGoalKeepers, selectedDefenders, selectedMidfielders, selectedForwards;

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();

            Random rnd = new Random();

            string[] firstNames = {
                "Adam", "Amelia", "Ava", "Chloe", "Conor", "Daniel", "Emily",
                "Emma", "Grace", "Hannah", "Harry", "Jack", "James",
                "Lucy", "Luke", "Mia", "Michael", "Noah", "Sean", "Sophie"};

            string[] lastNames = {
                "Brennan", "Byrne", "Daly", "Doyle", "Dunne", "Fitzgerald", "Kavanagh",
                "Kelly", "Lynch", "McCarthy", "McDonagh", "Murphy", "Nolan", "O'Brien",
                "O'Connor", "O'Neill", "O'Reilly", "O'Sullivan", "Ryan", "Walsh"
            };

            Position currentPosition = Position.Goalkeeper;

            for (int i = 0; i < 18; i++)
            {
                // generate random date of birth
                DateTime d1 = DateTime.Now.AddYears(-30);
                DateTime d2 = DateTime.Now.AddYears(-20);

                int range = (d2 - d1).Days;
                DateTime dob = d1.AddDays(rnd.Next(range));

                // generate set number of positions

                switch (i)
                {
                    case 2:
                        currentPosition = Position.Defender;
                        break;
                    case 8:
                        currentPosition = Position.Midfielder;
                        break;
                    case 14:
                        currentPosition = Position.Forward;
                        break;
                }

                Player P = new Player()
                {
                    FirstName = firstNames[rnd.Next(20)],
                    Surname = lastNames[rnd.Next(20)],
                    DOB = dob,  
                    PreferedPositon = currentPosition
                };

                players.Add(P);
            } 
            return players;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create Players
            allPlayers = CreatePlayers();
            allPlayers.Sort();

            // Display in listbox
            lbxAllPlayers.ItemsSource = allPlayers;

            // populate combo box
            cbxFormation.ItemsSource = new string[] { "4-4-2", "4-3-3", "4-5-1" };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Check space available - can only have 11 selected
            if (selectedPlayers.Count > 10)
            {
                MessageBox.Show("You have too many players selected");
            }
            else
            {
                // Determine what is selected
                Player selected = lbxAllPlayers.SelectedItem as Player;

                // Check not null
                if (selected != null)
                {
                    // Take action - move to other list

                    // check if player allowed in current format
                    if (IsValidPlayer(selected))
                    {
                        selectedPlayers.Add(selected);
                        allPlayers.Remove(selected);

                        RefreshScreen();
                    }
                    else
                        MessageBox.Show("Player not allowed in formation");
                }
            }
        }

        private void cbxFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(Player player in selectedPlayers)
            {
                allPlayers.Add(player);
            }

            selectedPlayers.Clear();

            selectedGoalKeepers = 0;
            selectedForwards = 0;
            selectedDefenders = 0;
            selectedMidfielders = 0;

            RefreshScreen();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Determine what is selected
            Player selected = lbxSelectedPlayers.SelectedItem as Player;

            // Check not null
            if (selected != null)
            {
                // Take action - move to other list
                selectedPlayers.Remove(selected);
                allPlayers.Add(selected);

                RefreshScreen();
            }
        }

        private void RefreshScreen()
        {
            selectedPlayers.Sort();
            allPlayers.Sort();

            lbxAllPlayers.ItemsSource = null;
            lbxAllPlayers.ItemsSource = allPlayers;

            lbxSelectedPlayers.ItemsSource = null;
            lbxSelectedPlayers.ItemsSource = selectedPlayers;

            tblkSpaces.Text = (11 - selectedPlayers.Count).ToString();
        }

        private bool IsValidPlayer(Player player)
        {
            bool valid = false;

            string selectedFormation = cbxFormation.SelectedItem as string;
            string[] formation = selectedFormation.Split('-');

            int allowedGoalKeepers = 1;
            int allowedDefenders = int.Parse(formation[0]);
            int allowedMidfielders = int.Parse(formation[1]);
            int allowedForwards = int.Parse(formation[2]);

            switch (player.PreferedPositon)
            {
                case Position.Goalkeeper:
                    if (selectedGoalKeepers < allowedGoalKeepers)
                    {
                        selectedGoalKeepers++;
                        valid = true;
                    }
                    break;
                case Position.Defender:
                    if (selectedDefenders < allowedDefenders)
                    {
                        selectedDefenders++;
                        valid = true;
                    }
                    break;
                case Position.Midfielder:
                    if (selectedMidfielders < allowedMidfielders)
                    {
                        selectedMidfielders++;
                        valid = true;
                    }
                    break;
                case Position.Forward:
                    if (selectedForwards < allowedForwards)
                    {
                        selectedForwards++;
                        valid = true;
                    }
                    break;
            }

            return valid;
        }
    }

}
