using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface used for moving.
    /// </summary>
    public interface IMoveBehavior
    {
        void Move(Animal animal);
    }
}
