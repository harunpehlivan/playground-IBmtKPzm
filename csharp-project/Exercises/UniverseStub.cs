// { autofold
﻿using System;
using System.Linq;

namespace Answer
{
	public class UniverseStub
	{

		public static void allBench(int loop, int arraySize, int rerun)
		{
		    Stopwatch sw = new Stopwatch();
		    Random random = new Random();

		    Func<int, int[]> genRandomIntArray = n => Enumerable.Repeat(0, n).Select(i => random.Next(2 * n)).ToArray();
		    Func<int, int[]> genRandomIntArray2 = n =>
		    {
			int[] ar = new int[n];
			for (int k = 0; k < n; ++k) ar[k] = random.Next(2 * n);
			return ar;
		    };

		    Action<int, int> bench1 = (K, n) =>
		    {
			sw.Restart();
			int N = Vector<int>.Count * n;
			int[] x = genRandomIntArray2(N);
			int[] y = genRandomIntArray2(N);
			int[] r = new int[N];

			for (int k = 0; k < K; ++k)
			{
			    for (int i = 0; i < N; ++i) r[i] = x[i] + y[i];
			}

			//Console.WriteLine(String.Join(" ", x.Take(10)));
			//Console.WriteLine(String.Join(" ", y.Take(10)));
			//Console.WriteLine(String.Join(" ", r.Take(10)));
			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds,6} ms");
		    };

		    Action<int, int> bench2 = (K, n) =>
		    {
			sw.Restart();
			int N = Vector<int>.Count * n;
			int[] x = genRandomIntArray2(N);
			int[] y = genRandomIntArray2(N);
			int[] r = new int[N];
			Vector<int> v0 = Vector<int>.Zero;

			for (int k = 0; k < K; ++k)
			{
			    for (int i = 0; i < N; i += Vector<int>.Count)
			    {
				v0 = new Vector<int>(x, i) + new Vector<int>(y, i);
				v0.CopyTo(r, i);
			    }
			}

			//Console.WriteLine(String.Join(" ", x.Take(10)));
			//Console.WriteLine(String.Join(" ", y.Take(10)));
			//Console.WriteLine(String.Join(" ", r.Take(10)));
			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds,6} ms");
		    };


		    Action<int, int> bench3 = (K, n) =>
		    {
			sw.Restart();
			int N = Vector<short>.Count * n;
			int[] xb = genRandomIntArray2(N);
			short[] x = xb.Select(ix => (short)ix).ToArray();
			int[] yb = genRandomIntArray2(N);
			short[] y = yb.Select(ix => (short)ix).ToArray();
			short[] r = new short[N];
			Vector<short> v0 = Vector<short>.Zero;

			for (int k = 0; k < K; ++k)
			{
			    for (int i = 0; i < N; i += Vector<short>.Count)
			    {
				v0 = new Vector<short>(x, i) + new Vector<short>(y, i);
				v0.CopyTo(r, i);
			    }
			}

			//Console.WriteLine(String.Join(" ", x.Take(10)));
			//Console.WriteLine(String.Join(" ", y.Take(10)));
			//Console.WriteLine(String.Join(" ", r.Take(10)));
			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds,6} ms");
		    };

		    int m0 = rerun;
		    int m1 = loop;
		    int m2 = arraySize;

		    Console.WriteLine("#### Normal Computation");
		    for (int i = 0; i < m0; ++i) bench1(m1, m2);
		    Console.WriteLine();

		    Console.WriteLine("#### SIMD Computation");
		    for (int i = 0; i < m0; ++i) bench2(m1, m2);
		    Console.WriteLine();

		    Console.WriteLine("#### SIMD Computation 2");
		    for (int i = 0; i < m0; ++i) bench3(m1, m2 / 2);
		    Console.WriteLine();
		}
		
// }
public static void testSIMD_AVXonCSharp(){
	allBench(loop: 1000, arraySize: 10000, rerun: 5);
}
//{ autofold
	}
}
//}
