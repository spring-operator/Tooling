// Copyright 2018 the original author or authors.
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
using System.IO;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using Steeltoe.Tooling;
using Steeltoe.Tooling.Executor;

namespace Steeltoe.Cli
{
    public abstract class Command : IConfigurationListener
    {
        public const string CliName = "steeltoe";

        private bool _configDirty;

        protected int OnExecute(CommandLineApplication app)
        {
            try
            {
                var requiresInitializedProject = true;
                {
                    var executor = GetExecutor();
                    MemberInfo minfo = executor.GetType();
                    foreach (var attr in minfo.GetCustomAttributes(true))
                    {
                        var initAttr = attr as RequiresInitializationAttribute;
                        if (initAttr != null)
                        {
                            requiresInitializedProject = initAttr.IsRequired;
                        }
                    }
                }
                var projectDirectory = Directory.GetCurrentDirectory();
                var config = Program.Configuration;
                if (requiresInitializedProject && !config.Exists())
                {
                    throw new ToolingException("Project has not been initialized for Steeltoe Developer Tools");
                }

                config.AddListener(this);
                var context = new Context(projectDirectory, config, new CommandShell(app.Out));
                GetExecutor().Execute(context);
                if (_configDirty)
                {
                    config.Store();
                }
                return 0;
            }
            catch (ArgumentException e)
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    app.Error.WriteLine(e.Message);
                }

                return 1;
            }
            catch (ToolingException e)
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    app.Error.WriteLine(e.Message);
                }

                return 2;
            }
            catch (Exception e)
            {
                app.Error.WriteLine(e);
                return -1;
            }
        }

        protected abstract IExecutor GetExecutor();

        public void ConfigurationChangeEvent()
        {
            _configDirty = true;
        }
    }
}