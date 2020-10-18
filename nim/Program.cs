using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nim
{
    class Program
    {
        static void PrintArray(char[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                    Console.Write(arr[i][j]);
                Console.WriteLine();
            }
        }
        static int Turn(ref char[][] elems, string player, ref int counter)
        {
            Console.WriteLine($"Ходит {player} игрок!");
            Console.Write("Введите номер строки и количество спичек, которые хотите убрать справа, через пробел: ");
            int[] choice = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int raw = choice[0];
            int count = choice[1];
            elems[raw - 1] = new char[elems[raw - 1].Length - choice[1]];
            for (int i = 0; i < elems[raw - 1].Length; i++)
                elems[raw - 1][i] = '|';
            PrintArray(elems);
            if (elems[raw - 1].Length == 0)
                counter++;
            return counter;
        }
        static void Main(string[] args)
        {
            int emptyRawsCounter = 0;
            int playerCheck = 2;
            string[] players = { "первый", "второй" };
            char[][] elems = new char[4][];
            for (int i = 0, j = 1; i < elems.Length && j <= 7; i++, j+=2)
                elems[i] = new char[j];

            for (int i = 0; i < elems.Length; i++)
            {
                for (int j = 0; j < elems[i].Length; j++)
                    elems[i][j] = '|';
            }
            
            string playerNumber = string.Empty;
            Console.WriteLine(@"Ним — игра, в которой два игрока по очереди берут предметы, разложенные на несколько кучек.
За один ход может быть взято любое количество предметов (большее нуля) из одной кучки.
ПРОИГРЫВАЕТ игрок, взявший последний предмет.
Количество кучек = 4, в каждой кучке 1, 3, 5, 7.

Вводите номер кучки и количество предметов через пробел.
Например: 2 4

Нажмите любую клавишу для начала игры.");
            Console.ReadKey();
            while (emptyRawsCounter != 4)
            { 
                Console.Clear();
                PrintArray(elems);
                playerNumber = playerCheck % 2 == 0 ? players[0] : players[1];
                try
                {
                    emptyRawsCounter = Turn(ref elems, playerNumber, ref emptyRawsCounter);
                    playerCheck++;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка!\n");
                    Console.WriteLine("Нажмите любую клавишу для продолжения игры.");
                    Console.ReadKey();

                }
                
            }
            Console.Clear();
            string winner = playerNumber == players[0] ? players[1] : players[0];
            Console.WriteLine($"Победил {winner} игрок!");
        }
    }
}
