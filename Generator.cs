﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Generator
    {

        String[,] plansza;
        List<String> dok;
        List<List<char[]>> twory;
		int[,] uklad;

        Sprawdzator sprawdzator;
        Kalkulator kalkulator;

        Scrabblator scrabblator;

        int dobrych;
        int jakichkolwiek;

        String[,] najlepszaPlansza;
        int najlepszyWynik;

        public Generator(string[,] bM, Dictionary<char, int[]> bL, List<int> bD, string[,] p, List<String> d, List<List<char[]>> t, Trie trie,  Scrabblator s)
        {
            kalkulator = new Kalkulator(bM, bL, bD);
            plansza = p;
            dok = d;
            twory = t;
            sprawdzator = new Sprawdzator(trie);
            scrabblator = s;
			uklad = new int[plansza.GetUpperBound(0)+1, plansza.GetUpperBound(1) + 1];

        }

        public void generuj()
        {
            int wysokosc = plansza.GetUpperBound(0) + 1;
            int szerokosc = plansza.GetUpperBound(1) + 1;
            jakichkolwiek = 0;
            dobrych = 0;
            najlepszyWynik = 0;

            for (int i = 0; i < wysokosc; i++)
            {
                for (int j = 0; j < szerokosc; j++)
                {
                    for (int k = 0; k < dok.Count; k++)
                    {
                        if (!scrabblator.kontynuuj)
                            return;
                        int l1 = 0, l2 = 0;
                        for (int m = 0; m < wysokosc; m++)
                            for (int n = 0; n < szerokosc; n++)
                                uklad[m,n] = 0;

                        while (l1 != k + 1)
                        {
                            if (j + l2 >= wysokosc)
                                break;
                            while (plansza[i,j + l2][0] != 'p')
                            {
                                l2++;
                                if (j + l2 >= szerokosc)
                                    break;
                            }
                            if (j + l2 >= szerokosc)
                                break;
                            uklad[i,j + l2] = 1;
                            l1++;
                            l2++;
                        }

                        if (sprawdzPrzystawanie())
                        {
                            przeslij(l1, true);
                        }

                        if (j + l2 >= wysokosc)
                            break;
                    }
                }
            }


            for (int i = 0; i < wysokosc; i++)
            {                       //kolumny
                for (int j = 0; j < szerokosc; j++)
                {                   //wiersz
                    for (int k = 0; k < dok.Count; k++)
                    {
                        if (!scrabblator.kontynuuj)
                            return;
                        //ile liter
                        int l1 = 0, l2 = 0;                 //l1 - trzyma liczbe liter do postawienia     l2 - trzyma miejsce w ktorym ma byc polozona
                        for (int m = 0; m < wysokosc; m++)
                            for (int n = 0; n < szerokosc; n++)
                                uklad[m,n] = 0;

                        while (l1 != k + 1)
                        {
                            if (j + l2 >= wysokosc)
                                break;
                            while (plansza[j + l2,i][0] != 'p')
                            {
                                l2++;
                                if (j + l2 >= szerokosc)
                                    break;
                            }
                            if (j + l2 >= szerokosc)
                                break;
                            uklad[j + l2,i] = 1;
                            l1++;
                            l2++;
                        }

                        if (sprawdzPrzystawanie())
                        {
                            przeslij(l1, false);
                        }

                        if (j + l2 >= wysokosc)
                            break;
                    }
                }
            }
        }

        private void przeslij(int l1, bool poziomo)
        {
            int wysokosc = plansza.GetUpperBound(0) + 1;
            int szerokosc = plansza.GetUpperBound(1) + 1;

            String[,] nowaPlansza = new String[plansza.GetUpperBound(0) + 1, plansza.GetUpperBound(1) + 1];
            for (int p = 0; p < twory[l1 - 1].Count; p++)
            {
                int k = 0;
                for (int i = 0; i < wysokosc; i++)
                {
                    for (int j = 0; j < szerokosc; j++)
                    {
                        if (uklad[i, j] == 1)
                        {
                            nowaPlansza[i, j] = "a" + twory[l1 - 1][p][k];
                            k++;
                        }
                        else
                            nowaPlansza[i, j] = plansza[i, j];
                    }
                }

                jakichkolwiek++;

                if (sprawdzator.sprawdz(nowaPlansza, uklad)) {

                    dobrych++;
                    int wynik = kalkulator.oblicz(nowaPlansza, uklad, poziomo);
                    if(wynik > najlepszyWynik)
                    {
                        najlepszyWynik = wynik;
                        najlepszaPlansza = nowaPlansza;


                        Console.WriteLine("Wynik: " + najlepszyWynik);

                        /////WYPISYWANIE
                        for (int x = 0; x < wysokosc; x++)
                        {
                            for (int y = 0; y < szerokosc; y++)
                            {
                                if (najlepszaPlansza[x, y][0] == 'a')
                                {
                                    if (this.uklad[x, y] == 1)
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    else
                                        Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.Write(najlepszaPlansza[x, y][1] + " ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write(". ");
                                }
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("\n");

                    }
                }

            }
        }

        private bool sprawdzPrzystawanie()
        {
            int wysokosc = plansza.GetUpperBound(0) + 1;
            int szerokosc = plansza.GetUpperBound(1) + 1;

            for (int i = 0; i < wysokosc; i++)
            {
                for (int j = 0; j < szerokosc; j++)
                {
                    if (uklad[i, j] == 1)
                    {
                        if (i >= 1)
                            if (plansza[i - 1, j][0] != 'p')
                                return true;
                        if (j >= 1)
                            if (plansza[i, j - 1][0] != 'p')
                                return true;
                        if (i < 14)
                            if (plansza[i + 1, j][0] != 'p')
                                return true;
                        if (j < 14)
                            if (plansza[i, j + 1][0] != 'p')
                                return true;
                    }
                }
            }
            return false;
        }

    }
}
