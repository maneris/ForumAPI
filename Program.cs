using System;
using ForumAPI.Data;
using ForumAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IPostsRepository, PostsRepository>();
builder.Services.AddTransient<IThreadsRepository,ThreadsRepository>();
builder.Services.AddTransient<ITopicsRepository, TopicsRepository>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
