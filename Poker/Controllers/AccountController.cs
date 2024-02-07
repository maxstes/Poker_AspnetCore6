﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Poker.Models;
using Poker.Models.Auth;
using Poker.Models.Game;
using Poker.Services;

namespace Poker.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly ILogger<AccountController> _logger;
        readonly RoomAdapter _roomAdapter;
        readonly CookiesService cookiesService ;
        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,RoomAdapter roomAdapter,CookiesService _cookiesService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            cookiesService = _cookiesService;
            _roomAdapter = roomAdapter;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/register")]
        public IActionResult Register() => View();

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                Email = model.Email,
                DateRegister = DateTime.UtcNow,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            //Result not success
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConst.Member);
                Player Player = new Player { Name = user.UserName };
                await _roomAdapter.AddPlayer(Player);
                _logger.LogInformation($"User: {model.Email} {result} register");
                //TODO Create normal
                var IsSignIn = await _signInManager.CanSignInAsync(user);
                if (IsSignIn)
                {
                    await _signInManager.SignInAsync(user, false);
                    cookiesService.AddCookiesEmail(model.Email,HttpContext.Session);

                    _logger.LogInformation($"User {model.Email} {result} authorize");
                    
                    return RedirectToAction("Index", "Home");
                }
                _logger.LogInformation($"User {model.Email} register but not can authorize");
                return RedirectToAction("Index", "Home");
            }
            else { return BadRequest("result not success"); }

        }
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        { 
            if (!ModelState.IsValid) { return BadRequest(model); }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                cookiesService.AddCookiesEmail(model.Email, HttpContext.Session);
                _logger.LogInformation($"User {model.Email} {result} authorize");
                _logger.LogInformation("Session Email: {Email}", model.Email);

            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost("/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
