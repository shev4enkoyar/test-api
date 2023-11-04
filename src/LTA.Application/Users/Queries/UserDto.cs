namespace LTA.Application.Users.Queries;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public AddressDto Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Website { get; set; } = null!;

    public CompanyDto Company { get; set; } = null!;
}

public class AddressDto
{
    public string Street { get; set; } = null!;

    public string Suite { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public GeoDto Geo { get; set; } = null!;
}

public class GeoDto
{
    public string Lat { get; set; } = null!;

    public string Lng { get; set; } = null!;
}

public class CompanyDto
{
    public string Name { get; set; } = null!;

    public string CatchPhrase { get; set; } = null!;

    public string Bs { get; set; } = null!;
}