using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class ACS_Prediction : IPrediction
    {
        private Dictionary<int, Dictionary<int, double>> dict;
        private Dictionary<int, Dictionary<int, double>> sim;

        private double curMin = double.MaxValue;
        private double curMax = 0;

        public ACS_Prediction(Dictionary<int, Dictionary<int, double>> _dict, Dictionary<int, Dictionary<int, double>> simList)
        {
            dict = _dict;
            sim = simList;
        }

        public void PredictRating(int userId)
        {
            Norm norm = new Norm();

            Dictionary<int, double> items = dict[userId];

            foreach (KeyValuePair<int, Dictionary<int, double>> s in sim)
            {
                // if our user doesn't have this item we calculate the prediction
                if (!items.ContainsKey(s.Key))
                {
                    double p1 = 0;
                    double p2 = 0;
                    GetMaxMin(items);

                    foreach (KeyValuePair<int, double> i in s.Value)
                    {
                        p1 += (norm.Normalize(items[i.Key], curMin, curMax) * i.Value);
                        if (i.Value < 0) p2 += -i.Value;
                        else p2 += i.Value;
                    }
                    double prediction = norm.Denormalize((p1 / p2), curMin, curMax);
                    Console.WriteLine("The prediction of user: " + userId + ", item: " + s.Key + " == " + prediction);
                }
            }
        }

        private void GetMaxMin(Dictionary<int, double> list)
        {
            curMin = double.MaxValue;
            curMax = 0;
            foreach(KeyValuePair<int, double> item in list)
            {
                if (item.Value < curMin) curMin = item.Value;
                if (item.Value > curMax) curMax = item.Value;
            }
        }
    }
}
