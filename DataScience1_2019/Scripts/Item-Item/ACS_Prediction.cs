using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class ACS_Prediction
    {
        private Dictionary<int, Dictionary<int, double>> _dict;
        private Dictionary<int, Dictionary<int, double>> _sim;

        public ACS_Prediction(Dictionary<int, Dictionary<int, double>> _dict, Dictionary<int, Dictionary<int, double>> simList)
        {
            this._dict = _dict;
            this._sim = simList;
        }

        public void Main(int userId, int itemId)
        {
            PredictRating(userId, itemId);
        }

        private void PredictRating(int userId, int itemId)
        {
            Norm norm = new Norm();

            double p1 = 0;
            double p2 = 0;

            Dictionary<int, double> items = _dict[userId];

            foreach (KeyValuePair<int, double> curArticle in items)
            {
                if (curArticle.Key != itemId)
                {
                    if (_sim[itemId].ContainsKey(curArticle.Key))
                    {
                        p1 += (norm.Normalize(curArticle.Value, 1, 5) * _sim[itemId][curArticle.Key]);
                        p2 += _sim[itemId][curArticle.Key];
                    }
                    else
                    {
                        p1 += (norm.Normalize(curArticle.Value, 1, 5) * (-_sim[curArticle.Key][itemId]));
                        p2 += (-_sim[curArticle.Key][itemId]);
                    }

                }
                
            }

            double prediction = norm.Denormalize((p1 / p2), 1, 5);

            Console.WriteLine("The prediction of user: " + userId + ", item: " + itemId + " == " + prediction);
        }
    }
}
