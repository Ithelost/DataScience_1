using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience1_2019.Scripts
{
    class User_Item_Main
    {

        private Dictionary<int, Dictionary<int, double>> _dict;
        private Dictionary<int, double> curUserItems;

        public void Main()
        {
            // get the dictionary of data from the .txt file
            ReadFile readFile = new ReadFile();
            readFile.Main(true);
            _dict = readFile.dict;

            String userId = "-99";
            while (!_dict.ContainsKey(Int32.Parse(userId)))
            {
                Console.WriteLine("choose a user to use");
                userId = Console.ReadLine();
            }

            curUserItems = _dict[Int32.Parse(userId)];

            // TODO change so the dict doesn't have a million items
            RemoveUselessItems(curUserItems, Int32.Parse(userId));

            // The Factory design pattern is used here
            //IDistance d = new Euclidean();
            IDistance d = new Pearson();
            //IDistance d = new Cousine();

            Dictionary<int, double> dis = GetDistance(d);
            
            // this is only if we use the pearson
            if (d.GetType() == typeof(Pearson))
            {
                NearestNeighborg neighborg = new NearestNeighborg(dis);
                Dictionary<int, double> nn = neighborg.Main();
                Console.WriteLine("nearestNeighborg:");
                Ranking(nn);

                PredictRating(nn);
            }
           
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private void RemoveUselessItems(Dictionary<int, double> userItems, int userId)
        {
            // creating temp list of items we are going to delete
            List<int> removeList = new List<int>();


            if (_dict.ContainsKey(userId)) _dict.Remove(userId);

            // removing user we can't use in our prediction
            // we need at least 1 extra ratings from the other user
            foreach (KeyValuePair<int, Dictionary<int, double>> y_item in _dict)
            {
                int sim_items = 0;
                foreach (KeyValuePair<int, double> x_item in userItems)
                {
                    // check if user Y has at least 3 similar items if not we remove this user
                    if (sim_items >= 3) break;
                    if (y_item.Key == x_item.Key) sim_items += 1;
                }
                if (!(sim_items >= 3) && y_item.Value.Count <= userItems.Count + 1) removeList.Add(y_item.Key);
            }

            foreach (int key in removeList)
            {
                _dict.Remove(key);
            }
        }

        private void PredictRating(Dictionary<int, double> nn)
        {
            PredictedRating predictedRating = new PredictedRating();
            
            // here we are going to predict the rating
            List<int> toPredict = new List<int>();

            // get all the items our user didn't rate
            foreach (KeyValuePair<int, double> user in nn)
            {
                foreach (KeyValuePair<int, double> item in _dict[user.Key])
                {
                    if (!curUserItems.ContainsKey(item.Key) && !toPredict.Contains(item.Key)) toPredict.Add(item.Key);
                }
            }
            for (int i = 0; i < toPredict.Count; i++)
            {
                Dictionary<int, double> ratings = GetRatingOfNN(toPredict[i], nn);
                predictedRating.Main(ratings, nn, toPredict[i]);
            }
            
        }

        private Dictionary<int, double> GetRatingOfNN(int toPredict, Dictionary<int, double> nn)
        {
            // creating a dictionary with userId and rating of the product
            Dictionary<int, double> ratings = new Dictionary<int, double>();

            // get the rating of the item from the large dictionary
            foreach (KeyValuePair<int, double> user in nn)
            {
                if (_dict[user.Key].ContainsKey(toPredict)) ratings.Add(user.Key, _dict[user.Key][toPredict]);
            }
            return ratings;
        }

        private Dictionary<int, double> GetDistance(IDistance distance)
        {
            // userId with simularity
            Dictionary<int, double> userDis = new Dictionary<int, double>();

            // Getting other user with the same items rated
            foreach (KeyValuePair<int, Dictionary<int, double>> item in _dict)
            {
                int curUser = item.Key;

                List<double> p = new List<double>();
                List<double> q = new List<double>();
                foreach (KeyValuePair<int, double> curArticle in item.Value)
                {

                    if (curUserItems.ContainsKey(curArticle.Key))
                    {
                        p.Add(curUserItems[curArticle.Key]);
                        q.Add(curArticle.Value);
                    }
                }
                // calculate the simularity with euclidean
                double dis = distance.Calculate(p.ToArray(), q.ToArray());

                userDis.Add(item.Key, dis);
            }
            Console.WriteLine($"Interface = {distance.GetType()} Distance of items:");
            Ranking(userDis);
            return userDis;
        }

        private void Ranking(Dictionary<int, double> dict)
        {
            // creating a list beacuse we can sort it pretty easy
            List<KeyValuePair<int, double>> rankinglist = new List<KeyValuePair<int, double>>();

            foreach (KeyValuePair<int, double> item in dict)
            {
                rankinglist.Add(item);
            }

            rankinglist.Sort((x, y) => y.Value.CompareTo(x.Value));

            foreach (KeyValuePair<int, double> item in rankinglist)
            {
                Console.WriteLine("Key: {0}, Vaule {1}", item.Key, item.Value);
            }
        }
    }
}
