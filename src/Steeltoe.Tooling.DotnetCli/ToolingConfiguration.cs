﻿using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

// ReSharper disable InconsistentNaming

namespace Steeltoe.Tooling.DotnetCli
{
    public class ToolingConfiguration
    {
        public const string DefaultFileName = ".steeltoe.tooling.yml";

        public string target { get; set; }

        public SortedDictionary<string, Service> services { get; set; } = new SortedDictionary<string, Service>();

        public static ToolingConfiguration Load(string path)
        {
            var realPath = Directory.Exists(path) ? Path.Combine(path, DefaultFileName) : path;
            using (var reader = new StreamReader(realPath))
            {
                return Load(reader);
            }
        }

        public static ToolingConfiguration Load(TextReader reader)
        {
            var deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<ToolingConfiguration>(reader);
        }

        public void Store(string path)
        {
            var realPath = Directory.Exists(path) ? Path.Combine(path, DefaultFileName) : path;
            using (var writer = new StreamWriter(realPath))
            {
                Store(writer);
            }
        }

        public void Store(TextWriter writer)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(this);
            writer.Write(yaml);
            writer.Flush();
        }

        public class Service
        {
            public string type { get; set; }

            public Service()
            {
            }

            public Service(string type)
            {
                this.type = type;
            }
        }
    }
}
