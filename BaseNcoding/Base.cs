﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNcoding
{
	public abstract class Base
	{
		public uint CharsCount
		{
			get;
			protected set;
		}

		public double BitsPerChar
		{
			get
			{
				return Math.Log(CharsCount, 2);
			}
		}

		public string Alphabet
		{
			get;
			protected set;
		}

		public char Special
		{
			get;
			protected set;
		}

		public abstract bool HaveSpecial
		{
			get;
		}

		public Encoding Encoding
		{
			get;
			set;
		}

		protected int[] InvAlphabet;

		public Base(uint charsCount, string alphabet, char special, Encoding encoding = null)
		{
			if (alphabet.Length != charsCount)
				throw new ArgumentException("Base string should contain " + charsCount + " chars");

			for (int i = 0; i < charsCount; i++)
				for (int j = i + 1; j < charsCount; j++)
					if (alphabet[i] == alphabet[j])
						throw new ArgumentException("Base string should contain distinct chars");

			if (alphabet.Contains(special))
				throw new ArgumentException("Base string should not contain special char");

			CharsCount = charsCount;
			Alphabet = alphabet;
			Special = special;

			InvAlphabet = new int[Alphabet.Max() + 1];

			for (int i = 0; i < InvAlphabet.Length; i++)
				InvAlphabet[i] = -1;

			for (int i = 0; i < charsCount; i++)
				InvAlphabet[Alphabet[i]] = i;

			Encoding = encoding ?? Encoding.UTF8;
		}

		public virtual string EncodeString(string data)
		{
			return Encode(Encoding.GetBytes(data));
		}

		public abstract string Encode(byte[] data);

		public virtual string DecodeToString(string data)
		{
			return Encoding.GetString(Decode(data));
		}

		public abstract byte[] Decode(string data);

		/// <summary>
		/// From: http://stackoverflow.com/a/600306/1046374
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static bool IsPowerOfTwo(double x)
		{
			uint xint = (uint)x;
			if (x - xint != 0)
				return false;

			return (xint & (xint - 1)) == 0;
		}

		/// <summary>
		/// From: http://stackoverflow.com/a/13569863/1046374
		/// </summary>
		public static int LCM(int a, int b)
		{
			int num1, num2;
			if (a > b)
			{
				num1 = a;
				num2 = b;
			}
			else
			{
				num1 = b;
				num2 = a;
			}

			for (int i = 1; i <= num2; i++)
				if ((num1 * i) % num2 == 0)
					return i * num1;
			return num2;
		}

		public static uint NextPowOf2(uint x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x++;
			return x;
		}

		public static int LogBase2(uint x)
		{
			int r = 0;
			while ((x >>= 1) != 0)
				r++;
			return r;
		}
	}
}