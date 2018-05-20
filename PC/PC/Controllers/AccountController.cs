using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PC.Models;
using System.Data.Entity.Validation;
using PC.CustomLibraries;

namespace PC.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        PCContext db = new PCContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/LoginPatient
        [AllowAnonymous]
        public ActionResult LoginPatient(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPatient(LoginPatientViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var cardNumberCheck = db.AspNetUsers.FirstOrDefault(u => u.cardNumber == user.cardNumber);
            if (cardNumberCheck != null)
            {
                if (db.Patients.Find(user.cardNumber) != null)
                {
                    var getName = db.Patients.Where(u => u.cardNumber == user.cardNumber).Select(u => u.firstName);
                    var materName = getName.ToList();
                    var firstName = materName[0];

                    var getName1 = db.Patients.Where(u => u.cardNumber == user.cardNumber).Select(u => u.lastName);
                    var materName1 = getName1.ToList();
                    var lastName = materName1[0];

                    var getPassword = db.AspNetUsers.Where(u => u.cardNumber == user.cardNumber).Select(u => u.Password);
                    var materializePassword = getPassword.ToList();
                    var password = materializePassword[0];
                    var encryptedPass = CustomEncrypt.Encrypt(user.Password);
                    if (encryptedPass == password)
                    {
                        var getId = db.AspNetUsers.Where(u => u.cardNumber == user.cardNumber).Select(u => u.Id);
                        var materializeId = getId.ToList();
                        var id = materializeId[0];

                        var getCardNumber = db.AspNetUsers.Where(u => u.cardNumber == user.cardNumber).Select(u => u.cardNumber);
                        var materializeEmail = getCardNumber.ToList();
                        var cardNumber = materializeEmail[0];

                        var idRole = db.AspNetUsers.Where(u => u.cardNumber == user.cardNumber).Select(u => u.idRole);
                        var materializeRole = idRole.ToList();
                        var role = materializeRole[0];

                        var roleName = db.AspNetRoles.Find(role).Name.ToString();

                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                        new Claim(ClaimTypes.Name, firstName +" "+ lastName),
                        new Claim(ClaimTypes.Email, cardNumber),
                        new Claim(ClaimTypes.Role, roleName)
                }, "ApplicationCookie");
                        var ctx = Request.GetOwinContext();
                        var accountManager = ctx.Authentication;
                        accountManager.SignIn(identity);
                        TempData["SuccessRegistration"] = "You signed in into your account as ";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The username or password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect");

                }
            }
            return View(user);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var encryptedPassword = CustomEncrypt.Encrypt(user.Password);

                    if (db.Patients.Find(user.cardNumber) != null)
                    {
                        if (db.AspNetUsers.Any(o => o.cardNumber == user.cardNumber))
                        {
                            TempData["UserAlreadyExists"] = "This user already exists";
                            return View(user);
                        }
                        var userDb = new AspNetUser();
                        userDb.cardNumber = user.cardNumber;
                        userDb.Password = encryptedPassword;
                        userDb.Email = db.Patients.Find(user.cardNumber).email;
                        userDb.idRole = 4;
                        db.AspNetUsers.Add(userDb);
                        db.SaveChanges();
                        TempData["SuccessRegistration"] = "You registered successfully";
                        return RedirectToAction("LoginPatient", "Account");
                    }
                    else
                    {
                        TempData["Error"] = "You entered a wrong health card number";
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var emailCheck = db.AspNetUsers.FirstOrDefault(u => u.Email == user.Email && u.idRole != 4);
            if (emailCheck != null)
            {
                var getPassword = db.AspNetUsers.Where(u => u.Email == user.Email).Select(u => u.Password);
                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var encryptedPass = CustomEncrypt.Encrypt(user.Password);
                if (encryptedPass == password)
                {
                    var getEmail = db.AspNetUsers.Where(u => u.Email == user.Email).Select(u => u.Email);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    var idRole = db.AspNetUsers.Where(u => u.Email == user.Email).Select(u => u.idRole);
                    var materializeRole = idRole.ToList();
                    var role = materializeRole[0];

                    var roleName = db.AspNetRoles.Find(role).Name.ToString();

                    var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, roleName)
                }, "ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    var accountManager = ctx.Authentication;
                    accountManager.SignIn(identity);
                    TempData["SuccessRegistration"] = "You signed in into your account as ";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect");
                }

            }
            else
            {
                ModelState.AddModelError("", "The username or password is incorrect");

            }
            return View();
        }
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

       
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}