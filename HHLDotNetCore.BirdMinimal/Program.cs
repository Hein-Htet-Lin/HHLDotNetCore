using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    return Results.Ok(result.Tbl_Bird);

}).WithName("GetBirds").WithOpenApi();

app.MapGet("/bird/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if(item is null){
        return Results.BadRequest("No Data Found");
    }
    return Results.Ok(item);

}).WithName("GetBird").WithOpenApi();

app.Run();



    public  class BirdResponseModel
    {
        public BirdModel[] Tbl_Bird { get; set; }
    }

    public  class BirdModel
    {
        
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

    

