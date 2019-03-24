/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Configuration;
using Dolittle.Configuration.Files;
using Dolittle.IO;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationFileProvider : ICanProvideConfigurationObjects
    {
        static string _path = Path.Join("data","identities.json");
        readonly IFileSystem _fileSystem;
        readonly IConfigurationFileParsers _parsers;

        /// <summary>
        /// 
        /// </summary>
        public ConfigurationFileProvider(
            IFileSystem fileSystem,
            IConfigurationFileParsers parsers)
        {
            _fileSystem = fileSystem;
            _parsers = parsers;
        }

        /// <inheritdoc/>
        public bool CanProvide(Type type)
        {
            if( !File.Exists(_path)) return false;
            return type == typeof(TimeSeriesMap);
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            var json = _fileSystem.ReadAllText(_path);
            var configObject = _parsers.Parse(type, _path, json);
            return configObject;
        }
    }
}