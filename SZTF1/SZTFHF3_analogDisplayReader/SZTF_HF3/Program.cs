using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF_HF3
{
    class Program
    {
        static void Main(string[] args)
        {
            string row1 = Console.ReadLine();//" _     _  _     _  _  _  _  _ ";
            string row2 = Console.ReadLine();//"| |  | _| _||_||_ |_   ||_||_|";
            string row3 = Console.ReadLine();//"|_|  ||_  _|  | _||_|  ||_| _|";
            Convert c = new Convert(row1, row2, row3);
            Console.WriteLine(c.DisplayToNumeric());
        }
    }

    class Convert
    {
        private string row1;
        private string row2;
        private string row3;
        public Convert(string row1, string row2, string row3)
        {
            this.row1 = row1;
            this.row2 = row2;
            this.row3 = row3;
        }

        public string DisplayToNumeric()
        {
            string[,] matrix = new string[3, row1.Length];
            for (int j = 0; j < row1.Length; j++)
            {
                matrix[0, j] = row1[j].ToString();
                matrix[1, j] = row2[j].ToString();
                matrix[2, j] = row3[j].ToString();
            }
            string output = "";
            for (int i = 0; i < matrix.GetLength(1); i+=3)
            {
                output += Recogniser(new string[,]
                {
                    {matrix[0,i], matrix[0,i+1], matrix[0,i+2] },
                    {matrix[1,i], matrix[1,i+1], matrix[1,i+2] },
                    {matrix[2,i], matrix[2,i+1], matrix[2,i+2] }
                });
            }
            return output;
        }

        private string Recogniser(string[,] digit)
        {
            //space
            if (digit[1, 1] == " " && digit[2, 2] == " ")
                return " ";
            //divide by the bottom left char.
            else if (digit[2, 0] == "|")
            {
                if (digit[1, 1] != "_")
                    return "0";
                else if (digit[2, 2] != "|")
                    return "2";
                else if (digit[1, 2] != "|")
                    return "6";
                else
                    return "8";
            }
            else
            {
                //devide by the middle left char
                if (digit[1, 0] != "|")
                {
                    if (digit[0, 1] == "_" && digit[1, 1] == "_")
                        return "3";
                    else if (digit[0, 1] == "_")
                        return "7";
                    else
                        return "1";
                }
                else
                {
                    if (digit[0, 1] != "_")
                        return "4";
                    else if (digit[1, 2] != "|")
                        return "5";
                    else
                        return "9";
                }
            }
            return "";

        }
    }
}
