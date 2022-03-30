using AutoMapper;
using FamousQuoteQuiz.Api.Configuration;
using FamousQuoteQuiz.Business.Generators;
using FamousQuoteQuiz.Business.Identity;
using FamousQuoteQuiz.Business.Services;
using FamousQuoteQuiz.Core.Configuration;
using FamousQuoteQuiz.Core.Generators;
using FamousQuoteQuiz.Core.Identity;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FamousQuoteQuiz.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration.GetConnectionString("DbConnectionString"));
            services.AddAutoMapper();
            services.AddJwtIdentity(Configuration.GetSection(nameof(JwtConfiguration)));
            services.AddSwagger();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));

            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IRandomGenerator, RandomGenerator>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IQuizQuestionService, QuizQuestionService>();
            services.AddTransient<IAuthorService, AuthorService>();

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Seed();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("MyPolicy");

            loggerFactory.AddLogging();

            app.UseSwagger("My Web API.");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
