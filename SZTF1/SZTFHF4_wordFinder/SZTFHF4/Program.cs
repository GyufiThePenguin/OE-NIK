using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZTFHF4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            WordFinder f = new WordFinder(input);
            f.GenerateOutput();
        }
    }

    class WordFinder
    {
        string[,,] matrix;
        string[] dictionary;
        public WordFinder(string[] input)
        {
            dictionary = new string[Convert.ToInt32(input[0])];
            for (int i = 0; i < dictionary.Length; i++)
            {
                dictionary[i] = input[i + 1];
            }

            matrix = new string[Convert.ToInt32(input[dictionary.Length + 1].Split(' ')[0]), Convert.ToInt32(input[dictionary.Length + 1].Split(' ')[1]),2];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j,0] = input[dictionary.Length + 2+i][j].ToString();
                }
            }

            Generate3rdDimension();
        }

        private void Generate3rdDimension()
        {
            for (int i = 0; i < dictionary.Length; i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        if (dictionary[i][0].ToString() == matrix[j,k,0])
                        {
                            string direction = GetDirection(dictionary[i], j, k);
                            if (direction != "deadEnd")
                                WordEleminator(dictionary[i], j, k, direction);
                        }
                    }
                }
            }
            //Tester();
        }

        private string GetDirection(string word, int j, int k)
        {
            if (k < matrix.GetLength(1) - 1 && matrix[j, k + 1, 0] == word[1].ToString())
                if (IsValidDirection(word,j,k,"right"))
                    return "right";
            if (k > 0 && matrix[j, k - 1, 0] == word[1].ToString())
                if (IsValidDirection(word, j, k, "left"))
                    return "left";
            if (j < matrix.GetLength(1)-1 && matrix[j + 1, k, 0] == word[1].ToString())
                if (IsValidDirection(word, j, k, "down"))
                    return "down";
            if (j > 0 && matrix[j - 1, k, 0] == word[1].ToString())
                if (IsValidDirection(word, j, k, "up"))
                    return "up";
            if (k < matrix.GetLength(1) - 1 && j < matrix.GetLength(1) - 1 && matrix[j + 1, k + 1, 0] == word[1].ToString())//rightDown
                if (IsValidDirection(word, j, k, "rd"))
                    return "rd";
            if (k < matrix.GetLength(1) - 1 && j > 0 && matrix[j - 1, k + 1, 0] == word[1].ToString())//rightUp
                if (IsValidDirection(word, j, k, "ru"))
                    return "ru";
            if (k > 0 && j < matrix.GetLength(1) - 1 && matrix[j + 1, k - 1, 0] == word[1].ToString())//leftDown
                if (IsValidDirection(word, j, k, "ld"))
                    return "ld";
            if (k > 0 && j > 0 && matrix[j - 1, k - 1, 0] == word[1].ToString())//leftUp
                if (IsValidDirection(word, j, k, "lu"))
                    return "lu";

            return "deadEnd";
        }

        private bool IsValidDirection(string word, int j, int k, string direction)
        {
            bool correct = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (direction == "down")
                {
                    if (j + word.Length > matrix.GetLength(0))
                        return false;
                    if (matrix[j + i, k, 0] != word[i].ToString())
                        correct = false;
                }
                if (direction == "up")
                {
                    if ((j + 1) - word.Length < 0)
                        return false;
                    if(j-word.Length > 0 && matrix[j - i, k, 0] != word[i].ToString())
                        correct = false;
                }
                if (direction == "right")
                {
                    if (k + word.Length > matrix.GetLength(1))
                        return false;
                    if (matrix[j, k + i, 0] != word[i].ToString())
                        correct = false;
                }
                if(direction == "left")
                {
                    if ((k + 1) - word.Length  < 0)
                        return false;
                    if (matrix[j, k - i, 0] != word[i].ToString())
                        correct = false;
                }
                if(direction == "rd")
                {
                    if (k + word.Length > matrix.GetLength(1) || j + word.Length > matrix.GetLength(0))
                        return false;
                    if (matrix[j + i, k + i, 0] != word[i].ToString())
                        correct = false;
                }
                if (direction == "ru")
                {
                    if (k + word.Length > matrix.GetLength(1) || (j + 1) - word.Length < 0)
                        return false;
                    if (matrix[j - i, k + i, 0] != word[i].ToString())
                        correct = false;
                }
                if (direction == "ld")
                {
                    if ((k + 1) - word.Length < 0 || j + word.Length > matrix.GetLength(0))
                        return false;
                    if (matrix[j + i, k - i, 0] != word[i].ToString())
                        correct = false;
                }
                if (direction == "lu")
                {
                    if ((k + 1) - word.Length < 0 || (j + 1) - word.Length < 0)
                        return false;
                    if (matrix[j - i, k - i, 0] != word[i].ToString())
                        correct = false;
                }
            }
            return correct;
        }
        private void WordEleminator(string word, int j, int k, string direction)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (direction == "down")
                    matrix[j + i, k, 1] = "X";
                if (direction == "up")
                    matrix[j - i, k, 1] = "X";
                if (direction == "right")
                    matrix[j, k + i, 1] = "X";
                if (direction == "left")
                    matrix[j, k - i, 1] = "X";
                if (direction == "rd")
                    matrix[j + i, k + i, 1] = "X";
                if (direction == "ru")
                    matrix[j - i, k + i, 1] = "X";
                if (direction == "ld")
                    matrix[j + i, k - i, 1] = "X";
                if (direction == "lu")
                    matrix[j - i, k - i, 1] = "X";
            }
        } 
        private void Tester()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j, 1] == "X")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(matrix[i,j,0]);
                }
                Console.WriteLine();
            }
        }
        
        public void GenerateOutput()
        {
            string output = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j, 1] != "X")
                        output += matrix[i, j, 0];
                }
            }
            StreamWriter writer = new StreamWriter("output.txt");
            writer.Write(output);
            writer.Close();
        }
    }
}