﻿// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 

using System;
using NakedObjects.Architecture.Facets;

namespace NakedObjects.Architecture.Adapter.Value {
    public interface IGuidValueFacet : IFacet {
        Guid GuidValue(INakedObject nakedObject);

        INakedObject CreateValue(Guid guidValue);
    }

    // Copyright (c) Naked Objects Group Ltd.
}