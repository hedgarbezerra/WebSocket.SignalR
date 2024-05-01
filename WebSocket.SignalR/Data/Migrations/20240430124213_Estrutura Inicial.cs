using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebSocket.SignalR.Data.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sinopsys = table.Column<string>(type: "varchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DirectorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    Release = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Starring = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomSeats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false),
                    IsHandicapAccessible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomSeats_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Rooms_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionTakenRoomSeats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionTakenRoomSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionTakenRoomSeats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionTakenRoomSeats_RoomSeats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "RoomSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionTakenRoomSeats_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15e74541-70f5-452a-9421-a02b86b42cf7"), "Terror" },
                    { new Guid("3780e3e1-06b3-4d3b-aae4-18385d99d15e"), "Ação" },
                    { new Guid("74631e9a-cb50-4e47-b185-36535c6813d4"), "Comédia" },
                    { new Guid("76db2525-314d-4b91-bab2-e1390f09c68f"), "Thriller" },
                    { new Guid("7ff7e4ea-7abd-41d8-849b-a4a0ffdf5414"), "Aventura" },
                    { new Guid("a2d4b3e2-26fa-4930-b23e-d7c8fa1b61a9"), "Romance" },
                    { new Guid("a5d151b9-1788-4aa5-b983-a1fce161e756"), "Animação" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), "Sala Pequena IMAX", 3 },
                    { new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), "Sala padrão 2D", 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomSeats",
                columns: new[] { "Id", "Column", "IsHandicapAccessible", "RoomId", "Row" },
                values: new object[,]
                {
                    { new Guid("1f2c1dc5-7ed9-4083-8045-1d1d6b9bc46d"), 0, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 0 },
                    { new Guid("1f95fcc5-47f7-4e7f-96e0-81eb4b2792e0"), 0, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 1 },
                    { new Guid("30abacb2-eaaa-416e-adc9-0f30be118349"), 2, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 0 },
                    { new Guid("373f39c1-bf5f-42bc-abb0-6c60261cfca6"), 1, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 0 },
                    { new Guid("41eb7888-0655-43f1-847a-bcb520c32dfe"), 3, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 2 },
                    { new Guid("4a1e4fc1-cd3e-40ae-a376-408076eb9102"), 1, true, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 2 },
                    { new Guid("4e4bd0ca-4708-4ba8-93ef-2a9aae799355"), 3, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 0 },
                    { new Guid("4e97bc46-a1ba-410f-9c6d-f3ad6513ed19"), 3, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 0 },
                    { new Guid("5922d7c3-feba-4169-ab31-f861a7b722e2"), 0, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 0 },
                    { new Guid("5ff19dfe-b442-4210-9e4b-f7e6928dea06"), 0, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 2 },
                    { new Guid("710fa2f3-cb3b-44c8-8074-65e7a02541dd"), 3, true, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 1 },
                    { new Guid("a21f6176-1a04-4f36-b0c4-32ae7640414d"), 0, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 1 },
                    { new Guid("a4e4559b-95fb-4be9-9408-c5bd22805be2"), 2, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 1 },
                    { new Guid("b9d16f5e-a50c-4ba9-8e57-e29c58186959"), 2, true, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 2 },
                    { new Guid("d50831d2-8e20-4318-871f-bdaf524c277b"), 3, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 1 },
                    { new Guid("da9b786e-4210-459f-a894-99bbe3741256"), 2, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 0 },
                    { new Guid("ea0bd1c8-2c2a-4600-8b45-947d73a6cd1b"), 1, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 1 },
                    { new Guid("edea3885-2800-4ec8-8efe-b401a23396f5"), 2, true, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 1 },
                    { new Guid("efcb76fe-3605-4be5-8c99-27474531f397"), 1, false, new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"), 1 },
                    { new Guid("fd2c22cc-c46d-48f6-95e9-0b35c896c0ea"), 1, false, new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSeats_RoomId",
                table: "RoomSeats",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionTakenRoomSeats_SeatId",
                table: "SessionTakenRoomSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionTakenRoomSeats_SessionId",
                table: "SessionTakenRoomSeats",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionTakenRoomSeats_UserId",
                table: "SessionTakenRoomSeats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropTable(
                name: "SessionTakenRoomSeats");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RoomSeats");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
