using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class ACS
    {

        private Dictionary<int, Dictionary<int, double>> _dict;

        public ACS(Dictionary<int, Dictionary<int, double>> _dict)
        {
            this._dict = _dict;
        }
        public Dictionary<int, Dictionary<int, double>> Main(int userId, int itemId)
        {
            // Get the average rating for all of the users
            Dictionary<int, double> userAverageRating = new Dictionary<int, double>();

            foreach(KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {

                int countRatedItems = item.Value.Count;
                double sumRating = 0;
                foreach (KeyValuePair<int, double> curArticle in item.Value)
                {
                    sumRating += curArticle.Value;
                }

                double averageRating = sumRating / countRatedItems;

                userAverageRating.Add(item.Key, averageRating);
            }

            // First we create a list for the simularities
            Dictionary<int, Dictionary<int, double>> simList = new Dictionary<int, Dictionary<int, double>>();

            foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {
                foreach (KeyValuePair<int, double> curArticle in item.Value)
                {
                    if (!_dict[userId].ContainsKey(curArticle.Key)) break;
                    if (!simList.ContainsKey(curArticle.Key))
                    {
                        simList.Add((curArticle.Key), new Dictionary<int, double>());
                    }
                }
            }
            if (!simList.ContainsKey(itemId)) simList.Add((itemId), new Dictionary<int, double>());

            foreach (KeyValuePair<int, Dictionary<int, double>> x_item in simList)
            {
                foreach (KeyValuePair<int, Dictionary<int, double>> y_item in simList)
                {
                    if (!x_item.Value.ContainsKey(y_item.Key) && x_item.Key != y_item.Key && !simList[y_item.Key].ContainsKey(x_item.Key))
                    {
                        double sim = Simularity(x_item.Key, y_item.Key, userAverageRating);
                        simList[x_item.Key].Add(y_item.Key, sim);
                    }
                }
            }

            //foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            //{
            //    int x_item = -99;
            //    int y_item = -99;
            //    foreach (KeyValuePair<int, double> curArticle in item.Value)
            //    {

            //        if (x_item == -99)
            //        {
            //            x_item = curArticle.Key;
            //            break;
            //        }
            //        else
            //        {
            //            if (simList[x_item].ContainsKey(y_item))
            //            {
            //                x_item = y_item;
            //                break;
            //            } else y_item = curArticle.Key;
            //        }

            //        double sim = Simularity(x_item, y_item, userAverageRating);

            //        if (!simList.ContainsKey(x_item))
            //        {
            //            simList.Add((x_item), new Dictionary<int, double>());
            //        }
            //        simList[x_item].Add(y_item, sim);
            //    }
            //}
            return simList;
        }

        private double Simularity(int x_item, int y_item, Dictionary<int, double> averageRating)
        {
            double p1 = 0;
            double p2 = 0;
            double p3 = 0;
            foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {
                // check if this uesr rated both item x and y
                if (item.Value.ContainsKey(x_item) && item.Value.ContainsKey(y_item))
                {

                    p1 += (item.Value[x_item] - averageRating[item.Key]) * (item.Value[y_item] - averageRating[item.Key]);

                    p2 += ((item.Value[x_item] - averageRating[item.Key]) * (item.Value[x_item] - averageRating[item.Key]));
                    p3 += ((item.Value[y_item] - averageRating[item.Key]) * (item.Value[y_item] - averageRating[item.Key]));
                }
            }

            double sim = p1 / (Math.Sqrt(p2) * Math.Sqrt(p3));

            return sim;
        }

    }
}
