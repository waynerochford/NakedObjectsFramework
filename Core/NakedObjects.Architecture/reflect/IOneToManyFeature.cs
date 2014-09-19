// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

namespace NakedObjects.Architecture.Reflect {
    /// <summary>
    ///     Base interface for <see cref="IOneToManyAssociation" /> only.
    /// </summary>
    /// <para>
    ///     Introduced for symmetry with <see cref="IOneToOneFeature" />; if we ever
    ///     support collections as parameters then would also be the base
    ///     interface for a <c>IOneToManyActionParameter</c>.
    /// </para>
    /// <para>
    ///     Is also the route upto the <see cref="INakedObjectFeature" /> superinterface.
    /// </para>
    public interface IOneToManyFeature : INakedObjectFeature {}
}