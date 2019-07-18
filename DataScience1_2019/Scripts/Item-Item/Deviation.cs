using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Deviation
    {
        private Dictionary<int, Dictionary<int, double>> _dict;

        public Deviation(Dictionary<int, Dictionary<int, double>> _dict)
        {
            this._dict = _dict;
        }

        public Dictionary<int, Dictionary<int, Tuple<double, double>>> CreateDevList(int userId)
        {

            // First we create a list for the deviations
            Dictionary<int, Dictionary<int, Tuple<double, double>>> devList = new Dictionary<int, Dictionary<int, Tuple<double, double>>>();

            foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {
                foreach (KeyValuePair<int, double> curArticle in item.Value)
                {
                    if (!devList.ContainsKey(curArticle.Key))
                    {
                        devList.Add((curArticle.Key), new Dictionary<int, Tuple<double, double>>());
                    }
                }
            }

            // now we calculate and add all the items to the list
            foreach (KeyValuePair<int, Dictionary<int, Tuple<double, double>>> x_item in devList)
            {
                foreach (KeyValuePair<int, Dictionary<int, Tuple<double, double>>> y_item in devList)
                {
                    if (!x_item.Value.ContainsKey(y_item.Key) && x_item.Key != y_item.Key)
                    {
                        Tuple<double, double> dev = CalculateDev(x_item.Key, y_item.Key);
                        devList[x_item.Key].Add(y_item.Key, dev);
                    }
                }
            }

            return devList;
        }

        // this tuple has 2 doubles the first is the deviation and the second the count of user who rated both items
        private Tuple<double, double> CalculateDev(int x_item, int y_item)
        {

            double p1 = 0;
            double count = 0;
            foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {
                // check if this uesr rated both item x and y
                if (item.Value.ContainsKey(x_item) && item.Value.ContainsKey(y_item))
                {
                    p1 += item.Value[x_item] - item.Value[y_item];
                    count += 1;
                }
            }

            double dev = p1 / count;
            return Tuple.Create(dev, count);
        }
    }
}
