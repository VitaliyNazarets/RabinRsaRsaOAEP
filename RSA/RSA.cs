using MillerRabinSimplicityTest;
using System;
using System.Diagnostics;
using System.Numerics;

namespace RSA
{
	public class RSACipher
	{
		private readonly int keyLength = 1024;

		private static readonly Random _random = new Random();

		private static BigInteger GenerateRandomByLength(int len)
		{
			byte[] randomValueArray = new byte[len / 8 + 1];
			randomValueArray[randomValueArray.Length - 1] = (byte)_random.Next(1, 256);
			for (int i = randomValueArray.Length - 2; i >= 0; i--)
			{
				byte randomByte = (byte)_random.Next(256);
				randomValueArray[i] = randomByte;
			}

			var random =  new BigInteger(randomValueArray);
			random = random > 0 ? random : random * BigInteger.MinusOne;
			return random;
		}

		private BigInteger GenerateRandomPrime()
		{
			var primeBigInteger = GenerateRandomByLength(keyLength);
			while (!MillerRabinTest.IsPrime(primeBigInteger, 10))
				primeBigInteger++;
			return primeBigInteger;
		}

		public (Key publicKey, Key privateKey) GenerateRSAPair()
		{

			var p = GenerateRandomPrime();
			var q = GenerateRandomPrime();
			var n = BigInteger.Multiply(p, q);
			var phi = EulerFunction(p, q);
			var e = FindE(phi);
			var d = FindD(e, phi);
			if (d < 0)
				d += phi;
			return (new Key() { Value = e, N = n }, new Key() { Value = d, N = n });
		}

		private BigInteger FindE(BigInteger phi)
		{
			//Найбільш відомі, якщо немає генеруємо випадкове
			int[] staticE = new int[] { 3, 17, 65537 };
			foreach (var e in staticE)
			{
				if (ExtendedEuclid(phi, e).d == 1)
				{
					return e;
				}
			}
			
			BigInteger generatedE = BigIntegerRandomGenerator.Generate(phi);

			for (; ; generatedE++)
			{
				if (ExtendedEuclid(phi, generatedE).d == 1)
				{
					return  generatedE;
				}
			}
			throw new Exception("E not found");
		}

		private BigInteger FindD(BigInteger phi, BigInteger e)
		{
			return ExtendedEuclid(phi, e).x;
		}

		private (BigInteger d, BigInteger x, BigInteger y) ExtendedEuclid(BigInteger a, BigInteger b)
		{
			BigInteger prevx = 1, x = 0, prevy = 0, y = 1;
			while (b > 0)
			{
				var q = a / b;
				(x, prevx) = (prevx - q * x, x);
				(y, prevy) = (prevy - q * y, y);
				(a, b) = (b, a % b);
			}
			return (a, prevx, prevy);
		}

		private BigInteger EulerFunction(BigInteger p, BigInteger q)
		{
			return BigInteger.Multiply(p - 1, q - 1);
		}

		public BigInteger EncryptRSA(BigInteger text, Key key)
		{
			return BigInteger.ModPow(text, key.Value, key.N);
		}
		public BigInteger DecryptRSA(BigInteger text, Key key)
		{
			return BigInteger.ModPow(text, key.Value, key.N);
		}

	}
}
