using CourseSalesAPI.Application.Validators.Courses;
using CourseSalesAPI.Infrastructure.Filters;
using CourseSalesAPI.Persistance;
using CourseSalesAPI.Persistance.Context;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CourseSalesAPI.Infrastructure;
using CourseSalesAPI.Infrastructure.Services.Storage.Local;
using CourseSalesAPI.Application;
using Microsoft.Identity.Client.Extensions.Msal;
using CourseSalesAPI.Infrastructure.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Serilog;
using Serilog.Core;

using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Collections.ObjectModel;
using Serilog.Context;
using Microsoft.AspNetCore.HttpLogging;
using CourseSalesAPI.API.Extensions;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
   
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateCourseValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);






var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn("user_name", SqlDbType.NVarChar) 
    }
};

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "logs",
    AutoCreateSqlTable = true 
};

/*Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("SqlCon"), // SQL Server baðlantý dizesi
        sinkOptions: sinkOptions,
        columnOptions: columnOptions)
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
*/


builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseSalesAPI", Version = "v1" });

    // JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddStorage(StorageType.Local);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, 
            ValidateIssuer = true,
            ValidateLifetime = true, 
            ValidateIssuerSigningKey = true, 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = "name"
        };
    });


/*builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
*/

////Database
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});



var app = builder.Build();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

builder.Services.AddCors();

app.UseCors(x=>x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin=>true).AllowCredentials());
//app.UseCors(x=>x.AllowAnyMethod().AllowAnyHeader().AllowCredentials());



//app.UseSerilogRequestLogging();

//app.UseHttpLogging();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});


app.MapControllers();

app.Run();
