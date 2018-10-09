using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTDClasses
{
    /// <summary>
    /// Represents a generic Train for MTD
    /// </summary>
    public abstract class Train
    {

        /// <summary>
        /// The list of dominos in the train
        /// </summary>
        protected List<Domino> currentTrain;
        protected int reqEngVal;

        /// <summary>
        /// Creates an empty train
        /// </summary>
        public Train()
        {
            currentTrain = new List<Domino>();
        }

        /// <summary>
        /// Creates an empty train with an expected engine value
        /// </summary>
        /// <param name="engValue"></param>
        public Train(int engValue)
        {
            currentTrain = new List<Domino>();
            EngineValue = engValue;
        }

        public int Count => currentTrain.Count;

        /// <summary>
        /// The first domino value that must be played on a train
        /// </summary>
        public int EngineValue
        {
            get => reqEngVal;
            set => reqEngVal = value;
        }

        /// <summary>
        /// Checks if there are any tiles in the train
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => (Count == 0) ? true : false;

        /// <summary>
        /// Tracks the tail of the train
        /// </summary>
        public Domino LastDomino => currentTrain[Count - 1];

        /// <summary>
        /// Side2 of the last domino in the train.  It's the value of the next domino that can be played.
        /// </summary>
        public int PlayableValue => LastDomino.Side2;


        /// <summary>
        /// Adds a specified domino to the end of the train
        /// </summary>
        /// <param name="d">the domino being added</param>
        public void Add(Domino d) => currentTrain.Add(d);
        

        public Domino this[int index]
        {
            get { return currentTrain[index]; }
            set { currentTrain[index] = value; }
        }
 /*       
        /// <summary>
        /// Determines whether a hand can play a specific domino on this train and if the domino must be flipped.
        /// Because the rules for playing are different for Mexican and Player trains, this method is abstract.
        /// </summary>
        public abstract bool IsPlayable(Hand h, Domino d, out bool mustFlip);

        /// <summary>
        /// A helper method that determines whether a specific domino can be played on this train.
        /// It can be called in the Mexican and Player train class implementations of the abstract method
        /// </summary>
        protected bool IsPlayable(Domino d, out bool mustFlip)
        {
        }
*/
        // assumes the domino has already been removed from the hand
        public void Play(Hand h, Domino d)
        {
            Add(d);
        }
        /*
        public override string ToString()
        {
        }
        */

    }

}