using CourseSalesAPI.Application.Validators.Courses;
using CourseSalesAPI.Infrastructure.Filters;
using CourseSalesAPI.Persistance;
using CourseSalesAPI.Persistance.Context;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CourseSalesAPI.Infrastructure;
using CourseSalesAPI.Infrastructure.Services.Storage.Local;
using CourseSalesAPI.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
   
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateCourseValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);





builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage();






////Database
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});

builder.Services.AddCors();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x=>x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin=>true).AllowCredentials());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
