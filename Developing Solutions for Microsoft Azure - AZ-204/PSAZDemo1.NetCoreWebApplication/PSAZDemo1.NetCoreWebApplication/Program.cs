using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using PSAZDemo1.NetCoreWebApplication.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("AzureDatabase");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString), ServiceLifetime.Transient);

var docClient = new CosmosClient(builder.Configuration.GetConnectionString("CosmosDatabase"));
builder.Services.AddSingleton<CosmosClient>(docClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        dbContext.Database.EnsureCreated();
    }
    catch (Exception e)
    {

    }

}

app.Run();
