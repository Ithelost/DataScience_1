using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class PredictedRating
    {
        public void Main(Dictionary<int, double> rating, Dictionary<int, double> nearestNeighborg, int ratedItem)
        {
            double pred_p1 = 0;
            double sumCo = 0;

            foreach (KeyValuePair<int, double> item in rating)
            {
                foreach (KeyValuePair<int, double> nn in nearestNeighborg)
                {
                    if (item.Key == nn.Key)
                    {
                        pred_p1 += (item.Value * nn.Value);
                        sumCo += nn.Value;
                    }
                }
            }
            double prediction = pred_p1 / sumCo;

            Console.Write("the predition of " + ratedItem + " = " + prediction);
        }
    }
}
