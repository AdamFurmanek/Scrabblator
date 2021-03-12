using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Scrabblator
{
    class Scrabblator
    {
        Trie slownik;

        public string[,] bonusyMapy;
        public Dictionary<char, int[]> bonusyLiter;
        public List<int> bonusyDoku;
        public List<string> dok;
        public string[,] plansza;


        public bool kontynuuj;


        public Scrabblator(string path)
        {
            slownik = new Trie(path);
        }

        public void ustaw(string[,] bM, Dictionary<char, int[]> bL, List<int> bD)
        {
            bonusyMapy = bM;
            bonusyLiter = bL;
            bonusyDoku = bD;
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
            new Generator(bonusyMapy, bonusyLiter, bonusyDoku, plansza, dok, twory, slownik, this).generuj();
            Console.WriteLine("Scrabblowanie zakończone. Naciśnij Enter aby kontynuować...");
        }

        public void zatrzymaj()
        {
            kontynuuj = false;
        }

        static void Main(string[] args)
        {
            new Configuration("siemka");
            /*
            Dictionary dictionary = new Dictionary();

            StreamReader streamReader = new StreamReader(new FileStream(@"..\..\..\Resources\Slownik.txt", FileMode.Open, FileAccess.Read));
            String word;
            while ((word = streamReader.ReadLine()) != null)
            {
                dictionary.Insert(word);
            }
            streamReader.Close();
            */

            /*
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

            List<int> bonusyDoku = new List<int>();
            bonusyDoku.Add(0);
            bonusyDoku.Add(0);
            bonusyDoku.Add(0);
            bonusyDoku.Add(0);
            bonusyDoku.Add(0);
            bonusyDoku.Add(0);
            bonusyDoku.Add(50);

            List<string> dok;
            string[,] plansza;

            Console.WriteLine("Wczytuję słownik...");
            Scrabblator scrabblator = new Scrabblator(@"..\..\..\Resources\Slownik.txt");
            scrabblator.ustaw(bonusyMapy, bonusyLiter, bonusyDoku);
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Naciśnij Enter aby rozpocząć scrabblowanie...\nPamiętaj, że arkusz Excela musi być zapisany przed rozpoczęciem.\nScrabblowanie można przerwać w dowolnym momencie naciskając Enter.");
                Console.ReadLine();

                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"..\..\..\Resources\Scrabblator.xlsx")))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;

                    dok = new List<string>();
                    var rowDok = myWorksheet.Cells[1, 1, 1, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();
                    foreach (var cell in rowDok)
                    {
                        if (!String.IsNullOrWhiteSpace(cell))
                            dok.Add("a" + cell);
                    }

                    plansza = new string[totalRows - 1, totalColumns];
                    for (int rowNum = 2; rowNum <= totalRows; rowNum++) //select starting row here
                    {
                        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();
                        for (int i = 0; i < row.Count; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(row[i]))
                                plansza[rowNum - 2, i] = "a" + row[i];
                            else
                                plansza[rowNum - 2, i] = "pb";
                        }
                    }
                }

                scrabblator.scrabbluj(plansza, dok);
                Console.ReadLine();
                scrabblator.zatrzymaj();
            }

            */
        }

    }
}