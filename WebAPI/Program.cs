using System.Collections.Immutable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.Identity.Web;
using POS_App;
using WebAPi.Repositories;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUri = new Uri("https://pos-db-string.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
var dbkey = builder.Configuration["POS-App-DB"];

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");

builder.Services.AddControllers();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddSingleton((provider) =>
{
    var cosmosClient = new CosmosClient("https://pos-db-app.documents.azure.com:443/", dbkey);
    return new AdminRepository(cosmosClient, "POS_App", "admin_users");
});

builder.Services.AddSingleton((provider) =>
{
    var cosmosClient = new CosmosClient("https://pos-db-app.documents.azure.com:443/", dbkey);
    return new CashierRepository(cosmosClient, "POS_App", "cashier_users");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
