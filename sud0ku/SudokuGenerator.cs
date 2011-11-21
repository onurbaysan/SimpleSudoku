using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sud0ku
{
    class SudokuGenerator
    {
        private Cell[,] sudoku = new Cell[9, 9];
        private Cell[,] tmp = new Cell[9, 9];
        List<Cell> points = new List<Cell>();

        /*
         * Difficuly Level
         */
        public Cell[,] generateSudoku(DifficultyLevel.levels level)
        {
            int willOpen = 0;
                        
            
            switch (level)
            {
                case DifficultyLevel.levels.easy:
                    willOpen = 30;
                    break;
                case DifficultyLevel.levels.normal:
                    willOpen = 25;
                    break;
                case DifficultyLevel.levels.harder:
                    willOpen = 20;
                    break;
                case DifficultyLevel.levels.veteran:
                    willOpen = 15;
                    break;
            }

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j] = new Cell();
                    tmp[i, j] = new Cell();
                }

            SudokuSolver ss = new SudokuSolver();
            ss.initialize(sudoku);
            Random rand = new Random();

            while (true)
            {

                for (int i = 0; i < willOpen; )
                {
                    int row = rand.Next(0, 8);
                    int col = rand.Next(0, 8);

                    if (sudoku[row, col].value == 0)
                    {
                        int value = rand.Next(1, 9);
                        while (!isValid(row, col, value)) 
                            value = rand.Next(1, 9);

                        Cell cell = new Cell();
                        cell.row = row;
                        cell.column = col;
                        cell.value = value;
                        points.Add(cell);
                        sudoku[row, col].value = value;
                        i++;
                    }
                }

                if (ss.solveIt())
                    break;
                else
                {
                    clearSudoku();
                    ss.initialize(sudoku);
                }

            }

            maskIt();
            return sudoku;
        }

        public bool isValid(int i, int j, int val)
        {
            for (int a = 0; a < 9; a++)
                if (a != j && sudoku[i, a].value == val)
                    return false;

            for (int b = 0; b < 9; b++)
                if (b != i && sudoku[b, j].value == val)
                    return false;

            int minI, minJ;

            if (i >= 0 && i < 3)
                minI = 0;
            else if (i >= 3 && i < 6)
                minI = 3;
            else
                minI = 6;

            if (j >= 0 && j < 3)
                minJ = 0;
            else if (j >= 3 && j < 6)
                minJ = 3;
            else
                minJ = 6;



            for (int a = minI; a < minI + 3; a++)
            {
                for (int b = minJ; b < minJ + 3; b++)
                {
                    if (sudoku[a, b].value == val && !(a == i && b == j))
                        return false;
                }
            }

            return true;
        }

        public void clearSudoku()
        {

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    sudoku[i, j].value = 0;
        }

        public void maskIt()
        {
            clearSudoku();
            foreach (Cell point in points)
            {
                sudoku[point.row, point.column].value = point.value;
            }
        }

    }
}
