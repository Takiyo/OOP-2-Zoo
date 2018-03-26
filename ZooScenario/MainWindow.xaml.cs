using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }

        /// <summary>
        /// Adds an new animal to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;

                Animal animal = AnimalFactory.CreateAnimal(animalType, "Animal", 0, 0.0, Gender.Female);

                AnimalWindow window = new AnimalWindow(animal);

                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    this.comoZoo.AddAnimal(animal);

                    this.PopulateAnimalListBox();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal type must be selected before adding an animal to the zoo.");
            }
        }

        /// <summary>
        /// Adds a new guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest("Guest", 0,  0m, WalletColor.Black, Gender.Female, new Account());

            GuestWindow window = new GuestWindow(guest);

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                try
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);

                    this.comoZoo.AddGuest(guest, ticket);

                    this.PopulateGuestListBox();
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Admits a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account ethelAccount = new Account();
            ethelAccount.AddMoney(30.00m);
            Guest guest = new Guest("Ethel", 42, 30.00m, WalletColor.Salmon, Gender.Female, ethelAccount);

            try
            {
                Ticket ticket = this.comoZoo.SellTicket(guest);

                this.comoZoo.AddGuest(guest, ticket);

                this.PopulateGuestListBox();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Configures the birthing room controls in the main window.
        /// </summary>
        private void ConfigureBirthingRoomControls()
        {
             // Set label's text.
            this.temperatureLabel.Content = string.Format("{0:0.0} °F", this.comoZoo.BirthingRoomTemperature);

            // Size temperature bar.
            this.temperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;

            // Calculate temperature bar's color level (from 0 to 255).
            double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

            // Set temperature bar's color based upon the color level (red is directly proportional to color level; green and blue are inversely proportional).
            this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                Convert.ToByte(colorLevel),
                Convert.ToByte(255 - colorLevel),
                Convert.ToByte(255 - colorLevel)));
        }

        /// <summary>
        /// Decreases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
                this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Has the guest feed an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guest = this.guestListBox.SelectedItem as Guest;

                Animal animal = this.animalListBox.SelectedItem as Animal;

                if (guest != null && animal != null)
                {
                    guest.FeedAnimal(animal, this.comoZoo.AnimalSnackMachine);

                    this.PopulateAnimalListBox();
                    this.PopulateGuestListBox();
                }
                else
                {
                    MessageBox.Show("You must choose a guest and an animal to feed an animal.");
                }

                this.guestListBox.SelectedItem = guest;
                this.animalListBox.SelectedItem = animal;
            }
            catch
            {
                MessageBox.Show("You must create a zoo before feeding animals.");
            }
        }

        /// <summary>
        /// Increases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
                this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Populates the window's animal list.
        /// </summary>
        private void PopulateAnimalListBox()
        {
            this.animalListBox.ItemsSource = null;

            this.animalListBox.ItemsSource = this.comoZoo.Animals;
        }

        /// <summary>
        /// Populates the window's guest list.
        /// </summary>
        private void PopulateGuestListBox()
        {
            this.guestListBox.ItemsSource = null;

            this.guestListBox.ItemsSource = this.comoZoo.Guests;
        }

        /// <summary>
        /// Removes the selected animal from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // Checks if animal is adopted by any guests.
            foreach (Guest g in this.comoZoo.Guests)
            {
                if (g.AdoptedAnimal != null)
                {
                    // Removes guest from cage and animal from adopted status.
                    this.comoZoo.FindCage(g.AdoptedAnimal.GetType()).Remove(g);
                    g.AdoptedAnimal = null;            
                }
            }

            Animal animal = this.animalListBox.SelectedItem as Animal;

            // Makes sure animal is slected.
            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected animal.
                    this.comoZoo.RemoveAnimal(animal);
                    this.PopulateAnimalListBox();
                    this.PopulateGuestListBox();
                }
            }
            else
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        /// <summary>
        /// Removes the selected guest from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected guest from the zoo.
                    this.comoZoo.RemoveGuest(guest);

                    // Check if they have adopted an animal.
                    if (guest.AdoptedAnimal != null)
                    {
                        this.comoZoo.FindCage(guest.AdoptedAnimal.GetType()).Remove(guest);
                    }
                    

                    this.PopulateGuestListBox();
                }
            }
            else
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        /// <summary>
        /// This code runs when the window is loaded.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.comoZoo = Zoo.NewZoo();

            this.ConfigureBirthingRoomControls();

            try
            {
                this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
                this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));

                this.PopulateAnimalListBox();
                this.PopulateGuestListBox();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Must select the type of animal to create.");
            }   
        }

        /// <summary>
        /// Edits the selected animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Animal animal = (Animal)this.animalListBox.SelectedItem;
            if (animal != null)
            {
                AnimalWindow window = new AnimalWindow(animal);
                
                if (window.ShowDialog() == true)
                {
                    if (animal.IsPregnant == true)
                    {
                        this.comoZoo.AddAnimal(animal);
                        this.comoZoo.RemoveAnimal(animal);
                    }
                    this.PopulateAnimalListBox();
                }
            }
        }

        /// <summary>
        /// Edits the selected guest.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guest guest = (Guest)this.guestListBox.SelectedItem;
            if (guest != null)
            {
                GuestWindow window = new GuestWindow(guest);

                if (window.ShowDialog() == true)
                {
                    this.PopulateGuestListBox();
                }
            }
        }

        /// <summary>
        /// Shows the animal cage.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            
            if (animal != null)
            {
                CageWindow window = new CageWindow
                    (
                    this.comoZoo.FindCage(animal.GetType())
                    );
                    
                window.Show();
            }
        }

        /// <summary>
        /// Adopts an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;
            guest.AdoptedAnimal = animal;
            Cage cage = this.comoZoo.FindCage(animal.GetType());
            cage.Add(guest);
            this.PopulateGuestListBox();
        }

        /// <summary>
        /// Un-adopts an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            this.comoZoo.FindCage(guest.AdoptedAnimal.GetType()).Remove(guest);
            guest.AdoptedAnimal = null;
            this.PopulateGuestListBox();
        }

        /// <summary>
        /// Changes move behavior of the animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void changeMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object behaviorType = this.changeMoveBehaviorComboBox.SelectedItem;
                Animal animal = this.animalListBox.SelectedItem as Animal;
                if (animal != null && behaviorType != null)
                {
                    IMoveBehavior newmovebehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)behaviorType);
                    animal.MoveBehavior = newmovebehavior;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a behavior type and an animal to change its move behavior.");
            }
        }

        /// <summary>
        /// Births an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthAnimal(animalTypeComboBox.SelectedItem as IReproducer);
            this.PopulateAnimalListBox();
        }
    }
}