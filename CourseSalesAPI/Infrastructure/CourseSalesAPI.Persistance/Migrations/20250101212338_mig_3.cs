using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSalesAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseCourseImageFile",
                columns: table => new
                {
                    CourseImageFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseImageFile", x => new { x.CourseImageFilesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseCourseImageFile_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseImageFile_Files_CourseImageFilesId",
                        column: x => x.CourseImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseImageFile_CoursesId",
                table: "CourseCourseImageFile",
                column: "CoursesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseImageFile");
        }
    }
}
