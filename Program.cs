using FlagmanProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(opt => { 
    opt.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
    opt.RequireHeaderSymmetry = false;
    opt.ForwardLimit = 10;
    opt.KnownProxies.Clear();
    opt.KnownNetworks.Clear();
});

//builder.Services.AddDbContext<ContactAPIDbContext>(options => options.UseInMemoryDatabase("BlockDb"));
builder.Services.AddDbContext<ContactAPIDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));



var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
