using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class DeviationPrediction : IPrediction
    {

        private Dictionary<int, Dictionary<int, double>> dict;
        private Dictionary<int, Dictionary<int, Tuple<double, double>>> dev;

        public DeviationPrediction(Dictionary<int, Dictionary<int, double>> _dict, Dictionary<int, Dictionary<int, Tuple<double, double>>> devList)
        {
            dict = _dict;
            dev = devList;
        }
        public void PredictRating(int userId)
        {
            Dictionary<int, double> items = dict[userId];

            foreach (KeyValuePair<int, Dictionary<int, Tuple<double, double>>> d in dev)
            {
                if (!items.ContainsKey(d.Key))
                {
                    double p1 = 0;
                    double p2 = 0;
                    foreach (KeyValuePair<int, Tuple<double, double>> product in d.Value)
                    {
                        p1 += (items[product.Key] + product.Value.Item1) * product.Value.Item2;
                        p2 += product.Value.Item2;
                    }
                    double prediction = p1 / p2;
                    Console.WriteLine($"The prediction of user: {userId}, itemId = {d.Key}, prediction = {prediction}");
                }
            }
        }
    }
}
