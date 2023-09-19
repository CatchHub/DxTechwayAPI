
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductAppAPI.Mappers;
using ProductAppAPI.ServiceContracts;
using ProductAppAPI.Services;
using ProductListApp.Persistence.Contexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Add(new ServiceDescriptor(
    typeof(IProductsService),
    typeof(ProductsService),
    ServiceLifetime.Transient
    ));



builder.Services.Add(new ServiceDescriptor(
    typeof(IUsersService),
    typeof(UsersService),
    ServiceLifetime.Transient
    ));




builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:4200/", "https://localhost:4200/")
.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
));

builder.Services.AddDbContext<ProductListAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret......")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MappersController.RegisterMappers(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
