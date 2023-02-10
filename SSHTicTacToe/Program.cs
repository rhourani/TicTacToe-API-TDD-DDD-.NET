using SSHTicTacToe.ErrorMiddleware;
using SSHTicTacToe.Services;
using SSHTicTacToe.Services.AuthorizedKeysParserServices;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServices(builder.Services);


// Add services to the container.

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

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigurationServices(IServiceCollection services)
{
    builder.Services.AddScoped<ITicTacToeGameService, TicTacToeGameService>();
    builder.Services.AddScoped<IAuthorizedKeysParserService, AuthorizedKeysParserService>();
}
