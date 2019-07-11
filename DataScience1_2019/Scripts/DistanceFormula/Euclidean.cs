using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Euclidean : IDistance
    {
        public double Calculate(double[] p, double[] q)
        {
            double val = 0;

            for (int i = 0; i < p.Length; i++)
            {
                val += (p[i] - q[i]) * (p[i] - q[i]);
            }

            double dis = Math.Sqrt(val);
            double sim = dis;
            sim = 1 / (1 + dis);

            return sim;
        }
    }
}
