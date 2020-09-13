using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Sprawdzator
    {
        Trie slownik;

        public Sprawdzator (Trie trie)
        {
            slownik = trie;
        }

		public bool sprawdz(String[,] nowaPlansza, int[,] uklad)
		{

			int ileOK = 0, wymaganeOK = 0;
			for(int i=0;i< uklad.GetUpperBound(0) + 1; i++) {
				for(int j=0;j< uklad.GetUpperBound(1) + 1; j++) {
					if(uklad[i,j]==1) {
						wymaganeOK++;
						if(sprawdzKomorke(i, j, nowaPlansza)) {
							ileOK++;
						}
					}		
					if (wymaganeOK != ileOK)
						break;
				}
				if (wymaganeOK != ileOK)
					break;
			}
			if (ileOK == wymaganeOK)
			{

                return true;
			}
			return false;
		}

		private bool sprawdzKomorke(int i, int j, String[,] nowaPlansza)
		{
			String napis, napis2;
			napis=nowaPlansza [i,j][1]+"";
			int k = i;
			while(k>0&&nowaPlansza[k - 1,j][0]!='p') {
				k--;
				napis=nowaPlansza[k,j][1]+napis;
			}
			k=i;
			while(k<14&&nowaPlansza[k + 1,j][0]!='p') {
				k++;
				napis+=nowaPlansza[k,j][1];
			}

			napis2 = nowaPlansza[i,j][1] + "";
			k = j;
			while (k > 0 && nowaPlansza[i,k - 1][0] != 'p')
			{
				k--;
				napis2 = nowaPlansza[i,k][1] + napis2;
			}
			k = j;
			while (k < 14 && nowaPlansza[i,k + 1][0] != 'p')
			{
				k++;
				napis2 += nowaPlansza[i,k][1];
			}
			if (sprawdzWyraz(napis) && sprawdzWyraz(napis2))
			{
				return true;
			}
			else
				return false;
		}
	
		private bool sprawdzWyraz(String line){
		
			if(line.Length==1)
				return true;
		
			if(slownik.Search(line)) {
				return true;
			}
			return false;
		}

    }
}
