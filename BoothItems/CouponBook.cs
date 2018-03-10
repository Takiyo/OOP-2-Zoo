﻿using System;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a coupon book.
    /// </summary>
    public class CouponBook : Item
    {
        /// <summary>
        /// The date the coupon book was made.
        /// </summary>
        private DateTime dateMade;

        /// <summary>
        /// The expiration date of the coupon book.
        /// </summary>
        private DateTime dateExpired;

        /// <summary>
        /// Initializes a new instance of the CouponBook class.
        /// </summary>
        /// <param name="dateMade">The date the coupon book was made.</param>
        /// <param name="dateExpired">The date the coupon book expires.</param>
        /// <param name="weight">The weight of the coupon book.</param>
        public CouponBook(DateTime dateMade, DateTime dateExpired, double weight)
            : base(weight)
        {
            this.dateMade = dateMade;
            this.dateExpired = dateExpired;
        }

        /// <summary>
        /// Gets the date the coupon book was made.
        /// </summary>
        public DateTime DateMade
        {
            get
            {
                return this.dateMade;
            }
        }

        /// <summary>
        /// Gets the expiration date of the coupon book.
        /// </summary>
        public DateTime DateExpired
        {
            get
            {
                return this.dateExpired;
            }
        }
    }
}