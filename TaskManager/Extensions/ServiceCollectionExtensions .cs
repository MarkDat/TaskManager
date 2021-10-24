using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TM.API.Mappings;
using TM.API.Services.CardHistories;
using TM.API.Services.Cards;
using TM.API.Services.interfaces;
using TM.API.Services.Phases;
using TM.API.Services.Projects;
using TM.API.Services.Users;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.Cards;
using TM.Domain.Entities.CardTags;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.ProjectMembers;
using TM.Domain.Entities.ProjectPhases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Users;
using TM.Domain.Interfaces;
using TM.Domain.Models;
using TM.Infrastructure;
using TM.Infrastructure.Data;
using TM.Infrastructure.Data.Repositories;

namespace TaskManager.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Swagger API",
                        Description = "API for showing Swagger",
                        Version = "v1"
                    });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                option.IncludeXmlComments(filePath);

                option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement(){
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<TaskManagerContext>(options =>
            {
                options.UseSqlServer(AppSettings.ConnectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120));
            }
            );

            services.AddScoped<Func<TaskManagerContext>>((provider) => () => provider.GetService<TaskManagerContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPhaseRepository, PhaseRepository>()
                .AddScoped<IProjectRepository, ProjectRepository>()
                .AddScoped<IProjectPhaseRepository, ProjectPhaseRepository>()
                .AddScoped<ICardMovementRepository, CardMovementRepository>()
                .AddScoped<ICardAssignRepository, CardAssignRepository>()
                .AddScoped<ICardTagRepository, CardTagRepository>()
                .AddScoped<IMemberProjectRepository, ProjectMemberRepository>()
                .AddScoped<ICardRepository, CardRepository>();

        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
                    .AddScoped<IPhaseService, PhaseService>()
                    .AddScoped<IProjectService, ProjectService>()
                    .AddScoped<ICardService, CardService>()
                    .AddScoped<CardHistoryService>();

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
