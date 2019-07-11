using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Cousine : IDistance
    {
        public double Calculate(double[] p, double[] q)
        {
            double val = 0;
            double sumP = 0;
            double sumQ = 0;

            for (int i = 0; i < p.Length; i++)
            {
                val += p[i] * q[i];
                sumP += p[i] * p[i];
                sumQ += q[i] * q[i];
            }

            double dis = val / (Math.Sqrt(sumP) * Math.Sqrt(sumQ));

            return dis;
        }
    }
}
