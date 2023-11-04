using LTA.Domain.Common;

namespace LTA.Domain.Entities;

public class User : BaseEntity<int>
{
    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AddressStreet { get; set; } = null!;

    public string AddressSuite { get; set; } = null!;

    public string AddressCity { get; set; } = null!;

    public string AddressZipcode { get; set; } = null!;

    public string AddressLat { get; set; } = null!;

    public string AddressLng { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyCatchPhrase { get; set; } = null!;

    public string CompanyBs { get; set; } = null!;


    #region Relations

    public virtual List<Album> Albums { get; set; } = new();

    #endregion
}