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
    public class PlayerTrainTests
    {
        Hand playerOne;
        PlayerTrain playerOneTrain;
        bool mustFlip;

        [SetUp]
        public void SetUpAllTests()
        {
            playerOne = new Hand();
            playerOneTrain = new PlayerTrain(playerOne, 12);
            mustFlip = false;
        }

        [Test]
        public void TestOpenCloseTrain()
        {
            Assert.False(playerOneTrain.IsOpen);

            playerOneTrain.Open();

            Assert.True(playerOneTrain.IsOpen);

            playerOneTrain.Close();

            Assert.False(playerOneTrain.IsOpen);
        }

        [Test]
        public void TestIsPlayableNoFlipYesPlay()
        {
            Domino playableNoFlip = new Domino(12, 11);

            Assert.True(playerOneTrain.IsPlayable(playerOne, playableNoFlip, out mustFlip));
        }

        [Test]
        public void TestIsPlayableYesFlipYesPlay()
        {
            Domino playableYesFlip = new Domino(11, 12);

            Assert.True(playerOneTrain.IsPlayable(playerOne, playableYesFlip, out mustFlip));
        }

        [Test]
        public void TestIsPlayableNotPlayable()
        {
            Domino notPlayable = new Domino(3, 5);

            Assert.False(playerOneTrain.IsPlayable(playerOne, notPlayable, out mustFlip));
        }

        [Test]
        public void TestIsPlayableWrongHand()
        {
            Hand playerTwo = new Hand();
            Domino playableNoFlip = new Domino(12, 11);

            Assert.False(playerOneTrain.IsPlayable(playerTwo, playableNoFlip, out mustFlip));
        }

    }
}
