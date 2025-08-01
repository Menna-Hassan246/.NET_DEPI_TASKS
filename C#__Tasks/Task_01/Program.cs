using System;
using System.ComponentModel;
namespace Day_01
{
    internal class Program

    {
       static private double Add(double x, double y) { return x + y; }
        static private double Substract(double x, double y) { return x - y; }
        static private double Multiply(double x, double y) { return x * y; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!!\r\this is my simple calculator\r\n\n********************************\r\n");
            Console.Write("Please,Enter the first number:\r\n");
            double num1 = Convert.ToDouble( Console.ReadLine());
            Console.Write("Please,Enter the second number:\r\n");
            double num2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Now, What do you want to do with those numbers?\r\n" +
              "[A]dd\r\n" +
              "[S]ubtract\r\n" +
              "[M]ultiply\r\n"+
              "[E]nd\r\n");
            char c;
            do
            {
                 c = Convert.ToChar(Console.ReadLine());
                switch (c)
                {
                    case 'a' or 'A':
                        Console.WriteLine("There Sum equal= " + Add(num1, num2));
                        break;
                    case 's' or 'S':
                        Console.WriteLine("There Substract equal= " + Substract(num1, num2));
                        break;
                    case 'm' or 'M':
                        Console.WriteLine("There Multiply equal= " + Multiply(num1, num2));
                        break;
                    case 'e' or 'E':
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                Console.Write("\nNow, What is your option?\r\n");
            } while (true);
        }
    }
}
