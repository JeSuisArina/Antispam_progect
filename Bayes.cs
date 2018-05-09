using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antispam
{
    class Bayes
    {
        public double CBayes (List<double> probability)
        {
            double p = 0; double  up = 1, down1 = 1, down2 = 1;
            for (int a = 0; a < probability.Count; a++)
            {
                if (probability.Count > 1)
                {
                    up *= probability[a];
                    down1 *= probability[a];
                    down2 *= (100 - probability[a]);
                    p = p + Math.Abs(up / (down1 - down2));
                }
                else p = probability[0];
            }
            return p;
        }
    }
}
