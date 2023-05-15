using ISOBAR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBandService, BandService>();
builder.Services.AddHttpClient<IBandService, BandService>(client =>
{
    client.BaseAddress = new Uri("https://bands-api.vercel.app");
});

builder.Services.AddRazorPages();

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

app.Run();
