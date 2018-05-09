using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;


namespace Lab2Tech
{
    class Shingles
    {
        private string words;
        private List<string> shingles;
        private StringBuilder sBuilder;
        private  StringBuilder sBuilder1;

        public void ABC()
        {
            shingles = new List<string>();
        }

        public string Canonize(string Data)
        {
            words = "";
            string[] split = Data.Split( new Char[] { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '-', '\n' }); 

                foreach (string s in split)
                {
                    if (s.Trim() != "")
                        words += s.ToLower() + " ";

                words = words.Replace(" это ", " как ").Replace(" как ", " так ").Replace(" так ", " и ").
                Replace(" и ", " в ").Replace(" в ", " над ").Replace(" над ", " к ").Replace(" к ", " до ").
                Replace(" до ", " не ").Replace(" не ", " на ").Replace(" на ", " но ").Replace(" но ", " за ").
                Replace(" за ", " то ").Replace(" то ", " с ").Replace(" с ", " ли ").Replace(" ли ", " а ").
                Replace(" а ", " во ").Replace(" во ", " от ").Replace(" от ", " со ").Replace(" со ", " для ").Replace(" для ", " о ").
                Replace(" о ", " же ").Replace(" же ", " ну ").Replace(" ну ", " вы ").Replace(" вы ", " бы ").Replace(" бы ", " что ").
                Replace(" что ", " кто ").Replace(" кто ", " он ").Replace(" он ", " она ").Replace(" она ", " ");                                                   
                }
            return words;
        }

        public StringBuilder Shingle(string Data)
        {
            Canonize(Data);
            sBuilder = new StringBuilder();
            sBuilder1 = new StringBuilder();

            string shingle = "";
            string hash = "";
            int len = 5;
            string[] split = words.Split(new Char[] { ' ' });
                       
                for (int i = 0; i < split.Length - len; i++)
                {
                    if (i > 0)
                    shingle = "\n";
                    for (int j = i; j < i + len; j++)
                    {
                        shingle = shingle + split[j] + " ";
                    }
                    sBuilder.Append(shingle);
                }

            string[] splitagain = sBuilder.ToString().Split(new Char[] { '\n' });
            foreach (string sa in splitagain)
            {                
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = "\n";
                    hash += MD5hash(md5Hash, sa);
                }
                sBuilder1.Append(hash);
            }
            return sBuilder1;           
        }

        static string MD5hash (MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
