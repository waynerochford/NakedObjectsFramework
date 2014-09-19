// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using NakedObjects.Architecture.Facets.Objects.Immutable;

namespace NakedObjects.Architecture.Facets.Objects.Value {
    /// <summary>
    ///     Indicates that this class is aggregated, that is, wholly contained within a larger object.
    /// </summary>
    /// <para>
    ///     The object may or may not be immutable <see cref="IImmutableFacet" />, and may
    ///     reference regular entity domain objects or other aggregated objects.
    /// </para>
    /// <para>
    ///     One important subset of aggregated objects are  value types <see cref="IValueFacet" />
    ///     In fact, value types only have one mandatory semantic, that they are aggregated.
    /// </para>
    /// <para>
    ///     In terms of an analogy, aggregated is similar to Hibernate's component types
    ///     (for larger mutable in-line objects) or to Hibernate's user-defined types (for
    ///     smaller immutable values).
    /// </para>
    public interface IAggregatedFacet : IMarkerFacet {}
}