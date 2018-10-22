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
        Train testExpress;

        [SetUp]
        public void SetUpAllTests()
        {
            empty = new Hand();
            maxTwelve = new BoneYard(12);
            testExpress = new MexicanTrain(0);
            //stuff
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
    }
}
