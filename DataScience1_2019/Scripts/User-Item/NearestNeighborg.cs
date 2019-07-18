using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{

    class NearestNeighborg
    {

        private Dictionary<int, double> _dict;

        public NearestNeighborg(Dictionary<int, double> _dict)
        {
            this._dict = _dict;
        }

        public Dictionary<int, double> Main()
        {
            Dictionary<int, double> nn = new Dictionary<int, double>();

            // Ask the user for nearest neigbors and drempelwaarde value
            //Console.WriteLine("How many nearest neighborgs would you like to have");
            //int nnCount = Int32.Parse(Console.ReadLine());

            //Console.WriteLine("choose the (drempelwaarde) use [,]");
            //double drempelwaarde = double.Parse(Console.ReadLine());

            double drempelwaarde = 0.8;
            int nnCount = 3;

            // remove the user with a lower drempelwaarde
            Dictionary<int, double> dict = new Dictionary<int, double>();

            foreach (KeyValuePair<int, double> item in _dict)
            {
                if (item.Value > drempelwaarde) dict.Add(item.Key, item.Value);
            }

            // create a nearest neighbor list
            while (nn.Count != nnCount)
            {
                // check if the value of this item is higher compared to the old one && ingnore item already in the nn list
                Tuple<int, double> n = GetNearestNeigborg(nn, dict);
                if (n.Item1 == -1) break;
                else dict.Remove(n.Item1);

                nn.Add(n.Item1, n.Item2);
            }

            return nn;
        }

        private Tuple<int, double> GetNearestNeigborg(Dictionary<int, double> nn, Dictionary<int, double> dict)
        {
            int userId = -1;
            double val = -1;
            foreach (KeyValuePair<int, double> item in dict)
            {
                if (item.Value > val && !nn.ContainsKey(item.Key))
                {
                    userId = item.Key;
                    val = item.Value;
                }
            }
            Tuple<int, double> n = new Tuple<int, double>(userId, val);
            return n;
        }
    }
    
}
