var builder = DistributedApplication.CreateBuilder(args);



var rabbitMq = builder.AddRabbitMQ("eventbus");

builder.AddProject<Projects.ProductService_API>("productservice-api");

builder.AddProject<Projects.AuthService_API>("authservice-api");


builder.AddProject<Projects.RabbitMQConsumer>("rabbitmqconsumer")
   .WithReference(rabbitMq);

builder.Build().Run();