using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Configurator
    {
        public List<List<string>> configurations = new List<List<string>>();

        public Configurator(string letters)
        {
            for (int i = 0; i < letters.Length; i++)
                configurations.Add(new List<string>());
            generate("", letters);
        }

        public void generate(string word, string letters)
        {
            int n = letters.Length;

            for(int i = 0; i < n; i++)
            {
                string newWord = word + letters[i];
                configurations[newWord.Length - 1].Add(newWord);
                string newLetters = copyWithout(letters, i);
                if (newLetters.Length > 0)
                    generate(newWord, newLetters);
            }
        }

        string copyWithout(string word, int letter)
        {
            string newWord = "";
            if (letter > 0)
                newWord += word.Substring(0, letter);
            if(letter<word.Length-1)
                newWord += word.Substring(letter+1);
            return newWord;
        }



    }
}
