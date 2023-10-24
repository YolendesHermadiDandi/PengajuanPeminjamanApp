using API.Contracts;
using API.Data;
using API.Repositories;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DandiConnection");
builder.Services.AddDbContext<RequestFasilityDbContext>(option => option.UseSqlServer(connectionString));

// Add repositories to the container.
builder.Services.AddTransient<IEmailHandlerRepository, EmailHandlerRepository>(_ =>
            new EmailHandlerRepository
            (
                builder.Configuration["StmpServices:Server"],
                int.Parse(builder.Configuration["StmpServices:Port"]),
                builder.Configuration["StmpServices:FromEmailAddress"]
));

builder.Services.AddScoped<ITokenHandlerRepository, TokenHandlerRepository>();
// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = false; // for development only
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidIssuer = builder.Configuration["JWTServices:Issuer"],
               ValidateAudience = false,
               ValidAudience = builder.Configuration["JWTServices:Audience"],
               ValidateIssuerSigningKey = false,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                        builder.Configuration["JWTServices:SecretKey"])),
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
       });





builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IFasilityRepository, FasilityRepository>();
builder.Services.AddScoped<IListFasilityRepository, ListFasilityRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRespository>();
builder.Services.AddScoped<IRoleRepository, RoleRepositroy>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        //policy.WithOrigins() //dipakai untuk url khusus (site client)
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader(); //buat header ex: Authrization
        policy.AllowAnyMethod(); //method acess http GET POST PUT DELETE
    });
});

//Add fluent services
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
       {
           // Custom validation response
           options.InvalidModelStateResponseFactory = context =>
           {
               var errors = context.ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(v => v.ErrorMessage);

               return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
           };
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

app.MapControllers();

app.Run();
