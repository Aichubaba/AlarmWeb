using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorApp2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ip = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    nodeid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipmentsummaries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pvn = table.Column<int>(type: "integer", nullable: true),
                    nodeid = table.Column<int>(type: "integer", nullable: true),
                    fd = table.Column<string>(type: "text", nullable: true),
                    rubezh = table.Column<string>(type: "text", nullable: true),
                    equipment = table.Column<string>(type: "text", nullable: true),
                    equipmenttype = table.Column<string>(type: "text", nullable: true),
                    bu = table.Column<string>(type: "text", nullable: true),
                    addressip = table.Column<string>(type: "text", nullable: true),
                    network = table.Column<string>(type: "text", nullable: true),
                    gateway = table.Column<string>(type: "text", nullable: true),
                    mask = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipmentsummaries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    ticketid = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Group = table.Column<string>(type: "text", nullable: true),
                    pvnnumber = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    modelname = table.Column<string>(type: "text", nullable: true),
                    ignoredintask = table.Column<bool>(type: "boolean", nullable: true),
                    serialfd = table.Column<string>(type: "text", nullable: true),
                    fdip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nodes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reportid = table.Column<int>(type: "integer", nullable: true),
                    Group = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    pvn_number = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    node_id = table.Column<int>(type: "integer", nullable: true),
                    model_name = table.Column<string>(type: "text", nullable: true),
                    serial_fd = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    temperature = table.Column<double>(type: "double precision", nullable: true),
                    available = table.Column<bool>(type: "boolean", nullable: true),
                    errors = table.Column<bool>(type: "boolean", nullable: true),
                    errors_description = table.Column<string>(type: "text", nullable: true),
                    uptime = table.Column<int>(type: "integer", nullable: true),
                    downtime = table.Column<int>(type: "integer", nullable: true),
                    report_counter = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: true),
                    uptimehr = table.Column<string>(type: "text", nullable: true),
                    downtimehr = table.Column<string>(type: "text", nullable: true),
                    ping_at = table.Column<long>(type: "bigint", nullable: true),
                    started_at = table.Column<long>(type: "bigint", nullable: true),
                    disconnected_at = table.Column<long>(type: "bigint", nullable: true),
                    appstarted_at = table.Column<long>(type: "bigint", nullable: true),
                    queue_quantity = table.Column<int>(type: "integer", nullable: true),
                    last_report_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reportstats",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numberreport = table.Column<int>(type: "integer", nullable: true),
                    uploaded_at = table.Column<long>(type: "bigint", nullable: true),
                    countrecords = table.Column<int>(type: "integer", nullable: true),
                    processed = table.Column<bool>(type: "boolean", nullable: true),
                    processed_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportstats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ticket_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticketid = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    performed_by = table.Column<string>(type: "text", nullable: true),
                    performed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_histories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lastupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    closedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ticketstatus = table.Column<string>(type: "text", nullable: true),
                    nodeid = table.Column<int>(type: "integer", nullable: true),
                    available = table.Column<bool>(type: "boolean", nullable: true),
                    temperature = table.Column<double>(type: "double precision", nullable: true),
                    errors = table.Column<bool>(type: "boolean", nullable: true),
                    typeerror = table.Column<string>(type: "text", nullable: true),
                    errorsdescription = table.Column<string>(type: "text", nullable: true),
                    startedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    disconnectedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    queueqty = table.Column<int>(type: "integer", nullable: true),
                    lastreportat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    manualstopped = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nodeschemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nodeid = table.Column<int>(type: "integer", nullable: true),
                    placecode = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    schemenumber = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nodeschemas", x => x.id);
                    table.ForeignKey(
                        name: "FK_nodeschemas_nodes_nodeid",
                        column: x => x.nodeid,
                        principalTable: "nodes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_nodeschemas_nodeid",
                table: "nodeschemas",
                column: "nodeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cams");

            migrationBuilder.DropTable(
                name: "equipmentsummaries");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "nodeschemas");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "reportstats");

            migrationBuilder.DropTable(
                name: "ticket_histories");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "nodes");
        }
    }
}
