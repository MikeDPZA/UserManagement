using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "um_group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "um_permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "um_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "um_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserIdentifier = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "um_role_group",
                columns: table => new
                {
                    RoleGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_role_group", x => x.RoleGroupId);
                    table.ForeignKey(
                        name: "FK_um_role_group_um_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "um_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_um_role_group_um_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "um_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "um_role_permission",
                columns: table => new
                {
                    RolePermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_role_permission", x => x.RolePermissionId);
                    table.ForeignKey(
                        name: "FK_um_role_permission_um_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "um_permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_um_role_permission_um_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "um_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "um_user_group",
                columns: table => new
                {
                    UserGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_um_user_group", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_um_user_group_um_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "um_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_um_user_group_um_user_UserId",
                        column: x => x.UserId,
                        principalTable: "um_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_um_role_group_GroupId",
                table: "um_role_group",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_um_role_group_RoleId",
                table: "um_role_group",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_um_role_permission_PermissionId",
                table: "um_role_permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_um_role_permission_RoleId",
                table: "um_role_permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_um_user_group_GroupId",
                table: "um_user_group",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_um_user_group_UserId",
                table: "um_user_group",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "um_role_group");

            migrationBuilder.DropTable(
                name: "um_role_permission");

            migrationBuilder.DropTable(
                name: "um_user_group");

            migrationBuilder.DropTable(
                name: "um_permission");

            migrationBuilder.DropTable(
                name: "um_role");

            migrationBuilder.DropTable(
                name: "um_group");

            migrationBuilder.DropTable(
                name: "um_user");
        }
    }
}
