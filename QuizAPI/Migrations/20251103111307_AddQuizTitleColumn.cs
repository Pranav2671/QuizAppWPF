using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizTitleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "TopicTitle",
                table: "Quizzes",
                newName: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_TopicId",
                table: "Quizzes",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Topics_TopicId",
                table: "Quizzes",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Topics_TopicId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_TopicId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Quizzes",
                newName: "TopicTitle");

            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
