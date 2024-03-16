using System;
using System.Collections.Generic;

namespace KMZI
{
    public class FibonacciLFSR
    {
        private bool[] register;
        private bool[] outputBits;
        private bool[] stateChangeBits;

        public FibonacciLFSR(string polynomial)
        {
            // Проверяваме дължина на нашия масив. Максимален брой комбинации е равен на: n^2 - 1;
            int registerLength = (int)Math.Pow(2, polynomial.Length) - 1;

            // Инициялизираме масиви които ще ползваме за запазване на вътрешното състояние, промени и изход.
            register = new bool[registerLength];
            outputBits = new bool[registerLength];
            stateChangeBits = new bool[registerLength];

            // Инициялизираме стойности на всичките позиции на масива да са равни 0.
            for (int i = 0; i < registerLength; i++)
            {
                register[i] = false;
                outputBits[i] = false;
                stateChangeBits[i] = false;
            }

            // Последния елемент на листата става равен на 1.
            register[registerLength - 1] = true;

            // Инициялизираме коефициента които във нашия пример е позиция на крана (tap)
            Console.WriteLine("Polynomial coefficent: ");

            int[] polynomialCoefficients = new int[polynomial.Length];
            for (int i = 0; i < polynomial.Length; i++)
            {
                polynomialCoefficients[i] = int.Parse(polynomial[i].ToString());
                Console.Write(polynomialCoefficients[i]);
            }
            Console.WriteLine();

            // Минаваме целия масив докогато не дойде до повтаряне на елементи
            for (int i = 0; i < registerLength; i++)
            {
                // Последния (изходния) бит
                bool outputBit = register[registerLength - 1];
                bool stateChangeBit = register[0] ^ (register[registerLength - 1] && polynomialCoefficients[0] == 1);

                // Премахваме регистра за една позиция
                for (int j = 0; j < registerLength - 1; j++)
                {
                    register[j] = register[j + 1];
                }

                // Заместваме последния бит съвместимо на полинома
                register[registerLength - 1] = stateChangeBit;

                outputBits[i] = outputBit;
                stateChangeBits[i] = stateChangeBit;
            }
        }

        // Функция за принтиране
        public void PrintState()
        {
            Console.WriteLine("Internal State:");
            foreach (bool bit in register)
            {
                if (bit)
                {
                    Console.Write("1");
                } else
                {
                    Console.Write("0");
                }
               
            }
            Console.WriteLine();

            Console.WriteLine("Output Bits:");
            foreach (bool bit in outputBits)
            {
                if (bit)
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine();

            Console.WriteLine("State Change Bits:");
            foreach (bool bit in stateChangeBits)
            {
                if (bit)
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine();
        }

        // Функция да провериме дали максималния период е изпълнен
        public bool HasMaximalPeriod()
        {
            // Максимален период се постига, когато LFSR влезе в състояние изцяло на нула след (2^n - 1)
            // тактови цикъла, където n е броят битове в регистъра. Проверете дали регистърът влиза в
            // състояние изцяло на нула след (2^n - 1) тактови цикъла.

            int registerLength = register.Length;
            for (int i = 0; i < registerLength - 1; i++)
            {
                // Ако всеки елемент е равен на 0, тогава максималния период е изпълнен и понеже
                // 0 = false кода по долу няма да се изпълни, ще се изпълни само ако стойността е 1
                // т.е. някъде няма 0, т.е. максималния период не е изпълнен
                if (stateChangeBits[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Program
    {
        // главна програма за изпълнение на кода
        public static void Main(string[] args)
        {
            string polynomial = "110010101";

            FibonacciLFSR lfsr = new FibonacciLFSR(polynomial);
            lfsr.PrintState();

            bool hasMaximalPeriod = lfsr.HasMaximalPeriod();
            Console.WriteLine("Maximal Period: " + hasMaximalPeriod);
        }
    }
}
