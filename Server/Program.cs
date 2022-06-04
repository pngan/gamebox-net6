using Microsoft.AspNetCore.ResponseCompression;
using gamebox.Server.Hubs;
using gamebox.Server.HubHelpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddResponseCompression(opts =>
//{
//    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
//        new[] { "application/octet-stream" });
//});
builder.Services.AddSingleton<IGameRepository, GameRepository>();

var app = builder.Build();
//app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapHub<GameHub>("/planningpokerhub");
app.MapFallbackToFile("index.html");

string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

if (File.Exists("githash"))
{
    var gitHash = File.ReadAllText("githash");
    Environment.SetEnvironmentVariable("githash", gitHash);
}

app.Run();