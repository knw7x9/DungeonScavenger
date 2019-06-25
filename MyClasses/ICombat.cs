using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// ICompat.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - An interface that is implemented in Monster and Hero allowing one of the actors to attack the other
    /// </summary>
    public interface ICombat {
        /// <summary>
        /// An actor can attack another actor
        /// </summary>
        /// <param name="actor">the actor to be attacked</param>
        /// <returns></returns>
        bool Attack(Actor actor);
    }
}
