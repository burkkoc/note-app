using NoteApp.Business.Extensions;
using NoteApp.DataAccess.Extensions;
using NoteApp.DataAccessEFCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDataAccessServices(builder.Configuration)
    .AddEFCoreServices(builder.Configuration)
    .AddBusinessServices();
    


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await NoteApp.DataAccessEFCore.Extensions.DependencyInjection.Seed(app);
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
