using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class BoneYard
    {
        private List<Domino> boneYard;                                          // list instead of array because it's dynamic
        private static Random randGen = new Random();                           // random number basis for shuffling

        public BoneYard()                                                       // generates empty boneyard
        {
            boneYard = new List<Domino>();
        }

        public BoneYard(int maxDots)                                            // generates populated boneyard based on the maximum number of pips desired
        {
            boneYard = new List<Domino>();
            if (maxDots > 0)
            {
                for (int i = 0; i <= maxDots; i++)
                {
                    for (int j = i; j <= maxDots; j++)
                    {
                        boneYard.Add(new Domino(i, j));
                    }
                }
            }
            else
            {
                throw new ArgumentException("Dominoes can not have negative dots.");
            }
        }
        
        public void Shuffle()
        {
            Domino temp;                                                        // temporary domino to hold value while swapping things around

            for (int i = 0; i < 10; i++)                                        // in the spirit of thoroughness let's shuffle things 10 times
            {
                for (int j = 0; j < DominosRemaining; j++)                      // for all of the tiles that we have
                {
                    int toSwap = randGen.Next(boneYard.Count);                  // determine our random tile to switch with our current tile
                    temp = boneYard[j];                                         // set the value holding domino to match our current tile
                    boneYard[j] = boneYard[toSwap];                             // current tile now equals our random tile
                    boneYard[toSwap] = temp;                                    // random tile is modified with our value holder
                }
            }
        }

        public bool IsEmpty() => (DominosRemaining == 0) ? true : false;        // trying it lambda style

        public int DominosRemaining => boneYard.Count;                          // I think I might like lambda style
        
        public Domino Draw()                                                    // drawing the tiles
        {
            if (IsEmpty())                                                      // if it's empty throw an exception
            {
                throw new IndexOutOfRangeException("No dominoes remain to be drawn.");
            }
            Domino drawn = boneYard[DominosRemaining - 1];                      // predecrement the value since the remaining tile count is inherently one higher than the index
            boneYard.RemoveAt(DominosRemaining - 1);                                // remove the domino that was drawn from the list
            return drawn;                                                       // return the domino that was just drawn
        }

        public Domino this[int index]                                           // indexering (I don't understand this well yet)
        {
            get { return boneYard[index]; }
            set { boneYard[index] = value; }
        }

        public override string ToString()                                       // now it can be a string
        {
            string remainingTiles = null;
            foreach (Domino d in boneYard)
            {
                remainingTiles = d.ToString() + " ";
            }
            return remainingTiles;
        } 
    }
}
