using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVenta.Migrations
{
    /// <inheritdoc />
    public partial class InitApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              if not exists (select [Id] from AppInit where [Id] = 1)
              begin
              insert into AppInit ([Count])
                  values (0)
              end;    
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AppInit where [Id] = 1");
        }
    }
}
