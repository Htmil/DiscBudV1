using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscBudV1.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invdisc_AspNetUsers_UserId",
                table: "Invdisc");

            migrationBuilder.DropForeignKey(
                name: "FK_Invdisc_Discs_DiscId",
                table: "Invdisc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invdisc",
                table: "Invdisc");

            migrationBuilder.RenameTable(
                name: "Invdisc",
                newName: "invdiscs");

            migrationBuilder.RenameIndex(
                name: "IX_Invdisc_UserId",
                table: "invdiscs",
                newName: "IX_invdiscs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Invdisc_DiscId",
                table: "invdiscs",
                newName: "IX_invdiscs_DiscId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_invdiscs",
                table: "invdiscs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    BagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvdiscId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.BagId);
                    table.ForeignKey(
                        name: "FK_Bags_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bags_invdiscs_InvdiscId",
                        column: x => x.InvdiscId,
                        principalTable: "invdiscs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_InvdiscId",
                table: "Bags",
                column: "InvdiscId");

            migrationBuilder.CreateIndex(
                name: "IX_Bags_UserId",
                table: "Bags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_invdiscs_AspNetUsers_UserId",
                table: "invdiscs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_invdiscs_Discs_DiscId",
                table: "invdiscs",
                column: "DiscId",
                principalTable: "Discs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invdiscs_AspNetUsers_UserId",
                table: "invdiscs");

            migrationBuilder.DropForeignKey(
                name: "FK_invdiscs_Discs_DiscId",
                table: "invdiscs");

            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invdiscs",
                table: "invdiscs");

            migrationBuilder.RenameTable(
                name: "invdiscs",
                newName: "Invdisc");

            migrationBuilder.RenameIndex(
                name: "IX_invdiscs_UserId",
                table: "Invdisc",
                newName: "IX_Invdisc_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_invdiscs_DiscId",
                table: "Invdisc",
                newName: "IX_Invdisc_DiscId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invdisc",
                table: "Invdisc",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invdisc_AspNetUsers_UserId",
                table: "Invdisc",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invdisc_Discs_DiscId",
                table: "Invdisc",
                column: "DiscId",
                principalTable: "Discs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
