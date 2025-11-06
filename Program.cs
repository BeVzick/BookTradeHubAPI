using FluentValidation.AspNetCore;
using BookTradeHubAPI.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<StudentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TradeValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
