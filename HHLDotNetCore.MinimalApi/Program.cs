using HHLDotNetCore.Database.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger";

    });
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.MapGet("/blogs", () =>
{
    AppDbContext db = new AppDbContext(); 
    var model = db.TblBlogs.AsNoTracking().ToList();
    return Results.Ok(model);
}).WithName("GetBlogs").WithOpenApi();

app.MapGet("/blog/{id}", (int id) =>
{
    AppDbContext db = new AppDbContext(); 
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
    if(item is null)
    {
        return Results.BadRequest();
    }
    return Results.Ok(item);
}).WithName("GetBlog").WithOpenApi();

app.MapPost("/blogs", (TblBlog blog) =>
{
    AppDbContext db = new AppDbContext(); 
    var model = db.TblBlogs.Add(blog);
    db.SaveChanges();
    return Results.Ok(model);
}).WithName("CreateBlogs").WithOpenApi();

app.MapPut("/blog/{id}", (int id,TblBlog blog) =>
{
    AppDbContext db = new AppDbContext(); 
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
    if(item is null)
    {
        return Results.BadRequest();
    }
    item.BlogAuthor = blog.BlogAuthor;
    item.BlogTitle = blog.BlogTitle;
    item.BlogContent = blog.BlogContent;
    
    db.Entry(item).State =EntityState.Modified;
    db.SaveChanges();
    return Results.Ok(blog);
}).WithName("UpdateBlog").WithOpenApi();

app.MapDelete("/blog/{id}", (int id) =>
{
    AppDbContext db = new AppDbContext(); 
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
    if(item is null)
    {
        return Results.BadRequest();
    }
    item.DeleteFlag = 1;
    db.Entry(item).State = EntityState.Modified;
    db.SaveChanges();
    return Results.Ok();
}).WithName("DeleteBlog").WithOpenApi();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
