using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Application.Services;
using VirtualMachinesCore.Infrastructure.Repositories;

namespace VirtualMachinesCore;

public class Startup
{
    private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddSingleton<IAmazonDynamoDB>(GetClient());
        services.AddSingleton<IDynamoDBContext>(GetAmazonDynamoDB());

        
        services.AddCors(options =>
        options.AddPolicy(name: MyAllowSpecificOrigins,
        builder => { builder.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));
        services.AddControllers();
        services.AddHttpContextAccessor();
        
        var keyBytes = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(keyBytes);
        }
        var base64Key = Convert.ToBase64String(keyBytes);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(base64Key));
        
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IVirtualMachineService, VirtualMachineService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVirtualMachineRepository, VirtualMachineRepository>();
        services.AddScoped<IUserMiddlewareService, UserMiddlewareService>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Virtual Machines API",
                Version = "v1",
                Description = "API for managing virtual machines"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseMiddleware<JwtMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins); });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Virtual Machines API V1");
        });
    }

    private AmazonDynamoDBClient GetClient()
    {
        var chain = new CredentialProfileStoreChain();
        AWSCredentials credentials;
        AmazonDynamoDBConfig clientConfig;

        if (chain.TryGetAWSCredentials("default", out credentials))
        {
            clientConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
            };
            return new AmazonDynamoDBClient(credentials, clientConfig);
        }
        else
        {
            clientConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
            };
            return new AmazonDynamoDBClient(clientConfig);
        }
    }

    private IDynamoDBContext GetAmazonDynamoDB()
    {
        var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
        return new DynamoDBContext(GetClient(), config);
    }
}