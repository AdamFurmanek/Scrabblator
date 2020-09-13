using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Kalkulator
    {

        string[,] bonusyMapy;
        Dictionary<char, int[]> bonusyLiter;
        String[,] nowaPlansza;
        int[,] uklad;

        int wynik, wynikKomorki, mnoznik;

        public Kalkulator(string[,] bm, Dictionary<char, int[]> bl)
        {
            bonusyMapy = bm;
            bonusyLiter = bl;
        }
        public int oblicz(String[,] nowaPlansza, int[,] uklad, bool poziomo)
        {
            this.nowaPlansza = nowaPlansza;
            this.uklad = uklad;
            wynik = 0;
            bool pierwszaKomorka = true;
            for (int i = 0; i < uklad.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < uklad.GetUpperBound(1) + 1; j++)
                {
                    if (uklad[i, j] == 1)
                    {
                        if (pierwszaKomorka) {
                            wynik += obliczKomorkePoziomo(i, j);
                            wynik += obliczKomorkePionowo(i, j);
                            pierwszaKomorka = false;
                        }
                        else
                        {
                            if(poziomo)
                                wynik += obliczKomorkePionowo(i, j);
                            else
                                wynik += obliczKomorkePoziomo(i, j);
                        }
                    }
                }
            }

            return wynik;
        }

        public void obliczBonus(int i, int j)
        {
            if (bonusyMapy[i, j][1] == '0') //Jeśli typ pola jest równy 0 (mnożenie całego wyrazu)
            {
                mnoznik = mnoznik * (int)char.GetNumericValue(bonusyMapy[i, j][0]); //Mnożnik jest pomnożony o mnożnik pola
                wynikKomorki += bonusyLiter[nowaPlansza[i, j][1]][1]; //Do wyniku dodana zostaje wartość litery
            }
            else //Jeśli typ pola jest określony (mnożenie pojedynczej litery)
            {
                if ((int)char.GetNumericValue(bonusyMapy[i, j][1]) == bonusyLiter[nowaPlansza[i, j][1]][0]) //Sprawdza czy typ pola jest taki sam jak typ litery
                {
                    wynikKomorki += bonusyLiter[nowaPlansza[i, j][1]][1] * (int)char.GetNumericValue(bonusyMapy[i, j][0]); //Do wyniku zostaje dodana wartość litery pomnożona przez mnożnik pola
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[i, j][1]][1];
            }
        }

        public int obliczKomorkePoziomo(int i, int j)
        {
            wynikKomorki = 0;
            mnoznik = 1;
            int k = j;
            while (k > 0 && nowaPlansza[i, k - 1][0] != 'p')
            {
                k--;
                if(uklad[i, k] == 1 && bonusyMapy[i, k][0] != '0')
                {
                    obliczBonus(i, k);
                }
                else
                wynikKomorki += bonusyLiter[nowaPlansza[i, k][1]][1];
            }
            k = j;
            while (k < 14 && nowaPlansza[i, k + 1][0] != 'p')
            {
                k++;
                if (uklad[i, k] == 1 && bonusyMapy[i, k][0] != '0')
                {
                    obliczBonus(i, k);
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[i, k][1]][1];
            }
            string napis = nowaPlansza[i, j][1] + "";
            k = j;
            while (k > 0 && nowaPlansza[i, k - 1][0] != 'p')
            {
                k--;
                napis= nowaPlansza[i, k][1] + napis;
            }
            k = j;
            while (k < 14 && nowaPlansza[i, k + 1][0] != 'p')
            {
                k++;
                napis += nowaPlansza[i, k][1];
            }
            if (napis.Length>1) //JEŚLI TO NIE JEST JEDYNA LITERKA W TE STRONE
            {
                if (bonusyMapy[i, j][0] != '0')
                {
                    obliczBonus(i, j);
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[i, j][1]][1];
            }
            return wynikKomorki * mnoznik;
        }
        public int obliczKomorkePionowo(int i, int j)
        {
            wynikKomorki = 0;
            mnoznik = 1;
            int k = i;
            while (k > 0 && nowaPlansza[k - 1, j][0] != 'p')
            {
                k--;
                if (uklad[k, j] == 1 && bonusyMapy[k, j][0] != '0')
                {
                    obliczBonus(k, j);
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[k, j][1]][1];
            }
            k = i;
            while (k < 14 && nowaPlansza[k + 1, j][0] != 'p')
            {
                k++;
                if (uklad[k, j] == 1 && bonusyMapy[k, j][0] != '0')
                {
                    obliczBonus(k, j);
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[k, j][1]][1];
            }
            //OBLICZENIE DŁUGOŚCI NAPISU
            string napis = nowaPlansza[i, j][1] + "";
            k = i;
            while (k > 0 && nowaPlansza[k - 1, j][0] != 'p')
            {
                k--;
                napis = nowaPlansza[k, j][1] + napis;
            }
            k = i;
            while (k < 14 && nowaPlansza[k + 1, j][0] != 'p')
            {
                k++;
                napis += nowaPlansza[k, j][1];
            }
            if (napis.Length>1) //JEŚLI TO NIE JEST JEDYNA LITERKA W TE STRONE
            {
                if (bonusyMapy[i, j][0] != '0')
                {
                    obliczBonus(i, j);
                }
                else
                    wynikKomorki += bonusyLiter[nowaPlansza[i, j][1]][1];
            }

            return wynikKomorki * mnoznik;
        }

    }
}
