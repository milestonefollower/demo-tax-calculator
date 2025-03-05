using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using FluentValidation;
using TaxCalculator.Infra.Repositories;
using TaxCalculator.Infra.Abstraction;
using TaxCalculator.BL.CQRS.Commands;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Services;
using TaxCalculator.API.Validators;
using TaxCalculator.Infra.Data;

namespace TaxCalculator.API
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaxCalculatorDbContext>(options =>
                options.UseInMemoryDatabase("DemoTaxCalculator"));

            services.AddMemoryCache();
            services.AddScoped<TaxBandRepository>();
            services.AddScoped<ITaxBandRepository, TaxBandCachedRepository>();
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddMediatR(typeof(CalculateTaxCommand).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters()
                    .AddValidatorsFromAssemblyContaining<CalculateTaxRequestValidator>();

            services.AddSwaggerGen();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TaxCalculatorDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                dbContext.Database.EnsureCreated();
            }

            app.UseCors(opt => 
                opt.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tax Calculator API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}