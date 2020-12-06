using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTFHF02_Korrep_Prep
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            int rot = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(CaesarCode(text, rot));
        }

        static string CaesarCode(string message, int rot)
        {

            string abc = "abcdefghijklmnopqrstuvwxyz0123456789abcdefghijklmnopqrstuvwxyz0123456789".ToUpper();
            string upperMessage = message.ToUpper();
            string codedMessage = "";
            

            //Goes through the message char by char
            for (int i = 0; i < message.Length; i++)
            {
                int tmpIndex = -1;
                //Finds, if the char needs to be converted (according to ASCII table)
                if ((upperMessage[i] >= '0' && upperMessage[i] <= '9') || (upperMessage[i] >= 'A' && upperMessage[i] <= 'Z'))
                {
                    //Finds the index of the char in the abc array, than stores it in tmpIndex
                    for (int j = 0; j < abc.Length && tmpIndex==-1; j++)
                        if (upperMessage[i] == abc[j])
                            tmpIndex = j;
                    //convert the char according to it's position (tmpIndex) and the value of rot, overflow is handled
                    codedMessage += abc[tmpIndex + rot];
                }
                else
                    codedMessage += upperMessage[i];
            }
            //Convert UpperCase to Lowercase, based on the original text (message)
            string codedMessagev2 = "";
            for (int i = 0; i < codedMessage.Length; i++)
            {
                if (char.IsLower(message[i]))
                    codedMessagev2 += char.ToLower(codedMessage[i]);
                else
                    codedMessagev2 += codedMessage[i];
            }
            return codedMessagev2;
        }
    }
}
