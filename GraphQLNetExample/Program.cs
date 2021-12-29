using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQLNetExample.EntityFramework;
using GraphQLNetExample.Infoes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserInfoesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<ISchema, UserInfoesSchema>(services => new UserInfoesSchema(new SelfActivatingServiceProvider(services)));
builder.Services.AddGraphQL(options =>
                {
                    options.EnableMetrics = false;
                })
                .AddSystemTextJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseGraphQLAltair();
}

app.UseGraphQL<ISchema>();

app.Run();
