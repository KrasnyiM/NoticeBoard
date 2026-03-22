using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using NoticeBoard.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IAnnouncementApiService, AnnouncementApiService>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.SaveTokens = true;
    options.Scope.Add("openid");

    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
    {
        OnCreatingTicket = context =>
        {
            if (context.TokenResponse?.Response != null &&
                context.TokenResponse.Response.RootElement.TryGetProperty("id_token", out var idTokenElement))
            {
                var idToken = idTokenElement.GetString();

                if (!string.IsNullOrEmpty(idToken))
                {
                    context.Properties.StoreTokens(new[]
                    {
                        new AuthenticationToken { Name = "id_token", Value = idToken }
                    });
                }
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
