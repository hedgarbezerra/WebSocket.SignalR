using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebSocket.SignalR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetbSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Rooms_MovieId",
                table: "Sessions");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0d0d0417-b0f1-4c42-a950-caf741e5a521"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("131387f8-bc2e-4838-b2ec-948c6dd6c051"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("150b2a24-6ca9-4a84-8ae4-d884589ac658"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("25aab2d0-d389-4661-9b97-3b4b63bd9fd1"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9828e527-6a0f-4e6b-962a-384bd4759452"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("cbc7a9f2-28f7-4b35-bff9-957162cbbc31"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d7c7713c-a943-4623-86ce-6659d0ae5e4d"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("0850d9d5-ca1b-4863-b93d-0b3f4e0dca29"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("1732b929-eb07-46aa-a65e-9d26e9482403"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("2c9a0def-6529-49c0-a063-0a16585e8c9d"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("3ce71217-6634-4fa8-bcd8-f64ee3e3085f"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("438a8755-0327-40ec-835f-feb443e6c2d7"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("43a769c2-d328-4ae9-9cfb-154039692fc7"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("596695c5-70c9-4fa4-b0cb-d7b538308fd2"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("5e628ce9-f90c-4cd8-bf0e-8ad34ba11191"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("62039173-d7e1-48a1-905c-6ab3388d6373"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("7c528416-2237-4fc3-98eb-31a73f859502"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("84aa120c-c9b2-4b74-a03e-f008eb1d9fda"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("8507c188-7de4-4fb0-a047-c7eb1a67ebd3"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("8cc906a2-fbd2-4763-8216-27cb46505974"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("a1349ea5-a2a7-43e1-be56-b2bc7cc0265b"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("a5665c3a-3469-4cb2-9008-dab7de7cfe62"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("c796a0f9-f241-4afc-8668-281c43f3c0d2"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("d359ca09-0e23-479d-abf3-174d81f4999b"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("d7fe6785-2570-4cf8-a7ea-3b13d1b749f0"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("e23638f3-a8be-4255-872a-891a05488676"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("ec245938-d1d1-4803-8425-98a6f1526d14"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("54396e13-d0e5-4096-923b-7856990d63ae"), "Ação" },
                    { new Guid("65289e59-11b8-4208-a3d8-41f294d2879c"), "Aventura" },
                    { new Guid("9418fe5a-27a9-44f9-87f5-c3b3ff87baa7"), "Thriller" },
                    { new Guid("a0839071-dd25-417e-bdae-dfdcb9b3df99"), "Romance" },
                    { new Guid("a2c23ed1-ced5-4762-80e0-77c0707ff4df"), "Comédia" },
                    { new Guid("ee57d9e1-f4c1-4848-bd2c-502e7dd2d9df"), "Animação" },
                    { new Guid("f8417760-2d7e-4df5-bc75-fc2c898a2686"), "Terror" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), "Sala Pequena IMAX", 3 },
                    { new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), "Sala padrão 2D", 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomSeats",
                columns: new[] { "Id", "Column", "IsHandicapAccessible", "RoomId", "Row" },
                values: new object[,]
                {
                    { new Guid("076d1b46-ccbd-4bcb-82a0-97f28109d642"), 1, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 0 },
                    { new Guid("28dd8450-49c7-4cc6-8190-a51d7b0f8105"), 1, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 1 },
                    { new Guid("29c213f6-607a-4bf2-9d19-7a71b1e13370"), 2, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 0 },
                    { new Guid("60ef0c69-76b9-4e8a-a10b-1a95689095b4"), 1, true, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 2 },
                    { new Guid("685c7952-357d-4ee3-a10f-72fe8b5b6114"), 0, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 1 },
                    { new Guid("769b044f-9ad1-44bf-9f77-7f2ea12f875a"), 0, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 2 },
                    { new Guid("78dbc3b3-e958-4b7a-a515-c592c3185f39"), 3, true, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 1 },
                    { new Guid("7f459720-7820-45aa-a1e5-9a25e9e24dad"), 3, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 2 },
                    { new Guid("822e9bd0-1542-4ca5-95c8-8e7c087ff364"), 2, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 0 },
                    { new Guid("88943a95-f6ba-477e-8692-904b65a6a8db"), 0, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 0 },
                    { new Guid("8cfc42ea-3cc5-49c0-b35f-1df6b02196be"), 1, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 0 },
                    { new Guid("9087ea22-a463-4207-ba06-f957c758e1cc"), 3, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 0 },
                    { new Guid("b793dd6a-f957-45f7-bd0f-9cf4c43f4f52"), 2, true, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 1 },
                    { new Guid("d5cecf86-32ab-4b80-bad4-5d46bc0dbf6f"), 3, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 0 },
                    { new Guid("dbd38ba3-f6b6-4a18-b2e1-a111e96e266c"), 2, true, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 2 },
                    { new Guid("dd8eff28-f63f-4711-b732-45ec3d851f8d"), 2, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 1 },
                    { new Guid("e5702a70-66fb-4e3c-9a87-97346f228b0f"), 1, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 1 },
                    { new Guid("e92fc480-a08f-4bee-98c4-332b1837d800"), 0, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 0 },
                    { new Guid("eeb95a5e-5e29-4158-a332-46560c04c1a4"), 0, false, new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"), 1 },
                    { new Guid("fed43064-ba4b-4b37-9110-5459f6e4e702"), 3, false, new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RoomId",
                table: "Sessions",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Rooms_RoomId",
                table: "Sessions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Rooms_RoomId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_RoomId",
                table: "Sessions");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("54396e13-d0e5-4096-923b-7856990d63ae"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("65289e59-11b8-4208-a3d8-41f294d2879c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9418fe5a-27a9-44f9-87f5-c3b3ff87baa7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a0839071-dd25-417e-bdae-dfdcb9b3df99"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a2c23ed1-ced5-4762-80e0-77c0707ff4df"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ee57d9e1-f4c1-4848-bd2c-502e7dd2d9df"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f8417760-2d7e-4df5-bc75-fc2c898a2686"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("076d1b46-ccbd-4bcb-82a0-97f28109d642"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("28dd8450-49c7-4cc6-8190-a51d7b0f8105"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("29c213f6-607a-4bf2-9d19-7a71b1e13370"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("60ef0c69-76b9-4e8a-a10b-1a95689095b4"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("685c7952-357d-4ee3-a10f-72fe8b5b6114"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("769b044f-9ad1-44bf-9f77-7f2ea12f875a"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("78dbc3b3-e958-4b7a-a515-c592c3185f39"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("7f459720-7820-45aa-a1e5-9a25e9e24dad"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("822e9bd0-1542-4ca5-95c8-8e7c087ff364"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("88943a95-f6ba-477e-8692-904b65a6a8db"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("8cfc42ea-3cc5-49c0-b35f-1df6b02196be"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("9087ea22-a463-4207-ba06-f957c758e1cc"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("b793dd6a-f957-45f7-bd0f-9cf4c43f4f52"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("d5cecf86-32ab-4b80-bad4-5d46bc0dbf6f"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("dbd38ba3-f6b6-4a18-b2e1-a111e96e266c"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("dd8eff28-f63f-4711-b732-45ec3d851f8d"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("e5702a70-66fb-4e3c-9a87-97346f228b0f"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("e92fc480-a08f-4bee-98c4-332b1837d800"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("eeb95a5e-5e29-4158-a332-46560c04c1a4"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("fed43064-ba4b-4b37-9110-5459f6e4e702"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a1ff1623-2add-415a-957a-604d6f7c1094"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("bc90e7eb-efb4-4ee4-933d-a3772de6c0ba"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d0d0417-b0f1-4c42-a950-caf741e5a521"), "Animação" },
                    { new Guid("131387f8-bc2e-4838-b2ec-948c6dd6c051"), "Terror" },
                    { new Guid("150b2a24-6ca9-4a84-8ae4-d884589ac658"), "Romance" },
                    { new Guid("25aab2d0-d389-4661-9b97-3b4b63bd9fd1"), "Ação" },
                    { new Guid("9828e527-6a0f-4e6b-962a-384bd4759452"), "Comédia" },
                    { new Guid("cbc7a9f2-28f7-4b35-bff9-957162cbbc31"), "Thriller" },
                    { new Guid("d7c7713c-a943-4623-86ce-6659d0ae5e4d"), "Aventura" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), "Sala Pequena IMAX", 3 },
                    { new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), "Sala padrão 2D", 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomSeats",
                columns: new[] { "Id", "Column", "IsHandicapAccessible", "RoomId", "Row" },
                values: new object[,]
                {
                    { new Guid("0850d9d5-ca1b-4863-b93d-0b3f4e0dca29"), 3, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 0 },
                    { new Guid("1732b929-eb07-46aa-a65e-9d26e9482403"), 2, true, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 2 },
                    { new Guid("2c9a0def-6529-49c0-a063-0a16585e8c9d"), 3, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 0 },
                    { new Guid("3ce71217-6634-4fa8-bcd8-f64ee3e3085f"), 0, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 0 },
                    { new Guid("438a8755-0327-40ec-835f-feb443e6c2d7"), 0, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 0 },
                    { new Guid("43a769c2-d328-4ae9-9cfb-154039692fc7"), 3, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 2 },
                    { new Guid("596695c5-70c9-4fa4-b0cb-d7b538308fd2"), 1, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 1 },
                    { new Guid("5e628ce9-f90c-4cd8-bf0e-8ad34ba11191"), 3, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 1 },
                    { new Guid("62039173-d7e1-48a1-905c-6ab3388d6373"), 3, true, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 1 },
                    { new Guid("7c528416-2237-4fc3-98eb-31a73f859502"), 1, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 0 },
                    { new Guid("84aa120c-c9b2-4b74-a03e-f008eb1d9fda"), 2, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 0 },
                    { new Guid("8507c188-7de4-4fb0-a047-c7eb1a67ebd3"), 2, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 0 },
                    { new Guid("8cc906a2-fbd2-4763-8216-27cb46505974"), 2, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 1 },
                    { new Guid("a1349ea5-a2a7-43e1-be56-b2bc7cc0265b"), 0, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 1 },
                    { new Guid("a5665c3a-3469-4cb2-9008-dab7de7cfe62"), 0, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 1 },
                    { new Guid("c796a0f9-f241-4afc-8668-281c43f3c0d2"), 2, true, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 1 },
                    { new Guid("d359ca09-0e23-479d-abf3-174d81f4999b"), 0, false, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 2 },
                    { new Guid("d7fe6785-2570-4cf8-a7ea-3b13d1b749f0"), 1, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 0 },
                    { new Guid("e23638f3-a8be-4255-872a-891a05488676"), 1, false, new Guid("28ec0f7e-6c9a-4ffa-9098-41181eacd861"), 1 },
                    { new Guid("ec245938-d1d1-4803-8425-98a6f1526d14"), 1, true, new Guid("c8e4d09f-926f-41f3-abbc-9d16690599b4"), 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Rooms_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
