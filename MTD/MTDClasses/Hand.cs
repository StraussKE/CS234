using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    /// <summary>
    /// Represents a hand of dominos
    /// </summary>
    public class Hand
    {

        /// <summary>
        /// The maximum number of pips allowable
        /// </summary>
        private const int MAXPIPS = 12;

        /// <summary>
        /// The list of dominos in the hand
        /// </summary>
        private List<Domino> playerHand;

        /// <summary>
        /// Creates an empty hand
        /// </summary>
        public Hand()
        {
            playerHand = new List<Domino>();
        }

        /// <summary>
        /// Creates a hand of dominos from the boneyard.
        /// The number of dominos is based on the number of players
        /// 2–4 players: 10 dominoes each
        /// 5–6 players: 9 dominoes each
        /// 7–8 players: 7 dominoes each
        /// </summary>
        /// <param name="by"></param>
        /// <param name="numPlayers"></param>
        public Hand(BoneYard by, int numPlayers)
        {
            switch (numPlayers)
            {
                case 2:
                case 3:
                case 4:
                    playerHand = new List<Domino>(10);
                    while (Count < 10)
                    {
                        Draw(by);
                    }
                    break;
                case 5:
                case 6:
                    playerHand = new List<Domino>(9);
                    while (Count < 9)
                    {
                        Draw(by);
                    }
                    break;
                case 7:
                case 8:
                    playerHand = new List<Domino>(7);
                    while (Count < 7)
                    {
                        Draw(by);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid quantity of players");
            }
        }

        /// <summary>
        /// Adds a domino to the hand
        /// </summary>
        /// <param name="d"></param>
        public void Add(Domino d)
        {
            playerHand.Add(d);
        }

        /// <summary>
        /// Tracks the number of dominos currently in the hand
        /// </summary>
        public int Count => playerHand.Count;

        /// <summary>
        /// Checks to see if the hand is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => (Count == 0) ? true : false;

        /// <summary>
        /// Sum of the score of each domino in the hand
        /// </summary>
        public int Score
        {
            get
            {
                int score = 0;
                foreach (Domino d in playerHand)
                {
                    score += d.Score;
                }
                return score;
            }
        }

        /// <summary>
        /// Does the hand contain a domino with value in side 1 or side 2?
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        public bool HasDomino(int value)
        {
            foreach (Domino d in playerHand)
            {
                if (d.Side1 == value || d.Side2 == value)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///  Does the hand contain a double of a certain value?
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        public bool HasDoubleDomino(int value)
        {
            foreach (Domino d in playerHand)
            {
                if (d.Side1 == value && d.Side2 == value)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The index of a domino with a value in the hand
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDomino(int value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (playerHand[i].Side1 == value || playerHand[i].Side2 == value)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// The index of the double domino with a value in the hand
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDoubleDomino(int value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (playerHand[i].Side1 == value && playerHand[i].IsDouble())
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// The index of the highest double domino in the hand
        /// </summary>
        /// <returns>-1 if there isn't a double in the hand</returns>
        public int IndexOfHighDouble()
        {
            int areYouTheOne;
            for (int i = MAXPIPS; i >= 0; i--)
            {
                areYouTheOne = IndexOfDoubleDomino(i);
                if (areYouTheOne != -1)
                    return areYouTheOne;
            }
            return -1;
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Domino this[int index]
        {
            get => playerHand[index];
            set => playerHand[index] = value;
        }

        /// <summary>
        /// Removes domino from specified index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            playerHand.RemoveAt(index);
        }

        /// <summary>
        /// Finds a domino with a certain number of dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDomino(int value)
        {
            int index = (IndexOfDomino(value));
            if (index == -1)
                return null;
            Domino gotIt = playerHand[index];
            RemoveAt(index);
            return gotIt;
        }
 
        /// <summary>
        /// Finds a domino with a certain number of double dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDoubleDomino(int value)
           {
               int index = (IndexOfDomino(value));
               if (index == -1)
                   return null;
               Domino gotIt = playerHand[index];
               RemoveAt(index);
               return gotIt;
           }
 
        /// <summary>
        /// Draws a domino from the boneyard and adds it to the hand
        /// </summary>
        /// <param name="by"></param>
        public void Draw(BoneYard by)
        {
            Add(by.Draw());
        }

        /// <summary>
        /// Plays the domino at the index on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino at the index
        /// is not playable.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="t"></param>
        private void Play(int index, Train t)
        {
            Domino d = this.playerHand[index];
            if (d.Side1 != t.PlayableValue && d.Side2 != t.PlayableValue)
            {
                throw new ArgumentException("The selected domino is not playable on the selected train.");
            }
            if (d.Side2 == t.PlayableValue)
            {
                if (d.Side1 != t.PlayableValue)
                {
                    d.Flip();
                }
            }
            RemoveAt(index);
            t.Add(d);
        }
  

        /// <summary>
        /// Plays the domino from the hand on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino is not in the hand
        /// or is not playable.
        /// </summary>
        public void Play(Domino d, Train t)
        {
            bool located = false;
            int index = 0;

            do
            {
                if (this.playerHand[index] == d)
                {
                    located = true;
                }
                else
                { index++; }
            } while (located == false && index < Count);

            if (located == false)
            {
                throw new ArgumentException("Selected domino is not contained in this hand.");
            }
            Play(index, t);
        }

        /// <summary>
        /// Plays the first playable domino in the hand on the train
        /// Removes the domino from the hand.
        /// Returns the domino.
        /// Throws an exception if no dominos in the hand are playable.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Domino Play(Train t)
        {
            Domino d = GetDomino(t.PlayableValue);
            if (d == null)
            {
                throw new ArgumentException("There is no playable domino in this hand");
            }
            if (d.Side2 == t.PlayableValue)
            {
                if (d.Side1 != t.PlayableValue)
                {
                    d.Flip();
                }
            }
            this.playerHand.Remove(d);
            return d;
        }

        public override string ToString()
        {
            string showHand = null;
            foreach (Domino d in this.playerHand)
            {
                showHand = d.ToString() + " ";
            }
            return showHand;
        }
        
    }
}
