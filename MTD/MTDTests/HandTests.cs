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
    public class HandTests
    {
        Hand empty;
        BoneYard maxTwelve;

        [SetUp]
        public void SetUpAllTests()
        {
            empty = new Hand();
            maxTwelve = new BoneYard(12);
        }

        [Test]
        public void TestDraw()
        {
            Assert.AreEqual(empty.Count, 0);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(empty.Count, i);
                empty.Draw(maxTwelve);
                Assert.AreNotEqual(empty.Count, i);
            }

            Assert.AreEqual(empty.Count, 10);
        }

        [Test]
        public void TestConstructor()
        {
            Domino d = new Domino(12, 12);

            Hand twoPlayers = new Hand(maxTwelve, 2);
            Assert.AreEqual(twoPlayers.Count, 10);
            Assert.AreEqual(twoPlayers[0], d);

            Hand fourPlayers = new Hand(maxTwelve, 4);
            Assert.AreEqual(fourPlayers.Count, 10);
            Assert.AreEqual(twoPlayers[0], d);

            Hand sixPlayers = new Hand(maxTwelve, 6);
            Assert.AreEqual(sixPlayers.Count, 9);
            Assert.AreEqual(twoPlayers[0], d);

            Hand sevenPlayers = new Hand(maxTwelve, 7);
            Assert.AreEqual(sevenPlayers.Count, 7);
            Assert.AreEqual(twoPlayers[0], d);
        }

        [Test]
        public void TestInvalidPlayerCountLow()
        {
            try
            {
                Hand invalidHand = new Hand(maxTwelve, 1);
                Assert.Fail("The constructor should throw an exception for values below 2");
            }
            catch (ArgumentException)
            {
                Assert.Pass("The constructor threw an exception for a value less than 2 as expected");
            }
        }

        [Test]
        public void TestInvalidPlayerCountHigh()
        {
            try
            {
                Hand invalidHand = new Hand(maxTwelve, 9);
                Assert.Fail("The constructor should throw an exception for values above 8");
            }
            catch (ArgumentException)
            {
                Assert.Pass("The constructor threw an exception for a value greater than 8 as expected");
            }
        }

        [Test]
        public void TestIsEmpty()
        {
            Assert.AreEqual(empty.Count, 0);
            Assert.True(empty.IsEmpty());

            Domino d13 = new Domino(1, 3);

            empty.Add(d13);

            Assert.False(empty.IsEmpty());
        }

        [Test]
        public void testScore()
        {
            Domino d12 = new Domino(1, 2);
            Domino d21 = new Domino(2, 1);
            Domino d33 = new Domino(3, 3);

            empty.Add(d12);
            Assert.AreEqual(empty.Score, 3);

            empty.Add(d21);
            empty.Add(d33);

            Assert.AreEqual(empty.Score, 12);
        }

        [Test]
        public void testHasDomino()
        {
            Hand testHand = new Hand(maxTwelve, 8);
            Assert.True(testHand.HasDomino(12));
            Assert.False(testHand.HasDomino(0));
            
            Domino d01 = new Domino(0, 1);
            testHand.Add(d01);

            Assert.True(testHand.HasDomino(0));
        }

        [Test]
        public void testHasDoubleDomino()
        {
            Domino d00 = new Domino(0, 0);
            Domino d01 = new Domino(0, 1);
            Domino d52 = new Domino(5, 2);

            empty.Add(d00);
            empty.Add(d01);
            empty.Add(d52);

            Assert.True(empty.HasDoubleDomino(0));
            Assert.False(empty.HasDoubleDomino(1));
            Assert.False(empty.HasDoubleDomino(5));
            Assert.False(empty.HasDoubleDomino(12));
        }

        [Test]
        public void testIndexof()
        {
            empty.Add(new Domino(3, 4));

            Assert.AreEqual(0, empty.IndexOfDomino(3));
            Assert.AreEqual(0, empty.IndexOfDomino(4));

            Assert.AreEqual(-1, empty.IndexOfDomino(9));
            Assert.AreEqual(-1, empty.IndexOfDoubleDomino(5));
            Assert.AreEqual(-1, empty.IndexOfHighDouble());

            empty.Add(new Domino(5, 5));
            empty.Add(new Domino(8, 8));
            empty.Add(new Domino(4, 4));

            Assert.AreEqual(3, empty.IndexOfDoubleDomino(4));
            Assert.AreEqual(2, empty.IndexOfHighDouble());
        }


        [Test]
        public void testGetDomino()
        {
            Assert.AreEqual(null, empty.GetDomino(0));

            empty.Add(new Domino(12, 12));
            empty.Add(new Domino(8, 7));
            empty.Add(new Domino(3, 4));
            empty.Add(new Domino(9, 9));
            empty.Add(new Domino(8, 7));
            empty.Add(new Domino(3, 4));

            Domino dBB = new Domino(12, 12);
            Domino d99 = new Domino(9, 9);
            Domino d87 = new Domino(8, 7);
            Domino d34 = new Domino(3, 4);

            Assert.AreEqual(dBB, empty.GetDomino(12));
            Assert.AreEqual(null, empty.GetDomino(12));

            Assert.AreEqual(d87, empty.GetDomino(8));
            Assert.AreEqual(d34, empty.GetDomino(4));

            Assert.AreEqual(d99, empty.GetDoubleDomino(9));
        }

        [Test]
        public void testPlay()
        {
            PlayerTrain myTrain = new PlayerTrain(empty, 12);
            MexicanTrain mexiTrain = new MexicanTrain(12);

            Domino dBB = new Domino(12, 12);
            Domino dB4 = new Domino(12, 4);
            Domino d4B = new Domino(4, 12);
            Domino d73 = new Domino(7, 3);

            empty.Add(dBB);

            try
            {
                empty.Play(dBB, myTrain);
            }
            catch(ArgumentException)
            {
                Assert.Fail("Indicated domino should be playable on player train.");
            }

            empty.Add(dB4);

            try
            {
                empty.Play(myTrain);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Playable domino exists in player hand.");
            }

            empty.Add(d4B);

            try
            {
                empty.Play(myTrain);
                Assert.Fail("No playable domino exists in player hand.");
            }
            catch { }

            Assert.Pass("All try/catch play tests completed successfully.");
        }

        [Test]
        public void TestPlayFirstPlayableDominoOnMexicanTrain()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);
            Assert.AreEqual(mexiTrain.PlayableValue(), 12); // This test belongs in the mexican train class and is performed... because.  It makes sense to do it to me to illustrate since I wrote play differently than you did.

            empty.Add(new Domino(12, 3));

            try
            {
                empty.Play(mexiTrain);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Indicated domino should be playable on mexican train.");
            }

            Assert.AreEqual(3, mexiTrain.PlayableValue());
            Assert.AreEqual(1, mexiTrain.Count());
        }

        [Test]
        public void TestPlaySpecifiedDominoOnMexicanTrain()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);
            Domino testDomino = new Domino(12, 3);

            empty.Add(testDomino);

            try
            {
                empty.Play(testDomino, mexiTrain);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Indicated domino should be playable on mexican train.");
            }

            Assert.AreEqual(3, mexiTrain.PlayableValue());
            Assert.AreEqual(1, mexiTrain.Count());
        }

        [Test]
        public void TestPlayFlippingDominoOnMexicanTrain()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);
            Assert.AreEqual(mexiTrain.PlayableValue(), 12); // This test belongs in the mexican train class and is performed... because.  It makes sense to do it to me to illustrate since I wrote play differently than you did.

            empty.Add(new Domino(3, 12));

            try
            {
                empty.Play(mexiTrain);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Indicated domino should be playable on mexican train.");
            }

            Assert.AreEqual(3, mexiTrain.PlayableValue());
            Assert.AreEqual(1, mexiTrain.Count());
        }

        [Test]
        public void TestPlayExceptionThrownInvalidDominoFirstPlayableDominoOnMexicanTrain()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);

            empty.Add(new Domino(3, 4));

            try
            {
                empty.Play(mexiTrain);
                Assert.Fail("No playable Domino should exist in this hand.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(
                    "There is no playable domino in this hand",
                    e.Message);
            }
        }

        [Test]
        public void TestPlayExceptionThrownInvalidDominoSpecifiedDominoOnMexicanTrain()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);
            Domino testDomino = new Domino(4, 3);

            empty.Add(testDomino);

            try
            {
                empty.Play(testDomino, mexiTrain);
                Assert.Fail("No playable Domino should exist in this hand.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(
                    "The selected domino is not playable on the selected train.",
                    e.Message);
            }
        }

        [Test]
        public void TestPlayExceptionThrownInvalidDominoSpecifiedDominoNotInHand()
        {
            MexicanTrain mexiTrain = new MexicanTrain(12);
            Domino dominoForHand = new Domino(4, 3);
            Domino testDomino = new Domino(6, 7);

            empty.Add(dominoForHand);

            try
            {
                empty.Play(testDomino, mexiTrain);
                Assert.Fail("Selected Domino does not exist in this hand.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(
                    "Selected domino is not contained in this hand.",
                    e.Message);
            }
        }
    }
}
