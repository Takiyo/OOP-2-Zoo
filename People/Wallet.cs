﻿using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private WalletColor color;

        /// <summary>
        /// The wallet's money pocket.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        /// <param name="moneyPocket">The wallet's money pocket.</param>
        public Wallet(WalletColor color, IMoneyCollector moneyPocket)
        {
            this.color = color;
            this.moneyPocket = moneyPocket;
        }

        /// <summary>
        /// Gets or sets the wallet's color.
        /// </summary>
        public WalletColor Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets the money balance of the wallet's money pocket.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }
        }

        /// <summary>
        /// Adds money to the wallet's money pocket.
        /// </summary>
        /// <param name="amount">The amount to add to the money pocket.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// Removes a specified amount of money from the wallet's money pocket.
        /// </summary>
        /// <param name="amount">The amount to remove from the money pocket.</param>
        /// <returns>The amount removed from the money pocket.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyPocket.RemoveMoney(amount);
        }
    }
}