
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Creates move behaviors.
    /// </summary>
    [Serializable]
    public static class MoveBehaviorFactory
    {
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            IMoveBehavior result = null;
            switch (type)
            {
                case MoveBehaviorType.Fly:                   
                    result = new FlyBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    result = new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    result = new SwimBehavior();
                    break;
                case MoveBehaviorType.Climb:
                    result = new ClimbBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    result = new HoverBehavior();
                    break;
                case MoveBehaviorType.Hop:
                    result = new HopBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    result = new NoMoveBehavior();
                    break;                  
            }

            return result;
        }
    }
}
