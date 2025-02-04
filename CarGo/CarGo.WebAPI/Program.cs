using Autofac;
using Autofac.Extensions.DependencyInjection;
using CarGo.Common;
using CarGo.Repository;
using CarGo.Repository.Common;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((containerBuilder) =>
    {

        containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
        containerBuilder.RegisterType<UserService>().As<IUserService>();
        containerBuilder.RegisterType<RoleRepository>().As<IRoleRepository>();
        containerBuilder.RegisterType<RoleService>().As<IRoleService>();
        containerBuilder.RegisterType<TokenService>().As<ITokenService>();
        containerBuilder.RegisterType<BookingRepository>().As<IBookRepository>();
        containerBuilder.RegisterType<BookingService>().As<IBookService>();
        containerBuilder.RegisterType<LocationService>().As<ILocationService>();
        containerBuilder.RegisterType<LocationRepository>().As<ILocationRepository>();
        containerBuilder.RegisterType<ImageRepository>().As<IImageRepository>();
        containerBuilder.RegisterType<ImageService>().As<IImageService>();
        containerBuilder.RegisterType<CompanyVehicleMaintenanceRepository>().As<ICompanyVehicleMaintenanceRepository>();
        containerBuilder.RegisterType<CompanyVehicleMaintenanceService>().As<ICompanyVehicleMaintenanceService>();
        containerBuilder.RegisterType<NotificationRepository>().As<INotificationRepository>();
        containerBuilder.RegisterType<NotificationService>().As<INotificationService>();
        containerBuilder.RegisterType<CarColorRepository>().As<ICarColorRepository>();
        containerBuilder.RegisterType<CarColorService>().As<ICarColorService>();
        containerBuilder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();
        containerBuilder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
        containerBuilder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>();
        containerBuilder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
        containerBuilder.RegisterType<VehicleStatusRepository>().As<IVehicleStatusRepository>();
        containerBuilder.RegisterType<VehicleStatusService>().As<IVehicleStatusService>();
        containerBuilder.RegisterType<VehicleTypeRepository>().As<IVehicleTypeRepository>();
        containerBuilder.RegisterType<VehicleTypeService>().As<IVehicleTypeService>();
        containerBuilder.RegisterType<CompanyService>().As<ICompanyService>();
        containerBuilder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
        containerBuilder.RegisterType<CompanyRequestService>().As<ICompanyRequestService>();
        containerBuilder.RegisterType<CompanyRequestRepostiroy>().As<ICompanyRequestRepository>();
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

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

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