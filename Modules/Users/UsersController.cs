using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Auth.Dtos;
using PuntoVenta.Modules.Users.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Modules.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        [HttpPut("update/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserDetail([FromRoute] string id, [FromBody] AuthRegisterDto dto)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) { return NotFound("Usuario no Econtrado"); }

            user.ToEntityUpdate(dto);

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) { return Ok(user.ToDto()); }

            return BadRequest(result.Errors);

        }

        [HttpGet("employees")]
        public async Task<ActionResult<List<UserWithRolesDto>>> List(string rol)
        {

            var usuarios = await userManager.GetUsersInRoleAsync(rol);
            var usuariosConRolesYClaims = new List<UserWithRolesDto>();

            foreach (var user in usuarios)
            {
                var roles = await userManager.GetRolesAsync(user);
                var claims = await userManager.GetClaimsAsync(user);

                usuariosConRolesYClaims.Add(user.ToWithRolesDto(roles, claims));
            }

            return usuariosConRolesYClaims;
        }

        [HttpGet("customers")]
        public async Task<ActionResult<List<CustomerDto>>> ListCustomers()
        {
            var users = await context.Users.Where(x => x.Salary == 0).ToListAsync();

            return users.Select(x => x.ToCustomerDto()).ToList();

        }

        [HttpGet]
        public async Task<ActionResult<List<UserWithRolesDto>>> List()
        {
            //var usersDB = await context.Users.Include(ToListAsync()

            var usuarios = await userManager.Users.ToListAsync();
            var usuariosConRolesYClaims = new List<UserWithRolesDto>();


            foreach (var user in usuarios)
            {
                var roles = await userManager.GetRolesAsync(user);
                var claims = await userManager.GetClaimsAsync(user);

                usuariosConRolesYClaims.Add(user.ToWithRolesDto(roles, claims));
            }

            return usuariosConRolesYClaims;
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
                await userManager.AddClaimAsync(user, new Claim("NiveAcceso", rol));
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


            var roles = await userManager.GetRolesAsync(user);


            return Ok(roles);
        }

        [HttpPost("remove-rol")]
        public async Task<ActionResult> RemoveRol(AuthAddRolDto authAddRolDto)
        {
            var user = await userManager.FindByEmailAsync(authAddRolDto.Email!);
            if (user == null) { return NotFound(); }

            await userManager.RemoveFromRolesAsync(user, authAddRolDto.Roles!);

            var roles = await userManager.GetRolesAsync(user);


            return Ok(roles);
        }

        [HttpPost("employe")]
        public async Task<ActionResult> Registrar(AuthRegisterDto authRegisterDto)
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
                var roleExist = await roleManager.RoleExistsAsync("Empleado");

                if (!roleExist)
                {
                    var newRole = new IdentityRole("Empleado");
                    await roleManager.CreateAsync(newRole);
                }
                await userManager.AddToRoleAsync(userEntity, "Empleado");

                return Ok(userEntity.ToWithRolesDto(["Empleado"], []));
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("customer")]
        public async Task<ActionResult> RegistrarCustomer(AuthRegisterDto authRegisterDto)
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
              
                return Ok(userEntity.ToCustomerDto());
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove([FromRoute] string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) { return NotFound(new { message = "Usuario no encontrado" }); }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { id });
            }
            else
            {
                return BadRequest(new { message = "No se puede eliminar" });
            }

        }

    }
}
