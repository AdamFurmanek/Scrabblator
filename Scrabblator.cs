using System;
using System.Collections.Generic;
using System.Threading;

namespace Scrabblator
{
    class Scrabblator
    {
        Trie slownik;

        public string[,] bonusyMapy;
        public Dictionary<char, int[]> bonusyLiter;
        public List<string> dok;
        public string[,] plansza;


        public bool kontynuuj;


        public Scrabblator(string path)
        {
            slownik = new Trie(path);
        }

        public void ustaw(string[,] bM, Dictionary<char, int[]> bL)
        {
            bonusyMapy = bM;
            bonusyLiter = bL;
        }

        public void scrabbluj(string[,] p, List<string> d)
        {

            plansza = p;
            dok = d;

            Thread t = new Thread(new ThreadStart(scrabblujThread));
            t.Start();
        }

        public void scrabblujThread()
        {
            kontynuuj = true;
            List<List<char[]>> twory = new Wariator(dok).wariacja();
            new Generator(bonusyMapy, bonusyLiter, plansza, dok, twory, slownik, this).generuj();

        }

        public void zatrzymaj()
        {
            kontynuuj = false;
        }

        static void Main(string[] args)
        {

            //USTAWIENIA DLA KURNIKA
            string[,] bonusyMapy = {
                {"34", "00", "30", "00", "00", "32", "00", "34", "00", "32", "00", "00", "30", "00", "34", },
                {"00", "00", "00", "00", "32", "00", "34", "00", "34", "00", "32", "00", "00", "00", "00", },
                {"30", "00", "00", "32", "00", "20", "00", "31", "00", "20", "00", "32", "00", "00", "30", },
                {"00", "00", "32", "00", "20", "00", "31", "00", "31", "00", "20", "00", "32", "00", "00", },
                {"00", "32", "00", "20", "00", "31", "00", "00", "00", "31", "00", "20", "00", "32", "00", },
                {"32", "00", "20", "00", "31", "00", "00", "33", "00", "00", "31", "00", "20", "00", "32", },
                {"00", "34", "00", "31", "00", "00", "33", "00", "33", "00", "00", "31", "00", "34", "00", },
                {"34", "00", "31", "00", "00", "33", "00", "00", "00", "33", "00", "00", "31", "00", "34", },
                {"00", "34", "00", "31", "00", "00", "33", "00", "33", "00", "00", "31", "00", "34", "00", },
                {"32", "00", "20", "00", "31", "00", "00", "33", "00", "00", "31", "00", "20", "00", "32", },
                {"00", "32", "00", "20", "00", "31", "00", "00", "00", "31", "00", "20", "00", "32", "00", },
                {"00", "00", "32", "00", "20", "00", "31", "00", "31", "00", "20", "00", "32", "00", "00", },
                {"30", "00", "00", "32", "00", "20", "00", "31", "00", "20", "00", "32", "00", "00", "30", },
                {"00", "00", "00", "00", "32", "00", "34", "00", "34", "00", "32", "00", "00", "00", "00", },
                {"34", "00", "30", "00", "00", "32", "00", "34", "00", "32", "00", "00", "30", "00", "34", }
            };

            Dictionary<char, int[]> bonusyLiter = new Dictionary<char, int[]>();
            int[] typ1 = { 1, 1 };
            int[] typ2 = { 2, 2 };
            int[] typ3 = { 3, 3 };
            int[] typ4 = { 4, 5 };
            bonusyLiter.Add('a', typ1);
            bonusyLiter.Add('ą', typ4);
            bonusyLiter.Add('b', typ3);
            bonusyLiter.Add('c', typ2);
            bonusyLiter.Add('ć', typ4);
            bonusyLiter.Add('d', typ2);
            bonusyLiter.Add('e', typ1);
            bonusyLiter.Add('ę', typ4);
            bonusyLiter.Add('f', typ4);
            bonusyLiter.Add('g', typ3);
            bonusyLiter.Add('h', typ3);
            bonusyLiter.Add('i', typ1);
            bonusyLiter.Add('j', typ3);
            bonusyLiter.Add('k', typ2);
            bonusyLiter.Add('l', typ2);
            bonusyLiter.Add('ł', typ3);
            bonusyLiter.Add('m', typ2);
            bonusyLiter.Add('n', typ1);
            bonusyLiter.Add('ń', typ4);
            bonusyLiter.Add('o', typ1);
            bonusyLiter.Add('ó', typ4);
            bonusyLiter.Add('p', typ2);
            bonusyLiter.Add('r', typ1);
            bonusyLiter.Add('s', typ1);
            bonusyLiter.Add('ś', typ4);
            bonusyLiter.Add('t', typ2);
            bonusyLiter.Add('u', typ3);
            bonusyLiter.Add('w', typ1);
            bonusyLiter.Add('y', typ2);
            bonusyLiter.Add('z', typ1);
            bonusyLiter.Add('ź', typ4);
            bonusyLiter.Add('ż', typ4);

            //TESTY
            char[,] plansza0 =
            {
                {'.', '.', 'p', '.', '.', 't', '.', '.', '.', '.', '.', '.', 'z', '.', '.'},
                {'.', '.', 'ą', '.', 't', 'a', 'ś', '.', '.', '.', '.', '.', 'ł', '.', '.'},
                {'w', 'a', 's', 'z', 'e', 'j', '.', '.', '.', '.', '.', 't', 'a', 'n', 'i'},
                {'.', '.', 'y', '.', 'g', '.', '.', '.', 'm', 'n', 'i', 'e', 'j', '.', '.'},
                {'.', '.', '.', 'p', 'o', 's', 'i', 'l', 'ę', '.', '.', 'm', 'a', 'ź', '.'},
                {'k', 'a', 'c', 'a', '.', '.', 'l', 'u', '.', '.', '.', 'b', '.', '.', '.'},
                {'.', 'ż', 'o', 'n', '.', '.', 'u', 'd', '.', '.', '.', 'r', '.', 's', '.'},
                {'.', '.', '.', 'n', '.', '.', '.', 'z', '.', 'w', 'ł', 'ó', 'k', 'a', '.'},
                {'.', '.', 'g', 'i', 'l', '.', '.', 'i', '.', '.', '.', 'w', 'e', 'ń', '.'},
                {'d', 'b', 'a', 'c', 'i', 'e', '.', 'e', '.', '.', '.', '.', 'r', '.', '.'},
                {'o', '.', '.', 'a', 'd', '.', '.', '.', 'h', 'a', 's', 'h', 'y', '.', '.'},
                {'m', '.', '.', '.', 'e', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                {'i', '.', '.', 'p', 'r', 'z', 'y', 'w', 'r', 'o', '.', '.', '.', '.', '.'},
                {'e', '.', '.', '.', 'y', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                {'.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'}
            };

            string[,] plansza = new string[15, 15];
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    if (plansza0[i, j] == '.')
                    {
                        plansza[i, j] = "pb";
                    }
                    else
                        plansza[i, j] = "a" + plansza0[i, j];
                }
            }

            char[] dok0 = { 'c', 's', 'o', 'o', 'a'};

        List<string> dok = new List<string>();

            for (int i = 0; i < dok0.Length; i++)
                dok.Add("a" + dok0[i]);

            Scrabblator scrabblator = new Scrabblator(@"..\..\..\Resources\Slownik.txt");
            scrabblator.ustaw(bonusyMapy, bonusyLiter);
            scrabblator.scrabbluj(plansza,dok);
        }

    }
}
