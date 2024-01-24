using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SimpleOLX;
using SimpleOLX.Entities;
using SimpleOLX.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}
);

// Add services to the container.
builder.Services.AddDbContext<SimpleOLXDbContext>();

builder.Services.AddScoped<JWTService>();

builder.Services.AddControllers();

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo()
        {
            Title = "Simple OLX Web API",
            Version = "version 1.0",
            Description = ""
        }
    );
    options.AddSecurityDefinition("jwt_auth",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            BearerFormat = "JWT",
            Scheme = "bearer",
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new string[] { }
            }
        }
    );
}
);

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<SimpleOLXDbContext>()
.AddSignInManager<SignInManager<User>>()
.AddUserManager<UserManager<User>>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opts =>
{
    opts.RequireHttpsMetadata = false;
    opts.SaveToken = false;
    opts.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]!)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    opts.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            context.NoResult();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync(context.Exception.ToString());
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject("401 Unauthorized"));
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject("403 Forbidden"));
        },
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(CORSbuilder =>
    CORSbuilder.WithOrigins("http://localhost:4200")
               .WithMethods("GET", "POST", "PUT", "DELETE")
               .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
