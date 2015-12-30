
namespace XBio.Core.Dtos
{
    public class KvpItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public KvpItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}