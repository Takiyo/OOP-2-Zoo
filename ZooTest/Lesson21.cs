using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestLibrary;

namespace ZooTest
{
    /// <summary>
    /// The class which represents the unit testing for the 2.1 Zoo.
    /// </summary>
    [TestClass]
    public class Lesson21 : ZooTestBase
    {
        [TestMethod]
        public void Zoo21Step01ECheckConsoleUtil()
        {
            this.CheckMethod(ConsoleUtil, "ReadAlphabeticValue", "string", Modifier.Static, false);
            this.CheckMethod(ConsoleUtil, "ReadDoubleValue", "double", Modifier.Static, false);
            this.CheckMethod(ConsoleUtil, "ReadGender", "Gender", Modifier.Static, false);
            this.CheckMethod(ConsoleUtil, "ReadIntValue", "int", Modifier.Static, false);
            this.CheckMethod(ConsoleUtil, "ReadStringValue", "string", Modifier.Static, false);

            this.CheckMethod(ConsoleHelper, "ProcessAddCommand", "void", Modifier.Static, true, "zoo:Zoo", "type:string");
            this.CheckMethod(ConsoleHelper, "AddAnimal", "void", Visibility.Private, Modifier.Static, true, "zoo:Zoo");

            object console = this.StartConsole("ZooConsole", "Welcome to the Como Zoo!");

            //try
            //{
            //    this.CheckWriteConsoleLine(console, "add misspelling", ArgumentNullException);
            //}
            //catch
            //{
            //    this.CheckReadConsoleLine(console, "] Unknown type. Only animals and guests can be added.");
            //}

            //this.CheckWriteConsoleLine(console, "add animal", "Animal type");
            //this.CheckWriteConsoleLine(console, "dingo", "Name");
            //this.CheckWriteConsoleLine(console, "Bob", "Gender");
            //this.CheckWriteConsoleLine(console, "Male", "Age");
            //this.CheckWriteConsoleLine(console, "15", "Weight");
            //this.CheckWriteConsoleLine(console, "150", "The following animal was found: Bob: Dingo (15, 150).");


            this.AddInstructorInfoMessage("Not Implemented: Checking the validation of the methods.");
        }

        [TestMethod]
        public void Zoo21Step01ECheckConsoleUtilExceptions()
        {
            object console = this.StartConsole("ZooConsole", "Welcome to the Como Zoo!");
        }

        [TestMethod]
        public void Zoo21Step01HConsoleUtil()
        {
            object console = this.StartConsole("ZooConsole", "Welcome to the Como Zoo!");
            //this.CheckWriteConsoleLine(console, "add animal", "Animal type");
            this.AddInstructorInfoMessage("Not Implemented: Console doesn't support invoked prompts.");
        }

        [TestMethod]
        public void Zoo21Step02ConsoleAddGuest()
        {
            // Test to instantiate a guest via Console. <-- NOT SUPPORTED YET
            this.CheckMethod(ConsoleHelper, "AddGuest", "void", Visibility.Private, Modifier.Static, true, "zoo:Zoo");
        }

        [TestMethod]
        public void Zoo21Step03ConsoleHelperCheck()
        {
            this.CheckMethod(Zoo, "RemoveAnimal", "void", Visibility.Public, true, "animal:Animal");
            this.CheckMethod(ConsoleHelper, "ProcessRemoveCommand", "void", Visibility.Public, Modifier.Static, true, "zoo:Zoo", "type:string", "name:string");
            this.CheckMethod(ConsoleHelper, "RemoveAnimal", "void", Visibility.Private, Modifier.Static, true, "zoo:Zoo", "name:string");
        }

        [TestMethod]
        public void Zoo21Step04RemoveGuestViaConsole()
        {
            this.CheckMethod(ConsoleHelper, "RemoveGuest", "void", Visibility.Private, Modifier.Static, false);
            this.CheckMethod(Zoo, "RemoveGuest", "void", Visibility.Public, false);

            this.AddInstructorInfoMessage("Not Implemented: value check to see if guest is removed via Console.");
        }

        [TestMethod]
        public void Zoo21Step05RemoveAnimalWPF()
        {
            object mainWindow = this.CreateMainWindowAndSetComoZooField();
            this.CheckMethod(MainWindow, "removeAnimalButton_Click", "void", false);
            this.CheckField(MainWindow, "removeAnimalButton", "Button");

            this.AddInstructorInfoMessage("Not Implemented: Check to see if animal is removed via WPF");
        }

        [TestMethod]
        public void Zoo21Step06RemoveGuestWPF()
        {
            this.CheckMethod(MainWindow, "removeGuestButton_Click", "void", false);     
        }

        [TestMethod]
        public void Zoo21Step07AddAnimalWPF()
        {
            // Add Animal button on main window
            this.CheckMethod(MainWindow, "addAnimalButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            // Animal window exists.
            this.CheckClass(AnimalWindow, Visibility.Public);
            this.CheckField(AnimalWindow, "animal", "Animal");
            this.CheckConstructor(AnimalWindow, true, "animal:Animal");

            this.CheckField(MainWindow, "addAnimalButton", "Button");
            //this.CheckField(MainWindow, "animalTypeComboBox", "ComboBox");

            object mainWindow = this.CreateMainWindowAndSetComoZooField();
            this.InvokeMethod(mainWindow, "window_Loaded", null, null);
        }

        [TestMethod]
        public void Zoo21Step08CCheckAnimalWindowWPF()
        {
            this.CheckField(AnimalWindow, "nameTextBox", "TextBox");
            this.CheckField(AnimalWindow, "ageTextBox", "TextBox");
            this.CheckField(AnimalWindow, "weightTextBox", "TextBox");
            this.CheckField(AnimalWindow, "genderComboBox", "ComboBox");
            this.CheckField(AnimalWindow, "okButton", "Button");
            this.CheckField(AnimalWindow, "cancelButton", "Button");
            this.AddInstructorInfoMessage("Not Implemented: Checking the grids.");
        }

        [TestMethod]
        public void Zoo21Step08ECheckAnimalWindowWPF()
        {
            this.CheckMethod(AnimalWindow, "window_Loaded", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            object animal = this.CreateDingo();
            object animalWindow = this.CreateObject(AnimalWindow, animal);
            this.InvokeMethod(animalWindow, "window_Loaded", null, null);       
        }

        [TestMethod]
        public void Zoo21Step09CheckOkCancelFunctionality()
        {
            // There are three Check your work steps within Step 9 out of the six total steps. It's a short step that I'm combining here. 
            this.CheckMethod(AnimalWindow, "okButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            this.AddInstructorInfoMessage("Not Implemented: Check to see if OK/Cancel functionality works... is present on AnimalWindow.");
        }

        [TestMethod]
        public void Zoo21Step10BCheckUserInputAnimalWindow()
        {
            this.CheckMethod(AnimalWindow, "ageTextBox_LostFocus", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(AnimalWindow, "nameTextBox_LostFocus", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(AnimalWindow, "weightTextBox_LostFocus", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            this.AddInstructorInfoMessage("Not Implemented: Value check Lost Focus events to see that they work.");
        }

        public void Zoo21Step10FCheckUserInputAnimalWindow()
        {
            this.CheckMethod(AnimalWindow, "genderComboBox_SelectionChanged", "void", Visibility.Private, Modifier.None, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(AnimalWindow, "makePregnantButton_Click", "void", Visibility.Private, Modifier.None, true, "sender:object", "e:RoutedEventArgs");
            // Test the outer bounds of the methods
            object animal = this.CreateObject(Dingo, "Bob", 10, 100, "Gender");
            object animalWindow = this.CreateObject(AnimalWindow, animal);
            object eventArgs = this.CreateObject(RoutedEventArgs);
            this.SetValue(animalWindow, "this.weightTextBox.Text", 2050.0); // check this, not setting
            this.InvokeMethod(animalWindow, "weightTextBox_LostFocus", this, eventArgs);
            this.AddInstructorInfoMessage("Not Implemented: Checking validation of the gender and pregnant events.");
            this.AddInstructorInfoMessage("Not Implemented: Check that animal is added to the zoo with user input.");
        }

        [TestMethod]
        public void Zoo21Step11CheckAnimalWindowValidation()
        {
            this.AddInstructorInfoMessage("Not Implemented: Check the validation and thrown exceptions for AnimalWindow.");
            // Also possibly writing a test that checks for the optional challenge where the OK button is disabled.
            // Check age validation
            object dingo = this.CreateDingo();
            this.SetValue(dingo, "Age", 100);
            this.SetValueM("Testing the validation for the animal's age, which must be within the range of 0-100. Currently it is being set beyond 100.", dingo, "Age", 100.1, 100);
            this.SetValue(dingo, "Age", 0);
            this.SetValueM("Testing the validation for the animal's age, which must be within the range of 0-100. Currently it is being set below 0.", dingo, "Age", -0.9, 0);

            // Check animal weight validation
            this.SetValue(dingo, "Weight", 1000.0);
            this.SetValueM("Testing the validation for the animal's weight, which must be wihtin the range of 0-1000. Currently, it is being set beyond 1000", dingo, "Weight", 1000.1, 1000.0);
            this.SetValue(dingo, "Weight", 0.0);
            this.SetValueM("Testing the validation for the animal's weight, which must be wihtin the range of 0-1000. Currently, it is being set below 0", dingo, "Weight", -0.9, 0.0);

            // Check animal name validation
            this.SetValue(dingo, "Name", "Dave");
            this.SetValue(dingo, "Name", "Dave!", "Dave");
            this.SetValue(dingo, "Name", "Da4e", "Dave");
            this.SetValue(dingo, "Name", "Dave Dave");
            this.SetValue(dingo, "Name", "Dave 4Dave", "Dave Dave");

        }

        [TestMethod]
        public void Zoo21Step12CCheckGuestWindow()
        {
            this.CheckClass(GuestWindow, Visibility.Public, Modifier.None);
            this.CheckField(GuestWindow, "guest", "Guest");
            this.CheckConstructor(GuestWindow, true, "guest:Guest");
            this.CheckMethod(GuestWindow, "window_Loaded", "void", false);
        }

        [TestMethod]
        public void Zoo21Step12FGuestWindow()
        {
            this.CheckField(GuestWindow, "nameTextBox", "TextBox");
            this.CheckField(GuestWindow, "ageTextBox", "TextBox");
            this.CheckField(GuestWindow, "genderComboBox", "ComboBox");
            this.CheckField(GuestWindow, "walletColorComboBox", "ComboBox");
            this.CheckField(GuestWindow, "okButton", "Button");
            this.CheckField(GuestWindow, "cancelButton", "Button");
            this.AddInstructorInfoMessage("Not Implemented: Checking WPF Elements (mostly the Grid) to make sure the GuestWindow looks like the wireframe.");
        }

        [TestMethod]
        public void Zoo21Step12KGuestWindow()
        {
            this.CheckMethod(GuestWindow, "window_Loaded", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "nameTextBox_LostFocus", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "ageTextBox_LostFocus", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "genderComboBox_SelectionChanged", "void", Visibility.Private, true, "sender:object", "e:SelectionChangedEventArgs");
            this.CheckMethod(GuestWindow, "walletColorComboBox_SelectionChanged", "void", Visibility.Private, true, "sender:object", "e:SelectionChangedEventArgs");
            this.CheckMethod(GuestWindow, "okButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            // Check Guest's age validation
            object guest = this.CreateRichGuest();
            this.SetValue(guest, "Age", 0);
            this.SetValueM("Guest must not go outside the range of 0-120. It is currently being set below zero.", guest, "Age", -1, 0);
            this.SetValue(guest, "Age", 120);
            this.SetValueM("Guest must not go outside the range of 0-120. It is currently being set above 120.", guest, "Age", 121, 120);

            // Check Guest's name validation
            this.SetValue(guest, "Name", "George");
            this.SetValueM("The guest's name must only have letters and spaces. It is currently allowing numbers.", guest, "Name", "George1", "George");
            this.SetValueM("The guest's name must only have letters and spaces. It is currently allowing punctuation.", guest, "Name", "George!", "George");
            this.SetValue(guest, "Name", "George George");
            this.SetValueM("The guest's name must only have letters and spaces. It is currently allowing nummbers.", guest, "Name", "George4George", "George George");

            this.AddInstructorInfoMessage("Not Implemented: Check the validation and how the methods work using values");
        }

        [TestMethod]
        public void Zoo21Step12MGuestWindow()
        {
            this.CheckMethod(GuestWindow, "subtractAccountButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "addAccountButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "addMoneyButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");
            this.CheckMethod(GuestWindow, "subtractAccountButton_Click", "void", Visibility.Private, true, "sender:object", "e:RoutedEventArgs");

            this.AddInstructorInfoMessage("Not Implemented: Checking Account/Wallet Balance WPF elements/validation.");
        }

        [TestMethod]
        public void Zoo21Step13CheckNewZoo()
        {
            object zoo = this.InvokeMethod(this.CreateZoo(), "NewZoo");
            this.CheckValue(zoo, "animals.Count", 0);
            this.CheckValue(zoo, "guests.Count", 0);           
        }

        [TestMethod]
        public void Zoo21Step99FinalState()
        {
            // MainWindow
            //this.CheckFieldCount(MainWindow, 4);
            //this.CheckMethodCount(MainWindow, 8);
            //this.CheckPropertyCount(MainWindow, 0);
            //this.CheckControlCount(MainWindow, 0);
            //this.CheckConstructorCount(MainWindow, 1);

            // ZooConsole
            //this.CheckFieldCount(Program, 0);
            //this.CheckMethodCount(Program, 1);
            //this.CheckPropertyCount(Program, 0);
            //this.CheckConstructorCount(Program, 0);

            //this.CheckFieldCount(ConsoleHelper, 0);
            //this.CheckMethodCount(ConsoleHelper, 10);
            //this.CheckPropertyCount(ConsoleHelper, 0);
            //this.CheckConstructorCount(ConsoleHelper, 0);

            //this.CheckFieldCount(ConsoleUtil, 0);
            //this.CheckMethodCount(ConsoleUtil, 8);
            //this.CheckPropertyCount(ConsoleUtil, 0);
            //this.CheckConstructorCount(ConsoleUtil, 0);

            //// Account
            //this.CheckFieldCount(Account, 1);
            //this.CheckMethodCount(Account, 2);
            //this.CheckPropertyCount(Account, 1);
            //this.CheckConstructorCount(Account, 0);

            //// Animal
            //this.CheckFieldCount(Animal, 6);
            //this.CheckMethodCount(Animal, 5);
            //this.CheckPropertyCount(Animal, 7);
            //this.CheckConstructorCount(Animal, 1);

            //this.CheckFieldCount(Bird, 0);
            //this.CheckMethodCount(Bird, 5);
            //this.CheckPropertyCount(Bird, 1);
            //this.CheckConstructorCount(Bird, 1);

            //this.CheckFieldCount(Mammal, 0);
            //this.CheckMethodCount(Mammal, 3);
            //this.CheckPropertyCount(Mammal, 1);
            //this.CheckConstructorCount(Mammal, 1);

            //this.CheckFieldCount(Fish, 0);
            //this.CheckMethodCount(Fish, 1);
            //this.CheckPropertyCount(Fish, 1);
            //this.CheckConstructorCount(Fish, 1);

            //this.CheckFieldCount(Chimpanzee, 0);
            //this.CheckMethodCount(Chimpanzee, 2);
            //this.CheckPropertyCount(Chimpanzee, 1);
            //this.CheckConstructorCount(Chimpanzee, 1);

            //this.CheckFieldCount(Dingo, 0);
            //this.CheckMethodCount(Dingo, 4);
            //this.CheckPropertyCount(Dingo, 0);
            //this.CheckConstructorCount(Dingo, 1);

            //this.CheckFieldCount(Eagle, 0);
            //this.CheckMethodCount(Eagle, 0);
            //this.CheckPropertyCount(Eagle, 0);
            //this.CheckConstructorCount(Eagle, 1);

            //this.CheckFieldCount(Hummingbird, 0);
            //this.CheckMethodCount(Hummingbird, 1);
            //this.CheckPropertyCount(Hummingbird, 0);
            //this.CheckConstructorCount(Hummingbird, 1);

            //this.CheckFieldCount(Kangaroo, 0);
            //this.CheckMethodCount(Kangaroo, 1);
            //this.CheckPropertyCount(Kangaroo, 0);
            //this.CheckConstructorCount(Kangaroo, 1);

            //this.CheckFieldCount(Ostrich, 0);
            //this.CheckMethodCount(Ostrich, 1);
            //this.CheckPropertyCount(Ostrich, 0);
            //this.CheckConstructorCount(Ostrich, 1);

            //this.CheckFieldCount(Platypus, 0);
            //this.CheckMethodCount(Platypus, 7);
            //this.CheckPropertyCount(Platypus, 0);
            //this.CheckConstructorCount(Platypus, 1);

            //this.CheckFieldCount(Shark, 0);
            //this.CheckMethodCount(Shark, 0);
            //this.CheckPropertyCount(Shark, 0);
            //this.CheckConstructorCount(Shark, 1);

            //this.CheckFieldCount(Squirrel, 0);
            //this.CheckMethodCount(Squirrel, 1);
            //this.CheckPropertyCount(Squirrel, 0);
            //this.CheckConstructorCount(Squirrel, 1);

            //this.CheckFieldCount(AnimalFactory, 0);
            //this.CheckMethodCount(AnimalFactory, 1);
            //this.CheckPropertyCount(AnimalFactory, 0);
            //this.CheckConstructorCount(AnimalFactory, 0);

            //this.CheckMethodCount(IHatchable, 1);
            //this.CheckPropertyCount(IHatchable, 0);

            //this.CheckMethodCount(IMover, 1);
            //this.CheckPropertyCount(IMover, 0);

            //// BirthingRoom
            //this.CheckFieldCount(BirthingRoom, 5);
            //this.CheckPropertyCount(BirthingRoom, 1);
            //this.CheckMethodCount(BirthingRoom, 1);
            //this.CheckConstructorCount(BirthingRoom, 1);

            //// BoothItems
            //this.CheckFieldCount(CouponBook, 2);
            //this.CheckMethodCount(CouponBook, 0);
            //this.CheckPropertyCount(CouponBook, 2);
            //this.CheckConstructorCount(CouponBook, 1);

            //this.CheckFieldCount(Item, 1);
            //this.CheckMethodCount(Item, 0);
            //this.CheckPropertyCount(Item, 1);
            //this.CheckConstructorCount(Item, 1);

            //this.CheckFieldCount(Ticket, 2);
            //this.CheckMethodCount(Ticket, 1);
            //this.CheckPropertyCount(Ticket, 2);
            //this.CheckConstructorCount(Ticket, 1);

            //this.CheckFieldCount(Map, 1);
            //this.CheckMethodCount(Map, 0);
            //this.CheckPropertyCount(Map, 1);
            //this.CheckConstructorCount(Map, 1);

            //this.CheckFieldCount(SoldItem, 1);
            //this.CheckMethodCount(SoldItem, 0);
            //this.CheckPropertyCount(SoldItem, 0);
            //this.CheckConstructorCount(SoldItem, 1);

            //this.CheckFieldCount(WaterBottle, 1);
            //this.CheckMethodCount(WaterBottle, 0);
            //this.CheckPropertyCount(WaterBottle, 1);
            //this.CheckConstructorCount(WaterBottle, 1);

            //// Food
            //this.CheckFieldCount(Food, 1);
            //this.CheckMethodCount(Food, 0);
            //this.CheckPropertyCount(Food, 1);
            //this.CheckConstructorCount(Food, 1);

            //this.CheckPropertyCount(IEater, 1);
            //this.CheckMethodCount(IEater, 1);

            //// MoneyCollector
            //this.CheckFieldCount(MoneyCollector, 1);
            //this.CheckMethodCount(MoneyCollector, 2);
            //this.CheckPropertyCount(MoneyCollector, 1);
            //this.CheckConstructorCount(MoneyCollector, 1);

            //this.CheckFieldCount(MoneyBox, 0);
            //this.CheckMethodCount(MoneyBox, 3);
            //this.CheckPropertyCount(MoneyBox, 0);
            //this.CheckConstructorCount(MoneyBox, 0);

            //this.CheckFieldCount(MoneyPocket, 0);
            //this.CheckMethodCount(MoneyPocket, 3);
            //this.CheckPropertyCount(MoneyPocket, 0);
            //this.CheckConstructorCount(MoneyPocket, 0);

            //this.CheckMethodCount(IMoneyCollector, 2);
            //this.CheckPropertyCount(IMoneyCollector, 1);

            //// People
            //this.CheckFieldCount(Wallet, 2);
            //this.CheckMethodCount(Wallet, 2);
            //this.CheckPropertyCount(Wallet, 2);
            //this.CheckConstructorCount(Wallet, 1);

            //this.CheckFieldCount(Restroom, 2);
            //this.CheckMethodCount(Restroom, 0);
            //this.CheckPropertyCount(Restroom, 0);
            //this.CheckConstructorCount(Restroom, 1);

            //this.CheckFieldCount(Guest, 6);
            //this.CheckMethodCount(Guest, 6);
            //this.CheckPropertyCount(Guest, 6);
            //this.CheckConstructorCount(Guest, 1);

            //this.CheckFieldCount(Employee, 3);
            //this.CheckMethodCount(Employee, 5);
            //this.CheckPropertyCount(Employee, 1);
            //this.CheckConstructorCount(Employee, 1);

            //this.CheckFieldCount(Booth, 2);
            //this.CheckMethodCount(Booth, 0);
            //this.CheckPropertyCount(Booth, 2);
            //this.CheckConstructorCount(Booth, 1);

            //this.CheckFieldCount(MoneyCollectingBooth, 3);
            //this.CheckMethodCount(MoneyCollectingBooth, 4);
            //this.CheckPropertyCount(MoneyCollectingBooth, 3);
            //this.CheckConstructorCount(MoneyCollectingBooth, 1);

            //this.CheckFieldCount(GivingBooth, 0);
            //this.CheckMethodCount(GivingBooth, 2);
            //this.CheckPropertyCount(GivingBooth, 0);
            //this.CheckConstructorCount(GivingBooth, 1);

            //// Reproducer
            //this.CheckMethodCount(IReproducer, 2);
            //this.CheckPropertyCount(IReproducer, 2);

            //// Toys
            //this.CheckFieldCount(WuvLuv, 3);
            //this.CheckMethodCount(WuvLuv, 5);
            //this.CheckPropertyCount(WuvLuv, 3);
            //this.CheckConstructorCount(WuvLuv, 1);

            //// VendingMachines
            //this.CheckFieldCount(VendingMachine, 5);
            //this.CheckMethodCount(VendingMachine, 6);
            //this.CheckPropertyCount(VendingMachine, 1);
            //this.CheckConstructorCount(VendingMachine, 1);

            //// Zoo
            //this.CheckFieldCount(Zoo, 10);
            //this.CheckMethodCount(Zoo, 11);
            //this.CheckPropertyCount(Zoo, 6);
            //this.CheckConstructorCount(Zoo, 1);
        }
    }
}
