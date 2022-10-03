using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuongTruong.IdentityServer.Infrastructure.SqlServer.Identity.Migrations;

public partial class RemoveIPersistedGrantDbContext : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DeviceCodes");

        migrationBuilder.DropTable(
            name: "Keys");

        migrationBuilder.DropTable(
            name: "PersistedGrants");

        migrationBuilder.DropTable(
            name: "ServerSideSessions");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DeviceCodes",
            columns: table => new
            {
                UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
            });

        migrationBuilder.CreateTable(
            name: "Keys",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DataProtected = table.Column<bool>(type: "bit", nullable: false),
                IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                Version = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Keys", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PersistedGrants",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersistedGrants", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ServerSideSessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Renewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                Scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                SubjectId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ServerSideSessions", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_DeviceCode",
            table: "DeviceCodes",
            column: "DeviceCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_Expiration",
            table: "DeviceCodes",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_Keys_Use",
            table: "Keys",
            column: "Use");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_ConsumedTime",
            table: "PersistedGrants",
            column: "ConsumedTime");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_Expiration",
            table: "PersistedGrants",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_Key",
            table: "PersistedGrants",
            column: "Key",
            unique: true,
            filter: "[Key] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_ClientId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "ClientId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_SessionId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "SessionId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_ServerSideSessions_DisplayName",
            table: "ServerSideSessions",
            column: "DisplayName");

        migrationBuilder.CreateIndex(
            name: "IX_ServerSideSessions_Expires",
            table: "ServerSideSessions",
            column: "Expires");

        migrationBuilder.CreateIndex(
            name: "IX_ServerSideSessions_Key",
            table: "ServerSideSessions",
            column: "Key",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ServerSideSessions_SessionId",
            table: "ServerSideSessions",
            column: "SessionId");

        migrationBuilder.CreateIndex(
            name: "IX_ServerSideSessions_SubjectId",
            table: "ServerSideSessions",
            column: "SubjectId");
    }
}
