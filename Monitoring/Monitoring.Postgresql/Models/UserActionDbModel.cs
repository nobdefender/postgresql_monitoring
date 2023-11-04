using MongoDB.Bson.Serialization.Attributes;

namespace Monitoring.Postgresql.Models
{
    public class UserActionDbModel
    {
        [BsonId]
        public long Hash { get; set; }
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string[] ActionParams { get; set; }

        public bool IsSelected { get; set; }
    }
}
