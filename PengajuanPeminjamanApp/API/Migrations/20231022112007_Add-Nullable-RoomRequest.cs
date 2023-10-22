using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddNullableRoomRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request");

            migrationBuilder.AlterColumn<Guid>(
                name: "room_guid",
                table: "tr_m_request",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                unique: true,
                filter: "[room_guid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request");

            migrationBuilder.AlterColumn<Guid>(
                name: "room_guid",
                table: "tr_m_request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tr_m_request_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tr_m_request_tb_m_rooms_room_guid",
                table: "tr_m_request",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
