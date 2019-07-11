using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Pearson : IDistance
    {
        public double Calculate(double[] p, double[] q)
        {
            double p1 = 0;
            double p2 = 0;
            double p3 = 0;
            double p4 = 0;
            double p5 = 0;
            double p6 = 0;

            double sumP = 0;
            double sumQ = 0;
            for (int i = 0; i < p.Length; i++)
            {
                p1 += p[i] * q[i];
                sumP += p[i];
                sumQ += q[i];

                p3 += p[i] * p[i];
                p5 += q[i] * q[i];
            }
            p2 = (sumP * sumQ) / p.Length;
            p4 = sumP * sumP / p.Length;
            p6 = sumQ * sumQ / p.Length;

            double dis = (p1 - p2) / (Math.Sqrt(p3 - p4) * Math.Sqrt(p5 - p6));

            return dis;
        }
    }
}
