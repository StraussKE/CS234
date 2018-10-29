using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using MTDClasses;

namespace MTDTests
{
    [TestFixture]
    public class MexicanTrainTests
    {
        Hand anyHand;
        MexicanTrain trialExpress;
        bool mustFlip;

        [SetUp]
        public void SetUpAllTests()
        {
            anyHand = new Hand();
            trialExpress = new MexicanTrain(12);
            mustFlip = false;
        }

        [Test]
        public void TestIsPlayableNoFlipYesPlay()
        {
            Domino playableNoFlip = new Domino(12, 11);

            Assert.True(trialExpress.IsPlayable(anyHand, playableNoFlip, out mustFlip));
        }

        [Test]
        public void TestIsPlayableYesFlipYesPlay()
        {
            Domino playableYesFlip = new Domino(11, 12);

            Assert.True(trialExpress.IsPlayable(anyHand, playableYesFlip, out mustFlip));
        }

        [Test]
        public void TestIsPlayableNotPlayable()
        {
            Domino notPlayable = new Domino(3, 5);

            Assert.False(trialExpress.IsPlayable(anyHand, notPlayable, out mustFlip));
        }
    }
}
