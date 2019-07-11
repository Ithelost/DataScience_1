using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class DeviationPrediction
    {

        private Dictionary<int, Dictionary<int, double>> _dict;
        private Dictionary<int, Dictionary<int, Tuple<double, double>>> _dev;

        public DeviationPrediction(Dictionary<int, Dictionary<int, double>> _dict, Dictionary<int, Dictionary<int, Tuple<double, double>>> devList)
        {
            this._dict = _dict;
            this._dev = devList;
        }

        public void Main(int userId, int itemId)
        {
            PredictRating(userId, itemId);
        }

        private void PredictRating(int userId, int itemId)
        {
            Dictionary<int, double> items = _dict[userId];

            double p1 = 0;
            double p2 = 0;

            foreach (KeyValuePair<int, double> curArticle in items)
            {
                if (curArticle.Key != itemId)
                {
                    Tuple<double, double> dev;

                    if (_dev[itemId].ContainsKey(curArticle.Key))
                    {
                        dev = _dev[itemId][curArticle.Key];
                    }
                    else
                    {
                        dev = _dev[curArticle.Key][itemId];
                    }

                    p1 += (curArticle.Value - dev.Item1) * dev.Item2;
                    p2 += dev.Item2;
                }
            }

            double prediction = p1 / p2;

            Console.WriteLine("The prediction of user: " + userId + ", item: " + itemId + " == " + prediction);
        }
    }
}
