using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataScience1_2019.Scripts
{
    class ReadFile
    {
        // the key in this dictionary is the userId where the value is another dictionary.
        // the second dictionary has a key of the article with the value of the rating.
        public Dictionary<int, Dictionary<int, double>> dict = new Dictionary<int, Dictionary<int, double>>();

        public void Main(bool isUserItem)
        {
            //Grouplens();
            SchoolData(isUserItem);

        }

        private void SchoolData(bool isUserItem)
        {
            // string text = System.IO.File.ReadAllText(@"C:\Users\jacob\Documents\Visual Studio 2017\Projects\DataScience1_2019\DataScience1_2019\resources\Lesson 1 USER_ITEM_les1.txt");
            // System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            string[] lines;
            if (isUserItem)
            {
                lines = System.IO.File.ReadAllLines(@"C:\Users\jacob\Documents\Visual Studio 2017\Projects\DataScience1_2019\DataScience1_2019\resources\Lesson 1 USER_ITEM_les1.txt");
            } else
            {
                //lines = System.IO.File.ReadAllLines(@"C:\Users\jacob\Documents\Visual Studio 2017\Projects\DataScience1_2019\DataScience1_2019\resources\ITEM_ITEM.txt");
                lines = System.IO.File.ReadAllLines(@"C:\Users\jacob\Documents\Visual Studio 2017\Projects\DataScience1_2019\DataScience1_2019\resources\DateDev.txt");

            }

            // populate the dictionary with the values given in the .txt file
            foreach (string line in lines)
            {
                AddDict(line);
            }
        }

        private void Grouplens()
        {
            var grouplens = new StreamReader(@"C:\Users\jacob\Documents\Visual Studio 2017\Projects\DataScience1_2019\DataScience1_2019\resources\ratings.csv");
            string headline = grouplens.ReadLine();

            while (!grouplens.EndOfStream)
            {
                var line = grouplens.ReadLine();

                // ignore first line of the csv file
                if (line != headline)
                {
                    AddDict(line);
                    var value = line.Split(',');
                }
                
            }
        }

        private void AddDict(String line)
        {
            String[] value = line.Split(',');

            int userId = Int32.Parse(value[0]);
            int articleId = Int32.Parse(value[1]);
            double rating = double.Parse(value[2], System.Globalization.CultureInfo.InvariantCulture);

            // if user excist within the dictionary add item to the user else create new item to dictionary
            if (!dict.ContainsKey(userId))
            {
                dict.Add((userId), new Dictionary<int, double>());

            }
            dict[userId].Add(articleId, rating);
        }

    }
}
