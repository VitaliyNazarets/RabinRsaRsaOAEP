using System;
using System.Numerics;

namespace MillerRabinSimplicityTest
{
	public static class MillerRabinTest
	{
		private static BigInteger Two = new BigInteger(2);
		public static bool IsPrime(BigInteger n, BigInteger k)
		{
			if (n <= 1)
				return false;
			else if (n <= 3)
				return true;
			else if (n % 2 == 0)
				return false;
			BigInteger s = BigInteger.One;
			BigInteger t = (n - 1) / Two;
			while (t % 2 == BigInteger.Zero)
			{
				t = BigInteger.Divide(t, Two);
				s++;
			}
			for (BigInteger i = BigInteger.Zero; i < k; i++)
			{
				BigInteger a = BigInteger.Add(Two, BigIntegerRandomGenerator.Generate(n - 4));
				var u = BigInteger.ModPow(a, t, n);
				if (u == 1 || u == n - 1)
					continue;
				var j = BigInteger.Zero;
				bool isBreak = false;
				while (j < s)
				{
					u = BigInteger.ModPow(u, Two, n);
					j++;
					if (u.IsOne)
						return false;
					if (u == n - 1)
					{
						isBreak = true;
						break;
					}
				}
				if (isBreak)
					continue;
				return false;
			}
			return true;
		}
	}
}