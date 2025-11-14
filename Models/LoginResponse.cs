namespace BookTradeHubAPI.Models;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime TokenExpiryTime { get; set; }
}
