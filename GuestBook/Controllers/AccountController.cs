using System;
using System.Linq;
using System.Web.Mvc;
using DataAccessObjects;
using GuestBook.Helpers;
using MongoLibrary;
using GuestBook.Models;
using GuestBook.Security;
using System.Configuration;

namespace GuestBook.Controllers
{
    /// <summary>
    /// Controller to handle account activities.
    /// </summary>
    public class AccountController : BaseController
    {
        readonly IGenericRepository<User> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AccountController(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.Get(x => x.UserName == loginModel.UserName).SingleOrDefault();
                if (user != null && PasswordHash.ValidatePassword(loginModel.Password, user.Password))
                {
                    FormsAuthHelper.SetAuthTicket(user, HttpContext);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("NotFound", "User not found");
            }
            return View(loginModel);



        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthHelper.Logout();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Registers the specified register model.
        /// </summary>
        /// <param name="registerModel">The register model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var userExists = _userRepository.Get(x => x.UserName.ToLower() == registerModel.UserName.ToLower());
                if (userExists != null && userExists.Any())
                {
                    ViewBag.Error = "User already exists";
                    ViewData.Model = null;
                    return View();
                }
                var hashPassword = PasswordHash.CreateHash(registerModel.Password);
                var user = new User
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.UserName,
                    Password = hashPassword,
                    Email = registerModel.Email
                };
                _userRepository.Add(user);
                Helper.SendEmail("nileshgokhale45@gmail.com", user.Email, "Welcome", string.Format("Please verify your email address\r\n by following this <a href={0}>link</a>", Helper.GetWelcomeEmailLink(user.UserName)));
            }
            return View();
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ValidateEmail(string param)
        {
            var result = Helper.FromBase64(param);
            var split = result.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var user = _userRepository.Get(x => x.UserName == split[0]).FirstOrDefault();
            DateTime date;
            DateTime.TryParse(split[1], out date);
            if (DateTime.Now < date.AddDays(1))
            {
                ViewBag.Message = "Your email has been validated.";
            }
            else
            {
                ViewBag.Error = "The link you accessed is really old. You might want to generate new validation link.";
                if (user != null)
                {
                    ViewBag.Email = user.Email;
                    ViewBag.UserName = user.UserName;
                }
            }

            return View();
        }

        /// <summary>
        /// Sends the verification email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult SendVerificationEmail(string email, string userName)
        {
            var result = Helper.SendEmail("nileshgokhale45@gmail.com", email, "Welcome", string.Format("Please verify your email address\r\n by following this <a href={0}>link</a>", Helper.GetWelcomeEmailLink(userName)));
            return Json(result);
        }

        /// <summary>
        /// Disables the client validation.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult DisableClientValidation()
        {
            ConfigurationManager.AppSettings["ClientValidationEnabled"] = false.ToString();
            return Json(true);
        }

        /// <summary>
        /// Registers the partial.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult RegisterPartial()
        {
            return PartialView("_RegisterPartial");
        }

        /// <summary>
        /// Determines whether [has ajax request].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has ajax request]; otherwise, <c>false</c>.
        /// </returns>
        [HttpGet]
        public bool HasAjaxRequest()
        {
            return SessionHelper.HasAjaxRequest;
        }
    }
}
