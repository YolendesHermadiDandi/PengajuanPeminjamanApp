using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class initialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    nik = table.Column<string>(type: "nchar(10)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_fasility",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_fasility", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_rooms",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_rooms", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tr_m_request",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_m_request", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tr_m_request_tb_m_employee_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tr_m_request_tb_m_rooms_room_guid",
                        column: x => x.room_guid,
                        principalTable: "tb_m_rooms",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account_role",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account_role", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_role_tb_m_account_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_account",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_account_role_tb_m_role_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_notification",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    massage = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    is_seen = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_notification", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_notification_tb_m_account_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_account",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_list_fasility",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    request_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fasility_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_fasility = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_list_fasility", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_list_fasility_tb_m_fasility_fasility_guid",
                        column: x => x.fasility_guid,
                        principalTable: "tb_m_fasility",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_list_fasility_tr_m_request_request_guid",
                        column: x => x.request_guid,
                        principalTable: "tr_m_request",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_role_account_guid",
                table: "tb_m_account_role",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_role_role_guid",
                table: "tb_m_account_role",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_email",
                table: "tb_m_employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_nik",
                table: "tb_m_employee",
                column: "nik",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_phone_number",
                table: "tb_m_employee",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_list_fasility_fasility_guid",
                table: "tb_m_list_fasility",
                column: "fasility_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_list_fasility_request_guid",
                table: "tb_m_list_fasility",
                column: "request_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_account_guid",
                table: "tb_m_notification",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tr_m_request_employee_guid",
                table: "tr_m_request",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account_role");

            migrationBuilder.DropTable(
                name: "tb_m_list_fasility");

            migrationBuilder.DropTable(
                name: "tb_m_notification");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_fasility");

            migrationBuilder.DropTable(
                name: "tr_m_request");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_rooms");

            migrationBuilder.DropTable(
                name: "tb_m_employee");
        }
    }
}
