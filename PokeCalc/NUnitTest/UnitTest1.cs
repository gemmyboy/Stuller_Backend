using NUnit.Framework;
using PokeCalc;

namespace NUnitTest
{
    public class Tests
    {
        private PokeCalc.PokeCalc pk1 = null;
        private PokeCalc.PokeCalc pk2 = null;

        [SetUp]
        public void Setup()
        {
            pk1 = PokeCalc.PokeCalc.Instance();
            pk2 = PokeCalc.PokeCalc.Instance();
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(pk1 == pk2);
        }
    }
}