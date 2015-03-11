// Copyright � Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 

using System;
using System.Linq;
using System.Reflection;
using NakedObjects.Architecture;
using NakedObjects.Architecture.Adapter;
using NakedObjects.Architecture.Persist;
using NakedObjects.Architecture.Reflect;
using NakedObjects.Architecture.Spec;
using NakedObjects.Core.Context;
using NakedObjects.Core.Persist;
using NakedObjects.Persistor.Transaction;

namespace NakedObjects.Persistor.Objectstore {
    public interface INakedObjectStore : IRequiresSetup {
        /// <summary>
        ///     Determine if the object store has been initialized with its set of start up objects. This method is
        ///     called only once after <see cref="IRequiresSetup.Init" /> has been called. If this flag returns
        ///     <c>false</c> the framework will run the fixtures to initialise the object store.
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        ///     The name of this object store (for logging/debugging purposes)
        /// </summary>
        string Name { get; }

        void AbortTransaction();

        /// <summary>
        ///     Makes a naked object persistent. The specified object should be stored away via this object store's
        ///     persistence mechanism, and have an new and unique OID assigned to it (by setting the object's
        ///     <see cref="INakedObject.Oid" />. The object, should also be added to the cache as the object is
        ///     implicitly 'in use'.
        /// </summary>
        /// <para>
        ///     If the object has any associations then each of these, where they aren't already persistent, should
        ///     also be made persistent by recursively calling this method.
        /// </para>
        /// <para>
        ///     If the object to be persisted is a collection, then each element of that collection, that is not
        ///     already persistent, should be made persistent by recursively calling this method.
        /// </para>
        ICreateObjectCommand CreateCreateObjectCommand(INakedObject nakedObject);

        void RegisterService(string name, IOid oid);

        /// <summary>
        ///     Removes the specified object from the object store. The specified object's data should be removed from
        ///     the persistence mechanism and, if it is cached (which it probably is), removed from the cache also.
        /// </summary>
        IDestroyObjectCommand CreateDestroyObjectCommand(INakedObject nakedObject);

        /// <summary>
        ///     Persists the specified object's state. Essentially the data held by the persistence mechanism should be
        ///     updated to reflect the state of the specified objects. Once updated, the object store should issue a
        ///     notification to all of the object's users via the <see cref="IUpdateNotifier" /> object.
        /// </summary>
        ISaveObjectCommand CreateSaveObjectCommand(INakedObject nakedObject);

        void EndTransaction();

        IQueryable<T> GetInstances<T>() where T : class;

        IQueryable GetInstances(Type type);

        IQueryable GetInstances(INakedObjectSpecification specification);

        T CreateInstance<T>() where T : class;

        object CreateInstance(Type type);


        /// <summary>
        ///     Retrieves the object identified by the specified OID from the object store. The cache should be checked
        ///     first and, if the object is cached, the cached version should be returned. It is important that if this
        ///     method is called again, while the originally returned object is in working memory, then this method
        ///     must return that same object.
        /// </summary>
        /// <para>
        ///     Assuming that the object is not cached then the data for the object should be retrieved from the
        ///     persistence mechanism and the object recreated (as describe previously). The specified OID should then
        ///     be assigned to the recreated object by settings its OID.
        /// </para>
        /// <para>
        ///     If the persistence mechanism does not known of an object with the specified OID then a
        ///     <see cref="FindObjectException" /> should be thrown
        /// </para>
        /// <para>
        ///     The OID could be for an internal collection, and is therefore related to the parent
        ///     object (using a <see cref="AggregateOid" />. The elements for an internal collection are commonly
        ///     stored as part of the parent object, so to get element the parent object needs to be retrieved first,
        ///     and the internal collection can be got from that
        /// </para>
        /// <param name="oid">of the object to be retrieved</param>
        /// <param name="hint"></param>
        /// <returns> the requested naked object</returns>
        /// <exception cref="FindObjectException">when no object corresponding to the oid can be found</exception>
        INakedObject GetObject(IOid oid, INakedObjectSpecification hint);

        void Reload(INakedObject nakedObject);

        /// <summary>
        ///     Returns the OID for the adapted service
        /// </summary>
        IOid GetOidForService(string name, string typeName);

        void ResolveField(INakedObject nakedObject, INakedObjectAssociation field);

        void ResolveImmediately(INakedObject nakedObject);

        void Execute(IPersistenceCommand[] commands);

        void StartTransaction();

        // TODO this should be done by the execute method
        bool Flush(IPersistenceCommand[] commands);

        void Reset();

        PropertyInfo[] GetKeys(Type type);

        void Refresh(INakedObject nakedObject);
        int CountField(INakedObject nakedObject, INakedObjectAssociation association);
        INakedObject FindByKeys(Type type, object[] keys);
    }

    // Copyright (c) Naked Objects Group Ltd.
}