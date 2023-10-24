namespace API.DTOs.Accounts;

public class ClaimsDto
{
    public string Email { get; set; }
    public string UserGuid { get; set; }
    public string Name { get; set; }
    public List<string> Role { get; set; }
}
