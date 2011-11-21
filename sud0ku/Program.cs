using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace sud0ku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //readSudoku(@"C:\Users\obaysan\Desktop\sudoku.txt");


            DateTime beginning = DateTime.Now;
           
            SudokuGenerator gen = new SudokuGenerator();
            Cell[,] sudoku = gen.generateSudoku(DifficultyLevel.levels.veteran);
            SudokuSolver ss = new SudokuSolver();
            ss.initialize(sudoku);

            Console.WriteLine("INITIAL STATE");
            ss.printSudoku();
            ss.solveIt();
            Console.WriteLine("-------------------------");
            Console.WriteLine("SOLVED");
            ss.printSudoku();

            DateTime end = DateTime.Now;
            long elapsedTicks = end.Ticks - beginning.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            Console.WriteLine("Elapsed Time: " + elapsedSpan.Milliseconds + "ms");


            Console.ReadKey();

        }

        static void readSudoku(string filePath)
        {
            TextReader reader = new StreamReader(filePath);

            string puzzle = reader.ReadToEnd();
            puzzle = puzzle.Trim('\r', ' ');
            string[] lines = puzzle.Split('\n');

            SudokuSolver ss = new SudokuSolver();

            ss.initialize(lines);
            if (ss.solveIt())
                ss.printSudoku();
        }
    }
}
