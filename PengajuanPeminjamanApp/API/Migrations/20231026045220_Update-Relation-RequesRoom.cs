using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UpdateRelationRequesRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request");

            migrationBuilder.CreateIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request",
                column: "room_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request");

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
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                unique: true,
                filter: "[room_guid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_account_guid",
                table: "tb_m_notification",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_request_guid",
                table: "tb_m_notification",
                column: "request_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid");
        }
    }
}
