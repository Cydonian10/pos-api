using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PuntoVenta.Modules.Auth.Dtos;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database.Mappers;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PuntoVenta.Modules.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;
        private readonly DataContext context;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            SignInManager<User> signInManager,
            DataContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthRequestDto>> Registrar(AuthRegisterDto authRegisterDto)
        {
            var userEntity = new User
            {
                UserName = authRegisterDto.Email,
                Email = authRegisterDto.Email,
                Salary = authRegisterDto.Salary,
                Birthday = authRegisterDto.Birthday,
                Name = authRegisterDto.Name,
            };

            var resultado = await userManager.CreateAsync(userEntity!, authRegisterDto.Password!);

            if (resultado.Succeeded)
            {

                return await ConstruirToken(authRegisterDto);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("register-customer")]
        public async Task<ActionResult<AuthRequestDto>> RegistrarCustomer(AuthRegisterDto authRegisterDto)
        {
            var userEntity = new User
            {
                UserName = authRegisterDto.Email,
                Email = authRegisterDto.Email,
                Salary = authRegisterDto.Salary,
                Birthday = authRegisterDto.Birthday,
                Name = authRegisterDto.Name,
            };

            var resultado = await userManager.CreateAsync(userEntity!, authRegisterDto.Password!);

            if (resultado.Succeeded)
            {

                return await ConstruirToken(authRegisterDto);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthRequestDto>> Login(AuthLoginDto authLoginDto)
        {
            var resultado = await signInManager.PasswordSignInAsync(
                authLoginDto.Email!,
                authLoginDto.Password!,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (resultado.Succeeded)
            {
                return await ConstruirToken(authLoginDto);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

     
        private async Task<AuthRequestDto> ConstruirToken(IAuthCredencial authRegisterDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, authRegisterDto.Email!),
            };

            var identityUser = await userManager.FindByEmailAsync(authRegisterDto.Email!);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, identityUser!.Id));
            var claimDb = await userManager.GetClaimsAsync(identityUser!);
            claims.AddRange(claimDb);

            var rolesDb = await userManager.GetRolesAsync(identityUser!);

            foreach (var role in rolesDb)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, signingCredentials: creds, expires: expiracion,
                claims: claims);

            return new AuthRequestDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiración = expiracion
            };
        }

        [HttpPost("asignar-claim")]
        public async Task<ActionResult> AsignarClaims(AuthAddRolDto authAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(authAddRolDto.Email!);

            if (user == null) { return NotFound(); }

            foreach (var rol in authAddRolDto.Roles!)
            {
                await userManager.AddClaimAsync(user, new Claim("NiveAcceso", rol));
            }

            return NoContent();
        }

        [HttpPost("remove-claim")]
        public async Task<ActionResult> RemoveClaims(AuthAddRolDto authAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(authAddRolDto.Email!);

            if (user == null) { return NotFound(); }

            foreach (var rol in authAddRolDto.Roles!)
            {
                await userManager.RemoveClaimAsync(user, new Claim("NiveAcceso", rol));
            }

            return NoContent();
        }


        [HttpPost("asignar-rol")]
        public async Task<ActionResult> AsignarRol(AuthAddRolDto authAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(authAddRolDto.Email!);

            if (user == null) { return NotFound(); }


            foreach (var rol in authAddRolDto.Roles!)
            {
                var roleExist = await roleManager.RoleExistsAsync(rol);

                if (!roleExist)
                {
                    var newRole = new IdentityRole(rol);
                    await roleManager.CreateAsync(newRole);
                }
                await userManager.AddToRoleAsync(user, rol);
            }

            return NoContent();
        }

        [HttpPost("remove-rol")]
        public async Task<ActionResult> RemoveRol(AuthAddRolDto authAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(authAddRolDto.Email!);
            if (user == null) { return NotFound(); }

            await userManager.RemoveFromRolesAsync(user, authAddRolDto.Roles!);

            return NoContent();
        }

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Profile()
        {
            var usuarioId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

            if (usuarioId == null) { NotFound(); }

            var usuarioDb = await context.Users.FirstOrDefaultAsync(x => x.Id == usuarioId);

            var userDto = usuarioDb!.ToDto();

            var roles = await userManager.GetRolesAsync(usuarioDb!);

            var claims = await userManager.GetClaimsAsync(usuarioDb!);

            var claimsDto = new List<ClaimsDto>();

            foreach (var claim in claims)
            {
                claimsDto.Add(new ClaimsDto { Typo = claim.Type, Value = claim.Value });
            }

            return Ok(new { Usuario = userDto, Roles = roles, Claims = claimsDto });
        }

    }
}
