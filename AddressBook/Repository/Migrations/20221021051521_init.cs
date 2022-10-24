using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefSets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    set = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RefTerms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTerms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    last_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.UniqueConstraint("AK_user_user_name", x => x.user_name);
                });

            migrationBuilder.CreateTable(
                name: "RefSetTerm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ref_set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ref_term_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSetTerm", x => x.id);
                    table.ForeignKey(
                        name: "FK_RefSetTerm_RefSets_ref_set_id",
                        column: x => x.ref_set_id,
                        principalTable: "RefSets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefSetTerm_RefTerms_ref_term_id",
                        column: x => x.ref_term_id,
                        principalTable: "RefTerms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressBooks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    last_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBooks", x => x.id);
                    table.ForeignKey(
                        name: "FK_AddressBooks_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    line_1 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    line_2 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    city = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    state_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    address_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Addresses_AddressBooks_address_book_id",
                        column: x => x.address_book_id,
                        principalTable: "AddressBooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    download_url = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<decimal>(type: "numeric", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    address_book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assets_AddressBooks_address_book_id",
                        column: x => x.address_book_id,
                        principalTable: "AddressBooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email_address = table.Column<string>(type: "text", nullable: false),
                    email_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email", x => x.id);
                    table.ForeignKey(
                        name: "FK_email_AddressBooks_address_book_id",
                        column: x => x.address_book_id,
                        principalTable: "AddressBooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_email_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    phone_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Phones_AddressBooks_address_book_id",
                        column: x => x.address_book_id,
                        principalTable: "AddressBooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBooks_user_id",
                table: "AddressBooks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_address_book_id",
                table: "Addresses",
                column: "address_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_user_id",
                table: "Addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_address_book_id",
                table: "Assets",
                column: "address_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_user_id",
                table: "Assets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_email_address_book_id",
                table: "email",
                column: "address_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_email_user_id",
                table: "email",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_address_book_id",
                table: "Phones",
                column: "address_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_user_id",
                table: "Phones",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefSetTerm_ref_set_id",
                table: "RefSetTerm",
                column: "ref_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefSetTerm_ref_term_id",
                table: "RefSetTerm",
                column: "ref_term_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "RefSetTerm");

            migrationBuilder.DropTable(
                name: "AddressBooks");

            migrationBuilder.DropTable(
                name: "RefSets");

            migrationBuilder.DropTable(
                name: "RefTerms");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
