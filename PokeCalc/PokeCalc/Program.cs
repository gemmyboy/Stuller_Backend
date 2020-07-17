/*
 *      Program.cs
 *          by Marcus Shannon
 * 
 *      Entry Point into the application and main thread of control.
 *      Treated and written as a procedure.
 */
using System;

namespace PokeCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Help();

            PokeCalc pokecalc = PokeCalc.Instance();

            //Create an REPL flow on control
            bool repl = true;
            while (repl)
            {
                //Collect user input
                string command = Console.ReadLine();

                //Normalize input
                command = command.Trim('\n');
                command = command.ToLower();

                switch (command)
                {
                    case "quit":
                        { repl = false;  break;}
                    case "help":
                        { Help(); break;}
                    default:
                        { pokecalc.Calculate(command); break;}
                }//end switch
            }//End while

        }//End Main()

        //Help -: outputs the commands for the REPL console app
        static void Help()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("| Welcome to the Pokemon Type Calculator Application    |");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("| Type the name of a Pokemon to determine: it's type,   |");
            Console.WriteLine("| types of pokemon it's strong against, and types of    |");
            Console.WriteLine("| pokemon it's weak against.                            |");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("| Type help to see this message again.                  |");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("| Type quit to exit the application.                    |");
            Console.WriteLine("---------------------------------------------------------");
        }//End Help()


    }//End class Program
}//End namespace PokeCalc
