namespace MongoDB_DotnetCore.Model
{
    public class TaskModel : MongoBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
