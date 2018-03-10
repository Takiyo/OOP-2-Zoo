using System;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class which is used to represent a giving booth.
    /// </summary>
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The booth's attendant.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            // Create maps.
            for (int i = 0; i < 10; i++)
            {
                this.Items.Add(new Map(0.5, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)));
            }

            // Create coupon books.
            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new CouponBook(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day), 0.8));
            }
        }

        /// <summary>
        /// Gives away a coupon book.
        /// </summary>
        /// <returns>The resulting coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
            // Find first coupon book.
            return this.Attendant.FindItem(this.Items, typeof(CouponBook)) as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Couponbook not found.", ex);
            }
        }

        /// <summary>
        /// Gives away a map.
        /// </summary>
        /// <returns>The resulting map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                // Find first map.
                return this.Attendant.FindItem(this.Items, typeof(Map)) as Map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Map not found.", ex);
            }
        }
    }
}