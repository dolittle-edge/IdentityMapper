using Autofac;
using Newtonsoft.Json;
using RaaLabs.IdentityMapper.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.Configuration
{
    class Configuration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationTypes = TypeFinder.ImplementationsOf<IConfiguration>();

            configurationTypes
                .Select(c => LoadConfigurationObject(c)).ToList()
                .ForEach(c => builder.RegisterInstance(c).AsSelf());
        }

        private static IConfiguration LoadConfigurationObject(Type type)
        {
            string filename = type.GetCustomAttribute<NameAttribute>().Name;
            string path = Path.Join(Directory.GetCurrentDirectory(), "data", filename);
            IConfiguration configuration = (IConfiguration)JsonConvert.DeserializeObject(File.ReadAllText(path), type);

            return configuration;
        }

    }
}
