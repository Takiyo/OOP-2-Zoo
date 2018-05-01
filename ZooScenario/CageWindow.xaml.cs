using Animals;
using CagedItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml
    /// </summary>
    public partial class CageWindow : Window
    {
        /// <summary>
        /// The cagewindow's cage.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// Draws the animal on the timer.
        /// </summary>
        private Timer redrawTimer;

        /// <summary>
        /// The window representing a cage.
        /// </summary>
        /// <param name="animal">The animal in the cage.</param>
        public CageWindow(Cage cage)
        {
            InitializeComponent();

            this.cage = cage;

            this.redrawTimer = new Timer(100);
            this.redrawTimer.Elapsed += this.RedrawHandler;
            this.redrawTimer.Start();
        }

        /// <summary>
        /// Draws animal on canvas.
        /// </summary>
        /// <param name="animal">The animal to be drawn.</param>
        private void DrawItem(ICageable item)
        {

            // Creates and aligns viewbox.
            Viewbox viewBox = GetViewBox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);
            viewBox.HorizontalAlignment = HorizontalAlignment.Left;
            viewBox.VerticalAlignment = VerticalAlignment.Top;

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewbox.
                viewBox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewbox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Apply the ScaleTransform to the viewbox
                viewBox.RenderTransform = flipTransform;
            }

            TransformGroup transformGroup = new TransformGroup();

            if (item.HungerState == HungerState.Unconscious)
            {
                SkewTransform unconsciousSkew = new SkewTransform();
                unconsciousSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;
                transformGroup.Children.Add(unconsciousSkew);

                transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));

                viewBox.RenderTransform = transformGroup;
            }

            this.cageGrid.Children.Add(viewBox);
        }

        /// <summary>
        /// Draws every animal in the cage.
        /// </summary>
        private void DrawAllItems()
        {
            // Initially clears the cage.
            this.cageGrid.Children.Clear();

            // Draws each animal in the enumerable list of animals.
            foreach (ICageable c in cage.CagedItems)
            {
                this.DrawItem(c);
            }
        }

        /// <summary>
        /// Gets the viewbox.
        /// </summary>
        /// <param name="maxXPosition">The max horizontal position of the entity.</param>
        /// <param name="maxYPosition">The max vertical position of the entity.</param>
        /// <param name="xPosition">The current horizontal position of the entity.</param>
        /// <param name="yPosition">The current vertical position of the entity.</param>
        /// <param name="canvas">The canvas to be </param>
        /// <returns>Returns the finished viewbox.</returns>
        private Viewbox GetViewBox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewbox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewbox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewbox.
            return finishedViewBox;
        }

        /// <summary>
        /// Executed when the window loads.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void cageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        /// <summary>
        /// Placeholder~
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.DrawAllItems();
                }));
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
