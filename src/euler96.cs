using System;
using System.IO;

namespace ProjectEuler96
{
	class MainClass
	{
		public static bool debug = false;
		public static int[,] sudoku;
		public static bool[,,] valid;
		
		public static void put(int x,int y, int val) {
			if (val==0) return;
			sudoku[x,y]=val;
			for (int z=0;z<9 ;z++ ) {
				valid[z,y,val] = (z==x);
				valid[x,z,val] = (z==y);
				valid[x-x%3+z/3,y-y%3+z%3,val] = (x-x%3+z/3==x && y-y%3+z%3==y);
				valid[x,y,z+1] = (z+1==val);
			}
		}
		public static void printvalids() {
			for (int z=1;z<10 ; z++) {
				Console.WriteLine("Valids for "+z);
				for (int y=0;y<9 ;y++ ) {
					for (int x=0;x<9 ;x++ ) {
						Console.Write((valid[x,y,z]?"1":"0"));
					}
					Console.WriteLine();
				}
			}
		}
		public static void printsudoku() {
			Console.WriteLine("Print Sudoku");
			for (int y=0;y<9 ;y++ ) {
				for (int x=0;x<9 ;x++ ) {
					Console.Write(sudoku[x,y]);
				}
				Console.WriteLine();
			
			}
		}
		public static bool deduct_row(int x, int y) {
			if (sudoku[x,y]>0) return false;
			bool r=false;
			for (int val=1;val<10;val++ ) if (valid[x,y,val]) {
				int c=0;
				for (int i=0;i<9;i++) {
					if (valid[i,y,val]) c++;
				}
				if (c==1) {
					if (debug) Console.WriteLine("Deduct_row ("+x+","+y+") = "+val);
					put(x,y,val);
					r=true;
					val=10;
				}
			}
			return r;
		}
		public static bool deduct_column(int x, int y) {
			if (sudoku[x,y]>0) return false;
			bool r=false;
			for (int val=1;val<10;val++ ) if (valid[x,y,val]) {
				int c=0;
				for (int i=0;i<9;i++) {
					if (valid[x,i,val]) c++;
				}
				if (c==1) {
					if (debug) Console.WriteLine("Deduct_column ("+x+","+y+") = "+val);
					put(x,y,val);
					r=true;
					val=10;
				}
			}
			return r;
		}
		public static bool deduct_tile(int x, int y) {
			if (sudoku[x,y]>0) return false;
			bool r=false;
			for (int val=1;val<10;val++ ) if (valid[x,y,val]) {
				int c=0;
				for (int i=0;i<9;i++) {
					if (valid[x-x%3+i%3,y-y%3+i/3,val]) c++;
				}
				if (c==1) {
					if (debug) Console.WriteLine("Deduct_tile ("+x+","+y+") = "+val);
					put(x,y,val);
					r=true;
					val=10;
				}
			}
			return r;
		}
		public static bool deduct_field(int x, int y) {
			if (sudoku[x,y]>0) return false;
			//bool r=false;
			int c=0;
			for (int val=1;val<10;val++ ) {
				if (valid[x,y,val]) {
					if (c==0) {
						c=val;
					} else {
						return false;
					}
				}
			}
			if (debug) Console.WriteLine("Deduct_field ("+x+","+y+") = "+c);
			put(x,y,c);
			return true;
		}
		public static bool deduct_rows() {
			bool r = false;
			for (int y=0;y<9 ;y++ ) {
				for (int x=0;x<9 ;x++ ) {
					r|=deduct_row(x,y);
				}
			}
			return r;
		}
		public static bool deduct_columns() {
			bool r = false;
			for (int y=0;y<9 ;y++ ) {
				for (int x=0;x<9 ;x++ ) {
					r|=deduct_column(x,y);
				}
			}
			return r;
		}
		public static bool deduct_tiles() {
			bool r = false;
			for (int y=0;y<9 ;y++ ) {
				for (int x=0;x<9 ;x++ ) {
					r|=deduct_tile(x,y);
				}
			}
			return r;
		}
		public static bool deduct_fields() {
			bool r = false;
			for (int y=0;y<9 ;y++ ) {
				for (int x=0;x<9 ;x++ ) {
					r|=deduct_field(x,y);
				}
			}
			return r;
		}

		public static void process(int rownum, long numbers) {
			int x=8;
			while (numbers>0) {
				put(x,rownum,(int)(numbers%10));
				if (debug) printvalids();
				x--;
				numbers/=10;
			}
		}
		public static void Main(string[] args)
		{
			int count=0;
			StreamReader f = File.OpenText("sudoku.txt");
			string read = null;
			while (f.ReadLine()!=null) {
				debug = (count==5);
				bool change=true;
				sudoku = new int[10,10];
				valid = new bool[10,10,11];

				for (int x=0;x<9 ;x++ ) {
					for (int y=0;y<9 ;y++ ) {
						sudoku[x,y]=0;
						for (int z=1;z<10 ; z++) {
							valid[x,y,z]=true;					
						}
					}
				}

				for (int i=0;i<9 ;i++ ) {
					read = f.ReadLine();
					process(i,Int64.Parse(read));			
				}
				
				while (change) {
					change = false;
					change |= deduct_rows();
					change |= deduct_columns();
					change |= deduct_tiles();
					change |= deduct_fields();
				}
				printsudoku();
				count++;
			}
			f.Close();
		}
	}
}
