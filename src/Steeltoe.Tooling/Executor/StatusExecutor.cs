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
    /// A workflow to display the deployment status of applications and dependent services.
    /// </summary>
    [RequiresTarget]
    public class StatusExecutor : GroupExecutor
    {
        /// <summary>
        /// Create a workflow to display the deployment status of applications and dependent services.
        /// </summary>
        public StatusExecutor() : base(false)
        {
        }

        internal override void ExecuteForApp(string app)
        {
            var status = Context.Driver.GetAppStatus(app);
            Context.Console.WriteLine($"{app} {status.ToString().ToLower()}");
        }

        internal override void ExecuteForService(string service)
        {
            var status = Context.Driver.GetServiceStatus(service);
            Context.Console.WriteLine($"{service} {status.ToString().ToLower()}");
        }
    }
}
