var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgresql = builder.AddPostgres("postgresql", port: 54030)
    .WithDataVolume();

var postgresdb = postgresql.AddDatabase("TicketingDB");

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var notificationsql = builder.AddPostgres("notificationsql", port: 54031)
    .WithDataVolume();

var notificationdb = notificationsql.AddDatabase("NotificationDB");

var rabbitmq = builder.AddRabbitMQ("messaging", username, password)
    .WithDataVolume()
    .WithManagementPlugin();

var apiService = builder.AddProject<Projects.TicketingSystem_ApiService>("apiservice")
    .WithReference(postgresdb)
    .WithReference(cache)
    .WithReference(rabbitmq);

var notificationService = builder.AddProject<Projects.TicketingSystem_NotificationService>("notificationservice")
    .WithReference(notificationdb)
    .WithReference(cache)
    .WithReference(rabbitmq);

builder.AddProject<Projects.TicketingSystem_MigrationService>("migration")
       .WithReference(postgresdb);

builder.AddProject<Projects.TicketingSystem_NotificationMigration>("notificationmigration")
    .WithReference(notificationdb);

builder.Build().Run();
