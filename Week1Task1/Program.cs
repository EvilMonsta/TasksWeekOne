using System;

namespace task11
{
    internal class Calculator
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("(+) Сложение");
                Console.WriteLine("(-) Вычитание");
                Console.WriteLine("(*) Умножение");
                Console.WriteLine("(/) Деление");
                Console.WriteLine("(Q) Выход");
                Console.Write("Выбор: ");

                char op = Console.ReadKey(true).KeyChar;

                if (op == 'q' || op == 'Q')
                    return;

                if (!IsOperation(op))
                {
                    Console.WriteLine();
                    Console.WriteLine("Ошибка! Неизвестная операция. Допустимо: +  -  *  /  (Q — выход)");
                    Pause();
                    continue;
                }

                while (true)
                {
                    Console.WriteLine();
                    PerformOperation(op);

                    Console.WriteLine();
                    Console.Write("[R] — ещё раз эту операцию, [O] — выбрать другую, [Q] — выход: ");
                    char k = Console.ReadKey(true).KeyChar;

                    if (k == 'r' || k == 'R') continue;         
                    if (k == 'o' || k == 'O') break;            
                    if (k == 'q' || k == 'Q') return;            
                }
            }
        }

        private static bool IsOperation(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private static decimal ReadNumber(string input)
        {
            while (true)
            {
                Console.Write(input);
                string s = Console.ReadLine();

                if (s == null)
                {
                    Console.WriteLine("Ошибка! Пустой ввод. Попробуйте ещё раз.");
                    continue;
                }

                s = s.Trim();
                if (s.Length == 0)
                {
                    Console.WriteLine("Ошибка! Ожидалось число. Попробуйте ещё раз.");
                    continue;
                }

                string normalized = s.Replace(',', '.');

                if (decimal.TryParse(s, out var value) ||
                  decimal.TryParse(s.Replace(',', '.'), out value) ||
                  decimal.TryParse(s.Replace('.', ','), out value))
                  return value;

                Console.WriteLine("Ошибка! Ожидалось число. Примеры: 12,5  |  12.5  |  -3");
            }
        }

        private static void PerformOperation(char op)
        {
            Console.WriteLine($"Операция: {op}");

            decimal a = ReadNumber("A: ");
            decimal b = ReadNumber("B: ");

            if (op == '/' && b == 0m)
            {
                Console.WriteLine("Ошибка! Деление на ноль.");
                return;
            }

            decimal result = 0m;
            if (op == '+') result = a + b;
            else if (op == '-') result = a - b;
            else if (op == '*') result = a * b;
            else if (op == '/') result = a / b;

            Console.WriteLine($"{a} {op} {b} = {result}");
        }

        private static void Pause()
        {
            Console.Write("Нажмите любую клавишу...");
            Console.ReadKey(true);
        }
    }
}