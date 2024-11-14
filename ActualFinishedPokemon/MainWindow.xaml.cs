using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ActualFinishedPokemon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initialize Animations
            aniUserFlareon.Initiate(2, "flareonidle", 7, "flareonatk1", 9, "flareonatk2", 9, "flareonatk3", 6, "flareonfaint", 80);
            aniUserLeafeon.Initiate(2, "leafeonidle", 9, "leafeonatk1", 10, "leafeonatk2", 8, "leafeonatk3", 9, "leafeonfaint", 80);
            aniUserVaporeon.Initiate(2, "vaporeonidle", 4, "vaporeonatk1", 6, "vaporeonatk2", 8, "vaporeonatk3", 8, "vaporeonfaint", 80);
            anienemyFlareon.Initiate(2, "flareonidle", 7, "flareonatk1", 9, "flareonatk2", 9, "flareonatk3", 6, "flareonfaint", 80);
            anienemyLeafeon.Initiate(2, "leafeonidle", 9, "leafeonatk1", 10, "leafeonatk2", 8, "leafeonatk3", 9, "leafeonfaint", 80);
            anienemyVaporeon.Initiate(2, "vaporeonidle", 4, "vaporeonatk1", 6, "vaporeonatk2", 8, "vaporeonatk3", 8, "vaporeonfaint", 80);
        }
        Pokemon? p1 = new Leafeon("Default");
        Pokemon? p2 = new Leafeon("Default");
        Random random = new Random();
        string gameStatus = "";
        int radioStatus = 0;
        //Game Start! Button
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            aniUserFlareon.Visibility = Visibility.Hidden;
            aniUserLeafeon.Visibility = Visibility.Hidden;
            aniUserVaporeon.Visibility = Visibility.Hidden;
            anienemyFlareon.Visibility = Visibility.Hidden;
            anienemyLeafeon.Visibility = Visibility.Hidden;
            anienemyVaporeon.Visibility = Visibility.Hidden;
            StartBtn.Visibility = Visibility.Hidden;
            TitlePage.Visibility = Visibility.Hidden;
            gameStatus = "choosePokemon";
            NameBox.Visibility = Visibility.Visible;
            GameText.Visibility = Visibility.Visible;
            GameText.Content = "What is your name?";
            SelectionPanel.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Visible;
        }
        //Next Event Button
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks "Game Status" to see what event needs to trigger
            switch (gameStatus)
            {
                // Starting Screen
                case "choosePokemon":
                    NameBox.Visibility = Visibility.Hidden;
                    RadioButtons.Visibility = Visibility.Visible;
                    string? trainerName = NameBox.Text;
                    GameText.Content = $"Hello {trainerName}! Please choose a Pokemon.\n1. Leafeon\n2. Vaporeon\n3. Flareon";
                    gameStatus = "pokemonChosen";
                    break;
                //User chooses Pokemon
                case "pokemonChosen":
                    RadioButtons.Visibility = Visibility.Hidden;
                    switch (radioStatus)
                    {
                        case 1:
                            aniUserLeafeon.Visibility = Visibility.Visible;
                            aniUserLeafeon.setMode(Animation.Modes.Idle);
                            p1 = new Leafeon("Leafeon");
                            p1.name = "Leafeon";
                            GameText.Content = "You have selected Leafeon!";
                            break;
                        case 2:
                            aniUserVaporeon.Visibility = Visibility.Visible;
                            aniUserVaporeon.setMode(Animation.Modes.Idle);
                            p1 = new Vaporeon("");
                            p1.name = "Vaporeon";
                            GameText.Content = "You have selected Vaporeon!";
                            break;
                        case 3:
                            aniUserFlareon.Visibility = Visibility.Visible;
                            aniUserFlareon.setMode(Animation.Modes.Idle);
                            p1 = new Flareon("");
                            p1.name = "Flareon";
                            GameText.Content = "You have selected Flareon!";
                            break;
                    }
                    gameStatus = "pokemonComputer";
                    break;
                //Computer chooses Pokemon
                case "pokemonComputer":
                    int compPokemon = random.Next(1, 4);
                    switch (compPokemon) {
                        case 1:
                            anienemyLeafeon.Visibility = Visibility.Visible;
                            anienemyLeafeon.setMode(Animation.Modes.Idle);
                            p2 = new Leafeon("");
                            p2.name = "Leafeon";
                            GameText.Content = "Enemy has selected Leafeon!";
                            break;
                        case 2:
                            anienemyVaporeon.Visibility = Visibility.Visible;
                            anienemyVaporeon.setMode(Animation.Modes.Idle);
                            p2 = new Vaporeon("");
                            p2.name = "Vaporeon";
                            GameText.Content = "Enemy has selected Vaporeon!";
                            break;
                        case 3:
                            anienemyFlareon.Visibility = Visibility.Visible;
                            anienemyFlareon.setMode(Animation.Modes.Idle);
                            p2 = new Flareon("");
                            p2.name = "Flareon";
                            GameText.Content = "Enemy has selected Flareon!";
                            break;
                    }
                    gameStatus = "pokemon1Atks";
                    break;
                //User chooses attack
                case "pokemon1Atks":
                    RadioButtons.Visibility = Visibility.Visible;
                    SubmitButton.Content = "Attack!";
                    GameText.Content = $"It is {p1.name}'s turn to attack! Choose an attack.\n1.{p1.atk1Name} ({p1.attack1Left}) \n2.{p1.atk2Name} ({p1.attack2Left})\n3.{p1.atk3Name} ({p1.attack3Left})";
                    gameStatus = "pokemon1Pursue";
                    break;
                    //User tries to attack
                case "pokemon1Pursue":
                    switch (radioStatus)
                    {
                        case 1:
                            //Checks if attack 1 is availible
                            if (p1.Attack1() == 0)
                            {
                                GameText.Content = $"You have no uses of {p1.atk1Name} left. \n1.{p1.atk1Name} ({p1.attack1Left}) \n2.{p1.atk2Name} ({p1.attack2Left})\n3.{p1.atk3Name} ({p1.attack3Left})";
                            }
                            else
                            {
                                aniUserLeafeon.setMode(Animation.Modes.Attack1);
                                aniUserVaporeon.setMode(Animation.Modes.Attack1);
                                aniUserFlareon.setMode(Animation.Modes.Attack1);
                                RadioButtons.Visibility = Visibility.Hidden;
                                GameText.Content = $"{p1.name} used {p1.atk1Name}!";
                                Program.doBattle(p1, p2, 1);
                                SubmitButton.Content = "Next";
                                gameStatus = "pokemonStats1";
                            }
                            break;
                        case 2:
                            //Checks if attack 2 is availible
                            if (p1.Attack2() == 0)
                            {
                                GameText.Content = $"You have no uses of {p1.atk2Name} left. \n1.{p1.atk1Name} ({p1.attack1Left}) \n2.{p1.atk2Name} ({p1.attack2Left})\n3.{p1.atk3Name} ({p1.attack3Left})";
                            }
                            else
                            {
                                aniUserLeafeon.setMode(Animation.Modes.Attack2);
                                aniUserVaporeon.setMode(Animation.Modes.Attack2);
                                aniUserFlareon.setMode(Animation.Modes.Attack2);
                                RadioButtons.Visibility = Visibility.Hidden;
                                GameText.Content = $"{p1.name} used {p1.atk2Name}!";
                                Program.doBattle(p1, p2, 2);
                                SubmitButton.Content = "Next";
                                gameStatus = "pokemonStats1";
                            }
                            break;
                          //Checks if attack 3 is availible
                        case 3:
                            if (p1.Attack3() == 0)
                            {
                                GameText.Content = $"You have no uses of {p1.atk3Name} left. \n1.{p1.atk1Name} ({p1.attack1Left}) \n2.{p1.atk2Name} ({p1.attack2Left})\n3.{p1.atk3Name} ({p1.attack3Left})";
                            }
                            else
                            {
                                aniUserLeafeon.setMode(Animation.Modes.Attack3);
                                aniUserVaporeon.setMode(Animation.Modes.Attack3);
                                aniUserFlareon.setMode(Animation.Modes.Attack3);
                                RadioButtons.Visibility = Visibility.Hidden;
                                GameText.Content = $"{p1.name} used {p1.atk3Name}!";
                                Program.doBattle(p1, p2, 3);
                                SubmitButton.Content = "Next";
                                gameStatus = "pokemonStats1";
                            }
                            break;
                    }
                    break;
                    //User pokemon attack sequence is finished
                case "pokemonStats1":
                    aniUserLeafeon.setMode(Animation.Modes.Idle);
                    aniUserVaporeon.setMode(Animation.Modes.Idle);
                    aniUserFlareon.setMode(Animation.Modes.Idle);
                    GameText.Content = $"Your {p1.name}'s health is at {p1.hp}.\nThe enemy {p2.name}'s health is at {p2.hp}.";
                    //check if pokemon faints
                    if (p2.hp <= 0) { gameStatus = "feint"; }
                    else gameStatus = "computeratks";
                    break;
                    //Computer turn to attack
                case "computeratks":
                    p2.atkTurn = true;
                    while (p2.atkTurn)
                    {
                        int p2Attack = random.Next(1, 4);
                        switch (p2Attack)
                        {
                            //Checks if attack 1 is availible
                            case 1:
                                if (p2.Attack1() == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Program.doBattle(p2, p1, 1);
                                    anienemyLeafeon.setMode(Animation.Modes.Attack1);
                                    anienemyVaporeon.setMode(Animation.Modes.Attack1);
                                    anienemyFlareon.setMode(Animation.Modes.Attack1);
                                    GameText.Content = $"{p2.name} used {p2.atk1Name}!";
                                    p2.atkTurn = false;
                                    break;
                                }
                            //Checks if attack 2 is availible
                            case 2:
                                if (p2.Attack2() == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Program.doBattle(p2, p1, 2);
                                    anienemyLeafeon.setMode(Animation.Modes.Attack2);
                                    anienemyVaporeon.setMode(Animation.Modes.Attack2);
                                    anienemyFlareon.setMode(Animation.Modes.Attack2);
                                    GameText.Content = $"{p2.name} used {p2.atk2Name}!";
                                    p2.atkTurn = false;
                                    break;
                                }
                            //Checks if attack 3 is availible
                            case 3:
                                if (p2.Attack3() == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Program.doBattle(p2, p1, 3);
                                    anienemyLeafeon.setMode(Animation.Modes.Attack3);
                                    anienemyVaporeon.setMode(Animation.Modes.Attack3);
                                    anienemyFlareon.setMode(Animation.Modes.Attack3);
                                    GameText.Content = $"{p2.name} used {p2.atk3Name}!";
                                    p2.atkTurn = false;
                                    break;
                                }
                        }
                    }
                    gameStatus = "pokemonStats2";
                    break;
                //Computer attack sequence is finished
                case "pokemonStats2":
                    anienemyLeafeon.setMode(Animation.Modes.Idle);
                    anienemyVaporeon.setMode(Animation.Modes.Idle);
                    anienemyFlareon.setMode(Animation.Modes.Idle);
                    //Checks if pokemon faints
                    GameText.Content = $"Your {p1.name}'s health is at {p1.hp}.\nThe enemy {p2.name}'s health is at {p2.hp}.";
                    if (p1.hp <= 0) gameStatus = "feint";
                    else gameStatus = "pokemon1Atks";
                    break;
                //Checks which Pokemon has fainted
                case "feint":
                    if (p1.hp <= 0)
                    {
                        aniUserLeafeon.setMode(Animation.Modes.Faint);
                        aniUserVaporeon.setMode(Animation.Modes.Faint);
                        aniUserFlareon.setMode(Animation.Modes.Faint);
                        GameText.Content = $"Your {p1.name} has fainted. \nThe enemy {p2.name} has won the battle.";
                    }
                    else 
                    {
                        anienemyLeafeon.setMode(Animation.Modes.Faint);
                        anienemyVaporeon.setMode(Animation.Modes.Faint);
                        anienemyFlareon.setMode(Animation.Modes.Faint);
                        GameText.Content = $"The enemy {p2.name} has fainted. \nYour {p1.name} has won the battle.";
                    }
                    gameStatus = "endGame";
                    break;
                //End screen
                case "endGame":
                    aniUserFlareon.Visibility = Visibility.Hidden;
                    aniUserLeafeon.Visibility = Visibility.Hidden;
                    aniUserVaporeon.Visibility = Visibility.Hidden;
                    anienemyFlareon.Visibility = Visibility.Hidden;
                    anienemyLeafeon.Visibility = Visibility.Hidden;
                    anienemyVaporeon.Visibility = Visibility.Hidden;
                    StartBtn.Visibility = Visibility.Hidden;
                    GameText.Visibility = Visibility.Hidden;
                    SubmitButton.Visibility = Visibility.Hidden;
                    SelectionPanel.Visibility = Visibility.Hidden;
                    TitlePage.Content = "Thanks for playing!";
                    TitlePage.Visibility = Visibility.Visible;
                    break;   
            }
      
        }
        private void Radio1_Checked(object sender, RoutedEventArgs e)
        {
            radioStatus = 1;
        }

        private void Radio_2_Checked(object sender, RoutedEventArgs e)
        {
            radioStatus = 2;
        }

        private void Radio_3_Checked(object sender, RoutedEventArgs e)
        {
            radioStatus = 3;
        }
    }
}