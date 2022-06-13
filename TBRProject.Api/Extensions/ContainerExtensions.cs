using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TBRProject.Api.Core;
using TBRProject.Application.Logging;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.Queries;
using TBRProject.DataAccess;
using TBRProject.Domain;
using TBRProject.Implementation.Logging;
using TBRProject.Implementation.UseCases.Commands;
using TBRProject.Implementation.UseCases.Queries;
using TBRProject.Implementation.Validators;

namespace TBRProject.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<TBRContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetRolesQuery, GetRolesQuery>();
            services.AddTransient<IGetUsersQuery, GetUsersQuery>();
            services.AddTransient<IGetBooksQuery, GetBooksQuery>();
            services.AddTransient<IFindUserQuery, FindUserQuery>();
            services.AddTransient<ICreateUserCommand, CreateUserCommand>();
            services.AddTransient<IGetGenresQuery, GetGenresQuery>();
            services.AddTransient<IGetUsersFromRoreIdQuery, GetUsersFromRoreIdQuery>();
            services.AddTransient<IFindBooksFromGenreIdQuery, FindBooksFromGenreIdQuery>();
            services.AddTransient<ICreateGenreCommand, CreateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, DeleteGenreCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
            services.AddTransient<IFindBookQuery, FindBookQuery>();
            services.AddTransient<IDeleteBookCommand, DeleteBookCommand>();
            services.AddTransient<ICreateBookCommand, CreateBookCommand>();
            services.AddTransient<ICreateReviewCommand, CreateReviewCommand>();
            services.AddTransient<IDeleteReviewCommand, DeleteReviewCommand>();
            services.AddTransient<ICreateLikeCommand, CreateLikeCommand>();
            services.AddTransient<ICreateReaderListCommand, CreateReaderListCommand>();
            services.AddTransient<IDeleteLikeCommand, DeleteLikeCommand>();
            services.AddTransient<IUpdateReaderListCommand, UpdateReaderListCommand>();
            #region Validators
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<CreateGenreValidator>();
            services.AddTransient<CreateBookValidator>();
            services.AddTransient<CreateReviewValidator>();
            #endregion
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                //Pristup payload-u
                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }

        public static void AddVezbeDbContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConnString;

                optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();

                var options = optionsBuilder.Options;

                return new TBRContext(options);
            });
        }
    }
}
