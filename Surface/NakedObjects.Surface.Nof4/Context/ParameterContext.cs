// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using NakedObjects.Architecture.Spec;
using NakedObjects.Surface.Nof4.Wrapper;

namespace NakedObjects.Surface.Nof4.Context {
    public class ParameterContext : Context {
        public IActionParameterSpec Parameter { get; set; }

        public IActionSpec Action { get; set; }

        public override string Id {
            get { return Parameter.Id; }
        }

        public override ITypeSpec Specification {
            get { return Parameter.Spec; }
        }

        public string OverloadedUniqueId { get; set; }

        public ParameterContextSurface ToParameterContextSurface(INakedObjectsSurface surface, INakedObjectsFramework framework) {
            var pc = new ParameterContextSurface {
                Parameter = new NakedObjectActionParameterWrapper(Parameter, surface, framework, OverloadedUniqueId ?? ""),
                Action = new NakedObjectActionWrapper(Action, surface, framework, OverloadedUniqueId ?? "")
            };
            return ToContextSurface(pc, surface, framework);
        }
    }
}