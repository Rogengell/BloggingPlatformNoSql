using MongoDBBloggerPost.Core.Factories;
using MongoDBBloggerPost.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(EntityServiceFactory<UsersModel>.Create());
builder.Services.AddSingleton(EntityServiceFactory<BlogsModel>.Create());
builder.Services.AddSingleton(EntityServiceFactory<PostsModel>.Create());
builder.Services.AddSingleton(EntityServiceFactory<CommentsModel>.Create());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
//app.UseHttpsRedirection();

app.Run();
