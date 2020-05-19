using System;

namespace CWLibrary
{
    [Serializable]
    public class Pair<T, U> : IComparable
        where T : IComparable
    {
        public T item1;
        public U item2;

        public Pair(T item1, U item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public int CompareTo(object obj) =>
            item1.CompareTo(((Pair<T, U>)obj).item1);


        public override string ToString() =>
            $"{item1} - {item2}";
    }
}