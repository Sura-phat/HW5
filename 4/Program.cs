using System;
using System.Collections.Generic;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize();

            while (true)
            {
                Console.Clear();
                board.Draw();

                Console.Write("Enter source position (e.g. E2): ");
                string source = Console.ReadLine();

                Console.Write("Enter destination position (e.g. E4): ");
                string destination = Console.ReadLine();

                if (board.Move(source, destination))
                {
                    Console.WriteLine("Move successful!");
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }

                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}