using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_bird
{
    class Game
    {

        private int gameSpeed = 125;

        private static bool Gameon = true;

        private static bool Nexton = true;

        private static int Y = 15; //Game box size

        private static int X = 40; //Game box size

        private static int XPos = 10; // Start position

        private static int YPos = 5; // Start position

        private static string B = "@"; //Bird

        private static void BuildWall()
        {
            for (int i = 0; i < Y; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(X-1, i);
                Console.Write("#");
            }

            for (int i = 0; i < X; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, Y-1);
                Console.Write("#");
            }
        }  

        private void Stolb(int j, int n)
        {
            for (int i = 1; i < n; i++)
            {
                Console.SetCursorPosition(j, i);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("+++");
            }
            for (int i = n + 3; i < Y - 1; i++)
            {
                Console.SetCursorPosition(j, i);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("+++");
            }
        }

        private void DelStolb(int j, int n)
        {
            for (int i = 1; i < n; i++)
            {
                Console.SetCursorPosition(j, i);
                Console.WriteLine("   ");
            }
            for (int i = n + 3; i < Y - 1; i++)
            {
                Console.SetCursorPosition(j, i);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("   ");
            }
        }

        private int Bird(ref int yPos)
        {
            Console.SetCursorPosition(XPos, yPos);
            Console.Write(" ");
            if (yPos < Y - 1)
                yPos++;
            Console.SetCursorPosition(XPos, yPos);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(B);
            return yPos;
        }

        private void Uprav(ref int xPos, ref int yPos, ConsoleKeyInfo userKey)
        {
            if (userKey.Key == ConsoleKey.Spacebar)
            {             
                    Console.SetCursorPosition(xPos, yPos);
                    Console.Write(" ");
                    if (yPos > 2)
                    {
                        yPos -= 2;
                    }
                    Console.SetCursorPosition(xPos, yPos);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(B);
            }          
        }

        private int Result(ref int xPos, ref int yPos, int j, int n, ref int ochki)
        {
            if ((xPos == j + 2) && (yPos > n - 1) && (yPos < n + 3))
                ochki++;
            Console.SetCursorPosition(0, Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Points:{ochki}");
            return ochki;
        }

        private bool GameOver(ref int xPos, ref int yPos, int j, int n, ref int ochki)
        {
            if (((yPos < n || yPos > n + 2) && (xPos == j || xPos == j + 1 || xPos == j + 2)) || (yPos > Y - 2))
            {
                Gameon = false;
                Console.SetCursorPosition(X / 2 - 5, Y / 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GAME OVER");
            }

            return Gameon;
        }

        private void GameOver(ref int ochki)
        {
            Console.SetCursorPosition(X / 2 - 5, Y / 2 - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GAME OVER");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(X / 2 - 7, Y / 2);
            Console.WriteLine($"YOUR RESULT:{ochki}");
            Console.SetCursorPosition(X / 2 - 17, Y / 2 + 1);
            Console.WriteLine("press on ENTER to start a NEW GAME");
            Console.SetCursorPosition(X / 2 - 11, Y / 2 + 2);
            Console.Write("press the ESC to EXIT");
        }

        private void StartGame()
        {
            Console.Clear();
            BuildWall();
            Console.SetCursorPosition(X / 2 - 5, Y / 2 - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FLAPPY BIRD");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(X / 2 - 7, Y / 2);
            Console.SetCursorPosition(X / 2 - 17, Y / 2 + 1);
            Console.WriteLine("press on ENTER to start a NEW GAME");
            Console.SetCursorPosition(X / 2 - 11, Y / 2 + 2);
            Console.Write("press the ESC to EXIT");
        }

        private void Next(ConsoleKeyInfo k)
        {
            if (k.Key == ConsoleKey.Enter)
            {
                Nexton = true;
                Gameon = true;
            }
            if (k.Key == ConsoleKey.Escape)
                Environment.Exit(0);
        }

        public void game ()

        {
            Console.SetWindowSize(X, Y + 1);
            Random r = new Random();
            ConsoleKeyInfo k;
            while (Nexton)
            {
                int yPos = YPos;
                int xPos = XPos;
                int ochki = 0;
                StartGame();
                k = Console.ReadKey(true);
                while (k.Key != ConsoleKey.Enter && k.Key != ConsoleKey.Escape)
                    k = Console.ReadKey(true);
                Next(k);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(X - 15, Y);
                Console.Write("control: space");
                Bird(ref yPos);
                BuildWall();
                Console.ReadKey(true);
                while (Gameon)
                {
                    int n = r.Next(3, Y - 5);
                    for (int j = X - 4; j > 0; j--)
                    {
                        Stolb(j, n);
                        yPos = Bird(ref yPos);
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo userKey = Console.ReadKey(true);
                            Uprav(ref xPos, ref yPos, userKey);
                        }
                        ochki = Result(ref xPos, ref yPos, j, n, ref ochki);
                        Gameon = GameOver(ref xPos, ref yPos, j, n, ref ochki);
                        if (Gameon == false)
                            j = 0;
                        System.Threading.Thread.Sleep(gameSpeed); 
                        DelStolb(j, n);
                    }
                }
                Console.Clear();
                BuildWall();
                GameOver(ref ochki);
                k = Console.ReadKey(true);
                while (k.Key != ConsoleKey.Enter && k.Key != ConsoleKey.Escape)
                k = Console.ReadKey(true);
                Next(k);
            }
        } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            g.game();
        }
    }
}
