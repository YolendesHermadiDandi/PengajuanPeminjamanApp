﻿using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts;

public interface IAccountRepository : IRepository<AccountDto, Guid>
{
    Task<ResponseOKHandler<TokenDto>> Login(LoginAccountDto login);
    Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token);
    Task<ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>>> ResetPassword(string email);
    Task<ResponseOKHandler<ChangePasswordAccountDto>> ChangePassword(ChangePasswordAccountDto entity);
    Task<ResponseOKHandler<ChangeProfileDto>> UpdateProfile(ChangeProfileDto entity);
    Task<ResponseOKHandler<RegisterAccountDto>> RegisterEmployee(RegisterAccountDto entity);
}
