using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Norm
    {
        public double Normalize(double rating, double ratingMin, double ratingMax)
        {
            double norm = 2 * ((rating - ratingMin) / (ratingMax - ratingMin)) - 1;

            return norm;
        }

        public double Denormalize(double normRating, double ratingMin, double ratingMax)
        {
            double deNorm = ((normRating + 1) / 2) * (ratingMax - ratingMin) + ratingMin;

            return deNorm;
        }
    }
}
