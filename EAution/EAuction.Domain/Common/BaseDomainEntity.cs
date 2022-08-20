using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

       /* [BsonId]
        [BsonRepresentation(BsonType.IntId)]
        public string Id { get; set; }*/
        public DateTime DateCreated { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
