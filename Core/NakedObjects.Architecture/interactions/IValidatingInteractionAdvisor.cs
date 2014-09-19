// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using NakedObjects.Architecture.Facets.Ordering.MemberOrder;
using NakedObjects.Architecture.Facets.Propparam.Validate.MaxLength;

namespace NakedObjects.Architecture.Interactions {
    /// <summary>
    ///     Mix-in interface for facets that can advise as to whether a proposed value is valid
    /// </summary>
    /// <para>
    ///     For example, <see cref="IMaxLengthFacet" /> does constrain the
    ///     length of candidate values, whereas <see cref="IMemberOrderFacet" /> does not - it's basically basically just
    ///     a UI hint
    /// </para>
    /// <seealso cref="IDisablingInteractionAdvisor" />
    /// <seealso cref="IHidingInteractionAdvisor" />
    public interface IValidatingInteractionAdvisor {
        /// <summary>
        ///     Whether the validation represented by this facet passes or fails
        /// </summary>
        /// <para>
        ///     Implementations should use the provided <see cref="InteractionContext" /> to
        ///     determine whether they declare the interaction invalid.  They must
        ///     however guard against a <c>null</c> <see cref="InteractionContext.Target" /> target
        ///     and <see cref="InteractionContext.Session" /> session} - neither are
        ///     guaranteed to be populated
        /// </para>
        string Invalidates(InteractionContext ic);

        /// <summary>
        ///     Create (not throw) an exception of the appropriate subclass
        ///     if the validation has failed
        /// </summary>
        InvalidException CreateExceptionFor(InteractionContext ic);
    }
}