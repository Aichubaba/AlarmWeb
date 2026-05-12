using Microsoft.EntityFrameworkCore;
using BlazorApp2.Models;

namespace BlazorApp2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Cam> Cams { get; set; }
        public DbSet<EquipmentSummary> EquipmentSummaries { get; set; }
        public DbSet<ReportStat> ReportStats { get; set; }
        public DbSet<NodeSchema> NodeSchemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Reports
            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("reports");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ReportId).HasColumnName("reportid");
                entity.Property(e => e.Group).HasColumnName("Group");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.PvnNumber).HasColumnName("pvn_number");
                entity.Property(e => e.Location).HasColumnName("location");
                entity.Property(e => e.NodeId).HasColumnName("node_id");
                entity.Property(e => e.ModelName).HasColumnName("model_name");
                entity.Property(e => e.SerialFd).HasColumnName("serial_fd");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
                entity.Property(e => e.Temperature).HasColumnName("temperature");
                entity.Property(e => e.Available).HasColumnName("available");
                entity.Property(e => e.Errors).HasColumnName("errors");
                entity.Property(e => e.ErrorsDescription).HasColumnName("errors_description");
                entity.Property(e => e.Uptime).HasColumnName("uptime");
                entity.Property(e => e.Downtime).HasColumnName("downtime");
                entity.Property(e => e.ReportCounter).HasColumnName("report_counter");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UptimeHr).HasColumnName("uptimehr");
                entity.Property(e => e.DowntimeHr).HasColumnName("downtimehr");
                entity.Property(e => e.PingAt).HasColumnName("ping_at");
                entity.Property(e => e.StartedAt).HasColumnName("started_at");
                entity.Property(e => e.DisconnectedAt).HasColumnName("disconnected_at");
                entity.Property(e => e.AppStartedAt).HasColumnName("appstarted_at");
                entity.Property(e => e.QueueQuantity).HasColumnName("queue_quantity");
                entity.Property(e => e.LastReportAt).HasColumnName("last_report_at");
            });

            // Nodes
            modelBuilder.Entity<Node>(entity =>
            {
                entity.ToTable("nodes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Group).HasColumnName("Group");
                entity.Property(e => e.PvnNumber).HasColumnName("pvnnumber");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Location).HasColumnName("location");
                entity.Property(e => e.ModelName).HasColumnName("modelname");
                entity.Property(e => e.IgnoredInTask).HasColumnName("ignoredintask");
                entity.Property(e => e.SerialFd).HasColumnName("serialfd");
                entity.Property(e => e.FdIp).HasColumnName("fdip");
            });

            // Tickets
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
                entity.Property(e => e.LastUpdated).HasColumnName("lastupdated");
                entity.Property(e => e.ClosedAt).HasColumnName("closedat");
                entity.Property(e => e.TicketStatus).HasColumnName("ticketstatus");
                entity.Property(e => e.NodeId).HasColumnName("nodeid");
                entity.Property(e => e.Available).HasColumnName("available");
                entity.Property(e => e.Temperature).HasColumnName("temperature");
                entity.Property(e => e.Errors).HasColumnName("errors");
                entity.Property(e => e.TypeError).HasColumnName("typeerror");
                entity.Property(e => e.ErrorsDescription).HasColumnName("errorsdescription");
                entity.Property(e => e.StartedAt).HasColumnName("startedat");
                entity.Property(e => e.DisconnectedAt).HasColumnName("disconnectedat");
                entity.Property(e => e.QueueQty).HasColumnName("queueqty");
                entity.Property(e => e.LastReportAt).HasColumnName("lastreportat");
                entity.Property(e => e.ManualStopped).HasColumnName("manualstopped");
            });

            // Messages
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.TicketId).HasColumnName("ticketid");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            });

            // Cams
            modelBuilder.Entity<Cam>(entity =>
            {
                entity.ToTable("cams");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Ip).HasColumnName("ip");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.NodeId).HasColumnName("nodeid");
            });

            // EquipmentSummaries
            modelBuilder.Entity<EquipmentSummary>(entity =>
            {
                entity.ToTable("equipmentsummaries");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Pvn).HasColumnName("pvn");
                entity.Property(e => e.NodeId).HasColumnName("nodeid");
                entity.Property(e => e.Fd).HasColumnName("fd");
                entity.Property(e => e.Rubezh).HasColumnName("rubezh");
                entity.Property(e => e.Equipment).HasColumnName("equipment");
                entity.Property(e => e.EquipmentType).HasColumnName("equipmenttype");
                entity.Property(e => e.Bu).HasColumnName("bu");
                entity.Property(e => e.AddressIp).HasColumnName("addressip");
                entity.Property(e => e.Network).HasColumnName("network");
                entity.Property(e => e.Gateway).HasColumnName("gateway");
                entity.Property(e => e.Mask).HasColumnName("mask");
            });

            // ReportStats
            modelBuilder.Entity<ReportStat>(entity =>
            {
                entity.ToTable("reportstats");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NumberReport).HasColumnName("numberreport");
                entity.Property(e => e.UploadedAt).HasColumnName("uploaded_at");
                entity.Property(e => e.CountRecords).HasColumnName("countrecords");
                entity.Property(e => e.Processed).HasColumnName("processed");
                entity.Property(e => e.ProcessedAt).HasColumnName("processed_at");
            });

            // NodeSchemas
            modelBuilder.Entity<NodeSchema>(entity =>
            {
                entity.ToTable("nodeschemas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NodeId).HasColumnName("nodeid");
                entity.Property(e => e.PlaceCode).HasColumnName("placecode");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.SchemeNumber).HasColumnName("schemenumber");
                entity.Property(e => e.Image).HasColumnName("image");
                entity.Ignore(e => e.PvnNumber);
                entity.Ignore(e => e.NodeTitle);
                entity.Ignore(e => e.Fd);
                entity.Ignore(e => e.Location);
            });

            // TicketHistories
            modelBuilder.Entity<TicketHistory>(entity =>
            {
                entity.ToTable("ticket_histories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TicketId).HasColumnName("ticketid");
                entity.Property(e => e.Action).HasColumnName("action");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.PerformedBy).HasColumnName("performed_by");
                entity.Property(e => e.PerformedAt).HasColumnName("performed_at");
            });
        }
    }
}