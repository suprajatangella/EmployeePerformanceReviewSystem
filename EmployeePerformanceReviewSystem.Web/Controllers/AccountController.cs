using EmployeePerformanceReview.Common.Constants;
using EmployeePerformanceReview.Common.Helpers;
using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Infrastructure.Data;
using EmployeePerformanceReviewSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeePerformanceReviewSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        //private readonly IEmailService _emailService;
        public AccountController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration config)
        //,
        //IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            //_emailService = emailService;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {

            if (string.IsNullOrEmpty(returnUrl) || returnUrl.Contains("/Account/Login"))
            {
                returnUrl = Url.Content("~/");
            }

            LoginVM loginVM = new()
            {
                RedirectUrl = returnUrl
            };

            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("JWToken"); // Clear the JWT token cookie
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult UnAuthorized()
        {
            return View();
        }

        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(Roles.Employee)).Wait();
            }
            
            if (!_roleManager.RoleExistsAsync(Roles.HR).GetAwaiter().GetResult())
            {
                // Create the role
                _roleManager.CreateAsync(new IdentityRole(Roles.HR)).Wait();
            }

            if (!_roleManager.RoleExistsAsync(Roles.Manager).GetAwaiter().GetResult())
            {
                // Create the role
                _roleManager.CreateAsync(new IdentityRole(Roles.Manager)).Wait();
            }

            if (!_roleManager.RoleExistsAsync(Roles.Reviewer).GetAwaiter().GetResult())
            {
                // Create the role
                _roleManager.CreateAsync(new IdentityRole(Roles.Reviewer)).Wait();
            }

            RegisterVM registerVM = new()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                RedirectUrl = returnUrl
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    FullName = registerVM.Name,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = registerVM.Email,
                    CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    LinkedInUrl = "https://www.linkedin.com/",
                    ProfilePictureUrl = AvatarGenerator.GenerateAvatar(registerVM.Name, _webHostEnvironment.WebRootPath) ?? "/profileimg/default-avatar-men.jpg" // Default profile picture URL
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Applicant");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(registerVM.RedirectUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                    .PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    // Get the currently logged-in user
                    var user = await _userManager.FindByEmailAsync(loginVM.Email);
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var token = GenerateJwtToken(user.UserName, userRoles[0].ToString());

                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    //var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    // return Ok(new { token });

                    // List of roles to validate against
                    var validRoles = new List<string> { Roles.Admin, Roles.HR, Roles.Manager, Roles.Reviewer, Roles.Employee};
                    
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    // Check if the user is in any of the valid roles
                    foreach (var role in validRoles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role))
                        {
                            HttpContext.Response.Cookies.Append("JWToken", token, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTimeOffset.Now.AddMinutes(60)
                            });
                            // If the user has a valid role, redirect to the home page
                            return RedirectToAction("Index", "Home");
                        }
                        
                    }         
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(loginVM);
        }

        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role) // Add role to token
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var dbContext = _context; // Inject your ApplicationDbContext

            // Remove related data
            var userRoles = dbContext.UserRoles.Where(ur => ur.UserId == userId);
            dbContext.UserRoles.RemoveRange(userRoles);

            var userClaims = dbContext.UserClaims.Where(uc => uc.UserId == userId);
            dbContext.UserClaims.RemoveRange(userClaims);

            var userLogins = dbContext.UserLogins.Where(ul => ul.UserId == userId);
            dbContext.UserLogins.RemoveRange(userLogins);

            var userTokens = dbContext.UserTokens.Where(ut => ut.UserId == userId);
            dbContext.UserTokens.RemoveRange(userTokens);

            await dbContext.SaveChangesAsync();

            // Now delete the user
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList", "Account");
            }

            return View("Error");
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync(); // Get all users
            return View(users);
        }

        public async Task<IActionResult> UpdateUserPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UpdatePasswordVM
            {
                UserId = user.Id,
                Email = user.Email
            };

            return View(model);
        }

        // Process the password update
        [HttpPost]
        public async Task<IActionResult> UpdateUserPassword(UpdatePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            // Generate reset token and update the password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                SendEmail(user.FullName, user.Email);
                ViewBag.Message = "Password updated successfully.A confirmation email has been sent.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        private void SendEmail(string name, string email)
        {

            //var emailService = HttpContext.RequestServices.GetRequiredService<EmailService>();
            var subject = "Password Updated Successfully";
            var body = $"Hello {name},<br><br>Your password has been updated successfully. If you didn't request this change, please contact support immediately.<br><br>Regards,<br>Expense Management Team";

            //_emailService.SendEmailAsync(email, subject, body);
        }
    }
}

