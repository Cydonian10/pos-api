using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVenta.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        if not exists (Select [Id] from AspNetRoles where [Id] in ('75fa8a57-34a9-4f34-9512-f31f12cbc5fa','bdfdfd04-55b8-46be-b0a1-1556711f6daf'))
         begin
         insert AspNetRoles (Id,[Name],[NormalizedName])
         values 
         ('75fa8a57-34a9-4f34-9512-f31f12cbc5fa','admin','ADMIN'),
         ('bdfdfd04-55b8-46be-b0a1-1556711f6daf','vendedor','VENDEDOR')
         end;
     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
         DELETE AspNetRoles where [Id] in ('75fa8a57-34a9-4f34-9512-f31f12cbc5fa','bdfdfd04-55b8-46be-b0a1-1556711f6daf');
     ");
        }
    }
}
