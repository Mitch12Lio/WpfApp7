using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfApp7.ViewModels.AtbashCipherViewModel;

namespace WpfApp7.Utilities
{
    public static class Algorithms
    {
        public static Dictionary<char, char> CreateAtbashDictionary()
        {
            Dictionary<char, char> atbashDictionary = new();

            char currentUpperCaseChar = 'Z';
            for (char c = 'A'; c <= 'Z'; c++)
            {
                atbashDictionary.Add(c, currentUpperCaseChar);
                currentUpperCaseChar--;
            }

            char currentLowerCaseChar = 'z';
            for (char c = 'a'; c <= 'z'; c++)
            {
                atbashDictionary.Add(c, currentLowerCaseChar);
                currentLowerCaseChar--;
            }
            return atbashDictionary;
        }

        public static Dictionary<char, String> CreateMorseDictionary()
        {
            return new Dictionary<char, String>()
            {
                { 'A' , ".-"},
                { 'B' , "-..."},
                { 'C' , "-.-."},
                { 'D' , "-.."},
                { 'E' , "."},
                { 'F' , "..-."},
                { 'G' , "--."},
                { 'H' , "...."},
                { 'I' , ".."},
                { 'J' , ".---"},
                { 'K' , "-.-"},
                { 'L' , ".-.."},
                { 'M' , "--"},
                { 'N' , "-."},
                { 'O' , "---"},
                { 'P' , ".--."},
                { 'Q' , "--.-"},
                { 'R' , ".-."},
                { 'S' , "..."},
                { 'T' , "-"},
                { 'U' , "..-"},
                { 'V' , "...-"},
                { 'W' , ".--"},
                { 'X' , "-..-"},
                { 'Y' , "-.--"},
                { 'Z' , "--.."},
                { '0' , "-----"},
                { '1' , ".----"},
                { '2' , "..---"},
                { '3' , "...--"},
                { '4' , "....-"},
                { '5' , "....."},
                { '6' , "-...."},
                { '7' , "--..."},
                { '8' , "---.."},
                { '9' , "----."},
                { ' ' , " / "},
            };
        }

        public static DataTable CreatePolybiusDataTable()
        {
            DataTable PolybiusDT = new DataTable();
            PolybiusDT.Clear();
            PolybiusDT.Columns.Add("-");
            PolybiusDT.Columns.Add("A");
            PolybiusDT.Columns.Add("B");
            PolybiusDT.Columns.Add("C");
            PolybiusDT.Columns.Add("D");
            PolybiusDT.Columns.Add("E");

            PolybiusDT.Rows.Add();
            PolybiusDT.Rows.Add();
            PolybiusDT.Rows.Add();
            PolybiusDT.Rows.Add();
            PolybiusDT.Rows.Add();

            Queue<char> charQ = new Queue<char>();
            charQ.Enqueue('1');
            charQ.Enqueue('2');
            charQ.Enqueue('3');
            charQ.Enqueue('4');
            charQ.Enqueue('5');
            char currentChar = 'A';
            while (currentChar <= 'Z')
            {
                charQ.Enqueue(currentChar);
                currentChar++;
            }

            for (int i = 0; i < 5; i++)
            {
                PolybiusDT.Rows[i][0] = charQ.Dequeue();
            }

            foreach (System.Data.DataRow dr in PolybiusDT.Rows)
            {
                foreach (System.Data.DataColumn dc in PolybiusDT.Columns)
                {
                    if (dc.Ordinal == 0) { }
                    else
                    {
                        if (charQ.Count != 0)
                        {
                            dr[dc] = charQ.Dequeue();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return PolybiusDT;
        }
    }
}
