
namespace XBio.Core.Dtos
{
    public class SelectItem
    {
        public int value { get; set; }
        public string text { get; set; }

        public SelectItem(int id, string text)
        {
            this.value = id;
            this.text = text;
        }
    }
}