using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tura_EgyFelevesHF
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("TURA.BE");
            Planner p = new Planner(input);
            p.GenerateOutput();
        }
    }

    class Planner
    {    
        int n;                  //Stores the first line of the input, which identicates the numbers of "connections" between rivers.
        string[,] routes;       //Stores the input in a 2D matrix, 1st row is thee source river, 2nd row is the destination river.
        string sourceRiver1;    //"Az utolsó két sorban egy-egy folyó neve van, az első és a második túra kezdete." [From the .PDF]
        string sourceRiver2;    //
        string[] tree1;         //Stores the connectivity links of sourceRiver1
        string[] tree2;         //Stores the connectivity links of sourceRiver2


        public Planner(string[] input)
        {
            n = Convert.ToInt32(input[0]);                                        
            routes = new string[(input.Length - 3) / 2, 2];     //Creating the matrix, where the datas will be properly stored.    
            int inputIndex = 1;                                 //Since the input[] and routes[,] are not aligned (because of the first row), I created a costum index to be able to refer to the item in input[].
            for (int i = 0; i < routes.GetLength(0); i++)       //Filling up the routes[,].
            {
                routes[i, 0] = input[inputIndex];
                inputIndex++;
                routes[i, 1] = input[inputIndex];
                inputIndex++;
            }
            sourceRiver1 = input[input.Length - 2];             //Assigns value to 'sourceRiver1'.
            sourceRiver2 = input[input.Length - 1];             //Assigns value to 'sourceRiver2'.

            tree1 = GenerateTree(sourceRiver1);                 //Assigns the connectivity links of 'sourceRiver1' to 'tree1'.
            tree2 = GenerateTree(sourceRiver2);                 //Assigns the connectivity links of 'sourceRiver2' to 'tree2'.
        }

        private string[] GenerateTree(string startRiver)        //A function that returns with a matrix[], which contains the connected rivers in order, started by 'startRiver' (aka: connectivity links).
        {
            string riverChain = startRiver + ",";               //A helper string this will contain the resoult of the recursion below (YouSpinMeRightRoundBabyRightRound). The elements are seperated by a ','.
            YouSpinMeRightRoundBabyRightRound(startRiver, ref riverChain);         //Opens the Gate of Hell (Okay, not exactly... It just starts the recursion).
            return riverChain.Split(',');                       //Returns the resoult of madness in form of a matrix[].
        }

        private void YouSpinMeRightRoundBabyRightRound(string searchedRiver, ref string riverChain)      //Yes, it's a recursion. The 'searchRiver' string will contain the river, which you want to find the estuary of. 'riverChain' is explained above.
        {
            for (int i = 0; i < routes.GetLength(0); i++)
            {
                if (routes[i, 0] == searchedRiver)                                                  //Checks, if the 'searchedRiver' is equal to the actual element of the 'routes[,]'.
                {
                    riverChain += routes[i, 1] + ",";                                               //Adds the search result to 'riverCahin'.
                    YouSpinMeRightRoundBabyRightRound(routes[i, 1], ref riverChain);                //Starts it all over again. Note that the 'searchedRiver' parameter is changed to the result.
                }                 
            }
        }

        public void GenerateOutput()        //Generates the output file (TURA.KI)
        {
            Console.WriteLine(IsConnected(tree1,tree2));//TESTING! 
            Console.WriteLine(CanItWaitForIt(tree1,sourceRiver2));//TESTING!
            StreamWriter writer = new StreamWriter("TURA.KI");
            writer.WriteLine(IsConnected(tree1, tree2));
            writer.Write(CanItWaitForIt(tree1, sourceRiver2));
            writer.Close();
        }

        private string IsConnected(string[] riverTree1, string[] riverTree2)        //Check if there is any connection between rivers, based on their connectivity links (riverTree). If there is, it returns the name of that river.
        {
            for (int i = 0; i < riverTree1.Length; i++)
            {
                for (int j = 0; j < riverTree2.Length; j++)
                {
                    if (riverTree1[i] == riverTree2[j])
                        return riverTree1[i];
                }
            }
            return "";
        }

        private int CanItWaitForIt(string[] riverTree1, string sourceRiver2)        //Checks if the tour in river2 could wait for river1 tour. If yes, returns the waiting time, else return 0. 
        {
            for (int i = 0; i < riverTree1.Length; i++)
            {
                if (riverTree1[i] == sourceRiver2)
                    return i+1;
            }
            return 0;
        }
    }
}


/*                             
   ▄▄▄▄▄▄▄  ▄   ▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄  
   █ ▄▄▄ █ ▀█▄█▀  █▀  ▄█ █ ▄▄▄ █  
   █ ███ █ ▀▀▄▀▄ ▀ ▄███▄ █ ███ █  
   █▄▄▄▄▄█ █ █ ▄▀█▀▄▀▄ █ █▄▄▄▄▄█  
   ▄▄▄▄▄ ▄▄▄▄▄ █ █ ▀▄ ▀ ▄ ▄ ▄ ▄   
   ▄█  ▀▄▄███ ▄▄▀▀██▄ ▀▀▄█▀▀   ▀  
   ▄█ ▄  ▄███▀█ ▄ ▄▀▄ █▀  ▀ █▄▀   
    ▀▄ ▄█▄ █▄▀ ▀▄▀█▀ █▀▄▀█▄▀▄▄ ▀  
   █ █▀█▄▄▀█▀▄ █▄▄█▄▄  ▀ ▀█▀▄▄▀   
   █▀▄▀ ▄▄██▀ ▄▄█▄██  ▀▀ ▀█▀ █ ▀  
   █  ▀▀▀▄ █ ██  ▄▀█▄▀▀▄▄▄██ ▄█▄  
   ▄▄▄▄▄▄▄ █ ▀ ▀▀▄▄ ▄█▄█ ▄ ███▀▀  
   █ ▄▄▄ █ ▄▄▀ █ ▄▄█▄ ▀█▄▄▄█ ▄▄█  
   █ ███ █ █▄▀▄▄ █▄█▀██  ▄█▄███▀  
   █▄▄▄▄▄█ █▀ █▄ ▀ █▄██▀▄ █▄█▄▀   
*/