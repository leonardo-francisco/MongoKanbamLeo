using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MKL.API.Filters;
using MKL.Application.Mappers;
using MKL.Application.Services.Project;
using MKL.Application.Services.Role;
using MKL.Application.Services.Tasks;
using MKL.Application.Services.User;
using MKL.Domain.Entities;
using MKL.Domain.Identity;
using MKL.Domain.Interfaces;
using MKL.Infrastructure.Persistence;
using MKL.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

// Configurar MongoDbSettings e MongoDbContext
builder.Services.Configure<MKL.Infrastructure.Persistence.MongoDbSettings>(builder.Configuration.GetSection("MongoConnectionStrings"));
builder.Services.AddSingleton<MongoDbContext>();

// Adicionar ASP.NET Mongo Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>();
    
var mongoDbIdentityConfiguration = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new AspNetCore.Identity.MongoDbCore.Infrastructure.MongoDbSettings
    {
        ConnectionString = "mongodb://localhost:27017",
        DatabaseName = "KanbamManagment"
    }
};
builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfiguration)
        .AddDefaultTokenProviders();

// Add Mapper
builder.Services.AddAutoMapper(typeof(ConfigurationMapping));

// Add application services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

//Add repository
builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
builder.Services.AddScoped<ITasksRepository,TasksRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Add JWT Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Key:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true)
);

app.Run();
