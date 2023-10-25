using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries;

public class AccountRepository : GeneralRepository<AccountDto, Guid>, IAccountRepository
{
    public AccountRepository(string request = "Account/") : base(request)
    {
    }

    public async Task<ResponseOKHandler<TokenDto>> Login(LoginAccountDto login)
    {
        string jsonEntity = JsonConvert.SerializeObject(login);
        StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

        using (var response = await httpClient.PostAsync($"{request}Login", content))
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
            return entityVM;
        }
    }
    public async Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token)
    {
        StringContent content = new StringContent(token, Encoding.UTF8, "application/json");

        using (var response = await httpClient.GetAsync($"{request}GetClaims/{token}"))
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ClaimsDto>>(apiResponse);
            return entityVM;
        }
    }
}
