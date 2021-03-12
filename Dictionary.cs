using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabblator
{
    class Dictionary
    {
        Dictionary<char, Dictionary> childrens = new Dictionary<char, Dictionary>();
        bool lastLetter = false;

        public void Insert(string word)
        {
            Dictionary node;
            if (!childrens.TryGetValue(word[0], out node))
            {
                node = new Dictionary();
                childrens.Add(word[0], node);
            }
            if (word.Length > 1)
                node.Insert(word.Substring(1));
            else
                lastLetter = true;
        }

        public bool Find(string word)
        {
            Dictionary node;
            if (childrens.TryGetValue(word[0], out node))
            {
                if (word.Length > 1)
                    return node.Find(word.Substring(1));
                else if (lastLetter)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
