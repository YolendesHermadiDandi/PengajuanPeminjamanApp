﻿using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts;

public interface IAccountRepository : IRepository<AccountDto, Guid>
{
    Task<ResponseOKHandler<TokenDto>> Login(LoginAccountDto login);
    Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token);
}
