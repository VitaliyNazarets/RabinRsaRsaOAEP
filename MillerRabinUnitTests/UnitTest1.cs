using MillerRabinSimplicityTest;
using System;
using System.Numerics;
using Xunit;

namespace MillerRabinUnitTests
{
	public class UnitTest1
	{
		private static int k = 20;
		[Fact]
		public void SimpyTest()
		{
			Assert.True(MillerRabinTest.IsPrime(5, k));
		}

		[Fact]
		public void SimplyTest2()
		{
			Assert.False(MillerRabinTest.IsPrime(-1, k));
		}
		[Fact]
		public void SimplyTest3()
		{
			Assert.False(MillerRabinTest.IsPrime(8, k));
		}

		[Fact]
		public void BigPrime1()
		{
			var prime1 = new BigInteger(6568056991);
			Assert.True(MillerRabinTest.IsPrime(prime1, k));
			
		}
		[Fact]
		public void BigPrime2()
		{
			var prime2 = new BigInteger(2854322099);
			Assert.True(MillerRabinTest.IsPrime(prime2, k));

		}
		[Fact]
		public void BigPrime3()
		{
			var prime3 = new BigInteger(9418227019);
			Assert.True(MillerRabinTest.IsPrime(prime3, k));

		}
		[Fact]
		public void BigNotPrime1()
		{
			var notPrime1 = new BigInteger(6732366601); // 82051 * 82051
			Assert.False(MillerRabinTest.IsPrime(notPrime1, k));

		}
		[Fact]
		public void BigNotPrime2()
		{
			var notPrime2 = new BigInteger(4202855089); //58403* 71963
			Assert.False(MillerRabinTest.IsPrime(notPrime2, k));

		}



	}
}
