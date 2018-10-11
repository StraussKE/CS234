using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class MexicanTrain : Train
    {
        /// Can the domino d be played by the hand h on this train?
        /// If it can be played, must it be flipped to do so?
        /// </summary>
        /// <param name="d"></param>
        /// <param name="mustFlip"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public override bool IsPlayable(Hand h, Domino d, out bool mustFlip)
        {
            mustFlip = false;
            if (IsPlayable(d, out mustFlip))
                return true;
            return false;
        }
    }
}
