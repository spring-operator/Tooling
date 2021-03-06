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

using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Steeltoe.Tooling.Executor;

namespace Steeltoe.Cli
{
    [Command(Description = "Add an app or service")]
    public class AddCommand : Command
    {
        public const string Name = "add";

        [Required(ErrorMessage = "'app' or service type not specified")]
        [Argument(0, Name = "type", Description = "'app' or service type")]
        private string AppOrServiceType { get; } = null;

        [Required(ErrorMessage = "App or service name not specified")]
        [Argument(1, Name = "name", Description = "App or service name")]
        private string AppOrServiceName { get; } = null;

        public AddCommand(IConsole console) : base(console)
        {
        }

        protected override Executor GetExecutor()
        {
            return AppOrServiceType == "app"
                ? new AddExecutor(AppOrServiceName)
                : new AddExecutor(AppOrServiceName, AppOrServiceType);
        }
    }
}
