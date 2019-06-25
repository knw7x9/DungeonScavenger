using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// IRepeatable.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - An interface that is implemented in Monster, Weapon and Potion and creates a deep copy of the object
    /// </summary>
    public interface IRepeatable<T> {
        /// <summary>
        /// Creates a deep copy of the object
        /// </summary>
        /// <returns>a copy of the object</returns>
        T CreateCopy();
    }
}
