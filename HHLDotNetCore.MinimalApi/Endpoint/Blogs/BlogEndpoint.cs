namespace HHLDotNetCore.MinimalApi.Endpoint.Blogs
{
    public static class BlogEndpoint
    {
        public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
{
    AppDbContext db = new AppDbContext();
    var model = db.TblBlogs.AsNoTracking().ToList();
    return Results.Ok(model);
}).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blog/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
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

            app.MapPut("/blog/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest();
                }
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogTitle = blog.BlogTitle;
                item.BlogContent = blog.BlogContent;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(blog);
            }).WithName("UpdateBlog").WithOpenApi();

            app.MapDelete("/blog/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest();
                }
                item.DeleteFlag = 1;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok();
            }).WithName("DeleteBlog").WithOpenApi();
        }
    }
}