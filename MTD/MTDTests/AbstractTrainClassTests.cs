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
    public class AbstractTrainClassTests
    {
        Hand playerOne;
        PlayerTrain playerOneTrain;
        MexicanTrain mexiTrain;
        bool mustFlip;

        [SetUp]
        public void SetUpAllTests()
        {
            playerOne = new Hand();
            playerOneTrain = new PlayerTrain(playerOne, 12);
            mexiTrain = new MexicanTrain(12);
            mustFlip = false;
        }

        [Test]
        public void TestConstructor()
        {
            MexicanTrain engVal3 = new MexicanTrain(3);

            Assert.AreEqual(12, mexiTrain.EngineValue);
            Assert.AreEqual(3, engVal3.EngineValue);

            Assert.AreEqual(mexiTrain.EngineValue, playerOneTrain.EngineValue);

            Assert.AreNotEqual(mexiTrain.EngineValue, engVal3.EngineValue);
        }

        [Test]
        public void TestChangeEngValue()
        {
            Assert.AreEqual(12, mexiTrain.EngineValue);

            mexiTrain.EngineValue = 5;

            Assert.AreNotEqual(12, mexiTrain.EngineValue);
            Assert.AreEqual(5, mexiTrain.EngineValue);
        }
    }
}
