// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System;
using NakedObjects.Architecture.Adapter;
using NakedObjects.Architecture.Component;
using NakedObjects.Architecture.Facet;
using NakedObjects.Architecture.Spec;

namespace NakedObjects.Meta.Facet {
    [Serializable]
    public abstract class ViewModelFacetAbstract : FacetAbstract, IViewModelFacet {
        protected ViewModelFacetAbstract(Type type, ISpecification holder)
            : base(type, holder) {}

        #region IViewModelFacet Members

        public abstract string[] Derive(INakedObject nakedObject, INakedObjectManager nakedObjectManager, IContainerInjector injector);
        public abstract void Populate(string[] keys, INakedObject nakedObject, INakedObjectManager nakedObjectManager, IContainerInjector injector);

        public virtual bool IsEditView(INakedObject nakedObject) {
            return false;
        }

        #endregion
    }
}