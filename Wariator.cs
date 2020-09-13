using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Wariator
    {
        List<List<char[]>> twory;

        int n, k;
        char[] arr;
        char[] free;

        public Wariator(List<string> dok)
        {
            n = dok.Count;
            k = n;
            arr = new char[n];
            free = new char[n];
            for (int i = 0; i < dok.Count; i++)
                free[i] = dok[i][1];
            twory = new List<List<char[]>>();
            for (int i = 0; i < dok.Count; i++)
                twory.Add(new List<char[]>());

        }

        public List<List<char[]>> wariacja()
        {
            while (k > 0)
            {
                wariuj(0);
                k--;
            }

            return twory;
        }

        public void wariuj(int index)
        {
            if (index >= k)
            {
                twory[k - 1].Add((char[])arr.Clone());
            }
            else
            {
                for (int i = index; i < n; i++)
                {
                    arr[index] = free[i];

                    char old = free[i];
                    free[i] = free[index];
                    free[index] = old;

                    wariuj(index + 1);

                    old = free[i];
                    free[i] = free[index];
                    free[index] = old;
                }
            }
        }
    }
}
