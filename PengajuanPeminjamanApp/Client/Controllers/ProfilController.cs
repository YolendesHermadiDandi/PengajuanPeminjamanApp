using API.DTOs.Accounts;
using Client.Contracts;
using Client.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ProfilController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;

        public ProfilController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("/Profile")]
        public IActionResult Profil()
        {
            return View();
        }

        [HttpGet("/profileData")]
        public async Task<JsonResult> GetProfileData()
        {
            var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            Guid userGuid = new Guid(result.Data.UserGuid);
            var employee = await _employeeRepository.Get(userGuid);
            var account = await _accountRepository.Get(userGuid);
            var data = new
            {
                employee = employee.Data,
                Img = account.Data.ImgProfile,
            };

            return Json(data);
        }


        [HttpPost("/imgUpload")]
        public async Task<JsonResult> UploadImg(IFormFile file)
        {
            var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            if (result == null)
            {
                return Json(result);
            }
            Guid userGuid = new Guid(result.Data.UserGuid);
            var accountdata = await _accountRepository.Get(userGuid);

            if (accountdata.Data.ImgProfile != "")
            {
                System.IO.File.Delete("wwwroot/assets/img/profiles/" + accountdata.Data.ImgProfile);
            }

            var path = "wwwroot/assets/img/profiles/";
            var fileName = Path.GetFileName(Guid.NewGuid() + ".jpg");
            var fullPath = Path.Combine(path, fileName);

            var data = new ChangeProfileDto
            {
                Guid = userGuid,
                ImgProfile = fileName
            };

            var updateProfile = await _accountRepository.UpdateProfile(data);

            using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Json(updateProfile);

        }

        [HttpPost("/profileUpdate")]
        public async Task<JsonResult> UpdateProfile(UpdateDataUserDto data)
        {
            if (data.Password == null || data.ConfirmPassword == null)
            {
                var message = -2;
                return Json(message);
            }

            if (data.Password != data.ConfirmPassword)
            {
                var message = -1;
                return Json(message);
            }

            var profile = await _accountRepository.Get(data.Guid);
            var toUpdate = profile.Data;
            toUpdate.Password = data.Password;

            var result = _accountRepository.Put(toUpdate.Guid, toUpdate);
            return Json(result);
        }
    }
}
