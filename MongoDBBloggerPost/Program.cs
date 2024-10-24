using MongoDBBloggerPost.Core.Factories;
using MongoDBBloggerPost.Core.Helpers;
using MongoDBBloggerPost.Model;
using MongoDBBloggerPost.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(EntityServiceFactory<UsersModel>.Create(CollectionName.Users));
builder.Services.AddSingleton(EntityServiceFactory<BlogsModel>.Create(CollectionName.Blogs));
builder.Services.AddSingleton(EntityServiceFactory<PostsModel>.Create(CollectionName.Posts));
builder.Services.AddSingleton(EntityServiceFactory<CommentsModel>.Create(CollectionName.Comments));
builder.Services.AddSingleton(ClientFactory.Create());
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
