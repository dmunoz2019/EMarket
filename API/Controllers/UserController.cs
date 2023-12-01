using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class UserController : BaseApiController
    {
       private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;


        private readonly ITokenService _tokenService;
    
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);



            if(result.Succeeded)
            {
                return new UserDTO
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName
                };
            }

            return Unauthorized( new CodeErrorResponse(401, result.ToString()));
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(

                new CodeErrorResponse(200, "Logout successful")
                
                );
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {



            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenService.CreateToken(user),
                    Email = user.Email,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName

                };
            }

            return BadRequest(new CodeErrorResponse(400, result.Errors.ElementAt(0).Description.ToString()));

        }
        [HttpGet("validEmail")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery]string email)
        {
            // vamos a chequear si el email existe.+

            var user= await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {

            var user = await _userManager.FindByEmailWithAddressAsync(HttpContext.User);

            if (user == null)
            {
                   return NotFound(new CodeErrorResponse(404, "Address not found"));
            }

            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(HttpContext.User);
            if (user == null)
            {
                   return NotFound(new CodeErrorResponse(404, "Address not found"));
            }

            user.Address = _mapper.Map<AddressDTO, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            }

            return BadRequest();
            
        }

       // [Authorize]
       // [HttpGet("addresses")]
       // public async Task<ActionResult<IReadOnlyList<AddressDTO>>> GetAllUserAddresses()
       // {
        //    var user = await _userManager.FindByEmailWithAddressAsync(HttpContext.User);

         //   if (user == null)
          //  {
          //      return NotFound(new CodeErrorResponse(404, "Usuario no encontrado"));
           // }

//var addresses = user.Addresses;
//
  //          var addressesDto = _mapper.Map<IReadOnlyList<Address>, IReadOnlyList<AddressDTO>>(addresses);
  //
    //        return Ok(addressesDto);
      //  }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindUserAsync(HttpContext.User);

            return new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };

        }

        

    }
}
