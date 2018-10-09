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
    public class BoneYardTests
    {
        BoneYard empty;                                                                     // empty boneyard
        BoneYard sixPip;                                                                    // small boneyard
        BoneYard twelvePipNoShuffle;                                                        // boneyard to be left unshuffled
        BoneYard shuffleTwelve1;                                                            // boneyard to be shuffled
        BoneYard shuffleTwelve2;                                                            // second boneyard to be shuffled to see if shuffling randomization is functional

        [SetUp]
        public void SetUpAllTests()                                                         // make the things
        {
            empty = new BoneYard();
            sixPip = new BoneYard(6);
            twelvePipNoShuffle = new BoneYard(12);
            shuffleTwelve1 = new BoneYard(12);
            shuffleTwelve2 = new BoneYard(12);
        }

        [Test]
        public void FirstAndLastDominoesCorrect()
        {
            Domino doubleAught = new Domino(0, 0);
            Domino doubleSix = new Domino(6, 6);

            Assert.AreEqual(sixPip[0], doubleAught);
            Assert.AreEqual(sixPip[27], doubleSix);

        }

        [Test]
        public void TestTileCount()                                                         // verify that the number of dominoes generated match the number of dominoes that should be generated
        {
            Assert.AreEqual(28, sixPip.DominosRemaining);
            Assert.AreEqual(91, twelvePipNoShuffle.DominosRemaining);
        }

        [Test]
        public void TestNegativePipsInvalidTry()                                            // prove that you can't make dominoes with negative values
        {
            try
            {
                BoneYard invalid = new BoneYard(-1);
                Assert.Fail("The constructor should throw an exception for negative values.");
            }
            catch (ArgumentException)
            {
                Assert.Pass("The constructor threw and exception for a negative value as expected.");
            }
        }


        [Test]
        public void TestIsEmpty()                                                           // test IsEmpty method
        {
            Assert.True(empty.IsEmpty());                                                   // empty boneyard should be empty
            Assert.False(sixPip.IsEmpty());                                                 // populated boneyard should not be empty
        }

        [Test]
        public void TestShuffle()                                                           // test shuffle method
        {
            Assert.AreEqual(twelvePipNoShuffle.ToString(), shuffleTwelve1.ToString());      // verify that unshuffled boneyards match
            shuffleTwelve1.Shuffle();                                                       // shuffle first boneyard to shuffle
            shuffleTwelve2.Shuffle();                                                       // shuffle second
            Assert.AreNotEqual(twelvePipNoShuffle.ToString(), shuffleTwelve1.ToString());   // check that shuffled boneyard does not match unshuffled boneyard
            Assert.AreNotEqual(shuffleTwelve1.ToString(), shuffleTwelve2.ToString());       // check that shuffled boneyards to not match one another
        }

        [Test]
        public void TestDraw()                                                              // tests draw method
        {
            while (!sixPip.IsEmpty())                                             // keep drawing while there are still tiles in the boneyard
            {
                Assert.IsNotEmpty(sixPip.Draw().ToString());                                // make sure that you actually get something when you draw
            }
            Assert.Throws<IndexOutOfRangeException>(() => sixPip.Draw());                   // make sure it won't draw when the boneyard is empty
        }

    }
}
