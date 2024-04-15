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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserTokenDto>> Registrar(UserRegisterDto userRegisterDto)
        {
            var userEntity = userRegisterDto.ToEntity();

            var resultado = await userManager.CreateAsync(userEntity!, userRegisterDto.Password!);

            if (resultado.Succeeded)
            {

                var userToken = await ConstruirToken(userRegisterDto);
                userToken.User = userEntity.ToDto();
                return userToken;

            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> Login(UserLoginDto authLoginDto)
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


        private async Task<UserTokenDto> ConstruirToken(IAuthCredencial authRegisterDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, authRegisterDto.Email!),
            };

            var user = await userManager.FindByEmailAsync(authRegisterDto.Email!);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user!.Id));
            var claimDb = await userManager.GetClaimsAsync(user!);
            claims.AddRange(claimDb);

            var rolesDb = await userManager.GetRolesAsync(user!);

            foreach (var role in rolesDb)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, signingCredentials: creds, expires: expiracion,
                claims: claims);

            return new UserTokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiración = expiracion
            };
        }

        [HttpPost("asignar-claim")]
        public async Task<ActionResult> AsignarClaims(UserAddRolDto authAddRolDto)
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
        public async Task<ActionResult> RemoveClaims(UserAddRolDto authAddRolDto)
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
        public async Task<ActionResult> AsignarRol(UserAddRolDto userAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(userAddRolDto.Email!);

            if (user == null) { return NotFound(); }


            foreach (var rol in userAddRolDto.Roles!)
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
        public async Task<ActionResult> RemoveRol(UserAddRolDto authAddRolDto)
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

            if (usuarioDb == null) { return NotFound(new { msg = "Usuario no encontrado" }); }

            var userDto = usuarioDb!.ToDto();

            var roles = await userManager.GetRolesAsync(usuarioDb!);

            var claims = await userManager.GetClaimsAsync(usuarioDb!);

            var claimsDto = new List<ClaimsDto>();

            foreach (var claim in claims)
            {
                claimsDto.Add(new ClaimsDto { Typo = claim.Type, Value = claim.Value });
            }

            return Ok(usuarioDb.ToProfileDto(roles, claimsDto));
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUserWithRol()
        {

            var usersInRole = await context.Users.ToListAsync();

            return usersInRole.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {

            var roles = await context.Roles.Select(x => x.Name).ToListAsync();

            return roles!;
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string id)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) { return NotFound(new { msg = "Usuario no encontrado" }); }


            return user.ToDto();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<UserDto>>> FilterUser([FromQuery] string rol)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(rol);

            return usersInRole.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{Id}/roles")]
        public async Task<ActionResult> GetRolesByUser([FromRoute] string id)
        {
            var userDB = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDB == null) { return NotFound(new { msg = "Usuario no encontrado" }); }
            var roles = await userManager.GetRolesAsync(userDB);
            return Ok(roles);
        }

        [HttpGet("app-init")]
        public async Task<ActionResult> InitApp()
        {
            var appInit = await context.AppInit.FirstOrDefaultAsync(x => x.Id == 1);

            if (appInit is null) { return Ok(new { Msg = "No hay inicio de aplicacion" }); }

            if (appInit.Count > 0) { return Ok(new { msg = "Ya inicio la app" }); }

            return Ok(new { Msg = "Iniciar la App" });

        }


        [HttpPost("app-init")]
        public async Task<ActionResult> CreateUserAdmin(UserRegisterDto userRegisterDto)
        {
           
            var appInit = await context.AppInit.FirstOrDefaultAsync(x => x.Id == 1);

            // context tabla uppinico > 0 entonces esto
            if (appInit is null) { return Ok(new { Msg = "No hay inicio de aplicacion" }); }    

            if (appInit.Count > 0) { return Ok(new { Msg = "Ya inicio la app" }); }

           
            var userEntity = userRegisterDto!.ToEntity();
            userEntity.Active = true;

            var resultado = await userManager.CreateAsync(userEntity!, userRegisterDto!.Password!);

            if (resultado.Succeeded)
            {
                //context tabla appincio 
                // +1
                appInit.Count = 1;
                await userManager.AddToRoleAsync(userEntity, "admin");
                await context.SaveChangesAsync();
                var userToken = await ConstruirToken(userRegisterDto);
                userToken.User = userEntity.ToDto();
                return Ok(userToken);

            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }
        
    }
}
