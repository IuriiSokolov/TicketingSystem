var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithMongoExpress();

var mongodb = mongo.AddDatabase("TicketingDB");

var apiService = builder.AddProject<Projects.TicketingSystem_ApiService>("apiservice")
    .WithReference(mongodb);

builder.AddProject<Projects.TicketingSystem_MigrationService>("migration")
       .WithReference(mongodb);

builder.AddProject<Projects.TicketingSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
