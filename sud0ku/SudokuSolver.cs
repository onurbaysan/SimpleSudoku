using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace sud0ku
{
    class SudokuSolver
    {
        public Cell[,] sudoku = new Cell[9,9];
        public static int sum = 0;                           
                           
        public void initialize(string[] lines)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j] = new Cell();                    
                }
            }       

            
            int counter = 0;

            foreach(string line in lines)
            {
                for (int i = 0; i < line.Length - 1; i++)
                    sudoku[counter, i].value = Int32.Parse(line[i].ToString());

                counter++;                               
            }            
            
        }

        public void initialize(Cell[,] generated)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j] = new Cell();
                    sudoku[i, j] = generated[i, j];
                }

        }

        public void checkOnePossibility()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (getPossibilities(i, j).Count == 1)
                        getPossibilities(i, j).ElementAt(0);
                }
            }
        }

        public bool solveIt()
        {
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j].value == 0)
                    {
                        List<int> pos = getPossibilities(i, j);
                        if (pos.Count == 1)
                            sudoku[i, j].value = pos[0];
                    }                                       
                }
            }
            
            
            if (isSolved())
                return true;
            else {
                return backtracking(0);                    
            }           
        }

        public void checkDifferentPossibilityR(int i, int j)
        {
            List<int> poss = getPossibilities(i, j);

            for (int a = 0; a < 9; a++)
            {
                if (a != i)
                {
                    if (sudoku[a, j].value == 0)
                        poss.Except(getPossibilities(a, j));
                    else
                        poss.Remove(sudoku[a, j].value);
                }
            }

            if (poss.Count == 1)
                sudoku[i, j].value = poss[0];

        }

        public void checkDifferentPossibilityC(int i, int j)
        {
            List<int> poss = getPossibilities(i, j);

            for (int a = 0; a < 9; a++)
            {
                if (a != j)
                {
                    if (sudoku[i, a].value == 0)
                        poss.Except(getPossibilities(i,a));
                    else
                        poss.Remove(sudoku[i, a].value);
                }
            }

            if (poss.Count == 1)
                sudoku[i, j].value = poss[0];

        }

        public void checkDifferentPossibilityS(int i, int j)
        {
            List<int> poss = getPossibilities(i, j);

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
                    if (!(a == i && b == j))
                    {
                        if (sudoku[a, b].value == 0)
                            poss.Except(getPossibilities(a,b));
                        else
                            poss.Remove(sudoku[a,b].value);
                    }
                }
            }


            if (poss.Count == 1)
                sudoku[i, j].value = poss[0];

        }

        public void printSudoku()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(sudoku[i, j].value + " ");
                }
                Console.WriteLine();
            }
        }

        public void printPossibilities()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(getPossibilities(i, j).Count + " ");
                }
                Console.WriteLine();
            }
        }

        public bool backtracking(int x)
        {
            if (x == 81)
            {                
                return true;
            }

            int i = (int)Math.Floor(x / 9.0);
            int j = x % 9;

            if(sudoku[i, j].value != 0) {
                return backtracking(x + 1);
            } 
            else 
            {
                foreach (int possibility in getPossibilities(i,j))
                {
                    sudoku[i, j].value = possibility;                        

                    if (!backtracking(x + 1))
                        sudoku[i, j].value = 0;
                    else
                    {
                        return true;
                    }
                }
            }            
                         
            return false;
                
        }

        public bool isSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j].value == 0)
                        return false;
                }
            }
            return true;
        }

        public bool isValid(int i, int j, int val)
        {
            for (int a=0; a < 9; a++)
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

        public List<int> getPossibilities(int i, int j)
        {
            List<int> possibilities = new List<int>(Enumerable.Range(1,9));

            for (int a = 0; a < 9; a++)
                if (sudoku[i, a].value != 0 && a!=j)
                    possibilities.Remove(sudoku[i, a].value);

            for (int b = 0; b < 9; b++)
                if (sudoku[b, j].value != 0 && b!=i)
                    possibilities.Remove(sudoku[b ,j].value);

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
                    if (sudoku[a, b].value != 0 && !(a == i && b == j))
                        possibilities.Remove(sudoku[a, b].value);
                }
            }

            return possibilities;            
        }
                
    }
}
