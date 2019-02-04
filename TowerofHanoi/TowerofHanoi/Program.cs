using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerofHanoi
{
    public class Program
    {
        public static int counter = 0;

        public static void Main(string[] args)
        {

            Stack<int> FirstRod = new Stack<int>();
            Stack<int> SecondRod = new Stack<int>();
            Stack<int> ThirdRod = new Stack<int>();

            Stack<int>[] stacks = { FirstRod, SecondRod, ThirdRod };

            int numberOfDisks;
            

            Console.WriteLine("T O W E R  O F  H A N O I");
            Console.WriteLine("");
            Console.WriteLine("Legend says, in some forgotten somewhere there lies a tower made");
            Console.WriteLine("from 64 disks of pure gold resting on top of three diamond needles.");
            Console.WriteLine("At the beginning of time, God placed the disks on the first needle,");
            Console.WriteLine("largest disk resting at the base and smallest at the top, and led a group of");
            Console.WriteLine("ordained priests to the holy tower. He commanded only the following:");
            Console.WriteLine("that the priests should spend their lives transferring the disks from one");
            Console.WriteLine("needle to the other, that they must only move one disk at a time, that");
            Console.WriteLine("they could never place a larger disk on a smaller, and once all 64 disks");
            Console.WriteLine("were transferred to the last diamond needle, the tower will crumble and");
            Console.WriteLine("the universe will end.");
            Console.WriteLine("");

            numberOfDisks = 0;

            Console.Write("Choose a number of disks: ");
            string s = Console.ReadLine();
            Console.WriteLine("");

            numberOfDisks = Convert.ToInt32(s);

            while (numberOfDisks > 64 || numberOfDisks < 1)
            {
                Console.WriteLine("64 disks will end the universe. Surley you do not want something even worse to happen, do you?");
                Console.WriteLine("");

                Console.Write("Choose a number of disks: ");
                s = Console.ReadLine();
                Console.WriteLine("");
                numberOfDisks = Convert.ToInt32(s);
            }


                //populate first stack
                for (int i = numberOfDisks; i > 0; i--)
            {
                FirstRod.Push(i);
            }

            Console.Write("Do you want the computer to solve?: (Y/N)");
            s = Console.ReadLine();
            s = s.ToUpper();
            Console.WriteLine("");

            drawGame(numberOfDisks, FirstRod, SecondRod, ThirdRod);
            Console.WriteLine("");

            if (s == "Y" || s == "YES")
            {
                ComputerMakeMove(numberOfDisks, FirstRod, ThirdRod, SecondRod, stacks, numberOfDisks);
            }
            else
            {
                Console.WriteLine("\nMake a move(in the form: ab - move the top disk of rod a to the top of rod b)");

                while (!isGameOver(FirstRod, SecondRod, ThirdRod))
                {
                    Console.WriteLine("\nWhat is your move?");
                    string choice = Console.ReadLine();
                    Console.WriteLine("");
                    while (!isValidMove(choice, FirstRod, SecondRod, ThirdRod))
                    {
                        Console.WriteLine("\nInvalid Move");
                        Console.WriteLine("\nWhat is your move?");
                        choice = Console.ReadLine();
                        Console.WriteLine("");
                    }

                    MakeMove(choice, FirstRod, SecondRod, ThirdRod);
                    counter++;
                    drawGame(numberOfDisks, FirstRod, SecondRod, ThirdRod);
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n   You're    ");
                Console.WriteLine("     a       ");
                Console.WriteLine(" ----------- ");
                Console.WriteLine(" W i n n e r ");
                Console.WriteLine(" ----------- ");

                
            }
            
            Console.WriteLine("\nSolved in: " + counter + " steps");
            Console.ReadKey();
        }

        public static void drawGame(int n, Stack<int> first, Stack<int> second, Stack<int> third)
        {
            Stack<int> tempFirst = new Stack<int>();
            Stack<int> tempSecond = new Stack<int>();
            Stack<int> tempThird = new Stack<int>();
            

            for (int j = n; j > 0; j--)
            {

                if (first.Count == j)
                {
                    int f = first.Pop();
                    if (second.Count == j)
                    {
                        int s = second.Pop();
                        if (third.Count == j)
                        {
                            int t = third.Pop();
                            Console.Write(f);
                            Console.Write(" ");
                            Console.Write(s);
                            Console.Write(" ");
                            Console.Write(t);
                            Console.Write(" ");
                            Console.WriteLine();
                            tempThird.Push(t);
                        }
                        else
                        {
                            Console.Write(f);
                            Console.Write(" ");
                            Console.Write(s);
                            Console.Write(" ");
                            Console.Write("-");
                            Console.Write(" ");
                            Console.WriteLine();
                        }
                        tempSecond.Push(s);
                    }
                    else
                    {
                        if (third.Count == j)
                        {
                            int t = third.Pop();
                            Console.Write(f);
                            Console.Write(" ");
                            Console.Write("-");
                            Console.Write(" ");
                            Console.Write(t);
                            Console.Write(" ");
                            Console.WriteLine();
                            tempThird.Push(t);
                        }
                        else
                        {
                            Console.Write(f);
                            Console.Write(" ");
                            Console.Write("-");
                            Console.Write(" ");
                            Console.Write("-");
                            Console.Write(" ");
                            Console.WriteLine();
                        }
                    }
                    tempFirst.Push(f);
                }
                else if (second.Count == j)
                {
                    int s = second.Pop();
                    if (third.Count == j)
                    {
                        int t = third.Pop();
                        Console.Write("-");
                        Console.Write(" ");
                        Console.Write(s);
                        Console.Write(" ");
                        Console.Write(t);
                        Console.Write(" ");
                        Console.WriteLine();
                        tempThird.Push(t);
                    }
                    else
                    {
                        Console.Write("-");
                        Console.Write(" ");
                        Console.Write(s);
                        Console.Write(" ");
                        Console.Write("-");
                        Console.Write(" ");
                        Console.WriteLine();
                    }
                    tempSecond.Push(s);
                }
                else if (third.Count == j)
                {
                    int t = third.Pop();
                    Console.Write("-");
                    Console.Write(" ");
                    Console.Write("-");
                    Console.Write(" ");
                    Console.Write(t);
                    Console.Write(" ");
                    Console.WriteLine();
                    tempThird.Push(t);
                }

            }

            while (tempFirst.Count!=0)
            {
                int f = tempFirst.Pop();
                first.Push(f);
            }
            while (tempSecond.Count != 0)
            {
                int s = tempSecond.Pop();
                second.Push(s);
            }
            while (tempThird.Count != 0)
            {
                int t = tempThird.Pop();
                third.Push(t);
            }

        }

        public static void MakeMove(string choice, Stack<int> first, Stack<int> second, Stack<int> third)
        {
            char source = choice[0];
            char destination = choice[1];
            Stack<int>[] stacks = { first, second, third };
            int start = source - 'a';
            int end = destination - 'a';

            stacks[end].Push(stacks[start].Pop());
        }

        public static void ComputerMakeMove(int n, Stack<int> fromRod, Stack<int> toRod, Stack<int> auxRod, Stack<int>[] stacks, int disks)
        {
            
            if (n == 1)
            {
                toRod.Push(fromRod.Pop());
                counter++;
                drawGame(disks, stacks[0], stacks[1], stacks[2]);
                Console.WriteLine("");
                return;
            }
            ComputerMakeMove(n - 1, fromRod, auxRod, toRod, stacks, disks);
            toRod.Push(fromRod.Pop());
            counter++;
            drawGame(disks, stacks[0], stacks[1], stacks[2]);
            Console.WriteLine("");
            ComputerMakeMove(n - 1, auxRod, toRod, fromRod,stacks, disks);

        }

        public static bool isGameOver(Stack<int> first, Stack<int> second, Stack<int> third)
        {
            if (first.Count == 0 && second.Count == 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool isValidMove(string choice, Stack<int> first, Stack<int> second, Stack<int> third)
        {
            char source = choice[0];
            char destination = choice[1];
            Stack<int>[] stacks = { first, second, third };
            int start = source - 'a';
            int end = destination - 'a';

            if (stacks[start].Count == 0)
            {
                return false;
            }
            else
            {
                if (stacks[end].Count == 0)
                {
                    return true;
                }
                else
                {
                    int firstDisk = stacks[start].Peek();
                    int secondDisk = stacks[end].Peek();
                    if (firstDisk > secondDisk)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
