﻿using System;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money collector.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The money collector's current money balance.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets the money collector's current money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }
        }

        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
            this.OnBalanceChange?.Invoke();
        }

        /// <summary>
        /// Removes a specified amount of money from the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to remove from the money collector.</param>
        /// <returns>The amount of money that was removed from the money collector.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Return all remaining money.
                amountRemoved = this.MoneyBalance;
            }

            // Subtract the amount removed from the wallet.
            this.moneyBalance -= amountRemoved;
            this.OnBalanceChange?.Invoke();

            return amountRemoved;
        }
    }
}