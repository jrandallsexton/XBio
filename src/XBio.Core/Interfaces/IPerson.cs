
namespace XBio.Core.Interfaces
{
    public interface IPerson
    {
        int Id { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string Display { get; set; }
    }
}