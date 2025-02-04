using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CarGo.Repository;
using CarGo.Repository.Common;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarGo.Service;
using CarGo.Service.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
        containerBuilder.RegisterType<UserService>().As<IUserService>();
        containerBuilder.RegisterType<RoleRepository>().As<IRoleRepository>();
        containerBuilder.RegisterType<RoleService>().As<IRoleService>();
        containerBuilder.RegisterType<TokenService>().As<ITokenService>();
        containerBuilder.RegisterType<BookingRepository>().As<IBookRepository>();
        containerBuilder.RegisterType<BookingService>().As<IBookService>();
        //Register types
        containerBuilder.RegisterType<ImageRepository>().As<IImageRepository>();
        containerBuilder.RegisterType<ImageService>().As<IImageService>();
    });

// Add services to the container.

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddControllers();
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

app.Run();