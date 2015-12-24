
namespace XBio.Core.Interfaces
{
    public interface IObjective : IAuditable
    {
        int Id { get; set; }
        string Value { get; set; }
    }
}