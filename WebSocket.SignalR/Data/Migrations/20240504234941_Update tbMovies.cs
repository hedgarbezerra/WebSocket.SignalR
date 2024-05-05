using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebSocket.SignalR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetbMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("15e74541-70f5-452a-9421-a02b86b42cf7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3780e3e1-06b3-4d3b-aae4-18385d99d15e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("74631e9a-cb50-4e47-b185-36535c6813d4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("76db2525-314d-4b91-bab2-e1390f09c68f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7ff7e4ea-7abd-41d8-849b-a4a0ffdf5414"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a2d4b3e2-26fa-4930-b23e-d7c8fa1b61a9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a5d151b9-1788-4aa5-b983-a1fce161e756"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("1f2c1dc5-7ed9-4083-8045-1d1d6b9bc46d"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("1f95fcc5-47f7-4e7f-96e0-81eb4b2792e0"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("30abacb2-eaaa-416e-adc9-0f30be118349"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("373f39c1-bf5f-42bc-abb0-6c60261cfca6"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("41eb7888-0655-43f1-847a-bcb520c32dfe"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("4a1e4fc1-cd3e-40ae-a376-408076eb9102"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("4e4bd0ca-4708-4ba8-93ef-2a9aae799355"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("4e97bc46-a1ba-410f-9c6d-f3ad6513ed19"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("5922d7c3-feba-4169-ab31-f861a7b722e2"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("5ff19dfe-b442-4210-9e4b-f7e6928dea06"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("710fa2f3-cb3b-44c8-8074-65e7a02541dd"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("a21f6176-1a04-4f36-b0c4-32ae7640414d"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("a4e4559b-95fb-4be9-9408-c5bd22805be2"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("b9d16f5e-a50c-4ba9-8e57-e29c58186959"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("d50831d2-8e20-4318-871f-bdaf524c277b"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("da9b786e-4210-459f-a894-99bbe3741256"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("ea0bd1c8-2c2a-4600-8b45-947d73a6cd1b"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("edea3885-2800-4ec8-8efe-b401a23396f5"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("efcb76fe-3605-4be5-8c99-27474531f397"));

            migrationBuilder.DeleteData(
                table: "RoomSeats",
                keyColumn: "Id",
                keyValue: new Guid("fd2c22cc-c46d-48f6-95e9-0b35c896c0ea"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("0acdd5d6-a868-465b-ac56-2a0a84a71e8f"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b0da2a16-cf90-4952-9efa-56f36016d2c5"));

            migrationBuilder.AlterColumn<int>(
                name: "Classification",
                table: "Movies",
                type: "int",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Classification",
                table: "Movies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255);

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
        }
    }
}
