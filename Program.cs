using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HackTonTemplate;
using HackTonTemplate.Extensions;
using HackTonTemplate.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddFluentNibernateConnection();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RefreshPolicy", policy =>
    {
        policy.RequireClaim("TokenType", "Refresh");
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOprosService, OprosService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthenticationTokenOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = AuthenticationTokenOptions.AUDIENCE,
            ValidateLifetime = true,

            IssuerSigningKey = AuthenticationTokenOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();
app.MapControllers();

app.UseStaticFiles();

app.UseAuthorization();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
});

app.Run();