using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Queen
{
    public static class StaticFunctions
    {

        public static Dictionary<string, string> chessSet = new Dictionary<string, string>();
        public static string last { get; set; }
        public static List<string> Position = new List<string>();

        public static string current { get; set; }
        public static int size { get; set; }

        public static List<EndScenerio> endpointlist { get; set; } = new List<EndScenerio>();

        public static string[] passed { get; set; }= new string[0];




        public static bool removeDiagnals()
        {
            if (current != last)
            {
                int Value1 = (int)Char.GetNumericValue(current[0]);
                int Value2 = (int)Char.GetNumericValue(current[2]);
                for (int i = -2*size; i < 2*size; i++)
                {
                    chessSet.Remove($"{Value1} {i}");
                    chessSet.Remove($"{i} {Value2}");
                    chessSet.Remove($"{Value1 + i} {Value2 + i}");
                    chessSet.Remove($"{Value1 + i} {Value2 - i}");
                    chessSet.Remove($"{Value1 - i} {Value2 - i}");
                    chessSet.Remove($"{Value1 - i} {Value2 + i}");
                    
                }
                chessSet.Add(current, "Valid");
                passed = passed.Append(current).ToArray();
                last = current;
                return true;
            }
            return false;
        }


        public static bool getNext()
        {
            Dictionary<string, string> tempChessSet = new Dictionary<string, string>(chessSet);
            foreach (string key in passed)
            {
                tempChessSet.Remove(key);
            }
            List<KeyValuePair<string, string>> chessSetList = tempChessSet.ToList();
            try
            {
                current = chessSetList[0].Key;
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }


        public static void runQueenTest()
        {
            bool done = false;
            done = removeDiagnals();

            List<KeyValuePair<string, string>> chessSetList = chessSet.ToList();

            string[] keys = chessSetList.Select(kvp => kvp.Key).ToArray();

            if (!done)
            {
                EndScenerio endScenerio = new EndScenerio(passed, passed.Length);

                endpointlist.Add(endScenerio);
            }
            else
            {
                if (getNext())
                {
                    runQueenTest();
                }
                else {
                    EndScenerio endScenerio = new EndScenerio(passed, passed.Length);
                    endpointlist.Add(endScenerio);
                }
               
            }
            
        }

        public static void resetboard()
        {
            Dictionary<string, string> newChessSet = new Dictionary<string, string>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newChessSet.Add($"{i} {j}","Test");
                }
            }
            chessSet.Clear();
            chessSet = newChessSet;
        }

        public static void setstartingPosition()
        {
            List<string> startingPositions = new List<string>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    startingPositions.Add($"{i} {j}");
                }
            }
            Position.Clear();
            Position = startingPositions;
        }


        public static void runboard() {

            setstartingPosition();
            foreach (string place in Position)
            {
                reZeroSystem();
                resetboard();
                current = place;
                runQueenTest();
               

            }
        }


        public static void reZeroSystem()
        {
            passed = new string[0];
            
        }


        public static string dispBoard(EndScenerio scenerio)
        {
            bool flag = false;
            string dysplay = $"{scenerio.amount}\n";
            List<string> InternalPositions = new List<string>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    flag = true;
                    foreach (string place in scenerio.strings) {
                        if (place == $"{i} {j}")
                        {
                            flag = false;
                            InternalPositions.Add("| * |");
                        }
                    }
                    if(flag) {
                        InternalPositions.Add($"|{i} {j}|");
                    }
                }
            }
            int counter = 0;
            foreach (string place in InternalPositions)
            {
                
                if (counter%size != 0 ) {
                    dysplay += place;
                }
                else
                {
                    dysplay += $"\n{place}";
                }
                counter++;
            }
            dysplay += $"\n\n\n";
            return dysplay;


        }
    }
}


