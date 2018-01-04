//|---------------------------------------------------------------|
//|                         AZURE MONGODB                         |
//|---------------------------------------------------------------|
//|                       Developed by Wonde Tadesse              |
//|                             Copyright ©2018 - Present         |
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

using MongoDB.Bson;
using MongoDB.Driver;

using AzureMongoDBSample.Entities;

namespace AzureMongoDBSample
{
    /// <summary>
    /// MongoDB People collection processor class
    /// </summary>
    public class MongoDBPeopleCollectionProcessor
    {
        #region Public Methods 

        /// <summary>
        /// Process People MongoDB collection
        /// </summary>
        public void ProcessPeopleCollection()
        {
            IMongoCollection<Person> people = InitializePeopleCollection();
            if (people != null)
            {
                ListAllPeople(people);
                Add2Persons(people);
                ListByOccupation(people, "Software Engineer");
                ListByOccupation(people, "Network Engineer");
                ListAllPeople(people);
            }
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Initialize People collection
        /// </summary>
        /// <returns>Mongo collection</returns>
        private IMongoCollection<Person> InitializePeopleCollection()
        {
            IMongoCollection<Person> collection = null;
            Console.WriteLine("\nAbout to initialize People MongoDB collection !\n");
            Thread.Sleep(1000);
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                MongoClient mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDBConnectionString"]);
                IMongoDatabase mongoDatabase = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDBDatabase"]);
                collection = mongoDatabase.GetCollection<Person>(ConfigurationManager.AppSettings["MongoDBCollection"]);
                if (collection != null && collection.Find(new BsonDocument()).Count() >= 0)
                {
                    Console.WriteLine("People MongoDB Collection successfully initialized !\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("People MongoDB collection is null !\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
            catch (MongoException mongoException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("MongoDB exception occurred !");
                Console.WriteLine(mongoException);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception occurred !");
                Console.WriteLine(exception);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            return collection;
        }

        /// <summary>
        /// Add Person to People MongoDB collection
        /// </summary>
        /// <param name="people">People MongoDB collection</param>
        /// <param name="person">Person object</param>
        /// <returns>true/false</returns>
        private bool AddPerson(IMongoCollection<Person> people, Person person)
        {
            bool isPersonAdded = false;
            try
            {
                if (people == null)
                    people = InitializePeopleCollection();
                people.InsertOne(person);
                isPersonAdded = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Person [{person.Id},{person.FirstName},{person.LastName},{person.Occupation}] is successfully added into People MongoDB collection !\n");
            }
            catch (MongoException mongoException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("MongoDB exception occurred !");
                Console.WriteLine(mongoException);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception occurred !");
                Console.WriteLine(exception);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            return isPersonAdded;
        }

        /// <summary>
        /// Add two persons to People MongoDB collection
        /// </summary>
        /// <param name="people">People MongoDB collection</param>
        private void Add2Persons(IMongoCollection<Person> people)
        {
            Console.WriteLine("\nAbout to add two person to People MongoDB collection !\n");
            Thread.Sleep(1000);
            Person person = new Person()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FirstName = "Joe",
                LastName = "Doe",
                Occupation = "Network Engineer"
            };

            bool isPersonAdded = AddPerson(people, person);
            Thread.Sleep(1000);
            if (isPersonAdded)
            {
                person = new Person()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Occupation = "Software Engineer"
                };
                isPersonAdded = AddPerson(people, person);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// List all People MongoDB collection
        /// </summary>
        /// <param name="people">People collection</param>
        private void ListAllPeople(IMongoCollection<Person> people)
        {
            if (people == null)
                people = InitializePeopleCollection();

            var searchPeople = people.Find(new BsonDocument()).ToList();
            if (searchPeople != null && searchPeople.Count > 0)
            {
                Console.WriteLine($"\nAbout to list all people !\n");
                Thread.Sleep(1000);
                Console.WriteLine("Id\t\t\t FirstName\tLastName\tOccupation\n");
                foreach (Person person in searchPeople)
                {
                    Console.WriteLine($"{person.Id} {person.FirstName}\t\t{person.LastName}\t\t{person.Occupation}\n");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("People MongoDB Collection is empty.");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        /// <summary>
        /// Get people by Occupation
        /// </summary>
        /// <param name="people">People MongoDB collection</param>
        /// <param name="occupation">Occupation value</param>
        /// <returns>List of person</returns>
        private List<Person> GetPeopleByOccupation(IMongoCollection<Person> people, string occupation)
        {
            List<Person> peopleByOccupation = null;
            try
            {
                if (people == null)
                    people = InitializePeopleCollection();
                var filter = Builders<Person>.Filter.Eq(p => p.Occupation, occupation ?? string.Empty);
                peopleByOccupation = people.Find(filter).ToList();
            }
            catch (MongoException mongoException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("MongoDB exception occurred !");
                Console.WriteLine(mongoException);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception occurred !");
                Console.WriteLine(exception);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            return peopleByOccupation;
        }

        /// <summary>
        /// List people by Occupation
        /// </summary>
        /// <param name="people">People MongoDB collection</param>
        /// <param name="occupation">Occupation value</param>
        private void ListByOccupation(IMongoCollection<Person> people, string occupation)
        {
            var searchPeople = GetPeopleByOccupation(people, occupation);
            if (searchPeople != null && searchPeople.Count > 0)
            {
                Console.WriteLine($"\nAbout to list people by occupation [{occupation}] !\n");
                Thread.Sleep(1000);
                Console.WriteLine("Id\t\t\t FirstName\tLastName\tOccupation\n");
                foreach (Person person in searchPeople)
                {
                    Console.WriteLine($"{person.Id} {person.FirstName}\t\t{person.LastName}\t\t{person.Occupation}\n");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("People MongoDB collection is null !");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        #endregion
    }
}
