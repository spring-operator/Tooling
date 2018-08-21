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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Steeltoe.Tooling.Cli.CloudFoundry
{
    public class CloudFoundryServiceManager : IServiceManager
    {
        private static Dictionary<string, string> serviceMapping = new Dictionary<string, string>
        {
            {"cloud-foundry-config-server", "p-config-server"}
        };

        public void StartService(Shell shell, string name, string type)
        {
            shell.Run("cf", $"create-service {serviceMapping[type]} standard {name}");
        }

        public void StopService(Shell shell, string name)
        {
            shell.Run("cf", $"delete-service {name} -f");
        }

        public string CheckService(Shell shell, string name)
        {
            var result = shell.Run("cf", $"service {name}");
            Regex exp = new Regex(@"^status:\s+(.*)$", RegexOptions.Multiline);
            Match match = exp.Match(result.Out);
            if (match.Groups[1].ToString().TrimEnd().Equals("create succeeded"))
            {
                return "online";
            }

            return "offline";
        }
    }
}