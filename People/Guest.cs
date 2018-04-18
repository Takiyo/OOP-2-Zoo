using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Animals;
using BoothItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// A bag for holding the guest's items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// The checking account for collecting money.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">The gender of the guest.</param>
        /// <param name="checkingAccount">The account for collecting money.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.bag = new List<Item>();
            this.checkingAccount = checkingAccount;
            this.gender = gender;
            this.name = name;
            this.wallet = new Wallet(walletColor, new MoneyPocket());

            // Add money to wallet.
            this.wallet.AddMoney(moneyBalance);

            this.XPosition = 0;
            this.YPosition = 0;
        }

        /// <summary>
        /// Gets or sets the guest's adopted animal.
        /// </summary>
        public Animal AdoptedAnimal { get; set; }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 120.");
                }

                this.age = value;
            }
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets the guest's display size for the cage window.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets or sets the guest's gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new ArgumentException("Names can contain only upper- and lowercase letters A-Z and spaces.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets the guest's resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the guest's wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }
            set
            {
                return;
            }
        }

        public double WeightGainPercentage { get { return 0; } }

        /// <summary>
        /// The following 4 properties get or set the position and direction of the guest while they're in the cage.
        /// </summary>
        public int XPosition{ get; private set; }

        public int YPosition { get; private set; }

        public HorizontalDirection XDirection { get; private set; }

        public VerticalDirection YDirection { get; private set; }


        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        /// <param name="animalSnackMachine">The animal snack machine from which to buy food.</param>
        public void FeedAnimal(IEater eater, VendingMachine animalSnackMachine)
        {
            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            // Check if guest has enough money on hand and withdraw from account if necessary.
            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 10);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            if (this.AdoptedAnimal != null)
            {
                return string.Format("{0}: {1} [${2} / ${3} - Adopted: {4}]", this.Name, this.Age, this.Wallet.MoneyBalance, this.CheckingAccount.MoneyBalance, this.AdoptedAnimal.Name);
            }
            else
            {
                return string.Format("{0}: {1} [${2} / ${3}]", this.Name, this.Age, this.Wallet.MoneyBalance, this.CheckingAccount.MoneyBalance);
            }
        }

        /// <summary>
        /// Visits the information booth to obtain a coupon book and a map.
        /// </summary>
        /// <param name="informationBooth">The booth to visit.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // Get map.
            Map map = informationBooth.GiveFreeMap();

            // Get coupon book.
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();

            // Add items to bag.
            this.bag.Add(map);
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// Visits the booth to purchase a ticket and a water bottle.
        /// </summary>
        /// <param name="ticketBooth">The booth to visit.</param>
        /// <returns>A purchased ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            if (this.wallet.MoneyBalance < ticketBooth.TicketPrice)
            {
                this.WithdrawMoney(ticketBooth.TicketPrice * 2);
            }

            // Take ticket money out of wallet.
            decimal ticketPayment = this.wallet.RemoveMoney(ticketBooth.TicketPrice);

            // Buy ticket.
            Ticket ticket = ticketBooth.SellTicket(ticketPayment);

            if (this.wallet.MoneyBalance < ticketBooth.WaterBottlePrice)
            {
                this.WithdrawMoney(ticketBooth.WaterBottlePrice * 2);
            }

            // Take water bottle money out of wallet.
            decimal waterPayment = this.wallet.RemoveMoney(ticketBooth.WaterBottlePrice);

            // Buy water bottle.
            WaterBottle bottle = ticketBooth.SellWaterBottle(waterPayment);

            // Add water bottle to bag.
            this.bag.Add(bottle);

            return ticket;
        }

        /// <summary>
        /// Withdraws money from the checking account and puts it into the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to withdraw.</param>
        public void WithdrawMoney(decimal amount)
        {
            decimal retrievedAmount = this.checkingAccount.RemoveMoney(amount);

            this.wallet.AddMoney(retrievedAmount);
        }
    }
}