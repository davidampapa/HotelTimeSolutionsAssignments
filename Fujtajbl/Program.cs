using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujtajbl
{
    enum Operation { Add = 1, Sub = 2, Mul = 3, Div = 4 }

    class BasicCalculator 
	{
        private readonly Dictionary<Operation, Func<int, int, int>> _opSolver =
            new Dictionary<Operation, Func<int, int, int>> {
				{ Operation.Add, (x,y) => x + y },
				{ Operation.Sub, (x,y) => x - y },
				{ Operation.Mul, (x,y) => x * y },
				{ Operation.Div, (x,y) => { if (y==0) throw new DivideByZeroException(); return x / y; } }
            };
        private readonly Dictionary<Operation, char> _opToChar =
            new Dictionary<Operation, char> {
                { Operation.Add, '+'},
                { Operation.Sub, '-'},
                { Operation.Mul, '*'},
                { Operation.Div, '/'}
            };
        public void RunCalculator() 
		{
            while (true) {
                Console.Clear();
                int a = NumberLoader('A');
                int b = NumberLoader('B');
                Operation operation = ChooseOperation();
                Compute(a, b, operation);
                if(!PlayAgain()) break;
            }
        }
        private int NumberLoader(char ch)
        {
            int number = 0;
            try
            {
                Console.WriteLine("Zadejte cislo " + ch);
                number = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                throw new Exception("Neplatný formát čísla");
            }
            return number;
        }

		private Operation ChooseOperation()
		{
			int operation = 0;
			Console.WriteLine();
			Console.WriteLine("Vyberte operaci");
			Console.WriteLine("1: a + b");
			Console.WriteLine("2: a - b");
			Console.WriteLine("3: a * b");
			Console.WriteLine("4: a / b");
			try
			{
                operation = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                throw new Exception("Neplatný formát operace");
            }
            if (!Enum.IsDefined(typeof(Operation), operation))
            {
                Console.WriteLine("Neznámá operace, zadejte 1–4.");
                throw new Exception("Neplatný formát operace");
            }
            return (Operation)operation;
        }

		private void Compute(int a, int b, Operation op) {
            Console.WriteLine($"Výsledek: {a} {_opToChar[op]} {b} = {_opSolver[op](a, b)}");
        }

        private bool PlayAgain()
        {
            Console.WriteLine("Chcete spustit novy vypocet?" + Environment.NewLine + "a/n");
            return Console.ReadLine() == "a";
        }
    }
	class Program
	{


		static void Main(string[] args)
		{
			BasicCalculator calculator = new BasicCalculator();
			calculator.RunCalculator();
		}
	}
}