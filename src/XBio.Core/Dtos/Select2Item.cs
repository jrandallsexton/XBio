
namespace XBio.Core.Dtos
{
    public class Select2Item
    {
        public int id { get; set; }
        public string text { get; set; }

        public Select2Item(int id, string text)
        {
            this.id = id;
            this.text = text;
        }
    }
}