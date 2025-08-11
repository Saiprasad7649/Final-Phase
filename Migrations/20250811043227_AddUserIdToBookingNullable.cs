using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equinox.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToBookingNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_EquinoxClasses_EquinoxClassId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_ClassCategories_ClassCategoryId",
                table: "EquinoxClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_Clubs_ClubId",
                table: "EquinoxClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_Users_UserId",
                table: "EquinoxClasses");

            migrationBuilder.DeleteData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "EquinoxClassId", "UserId" },
                values: new object[] { 1, null });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "EquinoxClassId", "UserId" },
                values: new object[,]
                {
                    { 3, 2, null },
                    { 4, 3, null }
                });

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 1,
                column: "ClassPicture",
                value: "yoga1.jpg");

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 2,
                columns: new[] { "ClassDay", "ClubId", "Name", "Time", "UserId" },
                values: new object[] { "Tuesday", 1, "HIIT Workout", "5 PM – 6 PM", "coach2" });

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 3,
                columns: new[] { "ClassDay", "ClassPicture", "ClubId", "Time", "UserId" },
                values: new object[] { "Wednesday", "cardio1.jpg", 2, "12 PM – 1 PM", "coach3" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_EquinoxClasses_EquinoxClassId",
                table: "Bookings",
                column: "EquinoxClassId",
                principalTable: "EquinoxClasses",
                principalColumn: "EquinoxClassId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_ClassCategories_ClassCategoryId",
                table: "EquinoxClasses",
                column: "ClassCategoryId",
                principalTable: "ClassCategories",
                principalColumn: "ClassCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_Clubs_ClubId",
                table: "EquinoxClasses",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "ClubId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_Users_UserId",
                table: "EquinoxClasses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_EquinoxClasses_EquinoxClassId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_ClassCategories_ClassCategoryId",
                table: "EquinoxClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_Clubs_ClubId",
                table: "EquinoxClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_EquinoxClasses_Users_UserId",
                table: "EquinoxClasses");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                column: "EquinoxClassId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 1,
                column: "ClassPicture",
                value: "yoga2.jpg");

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 2,
                columns: new[] { "ClassDay", "ClubId", "Name", "Time", "UserId" },
                values: new object[] { "Wednesday", 2, "Power HIIT", "6 PM – 7 PM", "coach1" });

            migrationBuilder.UpdateData(
                table: "EquinoxClasses",
                keyColumn: "EquinoxClassId",
                keyValue: 3,
                columns: new[] { "ClassDay", "ClassPicture", "ClubId", "Time", "UserId" },
                values: new object[] { "Friday", "barre-fusion.jpg", 1, "7 AM – 8 AM", "coach2" });

            migrationBuilder.InsertData(
                table: "EquinoxClasses",
                columns: new[] { "EquinoxClassId", "ClassCategoryId", "ClassDay", "ClassPicture", "ClubId", "Name", "Time", "UserId" },
                values: new object[,]
                {
                    { 4, 4, "Saturday", "strength-training.jpg", 2, "Strength Training", "10 AM – 11 AM", "coach3" },
                    { 5, 1, "Sunday", "hatha-yoga.jpg", 1, "Yoga 202", "5 PM – 6 PM", "coach4" },
                    { 6, 1, "Sunday", "boxing-101.jpg", 1, "Power Yoga", "3 PM – 4 PM", "coach5" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_EquinoxClasses_EquinoxClassId",
                table: "Bookings",
                column: "EquinoxClassId",
                principalTable: "EquinoxClasses",
                principalColumn: "EquinoxClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_ClassCategories_ClassCategoryId",
                table: "EquinoxClasses",
                column: "ClassCategoryId",
                principalTable: "ClassCategories",
                principalColumn: "ClassCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_Clubs_ClubId",
                table: "EquinoxClasses",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "ClubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquinoxClasses_Users_UserId",
                table: "EquinoxClasses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
