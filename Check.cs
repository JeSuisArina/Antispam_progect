using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Lab2Tech
{
    class Check
    { 
        public double CheckHashes(string firsthash, string secondhash)
        {
            int count = 0;
            int count1 = 0;
            double percent = 0;
            string[] split1 = firsthash.Split(new Char[] { '\n' });
            string[] split2 = secondhash.Split(new Char[] { '\n' });

            count1 = split2.Count();

            foreach (string s2 in split2)
            {
                foreach (string s1 in split1)
                {
                    if (s2.Equals(s1))
                        count++;
                }
            }
            if ((Convert.ToDouble(count) / Convert.ToDouble(count1)) == 1)
            {
                percent = 100;
            }                
            else
            {
                percent = Math.Round(((Convert.ToDouble(count) / Convert.ToDouble(count1)) * 100.0), 2);
            }                
            return percent;
        }
    }
}
