
namespace XBio.Core.Interfaces
{
    public interface IAddress
    {
        int Id { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        int PostalId { get; set; }
    }
}