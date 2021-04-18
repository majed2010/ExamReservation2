namespace MIU.AdmissionBackEnd.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    public class AutoMapperConfig
    {
        public static void Configure(Assembly assembly)
        {
            Mapper.Initialize(x => GetConfiguration(Mapper.Configuration, new[] { assembly }));
        }

        public static void Configure(IEnumerable<Assembly> assemblies)
        {
            Mapper.Initialize(x => GetConfiguration(Mapper.Configuration, assemblies));
        }

        private static void GetConfiguration(IConfiguration configuration)
        {
            var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            var assemblies = referencedAssemblies.Select(Assembly.Load).ToList();
            GetConfiguration(configuration, assemblies);
        }

        private static void GetConfiguration(IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var profiles = assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
                foreach (var profile in profiles)
                {
                    configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            }
        }
    }
}