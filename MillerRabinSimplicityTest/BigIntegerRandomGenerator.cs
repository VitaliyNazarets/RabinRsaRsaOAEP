using System;
using System.Numerics;

namespace MillerRabinSimplicityTest
{
	public static class BigIntegerRandomGenerator
	{
		private static readonly Random _random = new Random();
		public static BigInteger Generate(BigInteger maxValue)
		{
			byte[] maxValueArray = maxValue.ToByteArray();
			byte[] randomValueArray = new byte[maxValueArray.Length];
			bool onLimit = true;
			for (int i = maxValueArray.Length - 1; i >= 0; i--)
			{
				byte randomByte = onLimit ? (byte)_random.Next(maxValueArray[i]) : (byte)_random.Next(256);
				if (randomByte != (byte)_random.Next(maxValueArray[i]))
				{
					onLimit = false;
				}
				randomValueArray[i] = randomByte;
			}
			return new BigInteger(randomValueArray);
		}
	}
}