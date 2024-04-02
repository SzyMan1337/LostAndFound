namespace LostAndFound.ChatService.DataAccess.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        private readonly string _collectionName;

        public BsonCollectionAttribute(string collectionName)
        {
            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
        }

        public string CollectionName => _collectionName;
    }
}
