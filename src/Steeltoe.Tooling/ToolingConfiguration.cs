﻿// Copyright 2018 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;

namespace Steeltoe.Tooling
{
    public class ToolingConfiguration
    {
        private static readonly ILogger Logger = Logging.LoggerFactory.CreateLogger<ToolingConfiguration>();

        [YamlMember(Alias = "environment")]
        public string EnvironmentName { get; set; }

        [YamlMember(Alias = "services")]
        public SortedDictionary<string, Service> Services { get; protected set; } =
            new SortedDictionary<string, Service>();

        public class Service
        {
            [YamlMember(Alias = "type")]
            public string ServiceTypeName { get; set; }

            [YamlMember(Alias = "enabled")]
            public bool Enabled { get; set; }

            [YamlMember(Alias = "args")]
            public SortedDictionary<string, string> Args { get; protected set; } =
                new SortedDictionary<string, string>();
        }

        private readonly List<IToolingConfigurationListener> _listeners = new List<IToolingConfigurationListener>();

        public void AddListener(IToolingConfigurationListener listener)
        {
            _listeners.Add(listener);
        }

        public void NotifyChanged()
        {
            Logger.LogDebug("configuration changed");
            foreach (var listener in _listeners)
            {
                listener.ConfigurationChangeEvent();
            }
        }
    }
}
