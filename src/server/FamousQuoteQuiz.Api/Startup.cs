using AutoMapper;
using FamousQuoteQuiz.Api.Configuration;
using FamousQuoteQuiz.Api.Filters;
using FamousQuoteQuiz.Api.ModelBinders;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FamousQuoteQuiz.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration.GetConnectionString("DbConnectionString"));
            services.AddAutoMapper();
            services.AddSwagger();
            services.AddJwtIdentity(Configuration.GetSection(nameof(JwtConfiguration)));

            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));

            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IRandomGenerator, RandomGenerator>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IQuizQuestionService, QuizQuestionService>();
            services.AddTransient<IAuthorService, AuthorService>();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new OptionModelBinderProvider());
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ModelStateFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Seed();
            }
            else
            {
                app.UseHsts();
            }

            // Enable Cors
            app.UseCors("MyPolicy");

            loggerFactory.AddLogging(Configuration.GetSection("Logging"));

            app.UseHttpsRedirection();
            app.UseSwagger("My Web API.");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
