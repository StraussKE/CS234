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

        protected List<Domino> currentTrain;
        protected int reqEngVal;
        
        public Train()
        {
            currentTrain = new List<Domino>();
        }

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

        public bool IsEmpty() => (Count == 0) ? true : false;

        public Domino LastDomino => currentTrain[Count - 1];

        /// <summary>
        /// Side2 of the last domino in the train.  It's the value of the next domino that can be played.
        /// </summary>
        public int PlayableValue => LastDomino.Side2;

        /*
        public void Add(Domino d)
        {
        }
        */
/*
        public Domino this[int index]
        {
        }
        
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

        }
        /*
        public override string ToString()
        {
        }
        */

    }

}