using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRepository.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPatient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPatient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPatientProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    problem = table.Column<string>(nullable: true),
                    PatientAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPatientProblem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPatientProblem_tblPatient_PatientAddressId",
                        column: x => x.PatientAddressId,
                        principalTable: "tblPatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPatientTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treatmentName = table.Column<string>(nullable: true),
                    treatmentDose = table.Column<string>(nullable: true),
                    PatientProblemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPatientTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPatientTreatment_tblPatientProblem_PatientProblemId",
                        column: x => x.PatientProblemId,
                        principalTable: "tblPatientProblem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPatientProblem_PatientAddressId",
                table: "tblPatientProblem",
                column: "PatientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPatientTreatment_PatientProblemId",
                table: "tblPatientTreatment",
                column: "PatientProblemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPatientTreatment");

            migrationBuilder.DropTable(
                name: "tblPatientProblem");

            migrationBuilder.DropTable(
                name: "tblPatient");
        }
    }
}
