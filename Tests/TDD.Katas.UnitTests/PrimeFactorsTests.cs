using FluentAssertions;
using System.Collections.Generic;
using TDD_Katas.Katas.PrimeFactors;
using Xunit;

namespace TDD.Katas.UnitTests
{
    public class PrimeFactorsTests
    {
        private readonly PrimeFactors pf;

        public PrimeFactorsTests()
        {
            pf = new PrimeFactors();
        }

        [Fact]
        public void GenerateOf1ShouldReturn1()
        {
            pf.Generate(1).Should().BeEquivalentTo(new List<int>() { 1 });
        }

        [Fact]
        public void GenerateOf2ShouldReturn2()
        {
            pf.Generate(2).Should().BeEquivalentTo(new List<int>() { 2 }); ;
        }

        [Fact]
        public void GenerateOf3ShouldReturn3()
        {
            pf.Generate(3).Should().BeEquivalentTo(new List<int>() { 3 });
        }
    }
}
