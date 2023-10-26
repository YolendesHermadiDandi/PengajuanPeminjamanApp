using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class updaterelasinotificationtorequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "request_guid",
                table: "tb_m_notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_notification_request_guid",
                table: "tb_m_notification",
                column: "request_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_notification_tr_m_request_request_guid",
                table: "tb_m_notification",
                column: "request_guid",
                principalTable: "tr_m_request",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_notification_tr_m_request_request_guid",
                table: "tb_m_notification");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_notification_request_guid",
                table: "tb_m_notification");

            migrationBuilder.DropColumn(
                name: "request_guid",
                table: "tb_m_notification");
        }
    }
}
