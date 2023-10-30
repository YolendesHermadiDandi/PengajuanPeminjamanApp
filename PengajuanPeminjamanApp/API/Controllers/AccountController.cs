using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailHandlerRepository _emailHandlerRepository;
        private readonly ITokenHandlerRepository _tokenHandler;


        //Constructor
        public AccountController(IAccountRepository accountRepository,
                                 IEmployeeRepository employeeRepository,
                                 IEmailHandlerRepository emailHandlerRepository,
                                 ITokenHandlerRepository tokenHandler,
                                 IAccountRoleRepository accountRoleRepository,
                                 IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _emailHandlerRepository = emailHandlerRepository;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            //Linq
            var data = result.Select(x => (AccountDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }


        [HttpPut("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                Random random = new Random();
                var existingEmployee = _employeeRepository.GetEmail(email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL NOT FOUND"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                Account toUpdate = existingEmployeeAccount;
                toUpdate.Otp = random.Next(111111, 999999);
                toUpdate.IsUsed = false;
                toUpdate.ExpiredTime = DateTime.Now.AddMinutes(5);

                var result = _accountRepository.Update(toUpdate);


                var employee = _employeeRepository.GetAll();
                var account = _accountRepository.GetAll();

                var forgotPassword = from emp in employee
                                     join acc in account on emp.Guid equals acc.Guid
                                     where emp.Email == email
                                     select new ForgotPasswordAccountDto
                                     {
                                         Otp = acc.Otp,
                                         Message = "OTP hanya berlaku 5 menit"
                                     };
                _emailHandlerRepository.Send("Forgot Password", $"Yout OTP is {toUpdate.Otp}", email, "Admin@no-replay.com");
                return Ok(new ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>>("Success send OTP", forgotPassword));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "FAIL TO UPDATE DATA",
                    Error = ex.Message
                });
            }

        }

        [HttpPut("ChangePassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordAccountDto changePasswordDto)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetEmail(changePasswordDto.Email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL NOT FOUND"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                Account toUpdate = existingEmployeeAccount;
                if (existingEmployeeAccount.ExpiredTime < DateTime.Now)
                {

                    toUpdate.IsUsed = true;
                    var result = _accountRepository.Update(toUpdate);
                    return Ok(new ResponseOKHandler<string>("OTP EXPIRED"));
                }

                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    return Ok(new ResponseOKHandler<string>("PASSWORD NOT MATCH"));
                }

                if (existingEmployeeAccount.Otp != changePasswordDto.Otp || existingEmployeeAccount.Otp == 0)
                {

                    return Ok(new ResponseOKHandler<string>("OTP NOT MATCH"));
                }

                toUpdate.Otp = 0;
                toUpdate.IsUsed = false;
                toUpdate.ExpiredTime = existingEmployeeAccount.ExpiredTime;
                toUpdate.Password = PasswordHashHandler.HashPassword(changePasswordDto.NewPassword);

                var update = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<string>("CHANGE PASSWORD SUCCESS"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "FAIL TO UPDATE DATA",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("GetClaims/{token}")]
        public IActionResult GetClaims(string token)
        {
            var claims = _tokenHandler.ExtractClaimsFromJwt(token);
            return Ok(new ResponseOKHandler<ClaimsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Claims has been retrieved",
                Data = claims
            });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterAccountDto registerAccoutDto)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {

                    if (registerAccoutDto.Password != registerAccoutDto.ConfirmPassword)
                    {
                        return Ok(new ResponseOKHandler<string>("PASSWORD NOT MATCH"));
                    }

                    Employee toCreateEmployee = registerAccoutDto;
                    toCreateEmployee.Nik = AutoGenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                    var addEmployee = _employeeRepository.Create(toCreateEmployee);
                    Account toCreateAccount = registerAccoutDto;
                    toCreateAccount.Guid = addEmployee.Guid;
                    toCreateAccount.Password = PasswordHashHandler.HashPassword(registerAccoutDto.Password);

                    var addAccount = _accountRepository.Create(toCreateAccount);

                    //assign role
                    var accountRole = _accountRoleRepository.Create(new AccountRole
                    {
                        AccountGuid = toCreateAccount.Guid,
                        RoleGuid = _roleRepository.GetDefaultRoleGuid() ??
                        throw new Exception("Default Role Not Found")
                    });


                    transaction.Complete();
                    return Ok(new ResponseOKHandler<string>("REGISTER SUCCESS"));

                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status500InternalServerError,
                            Status = HttpStatusCode.InternalServerError.ToString(),
                            Message = "FAILED TO REGISTER",
                            Error = ex.InnerException?.Message ?? ex.Message
                        });
                }
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginAccountDto login)
        {
            try
            {
                var existingEmployee = _employeeRepository.GetEmail(login.Email);
                if (existingEmployee is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "EMAIL SALAH"
                    });
                }

                var existingEmployeeAccount = _accountRepository.GetByGuid(existingEmployee.Guid);

                if (!PasswordHashHandler.VerifyPassword(login.Password, existingEmployeeAccount.Password))
                {
                    return Ok(new ResponseOKHandler<string>("PASSWORD SALAH"));
                }

                var claims = new List<Claim>();
                claims.Add(new Claim("Email", existingEmployee.Email));
                claims.Add(new Claim("UserGuid", existingEmployee.Guid.ToString(), ClaimValueTypes.String));
                claims.Add(new Claim("FullName", string.Concat(
                    existingEmployee.FirstName, " ", existingEmployee.LastName)));

                var getRolesName = from ar in _accountRoleRepository.GetAll()
                                   join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                   where ar.AccountGuid == existingEmployee.Guid
                                   select r.Name;

                foreach (var role in getRolesName)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var generateToken = _tokenHandler.Generate(claims);




                return Ok(new ResponseOKHandler<object>("login success", new { Token = generateToken }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "FAIL TO UPDATE DATA",
                    Error = ex.Message
                });
            }

        }





        [HttpGet("{guid}")]
        /*
         * method dibawah digunakan untuk mendapatkan data berdasarkan guid
         * 
         * PHARAM :
         * - guid : primary key dari 1 baris data
         */
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result)); //konversi explisit
        }

        [HttpPost]

        /*
         * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
         * 
         * PHARAM :
         * - createAccountDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
         */
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Account toCreate = createAccountDto;
                toCreate.Password = PasswordHashHandler.HashPassword(createAccountDto.Password);
                var result = _accountRepository.Create(toCreate);
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "FAILED TO CREATE DATA"
                    });
            }
        }

        [HttpPut]
        /*
        * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - accountDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var existingAccount = _accountRepository.GetByGuid(accountDto.Guid);
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                Account toUpdate = accountDto;
                toUpdate.CreateDate = existingAccount.CreateDate;
                toUpdate.Password = PasswordHashHandler.HashPassword(accountDto.Password);

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to update data"


                });
            }
        }

        [HttpPut("updateProfile")]
        public IActionResult UpdateProfile(ChangeProfileDto changeProfileDto)
        {
            try
            {
                var existingAccount = _accountRepository.GetByGuid(changeProfileDto.Guid);
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                
                Account toUpdate = existingAccount;
                toUpdate.ImgProfile = changeProfileDto.ImgProfile;

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to update data"


                });
            }
        }

        [HttpDelete("{guid}")]
        /*
        * Method dibawah digunakan untuk menghapus data dengan menggunakan guid
        * 
        * PHARAM :
        * - guid : primary key dari 1 baris data
        */
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingAccount = _accountRepository.GetByGuid(guid); ;
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }
                var result = _accountRepository.Delete(existingAccount);
                return Ok(new ResponseOKHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "FAILED TO DELETED DATA"


                });
            }
        }

    }
}
