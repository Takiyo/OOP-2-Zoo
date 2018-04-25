using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Foods;
using Reproducers;
using Utilities;


namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// An animal's list of children.
        /// </summary>
        private List<Animal> children;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The timer the animal moves on.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// A random field ecks dee.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.gender = gender;
            this.name = name;
            this.weight = weight;
            this.children = new List<Animal>();

            this.XPositionMax = 800;
            this.YPositionMax = 400;

            // ~Ask about random field vs local random object.~
            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.Next(1, this.XPositionMax + 1);
            this.YPosition = random.Next(1, this.YPositionMax + 1);

            this.YDirection = Animal.random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            this.XDirection = Animal.random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;

            this.CreateTimers();
        }

        /// <summary>
        /// Initializes a new instance of the Animal class, chained.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal.</param>
        /// <param name="gender">The animal's gender.</param>
        public Animal(string name, double weight, Gender gender)
            : this(name, 0, weight, gender)
        {            
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 100.");
                }

                this.age = value;
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            protected set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// An animal's list of children.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets the animal's display size.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                
                double result = (this.Age == 0) ? 0.5 : 1.0;
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the gender of the animal.
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
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
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
                // Placeholder - not sure how to call OnTextChange from here.
            }
        }

        /// <summary>
        /// Gets the animal's resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name + (this.Age == 0 ? "Baby" : "Adult");
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException("weight", "Weight must be between 0 and 1000 lbs.");
                }

                this.weight = value;
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// The following properties get and set positions, distances, and directions related to the
        /// animal moving in its cage.
        /// </summary>
        public int MoveDistance { get; set; }
        public HorizontalDirection XDirection { get; set; }
        public int XPosition { get; set; }
        public int XPositionMax { get; set; }
        public VerticalDirection YDirection { get; set; }
        public int YPosition { get; set; }
        public int YPositionMax { get; set; }

        /// <summary>
        /// Adds a child to the list of animal's children.
        /// </summary>
        /// <param name="animal"></param>
        public void AddChild(Animal animal)
        {
            this.children.Add(animal);
        }

        /// <summary>
        /// Converts animal type to type.
        /// </summary>
        /// <param name="animalType">Animaltype to be converted.</param>
        /// <returns>The converted type.</returns>
        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            Type type = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    type = typeof(Chimpanzee);
                    break;
                case AnimalType.Dingo:
                    type = typeof(Dingo);
                    break;
                case AnimalType.Eagle:
                    type = typeof(Eagle);
                    break;
                case AnimalType.Hummingbird:
                    type = typeof(Hummingbird);
                    break;
                case AnimalType.Kangaroo:
                    type = typeof(Kangaroo);
                    break;
                case AnimalType.Ostrich:
                    type = typeof(Ostrich);
                    break;
                case AnimalType.Platypus:
                    type = typeof(Platypus);
                    break;
                case AnimalType.Shark:
                    type = typeof(Shark);
                    break;
                case AnimalType.Squirrel:
                    type = typeof(Squirrel);
                    break;
                default:
                    throw new Exception("Unsupported animal type.");
            }

            return type;
        }

        /// <summary>
        /// Creates a timer.
        /// </summary>
        private void CreateTimers()
        {
            this.moveTimer = new Timer(250);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();
        }

        /// <summary>
        /// placeholder
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            // Increase animal's weight as a result of eating food.
            this.Weight += food.Weight * (this.WeightGainPercentage / 100);
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.isPregnant = true;
            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// Moves the animal.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);
        }

        /// <summary>
        /// Gets or sets the animal's move behavior.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's eat behavior.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's reproduce behavior.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public virtual IReproducer Reproduce()
        {
            int gender = random.Next(1, Enum.GetNames(typeof(Gender)).Length + 1);

            // Create a new reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100), (gender == 1 ? Gender.Female : Gender.Male)) as Animal;

            // Adds baby to mother's list of children.
            this.AddChild(baby);

            // Reduce the parent's weight.
            this.Weight -= baby.Weight * 1.25;

            // Make the parent to be no longer pregnant.
            this.isPregnant = false;

            return baby;
        }

        /// <summary>
        /// Placeholder~~~
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if  DEBUG
            this.moveTimer.Stop();
#endif

            this.Move();

#if DEBUG
            this.moveTimer.Start();
#endif
        }



        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")" + (this.IsPregnant ? " P" : string.Empty);
        }
    }
}