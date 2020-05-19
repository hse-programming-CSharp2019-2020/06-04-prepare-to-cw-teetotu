using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CWLibrary
{
    [Serializable]
    public class Dictionary
    {
        private static readonly Random Rnd = new Random(DateTime.Now.Millisecond);

        private int locale;
        private List<Pair<string, string>> words = new List<Pair<string, string>>();

        public Dictionary()
        {
            locale = Rnd.Next(0, 2);
        }

        public Dictionary(List<Pair<string, string>> words)
        {
            locale = Rnd.Next(0, 2);
            this.words = words;
        }

        internal void Add(Pair<string, string> pair) =>
            words.Add(pair);


        internal void Add(string word1, string word2) =>
            words.Add(new Pair<string, string>(word1, word2));


        public IEnumerator GetEnumerator()
        {
            return locale == 0
                ? words.GetRange(0, words.Count)
                    .OrderBy(word => word.item1)
                    .Select(word => word)
                    .GetEnumerator()
                : words.GetRange(0, words.Count)
                    .OrderBy(word => word.item2)
                    .Select(word => word)
                    .GetEnumerator();
        }

        public IEnumerable GetWords(char letter)
        {
            return words
                .GetRange(0, words.Count)
                .OrderBy(word => word.item1)
                .Where(word => word.item1.StartsWith(letter.ToString()))
                .ToList();
        }

        public void SerializeDictionary(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(fs, this);
            }
        }

        public static Dictionary DeserializeDictionary(string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var dict = (Dictionary) formatter.Deserialize(fs);
                return dict;
            }
        }
    }
}