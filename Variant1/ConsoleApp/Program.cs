using System;
using System.Collections.Generic;
using System.IO;
using CWLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        private static Random Rnd = new Random(DateTime.Now.Millisecond);

        static int GetInt(int minVal = int.MinValue, int maxVal = int.MaxValue)
        {
            int a = 0;
            Console.WriteLine("Enter the amount of lines for your dictionary: ");
            Console.WriteLine();
            while (!int.TryParse(Console.ReadLine(), out a) || a < minVal || a > maxVal)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input");
            }
            Console.Clear();
            return a;
        }

        static string GenerateWord(char lBound, char rBound)
        {
            var len = Rnd.Next(4, 11);
            var str = string.Empty;

            for (var i = 0; i < len; i++)
                str += (char) (Rnd.Next(lBound, rBound + 1));
            return str;
        }

        private static void Main()
        {
            var n = GetInt(0);
            var path = "dictionary.txt";
            var dictList = new List<Pair<string, string>>();

            using (var sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
            {
                for (var i = 0; i < n; i++)
                    sw.WriteLine($"{GenerateWord('а', 'я')} " +
                                 $"{GenerateWord('a', 'z')}");
            }

            using (var sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var entry = line.Split();
                    dictList.Add(new Pair<string, string>(entry[0], entry[1]));
                }
            }

            var dictionary = new Dictionary(dictList);

            var path2 = "out.txt";
            dictionary.SerializeDictionary(path2);
            var newDictionary = Dictionary.DeserializeDictionary(path2);

            foreach (var pair in newDictionary)
                Console.WriteLine(pair);

            Console.WriteLine("\n\n");
            var temp = (char) Rnd.Next('а', 'я');
            Console.WriteLine(temp);
            foreach (var pair in newDictionary.GetWords(temp))
                Console.WriteLine(pair);
        }
    }
}