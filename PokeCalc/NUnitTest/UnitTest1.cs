using NUnit.Framework;
using System;
using System.Diagnostics;

namespace NUnitTest
{
    public class Tests
    {
        private PokeCalc.PokeCalc Pk1 = null;
        private PokeCalc.PokeCalc Pk2 = null;

        private Stopwatch Stp1 = null;
        private Stopwatch Stp2 = null;

        [SetUp]
        public void Setup()
        {
            Pk1 = PokeCalc.PokeCalc.Instance();
            Pk2 = PokeCalc.PokeCalc.Instance();

            Stp1 = new Stopwatch();
            Stp2 = new Stopwatch();

        }

        [Test]
        public void Test1()
        {
            //Check Simpleton Implementation
            Assert.IsTrue(Pk1 == Pk2);
        }

        [Test]
        public void Test2()
        {
            try
            {
                Pk1.Calculate("Pichu");
            }
            catch (Exception) { Assert.Fail(); }
            Assert.Pass();
        }

        [Test]
        public void Test3()
        {
            try
            {
                Pk1.Calculate("SomeWeirdNotAPokemon");
            }
            catch (Exception) { Assert.Fail(); }
            Assert.Pass();
        }

        [Test]
        public void Test4()
        {
            //Cache Check
            Stp1.Start();
            Pk1.Calculate("Ditto");
            Stp1.Stop();

            Stp2.Start();
            Pk1.Calculate("Ditto");
            Stp2.Stop();

            Assert.IsTrue(Stp1.ElapsedTicks > Stp2.ElapsedTicks);
        }
    }
}