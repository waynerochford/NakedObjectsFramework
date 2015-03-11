// Copyright � Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 

using System.Threading;
using NakedObjects.Core.Service;

namespace NakedObjects.Core.Context {
    /// <summary>
    ///     Basic multi-user implementation of NakedObjects that stores a set of components for each thread in use
    /// </summary>
    public class ThreadContext : MultiUserContext {
        private readonly ThreadLocal<NakedObjectsData> local = new ThreadLocal<NakedObjectsData>(InitialiseNewData);
        protected internal ThreadContext() {}


        /// <summary>
        ///     Get the component set used the current thread. If no set exists then a new component set
        ///     (NakedObjectsData) is created.
        /// </summary>
        protected override NakedObjectsData Local {
            get {
                if (InitialisationFatalError != null) {
                    throw InitialisationFatalError;
                }
                return local.Value;
            }
        }

        private static NakedObjectsData InitialiseNewData() {
            var local = new NakedObjectsData {ObjectPersistor = PersistorInstaller.CreateObjectPersistor()};
            local.ObjectPersistor.AddServices(MenuServicesInstaller, ContributedActionsInstaller, SystemServicesInstaller);
            local.ObjectPersistor.Init();
            return local;
        }

        protected internal override void TerminateSession() {}

        protected internal override string[] GetAllExecutionContextIds() {
            return new string[] {};
        }


        public override void ShutdownSession() {}

        public static NakedObjectsContext CreateInstance() {
            return new ThreadContext();
        }

        protected internal override void EnsureContextReady() {
            if (InitialisationFatalError != null) {
                throw InitialisationFatalError;
            }

            Local.UpdateNotifier.AllChangedObjects();
            Local.ObjectPersistor.Reset();
            ClearSession();
        }
    }

    // Copyright (c) Naked Objects Group Ltd.
}