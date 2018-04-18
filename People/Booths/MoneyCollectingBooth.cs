﻿using BoothItems;
using MoneyCollectors;
using System;
using System.Collections.Generic;

namespace People
{
    /// <summary>
    /// The class which is used to represent a money-collecting booth.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The booth's money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The stack of tickets.
        /// </summary>
        private Stack<Ticket> ticketStack;

        /// <summary>
        /// The price of a water bottle.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The booth's attendant.</param>
        /// <param name="ticketPrice">The booth's ticket price.</param>
        /// <param name="waterBottlePrice">The booth's water bottle price.</param>
        /// <param name="moneyBox">The booth's money box.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;
            this.ticketStack = new Stack<Ticket>();

            // Create tickets.
            for (int i = 0; i < 5; i++)
            {
                this.ticketStack.Push(new Ticket(this.TicketPrice, i + 1, 0.01));
            }

            // Create water bottles.
            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new WaterBottle(this.WaterBottlePrice, i + 1, 1));
            }
        }

        /// <summary>
        /// Gets the balance of the booth's money box.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets the price of a ticket.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets the price of a water bottle.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// Adds money to the booth's money box.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the booth's money box.
        /// </summary>
        /// <param name="amount">The amount to remove from the money box.</param>
        /// <returns>The amount removed from the money box.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// Sells a ticket.
        /// </summary>
        /// <param name="payment">The payment for the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Ticket ticket = null;

            try
            {
                // If a sufficient payment was received...
                if (payment >= this.TicketPrice)
                {
                    // Find the first ticket.
                    ticket = this.ticketStack.Pop();
                }

                // If a ticket was found...
                if (ticket != null)
                {
                    // Add payment to money box.
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }
            return ticket;
        }

        /// <summary>
        /// Sells a water bottle.
        /// </summary>
        /// <param name="payment">The payment for the water bottle.</param>
        /// <returns>The sold water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            WaterBottle waterBottle = null;

            try
            {
            // If a sufficient payment was received...
            if (payment >= this.WaterBottlePrice)
            {
                // Find the first water bottle.
                waterBottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle)) as WaterBottle;
            }

            // If a water bottle was found...
            if (waterBottle != null)
            {
                // Add payment to money box.
                this.AddMoney(payment);
            }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Water bottle not found.", ex);
            }


            return waterBottle;
        }
    }
}