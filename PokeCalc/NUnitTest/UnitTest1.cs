using NUnit.Framework;
using PokeCalc;

namespace NUnitTest
{
    public class Tests
    {
        private PokeCalc.PokeCalc Pk1 = null;
        private PokeCalc.PokeCalc Pk2 = null;

        [SetUp]
        public void Setup()
        {
            Pk1 = PokeCalc.PokeCalc.Instance();
            Pk2 = PokeCalc.PokeCalc.Instance();
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(Pk1 == Pk2);
        }
    }
}