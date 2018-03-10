using UnitTestLibrary;

namespace ZooTest
{
    /// <summary>
    /// Test cases.
    /// </summary>
    public class ZooTestBase : TestBase
    {
        protected MetaName Account = new MetaName("Accounts", "Account");
        protected MetaName Animal = new MetaName("Animals", "Animal");
        protected MetaName Bird = new MetaName("Animals", "Bird");
        protected MetaName Chimpanzee = new MetaName("Animals", "Chimpanzee");
        protected MetaName Dingo = new MetaName("Animals", "Dingo");
        protected MetaName Eagle = new MetaName("Animals", "Eagle");
        protected MetaName Fish = new MetaName("Animals", "Fish");
        protected MetaName Hummingbird = new MetaName("Animals", "Hummingbird");
        protected MetaName Kangaroo = new MetaName("Animals", "Kangaroo");
        protected MetaName Mammal = new MetaName("Animals", "Mammal");
        protected MetaName Ostrich = new MetaName("Animals", "Ostrich");
        protected MetaName Platypus = new MetaName("Animals", "Platypus");
        protected MetaName Shark = new MetaName("Animals", "Shark");
        protected MetaName Squirrel = new MetaName("Animals", "Squirrel");
        protected MetaName IHatchable = new MetaName("Animals", "IHatchable");
        protected MetaName IMover = new MetaName("Animals", "IMover");
        protected MetaName AnimalFactory = new MetaName("Animals", "AnimalFactory");
        protected MetaName AnimalType = new MetaName("Animals", "AnimalType");
        protected MetaName HorizontalDirection = new MetaName("Animals", "HorizontalDirection");
        protected MetaName VerticalDirection = new MetaName("Animals", "VerticalDirection");
        protected MetaName BirthingRoom = new MetaName("BirthingRooms", "BirthingRoom");
        protected MetaName CouponBook = new MetaName("BoothItems", "CouponBook");
        protected MetaName Map = new MetaName("BoothItems", "Map");
        protected MetaName Ticket = new MetaName("BoothItems", "Ticket");
        protected MetaName WaterBottle = new MetaName("BoothItems", "WaterBottle");
        protected MetaName Item = new MetaName("BoothItems", "Item");
        protected MetaName SoldItem = new MetaName("BoothItems", "SoldItem");
        protected MetaName MissingItemException = new MetaName("BoothItems", "MissingItemException");
        protected MetaName Food = new MetaName("Foods", "Food");
        protected MetaName IEater = new MetaName("Foods", "IEater");
        protected MetaName MoneyCollector = new MetaName("MoneyCollectors", "MoneyCollector");
        protected MetaName MoneyBox = new MetaName("MoneyCollectors", "MoneyBox");
        protected MetaName MoneyPocket = new MetaName("MoneyCollectors", "MoneyPocket");
        protected MetaName IMoneyCollector = new MetaName("MoneyCollectors", "IMoneyCollector");
        protected MetaName Booth = new MetaName("People", "Booth");
        protected MetaName GivingBooth = new MetaName("People", "GivingBooth");
        protected MetaName MoneyCollectingBooth = new MetaName("People", "MoneyCollectingBooth");
        protected MetaName Employee = new MetaName("People", "Employee");
        protected MetaName Guest = new MetaName("People", "Guest");
        protected MetaName Restroom = new MetaName("People", "Restroom");
        protected MetaName Wallet = new MetaName("People", "Wallet");
        protected MetaName WalletColor = new MetaName("People", "WalletColor");
        protected MetaName Gender = new MetaName("Reproducers", "Gender");
        protected MetaName IReproducer = new MetaName("Reproducers", "IReproducer");
        protected MetaName WuvLuv = new MetaName("Toys", "WuvLuv");
        protected MetaName VendingMachine = new MetaName("VendingMachines", "VendingMachine");
        protected MetaName Zoo = new MetaName("Zoos", "Zoo");
        protected MetaName Program = new MetaName("ZooConsole", "Program");
        protected MetaName ConsoleHelper = new MetaName("ZooConsole", "ConsoleHelper");
        protected MetaName ConsoleUtil = new MetaName("ZooConsole", "ConsoleUtil");
        protected MetaName MainWindow = new MetaName("ZooScenario", "MainWindow");
        protected MetaName GuestWindow = new MetaName("ZooScenario", "GuestWindow");
        protected MetaName AnimalWindow = new MetaName("ZooScenario", "AnimalWindow");
        protected MetaName CageWindow = new MetaName("ZooScenario", "CageWindow");

        /// <summary>
        /// Initializes a new instance of the ZooTestBase class.
        /// </summary>
        public ZooTestBase()
        {
            this.RegisterTypes(
                Account,
                Animal,
                Bird,
                Chimpanzee,
                Dingo,
                Eagle,
                Fish,
                Hummingbird,
                Kangaroo,
                Mammal,
                Ostrich,
                Platypus,
                Shark,
                Squirrel,
                IHatchable,
                IMover,
                AnimalFactory,
                AnimalType,
                BirthingRoom,
                CouponBook,
                Map,
                Ticket,
                WaterBottle,
                Item,
                SoldItem,
                Food,
                IEater,
                MoneyCollector,
                MoneyBox,
                MoneyPocket,
                IMoneyCollector,
                Booth,
                GivingBooth,
                MoneyCollectingBooth,
                Employee,
                Guest,
                Restroom,
                Wallet,
                WalletColor,
                Gender,
                IReproducer,
                WuvLuv,
                VendingMachine,
                Zoo,
                Program,
                ConsoleHelper,
                ConsoleUtil,
                AnimalWindow,
                GuestWindow,
                MainWindow);
        }

        /// <summary>
        /// Creates an instance of the MainWindow and creates and configures the como zoo.
        /// </summary>
        /// <returns>An instance of the MainWindow class.</returns>
        protected object CreateWindow()
        {
            object mainWindow = this.CreateObject(MainWindow);

            return mainWindow;
        }

        protected object CreateMainWindowAndSetComoZooField()
        {
            object zoo = this.InvokeMethod(this.CreateZoo(), "NewZoo");
            object mainWindow = this.CreateWindow();
            this.SetValue(mainWindow, "comoZoo", zoo);
            return mainWindow;
        }

        protected object CreateDingo()
        {
            return this.CreateObject(Dingo, "Betty", 10, 100.0, "Gender.Female");
        }

        protected object CreateEmployee()
        {
            return this.CreateObject(Employee, "Bob", 101);
        }

        protected object CreateBooth()
        {
            object e = this.CreateEmployee();
            return this.CreateObject(Booth, e, 25.00m, 10.00m);
        }

        protected object CreateZoo()
        {
            return this.CreateObject(Zoo, "Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m,
                                                                this.CreateObject(Employee, "Sam", 42),
                                                                this.CreateObject(Employee, "Flora", 98));
        }

        protected object CreateRichGuest()
        {
            object account = this.CreateObject(Account);
            return this.CreateObject(Guest, "Jack", 15, 250.00m, null, null, account);
        }

        protected object CreatePoorGuest()
        {
            object account = this.CreateObject(Account);
            return this.CreateObject(Guest, "Jane", 15, 0m, null, null, account);
        }
    }
}