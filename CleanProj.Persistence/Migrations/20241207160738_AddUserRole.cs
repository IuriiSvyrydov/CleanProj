using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanProj.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    normalizedname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    concurrencystamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "application_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    normalizedusername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    normalizedemail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    emailconfirmed = table.Column<bool>(type: "bit", nullable: false),
                    passwordhash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    securitystamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    concurrencystamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phonenumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    phonenumberconfirmed = table.Column<bool>(type: "bit", nullable: false),
                    twofactorenabled = table.Column<bool>(type: "bit", nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    lockoutenabled = table.Column<bool>(type: "bit", nullable: false),
                    accessfailedcount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roleclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    claimtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claimvalue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roleclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_roleclaims_application_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "application_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    claimtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claimvalue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_user_claims_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_user_logins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    providerkey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    providerdisplayname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user_logins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_application_user_logins_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_user_roles",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user_roles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_application_user_roles_application_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "application_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_application_user_roles_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_user_tokens",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loginprovider = table.Column<string>(type: "nvarchar(191)", maxLength: 191, nullable: false),
                    name = table.Column<string>(type: "nvarchar(191)", maxLength: 191, nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user_tokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_application_user_tokens_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_social_media_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    socialmediatype = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_social_media_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_social_media_accounts_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prompts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isactive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    imageurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    likecount = table.Column<int>(type: "int", nullable: false),
                    categoryid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prompts", x => x.id);
                    table.ForeignKey(
                        name: "fk_prompts_categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "placeholders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    propmptid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_placeholders", x => x.id);
                    table.ForeignKey(
                        name: "fk_placeholders_prompts_propmptid",
                        column: x => x.propmptid,
                        principalTable: "prompts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prompt_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    promptid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoryid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prompt_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_prompt_categories_categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prompt_categories_prompts_promptid",
                        column: x => x.promptid,
                        principalTable: "prompts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_favorite_prompts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    promptid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_favorite_prompts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_favorite_prompts_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_favorite_prompts_prompts_promptid",
                        column: x => x.promptid,
                        principalTable: "prompts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_like_prompt",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    promptid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_like_prompt", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_like_prompt_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_like_prompt_prompts_promptid",
                        column: x => x.promptid,
                        principalTable: "prompts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_prompt_comment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    promptid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parentcommentid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modifiedbyuserid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_prompt_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_prompt_comment_application_users_userid",
                        column: x => x.userid,
                        principalTable: "application_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_prompt_comment_prompts_promptid",
                        column: x => x.promptid,
                        principalTable: "prompts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_prompt_comment_user_prompt_comment_parentcommentid",
                        column: x => x.parentcommentid,
                        principalTable: "user_prompt_comment",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "application_roles",
                columns: new[] { "id", "concurrencystamp", "name", "normalizedname" },
                values: new object[,]
                {
                    { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "8d04dce2-969a-435d-bba4-df3f325983dc", "Admin", "ADMIN" },
                    { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), "cfd242d3-2107-4563-b2a4-15383e683964", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "application_users",
                columns: new[] { "id", "accessfailedcount", "concurrencystamp", "createat", "createdbyuserid", "email", "emailconfirmed", "lockoutenabled", "lockoutend", "modifiedat", "modifiedbyuserid", "normalizedemail", "normalizedusername", "passwordhash", "phonenumber", "phonenumberconfirmed", "securitystamp", "twofactorenabled", "username" },
                values: new object[] { new Guid("0b9bb71a-feb6-45cc-9784-7401d8ae85b8"), 0, "0B9BB71A-FEB6-45CC-9784-7401D8AE85B4", new DateTimeOffset(new DateTime(2024, 12, 7, 16, 7, 38, 195, DateTimeKind.Unspecified).AddTicks(8174), new TimeSpan(0, 0, 0, 0, 0)), "0b9bb71a-feb6-45cc-9784-7401d8ae85b8", "sviridov288@gmail.com", true, false, null, null, null, "SVIRIDOV288@GMAIL.COM", "FERRO5", "AQAAAAIAAYagAAAAEIk0FkSJv+gieNGqzARXvHfpQ+Qus4kZenXTpRlgM3IU89HNsz8wBYkmFFqsjSKRTA==", null, false, "0B9BB71A-FEB6-45CC-9784-7401D8AE85B9", false, "ferro5" });

            migrationBuilder.InsertData(
                table: "application_user_roles",
                columns: new[] { "roleid", "userid" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("0b9bb71a-feb6-45cc-9784-7401d8ae85b8") });

            migrationBuilder.CreateIndex(
                name: "rolenameindex",
                table: "application_roles",
                column: "normalizedname",
                unique: true,
                filter: "[normalizedname] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_claims_userid",
                table: "application_user_claims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_logins_userid",
                table: "application_user_logins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_roles_roleid",
                table: "application_user_roles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "emailindex",
                table: "application_users",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "ix_application_users_email",
                table: "application_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "usernameindex",
                table: "application_users",
                column: "normalizedusername",
                unique: true,
                filter: "[normalizedusername] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_placeholders_propmptid",
                table: "placeholders",
                column: "propmptid");

            migrationBuilder.CreateIndex(
                name: "ix_prompt_categories_categoryid",
                table: "prompt_categories",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "ix_prompt_categories_promptid_categoryid",
                table: "prompt_categories",
                columns: new[] { "promptid", "categoryid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prompts_categoryid",
                table: "prompts",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "ix_roleclaims_roleid",
                table: "roleclaims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_user_favorite_prompts_promptid",
                table: "user_favorite_prompts",
                column: "promptid");

            migrationBuilder.CreateIndex(
                name: "ix_user_favorite_prompts_userid_promptid",
                table: "user_favorite_prompts",
                columns: new[] { "userid", "promptid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_like_prompt_promptid",
                table: "user_like_prompt",
                column: "promptid");

            migrationBuilder.CreateIndex(
                name: "ix_user_like_prompt_userid",
                table: "user_like_prompt",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompt_comment_parentcommentid",
                table: "user_prompt_comment",
                column: "parentcommentid");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompt_comment_promptid",
                table: "user_prompt_comment",
                column: "promptid");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompt_comment_userid",
                table: "user_prompt_comment",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_user_social_media_accounts_userid_socialmediatype",
                table: "user_social_media_accounts",
                columns: new[] { "userid", "socialmediatype" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_user_claims");

            migrationBuilder.DropTable(
                name: "application_user_logins");

            migrationBuilder.DropTable(
                name: "application_user_roles");

            migrationBuilder.DropTable(
                name: "application_user_tokens");

            migrationBuilder.DropTable(
                name: "placeholders");

            migrationBuilder.DropTable(
                name: "prompt_categories");

            migrationBuilder.DropTable(
                name: "roleclaims");

            migrationBuilder.DropTable(
                name: "user_favorite_prompts");

            migrationBuilder.DropTable(
                name: "user_like_prompt");

            migrationBuilder.DropTable(
                name: "user_prompt_comment");

            migrationBuilder.DropTable(
                name: "user_social_media_accounts");

            migrationBuilder.DropTable(
                name: "application_roles");

            migrationBuilder.DropTable(
                name: "prompts");

            migrationBuilder.DropTable(
                name: "application_users");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
