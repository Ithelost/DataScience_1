using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts

{
    interface IDistance
    {
        double Calculate(double[] p, double[] q);
    }
}
