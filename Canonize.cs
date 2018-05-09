using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2TechCloud
{
    class Canonize
    {
        public List<string> canonize(string s)
        {
            char[] separate_symbols = { ' ', ',', '.', ')', '(', ':', ';', '"', '!', '?', '/', '\r', '\n' };

            List<string> stop_words = new List<string> { "В", "это", "как", "так", "и", "над", "к", "до", "не", "на", "но", "за", "то", "с", "ли", "а", "во", "от", "со", "для", "о", "же", "ну", "вы", "бы", "что", "кто", "он", "она", "" };

            List<string> listW = new List<string>();

            listW = s.Split(separate_symbols).ToList<string>();

            foreach (var item in stop_words)
            {
                if (listW.Contains(item))
                    listW.RemoveAll(obj => obj == item);
            }

            return listW;
        }

        public  int LevenshteinDistance(string string1, string string2)
        {
            int m = string1.Length;
            int n = string2.Length;
            int count;
            int[,] d = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
                d[i, 0] = i;
            for (int j = 0; j <= n; j++)
                d[0, j] = j;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (string2[j - 1] == string1[i - 1])
                        count = 0;
                    else
                        count = 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + count);
                }
            }
            return d[m, n];
        }
    }
}
