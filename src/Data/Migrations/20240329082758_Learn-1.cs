using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Learn1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnRelation_Classes_ClassId",
                table: "LearnRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_LearnRelation_Lessons_LeassonId",
                table: "LearnRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LearnRelation",
                table: "LearnRelation");

            migrationBuilder.RenameTable(
                name: "LearnRelation",
                newName: "LearnRelations");

            migrationBuilder.RenameIndex(
                name: "IX_LearnRelation_LeassonId",
                table: "LearnRelations",
                newName: "IX_LearnRelations_LeassonId");

            migrationBuilder.RenameIndex(
                name: "IX_LearnRelation_ClassId",
                table: "LearnRelations",
                newName: "IX_LearnRelations_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LearnRelations",
                table: "LearnRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnRelations_Classes_ClassId",
                table: "LearnRelations",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnRelations_Lessons_LeassonId",
                table: "LearnRelations",
                column: "LeassonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnRelations_Classes_ClassId",
                table: "LearnRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_LearnRelations_Lessons_LeassonId",
                table: "LearnRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LearnRelations",
                table: "LearnRelations");

            migrationBuilder.RenameTable(
                name: "LearnRelations",
                newName: "LearnRelation");

            migrationBuilder.RenameIndex(
                name: "IX_LearnRelations_LeassonId",
                table: "LearnRelation",
                newName: "IX_LearnRelation_LeassonId");

            migrationBuilder.RenameIndex(
                name: "IX_LearnRelations_ClassId",
                table: "LearnRelation",
                newName: "IX_LearnRelation_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LearnRelation",
                table: "LearnRelation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnRelation_Classes_ClassId",
                table: "LearnRelation",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnRelation_Lessons_LeassonId",
                table: "LearnRelation",
                column: "LeassonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
