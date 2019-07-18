using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class Item_Item_Main
    {

        private Dictionary<int, Dictionary<int, double>> _dict;

        public void Main()
        {
            // get the dictionary of data from the .txt file
            ReadFile readFile = new ReadFile();
            readFile.Main(false);
            _dict = readFile.dict;

            String alg = "0";
            while (alg != "acs" && alg != "dev")
            {
                Console.WriteLine("whould you like to use [acs] or [dev]");
                alg = Console.ReadLine();
            }

            String userId = "-99";
            while (!_dict.ContainsKey(int.Parse(userId)))
            {
                Console.WriteLine("which user are we predicting (make sure it exists)");
                userId = Console.ReadLine();
            }

            //String itemId = "-99";
            //while (itemId == "-99" || _dict.ContainsKey(int.Parse(itemId)))
            //{
            //    Console.WriteLine("which item are we predicting (make sure it doesn't exists)");
            //    itemId = Console.ReadLine();
            //}

            if (_dict.Count >= 1000) RemoveUselessItems(int.Parse(userId));

            // The Factory design pattern is used here
            switch (alg)
            {
                case "acs":
                    ACS(int.Parse(userId));
                    break;
                case "dev":
                    Dev(int.Parse(userId));
                    break;
                default:
                    break;
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private void RemoveUselessItems(int userId)
        {
            // creating temp list of items we are going to delete
            List<int> removeList = new List<int>();

            Dictionary<int, double> userItems = _dict[userId];

            // removing users we can't use in our prediction
            // we need at least 1 extra ratings from the other user
            foreach (KeyValuePair<int, Dictionary<int, double>> y_item in _dict)
            {
                int sim_items = 0;
                foreach (KeyValuePair<int, double> x_item in userItems)
                {
                    // check if user Y has at least 3 similar items if not we remove this user
                    if (sim_items >= 2) break;
                    if (y_item.Key == x_item.Key) sim_items += 1;
                }
                if (!(sim_items >= 2) && y_item.Value.Count <= userItems.Count + 1) removeList.Add(y_item.Key);
            }

            foreach (int key in removeList)
            {
                if (key != userId) _dict.Remove(key);
            }

        }

        private void ACS(int userId)
        {
            ACS acs = new ACS(_dict);
            Dictionary<int, Dictionary<int, double>>  sim = acs.CreateSimList(userId);

            IPrediction pred = new ACS_Prediction(_dict, sim);
            pred.PredictRating(userId);
        }

        private void Dev(int userId)
        {
            Deviation dev = new Deviation(_dict);
            Dictionary<int, Dictionary<int, Tuple<double, double>>> d = dev.CreateDevList(userId);

            IPrediction pred = new DeviationPrediction(_dict, d);
            pred.PredictRating(userId);
        }
    }
}
