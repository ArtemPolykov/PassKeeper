using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassKeePerLib.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "enterprises",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    enterprise_name = table.Column<string>(type: "varchar(200)", nullable: false),
                    enterprise_address = table.Column<string>(type: "varchar(75)", nullable: true),
                    enterprise_email = table.Column<string>(type: "varchar(75)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wallet_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    enterprise_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_id, x.enterprise_id });
                    table.ForeignKey(
                        name: "workers_ibfk_2",
                        column: x => x.enterprise_id,
                        principalTable: "enterprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workers_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wallet_id = table.Column<int>(nullable: false),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    accounta_ddress = table.Column<string>(type: "varchar(500)", nullable: true),
                    account_login = table.Column<string>(type: "varchar(256)", nullable: true),
                    account_password = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.ForeignKey(
                        name: "accounts_ibfk_1",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enterprise_wallets",
                columns: table => new
                {
                    wallet_id = table.Column<int>(nullable: false),
                    enterprise_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.wallet_id, x.enterprise_id });
                    table.ForeignKey(
                        name: "enterprise_wallets_ibfk_2",
                        column: x => x.enterprise_id,
                        principalTable: "enterprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "enterprise_wallets_ibfk_1",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_wallets",
                columns: table => new
                {
                    wallet_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.wallet_id, x.user_id });
                    table.ForeignKey(
                        name: "personal_wallets_ibfk_2",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "personal_wallets_ibfk_1",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_wallets_approved_users",
                columns: table => new
                {
                    wallet_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.wallet_id, x.user_id });
                    table.ForeignKey(
                        name: "personal_wallets_approved_users_ibfk_2",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "personal_wallets_approved_users_ibfk_1",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "browsing_history",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    account_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_id, x.account_id });
                    table.ForeignKey(
                        name: "browsing_history_ibfk_2",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "browsing_history_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enterprise_wallets_administrators",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    enterprise_id = table.Column<int>(nullable: false),
                    wallet_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_id, x.enterprise_id, x.wallet_id });
                    table.ForeignKey(
                        name: "enterprise_wallets_administrators_ibfk_1",
                        columns: x => new { x.user_id, x.enterprise_id },
                        principalTable: "workers",
                        principalColumns: new[] { "user_id", "enterprise_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "enterprise_wallets_administrators_ibfk_2",
                        columns: x => new { x.wallet_id, x.enterprise_id },
                        principalTable: "enterprise_wallets",
                        principalColumns: new[] { "wallet_id", "enterprise_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enterprise_wallets_approved_workers",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    enterprise_id = table.Column<int>(nullable: false),
                    wallet_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_id, x.enterprise_id, x.wallet_id });
                    table.ForeignKey(
                        name: "enterprise_wallets_approved_workers_ibfk_1",
                        columns: x => new { x.user_id, x.enterprise_id },
                        principalTable: "workers",
                        principalColumns: new[] { "user_id", "enterprise_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "enterprise_wallets_approved_workers_ibfk_2",
                        columns: x => new { x.wallet_id, x.enterprise_id },
                        principalTable: "enterprise_wallets",
                        principalColumns: new[] { "wallet_id", "enterprise_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "accounts",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "account_id",
                table: "browsing_history",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "enterprise_id",
                table: "enterprise_wallets",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "enterprise_wallets",
                column: "wallet_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "enterprise_wallets_administrators",
                columns: new[] { "wallet_id", "enterprise_id" });

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "enterprise_wallets_approved_workers",
                columns: new[] { "wallet_id", "enterprise_id" });

            migrationBuilder.CreateIndex(
                name: "enterprise_address",
                table: "enterprises",
                column: "enterprise_address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "enterprise_email",
                table: "enterprises",
                column: "enterprise_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id",
                table: "personal_wallets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "personal_wallets",
                column: "wallet_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id",
                table: "personal_wallets_approved_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "wallet_id",
                table: "personal_wallets_approved_users",
                columns: new[] { "wallet_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "enterprise_id",
                table: "workers",
                column: "enterprise_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "browsing_history");

            migrationBuilder.DropTable(
                name: "enterprise_wallets_administrators");

            migrationBuilder.DropTable(
                name: "enterprise_wallets_approved_workers");

            migrationBuilder.DropTable(
                name: "personal_wallets");

            migrationBuilder.DropTable(
                name: "personal_wallets_approved_users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "enterprise_wallets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "enterprises");

            migrationBuilder.DropTable(
                name: "wallets");
        }
    }
}
