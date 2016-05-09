using System;

namespace ProjectEuler248
{
	class MainClass
	{
		public static long result = 6227020800;
		public static int[] primes;
		public static long[] primedivisors;
		public static long[] primedivphis;
		public static long[,] primephis;
		public static int[] cprimephis;
		
		public static int cprimedivisors,cprimedivphis;
		
		public static bool is_prime(long n) {
			bool r = false;
			for (long i=2;i<=(long)Math.Sqrt(n);i++) {
				if (r=(n%i==0)) break;
			}
			return !r;
		}
			
		public static int totient_prime(int prime, int k)
		{
			int r;
			for (r=(prime-1);k>1;k--) {
				r*=prime;
			}
			return r;
		}

		public static void iterate(int p, int highestprime, long n, long m) {
			if (p<2) {
				if (is_prime(n+1) && result % n == 0) {
					//Console.WriteLine(result+" divisible by "+n);
					int l = 0;
					long z= (long)(result/n);
					primedivisors[cprimedivisors++]=n+1;
					primedivphis[cprimedivphis++]=n;
					primephis[highestprime,cprimephis[highestprime]++]=n+1;
					while (z % (n+1) == 0) {
						primedivisors[cprimedivisors]=primedivisors[cprimedivisors-1]*(n+1);
						cprimedivisors++;
						primedivphis[cprimedivphis]=primedivphis[cprimedivphis-1]*(n+1);
						cprimedivphis++;
						
						primephis[highestprime,cprimephis[highestprime]]=primephis[highestprime,cprimephis[highestprime]-1]*(n+1);
						cprimephis[highestprime]++;
						
						l++;
						z /= (n+1);
					}
					Console.WriteLine((n+1)+" is prime!");
					if (l>0) {
						Console.WriteLine(result+" divisible by "+(n+1)+", "+l+" times");
						Console.WriteLine((n+1)+" is prime!");
					}
				}
					
				//Console.WriteLine("n: "+n);
				return;
			}
			if (primes[p]==0) { iterate(p-1,highestprime, n,m);return;}
			Console.WriteLine(highestprime);

			//Console.WriteLine("p: "+p+" n: "+n+" m: "+m);
		
			long j=1;
			for (int i=0;i<=primes[p];i++) {
				Console.WriteLine(highestprime+" "+i);
				iterate(p-1,(highestprime==1 && i>0?p:highestprime),n*j,m*totient_prime(p,i));
				j*=p;
			}
		}
		public static void Main(string[] args)
		{
			primes = new int[14];
			primedivisors = new long[600];
			cprimedivisors = 0;
			primedivphis = new long[600];
			cprimedivphis = 0;
			
			primephis = new long[14,600];
			cprimephis = new int[14];
			
			long z = result;
			int i = 2;
			Console.Write(z+" = 1");
			while (z>1) {
				if (z % i == 0){
					z /= (int) i;
					primes[i]++;
				} else {
					if (primes[i]>0) Console.Write(" * "+i+"^"+primes[i]);
					i++;
				}
			}
			Console.WriteLine();
			
			iterate(13,1,1,1);
			Console.WriteLine("cprimedivisors: "+cprimedivisors);
			for (int j=0;j<cprimedivisors	;j++) {
				Console.WriteLine("primedivisors "+j+": "+primedivisors[j]+" ("+primedivphis[j]+")");
			}
			
			for (int y=1;y<14;y++) {
				if (cprimephis[y]>0) {
					Console.Write(y+": ");
					for (int x=0;x<cprimephis[y];x++) {
						//Console.Write("("+y+","+x+") ");
						Console.Write(primephis[y,x]+" ");
					}
					Console.WriteLine();
				}
			}

			//Console.WriteLine(is_prime(4));
		}
	}
}