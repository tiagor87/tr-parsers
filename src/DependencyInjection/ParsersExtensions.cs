using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TRParsers.Core;
using TRParsers.Core.Implementations;

namespace TRParsers.DependencyInjection.Extensions
{
    public static class ParsersExtensions
    {
        public static void AddParsers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IParserFactory, ParserFactory>();

            var types = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(IsParserImplementation).ToList();
            types.ForEach(type =>
            {
                var interfaces = type.GetParsersInterfaces();
                foreach (var i in interfaces)
                {
                    services.AddSingleton(i, type);
                }
            });
        }

        private static bool IsImplementation(this Type type)
        {
            return type.IsClass && !type.IsAbstract;
        }
        private static bool IsParserImplementation(this Type type)
        {
            return type.IsImplementation() && type.GetParsersInterfaces().Any();
        }
        private static IEnumerable<Type> GetParsersInterfaces(this Type type)
        {
            return type.GetInterfaces()
                .Where(i =>
                    i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(IParser<,>)).ToList();
        }
    }
}
