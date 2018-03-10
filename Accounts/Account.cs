using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// The class that represents an account.
    /// </summary>
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// The collector's current money balance.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets the collector's current money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }

            set
            {
                this.moneyBalance = value;
            }
        }

        /// <summary>
        /// Adds money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        /// <summary>
        /// Removes money from the account.
        /// </summary>
        /// <param name="amount">The amount to remove.</param>
        /// <returns>The amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            this.MoneyBalance -= amount;

            return amount;
        }
    }
}