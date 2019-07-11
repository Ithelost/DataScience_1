using System;
using System.Collections.Generic;

namespace DataScience1_2019.Scripts
{
    class Program
    {

        static void Main(string[] args)
        {
            // ask the user what we are going to do
            String excercise = "-99";

            while (excercise != "UserItem" && excercise != "ItemItem")
            {
                Console.WriteLine("choose a excercise [UserItem] or [ItemItem]");
                excercise = Console.ReadLine();
            }

            switch (excercise)
            {
                case "UserItem":
                    User_Item_Main userItem = new User_Item_Main();
                    userItem.Main();
                    break;
                case "ItemItem":
                    Item_Item_Main ItemItem = new Item_Item_Main();
                    ItemItem.Main();
                    break;
                default:
                    break;
            }
        }
    }
}
