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

namespace Steeltoe.Tooling.Executor
{
    /// <summary>
    /// A workflow to deploy applications and dependent services to the current target.
    /// </summary>
    [RequiresTarget]
    public class DeployExecutor : GroupExecutor
    {
        /// <summary>
        /// Create a new workflow to deploy applications and dependent services to the current target.
        /// </summary>
        public DeployExecutor() : base(false)
        {
        }

        internal override void ExecuteForApp(string app)
        {
            foreach (var service in Context.Configuration.GetServices())
            {
                int count = 0;
                while (Context.Driver.GetServiceStatus(service) != Lifecycle.Status.Online)
                {
                    ++count;
                    if (Settings.MaxChecks >= 0 && ++count > Settings.MaxChecks)
                    {
                        throw new ToolingException($"max check exceeded ({Settings.MaxChecks})");
                    }
                    Context.Console.WriteLine($"Waiting for service '{service}' to come online ({count})");
                }
            }
            Context.Console.WriteLine($"Deploying app '{app}'");
            new Lifecycle(Context, app).Deploy();
        }

        internal override void ExecuteForService(string service)
        {
            Context.Console.WriteLine($"Deploying service '{service}'");
            new Lifecycle(Context, service).Deploy();
        }
    }
}
