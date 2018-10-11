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

using System;
using McMaster.Extensions.CommandLineUtils;
using Steeltoe.Tooling;
using Steeltoe.Tooling.Executor;

// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Steeltoe.Cli
{
    [Command(Description = "Initialize a project for Steeltoe Developer Tools.")]
    public class InitCommand : Command
    {
        public const string Name = "init";

        [Option("-F|--force", Description = "Initialize the project even if already initialized")]
        private bool Force { get; }

        public InitCommand(IConsole console) : base(console)
        {
        }

        protected override IExecutor GetExecutor()
        {
            return new InitExecutor(Force);
        }
    }

    internal class InitExecutor : IExecutor
    {
        private readonly bool _force;

        internal InitExecutor(Boolean force = false)
        {
            _force = force;
        }

        public void Execute(Context context)
        {
            var cfgFilePath = Program.ProjectConfigurationPath;
            if (cfgFilePath == null)
            {
                cfgFilePath = context.ProjectDirectory;
            }

            var cfgFile = new ToolingConfigurationFile(cfgFilePath);
            if (cfgFile.Exists() && !_force)
            {
                throw new ToolingException("Project already initialized");
            }

            cfgFile.Store();
            context.Console.WriteLine("Project initialized for Steeltoe Developer Tools");
        }
    }
}
