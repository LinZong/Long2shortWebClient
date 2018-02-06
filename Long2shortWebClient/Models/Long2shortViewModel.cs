using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using MongoEntry;
using System;
using MongoDB.Bson;

namespace Long2shortWebClient.Models
{
    public class Long2shortViewModel
    {
        public ObjectId id { get; set; }
        [Range(1,Int64.MaxValue,ErrorMessage = "You can't enter a minus num or 0.")]
        public Int64? uid { get; set; }
        public string shortid { get; set; }
        [Url]
        [Required(ErrorMessage ="Long URL should not be null")]
        public string longid { get; set; }
    }
    public class Long2shortContext
    {
        private readonly IMongoDatabase context;
        public Long2shortContext()
        {
            if (MongoConnection.DatabaseName == null)
            {
                MongoConnection.GetDatabaseConnection();
            }
            context = MongoConnection.GetDatabaseContextHandler(MongoConnection.DatabaseName);
        }
        public IMongoCollection<Long2shortViewModel> Long2shortViewDetails
        {
            get
            {
                return context.GetCollection<Long2shortViewModel>(MongoConnection.CollectionName);
            }
        }
    }
}
