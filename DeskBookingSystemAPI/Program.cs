

using DataAccess;
using DeskBookingSystemAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IDataProcessor, DataProcessor>();
builder.Services.AddScoped<IDataAccess, DataAccessProcess>();
builder.Services.AddScoped<IGetData, GetData>();
builder.Services.AddScoped<IDeleteData, DeleteData>();
builder.Services.AddScoped<IInsertData, InsertData>();
builder.Services.AddScoped<IUpdateData, UpdateData>();
builder.Services.AddScoped<EveryoneController, EveryoneController>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();

//app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //??


app.UseAuthorization();


app.MapControllers();

app.Run();
