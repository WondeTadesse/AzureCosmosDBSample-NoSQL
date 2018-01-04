//|---------------------------------------------------------------|
//|                         AZURE MONGODB                         |
//|---------------------------------------------------------------|
//|                       Developed by Wonde Tadesse              |
//|                             Copyright ©2017 - Present         |
//|---------------------------------------------------------------|
//|                         AZURE MONGODB                         |
//|---------------------------------------------------------------|

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureMongoDBSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            new MongoDBPeopleCollectionProcessor().ProcessPeopleCollection();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}










