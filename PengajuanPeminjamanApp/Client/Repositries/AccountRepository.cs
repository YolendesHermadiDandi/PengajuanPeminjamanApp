using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Client.Repositories;
using Newtonsoft.Json;
using NuGet.Common;
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
        ResponseOKHandler<TokenDto> entityVM = null;
        using (var response = await httpClient.PostAsync($"{request}Login", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
        }
            return entityVM;
    }
    public async Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token)
    {
        ResponseOKHandler<ClaimsDto> entityVM = null;
        if(token == null)
        {
            return entityVM;
        }
        StringContent content = new StringContent(token, Encoding.UTF8, "application/json");
        using (var response = await httpClient.GetAsync($"{request}GetClaims/{token}"))
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ClaimsDto>>(apiResponse);
        }
            return entityVM;
    }

    public async Task<ResponseOKHandler<ChangeProfileDto>> UpdateProfile(ChangeProfileDto entity)
    {
        ResponseOKHandler<ChangeProfileDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PutAsync(request + "updateProfile", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ChangeProfileDto>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<RegisterAccountDto>> RegisterEmployee(RegisterAccountDto entity)
    {
        ResponseOKHandler<RegisterAccountDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "register", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<RegisterAccountDto>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>>> ResetPassword(string email)
    {
        ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>> entityVM = null;
        if (email == null)
        {
            return entityVM;
        }
        var data = new
        {
            emai = email
        };
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        using (var response = httpClient.PutAsync($"{request}ForgotPassword?email="+email, content).Result)
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>>>(apiResponse);
        }
        return entityVM;
    }
}
