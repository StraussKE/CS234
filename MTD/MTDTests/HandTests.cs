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
            //stuff
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
    }
}
