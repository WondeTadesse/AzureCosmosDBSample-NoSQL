//|---------------------------------------------------------------|
//|                         AZURE MONGODB                         |
//|---------------------------------------------------------------|
//|                       Developed by Wonde Tadesse              |
//|                             Copyright ©2018 - Present         |
//|---------------------------------------------------------------|
//|                         AZURE MONGODB                         |
//|---------------------------------------------------------------|

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMongoDBSample.Entities
{
    public class Person
    {
        #region Public Properties 

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Occupation")]
        public string Occupation { get; set; }

        #endregion
    }
}
