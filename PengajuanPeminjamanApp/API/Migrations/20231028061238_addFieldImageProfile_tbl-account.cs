using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addFieldImageProfile_tblaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_notification");

            migrationBuilder.AddColumn<string>(
                name: "img_profile",
                table: "tb_m_account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_profile",
                table: "tb_m_account");

            migrationBuilder.CreateTable(
                name: "tb_m_notification",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    request_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_seen = table.Column<bool>(type: "bit", nullable: false),
                    massage = table.Column<string>(type: "nvarchar(100)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_tb_m_notification_tr_m_request_request_guid",
                        column: x => x.request_guid,
                        principalTable: "tr_m_request",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_account_guid",
                table: "tb_m_notification",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_request_guid",
                table: "tb_m_notification",
                column: "request_guid");
        }
    }
}
