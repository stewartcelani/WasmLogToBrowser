using Microsoft.AspNetCore.ResponseCompression;
using WasmLogToBrowser.Server.Hubs;
using WasmLogToBrowser.Server.Logging;

Logger.Configure();
Logger.Log.Info("App starting");
// Lets log something every second to simulate legitimate log output
System.Timers.Timer timer = new System.Timers.Timer(1000);
timer.Elapsed += (source, e) =>
{
    Logger.Log.Trace("tick");
};
timer.Enabled = true;
timer.Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
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
app.MapHub<LoggingHub>("/hubs/logging");
app.MapFallbackToFile("index.html");

app.Run();