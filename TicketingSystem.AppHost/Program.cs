var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgresql = builder.AddPostgres("postgresql", port: 54030)
    .WithDataVolume();

var postgresdb = postgresql.AddDatabase("TicketingDB");

var apiService = builder.AddProject<Projects.TicketingSystem_ApiService>("apiservice")
    .WithReference(postgresdb);

builder.AddProject<Projects.TicketingSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
