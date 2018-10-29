using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    [Serializable()]
    public class Domino : IComparable<Domino>
    {
        /// <summary>
        /// Constants to control the minimum number of pips and the maximum number of pips on a domino
        /// </summary>
        const int MINPIPS = 0;
        const int MAXPIPS = 12;
        
        /// <summary>
        /// Sides of the domino
        /// </summary>
        private int side1;                                                  // the domino has sides
        private int side2;

        /// <summary>
        /// Makes a 0,0 domino
        /// </summary>
        public Domino()
        {
            Side1 = 0;                                                      // and a default constructor
            Side2 = 0;
        }

        /// <summary>
        /// Makes a domino with the designated sides
        /// </summary>
        /// <param name="p1">soon to be side 1</param>
        /// <param name="p2">soon to be side 2</param>
        public Domino(int p1, int p2)                                       // and a constructor for the rest of the dominoes
        {
            Side1 = p1;
            Side2 = p2;
        }

        /// <summary>
        /// Sets the value of side 1 within the specified constraints
        /// </summary>
        // don't use an auto implemented property because of the validation in the setter - p 390
        public int Side1
        {
            get
            {
                return side1;
            }
            set
            {
                if (value >= MINPIPS && value <= MAXPIPS)                                  // you can only make legitimate dominoes
                    side1 = value;
                else
                    throw new ArgumentException("A 12 pip domino cannot have that value.  Value must be between 0 and 12.");
            }
        }

        /// <summary>
        /// Same as side1, but for side2
        /// </summary>
        public int Side2
        {
            get
            {
                return side2;
            }
            set
            {
                if (value >= MINPIPS && value <= MAXPIPS)                                  // the secone side has to be legit too
                    side2 = value;
                else
                    throw new ArgumentException("A 12 pip domino cannot have that value.  Value must be between 0 and 12.");
            }
        }

        /// <summary>
        /// Swaps side1 and side2
        /// </summary>
        public void Flip()
        {
            int temp = Side1;
            Side1 = Side2;
            Side2 = temp;
        }

        /// <summary>
        /// Returns the score of a single domino, which is the sum of its parts
        /// </summary>
        // This is how I would have done this in 233N
        public int Score
        {
            get
            {
                return side1 + side2;                                           // domino is worth the points of the sum of all pips
            }
        }

        // because it's a read only property, I can use the "expression bodied syntax" or a lamdba expression - p 393
        //public int Score => side1 + side2;

        /// <summary>
        /// Checks to see if the domino is a double
        /// </summary>
        /// <returns></returns>
        //ditto for the first version of this method and the next one
        public bool IsDouble()
        {
            if (side1 == side2)                                                 // do the sides match?
            {
                return true;
            }
            return false;
        }

        // could you do this one using a lambda expression?      

        //public bool IsDouble() => (side1 == side2) ? true : false;

        public override string ToString()
        {
            return String.Format("Side 1: {0}  Side 2: {1}", side1, side2);
        }

        // could you overload the == and != operators?
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false ;
            Domino d = (Domino)obj;
            if (this.side1 == d.side1 &&
                this.side2 == d.side2)
                return true;
            return false;
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();                                        // also not full comprehension on my behalf here, but it seems to be required
        }

        public int CompareTo(Domino other) =>
            this.Score.CompareTo(other.Score);
    }
}
