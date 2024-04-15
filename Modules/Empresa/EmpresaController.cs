using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Empresa.Dtos;
using PuntoVenta.Services.StoreImage;

namespace PuntoVenta.Modules.Empresa
{
    [Route("api/empresa")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpresaController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IStoreImageService storeImage;

        public EmpresaController(DataContext context, IStoreImageService storeImage)
        {
            this.context = context;
            this.storeImage = storeImage;
        }

        [HttpGet(Name = "ObtenerEmpresa")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<EmpresaDto>> GetOne()
        {
            var empresaDb = await context.Empresa.FirstOrDefaultAsync(e => e.Id == 1);

            if (empresaDb == null) { return Ok(new { Msg = "Empresa no encontrada" }); }

            return empresaDb.ToDto();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromForm] CreateEmpresaDto createDto)
        {

            var appInit = await context.AppInit.FirstOrDefaultAsync(x => x.Id == 1);

            if (appInit is null) { return Ok(new { Msg = "No hay inicio de aplicacion" }); }

            if (appInit.Count > 0) { return Ok(new { Msg = "La App ya fue iniciada " }); }

            var empresa = createDto.ToEntity();

            if (createDto.Image != null)
            {
                using var ms = new MemoryStream();
                await createDto.Image.CopyToAsync(ms);
                var contenido = ms.ToArray();
                var extension = System.IO.Path.GetExtension(createDto.Image.FileName).ToLower();
                var contentType = createDto.Image.ContentType;
                empresa.Image = await storeImage.SaveImage(contenido, extension, "Empresa", contentType);
            }

            await context.Empresa.AddAsync(empresa);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtenerEmpresa", new { id = empresa.Id }, empresa.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateEmpresaDto createDto)
        {
            var empresaDb = await context.Empresa.FirstOrDefaultAsync(e => e.Id == id);

            if (empresaDb == null) { return NotFound(new { Msg = "Empresa no encontrada" }); }

            empresaDb.ToUpdateDto(createDto);

            if (createDto.Image != null)
            {
                using var ms = new MemoryStream();
                await createDto.Image.CopyToAsync(ms);
                var contenido = ms.ToArray();
                var extension = System.IO.Path.GetExtension(createDto.Image.FileName).ToLower();
                var contentType = createDto.Image.ContentType;
                empresaDb.Image = await storeImage.UpdateImage(contenido, extension, "Empresa", contentType, empresaDb.Image!);
            }

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
