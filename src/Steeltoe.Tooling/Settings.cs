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

using System.IO;

namespace Steeltoe.Tooling
{
    /// <summary>
    /// Represents Steeltoe Tooling project settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Whether debugging has been enabled.
        /// </summary>
        public static bool DebugEnabled { get; set; }

        /// <summary>
        /// Whether verbose console output has been enabled.
        /// </summary>
        public static bool VerboseEnabled { get; set; }

        /// <summary>
        /// Whether the dummy deployment target and dummy services have been enabled.  Used for debugging and development.
        /// </summary>
        public static bool DummiesEnabled { get; set; }

        /// <summary>
        /// The maximum number of checks when waiting for an application or application service lifecycle transition.
        /// </summary>
        public static int MaxChecks { get; set; } = -1;

        static Settings()
        {
            DummiesEnabled = File.Exists(".steeltoe.dummies");
        }
    }
}
