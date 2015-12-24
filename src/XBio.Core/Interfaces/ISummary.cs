
namespace XBio.Core.Interfaces
{
    public interface ISummary : IAuditable
    {
        int Id { get; set; }
        string Value { get; set; }
    }
}