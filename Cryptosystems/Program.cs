using MillerRabinSimplicityTest;
using System;
using System.Numerics;
using System.Text;
using RSA;
using System.Diagnostics;

namespace Cryptosystems
{
	class Program
	{
		static void Main(string[] args)
		{
			RSACipher rsa = new RSACipher();
			(var publicKey, var privateKey) = rsa.GenerateRSAPair();
			BigInteger secretText = BigInteger.Parse("13483148314831849481394811231232121");
			if (secretText > publicKey.N)
				throw new Exception("Too big");
			var crypt = rsa.EncryptRSA(secretText, publicKey);
			var decrypt = rsa.DecryptRSA(crypt, privateKey);
			Console.WriteLine("text: {0}\n crypted: {1}\n decrypted: {2}", secretText, crypt, decrypt);
		}
	}
}
