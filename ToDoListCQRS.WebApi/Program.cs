using ToDoListCQRS.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.ConfigureSevices();
builder.Services.AddDatabaseConfiguration(builder.Configuration);


builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();            
    app.UseSwaggerUI(c =>        
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
