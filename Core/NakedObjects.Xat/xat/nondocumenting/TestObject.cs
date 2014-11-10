// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System;
using System.Linq;
using System.Text;
using Common.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NakedObjects.Architecture.Adapter;
using NakedObjects.Architecture.Component;
using NakedObjects.Architecture.Facet;
using NakedObjects.Architecture.Resolve;
using NakedObjects.Core.Resolve;
using NakedObjects.Util;
using NakedObjects.Core.Util;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using NakedObjects.Architecture.Menu; 

namespace NakedObjects.Xat {
    internal class TestObject : TestHasActions, ITestObject {
        private static readonly ILog LOG;
        private readonly ILifecycleManager lifecycleManager;
        private readonly IObjectPersistor persistor;
        private readonly ITransactionManager transactionManager;

        static TestObject() {
            LOG = LogManager.GetLogger(typeof (TestObject));
        }

        public TestObject(ILifecycleManager lifecycleManager, IObjectPersistor persistor, INakedObject nakedObject, ITestObjectFactory factory, ITransactionManager transactionManager)
            : base(factory, lifecycleManager) {
            this.lifecycleManager = lifecycleManager;
            this.persistor = persistor;
            this.transactionManager = transactionManager;
            LOG.DebugFormat("Created test object for {0}", nakedObject);
            NakedObject = nakedObject;
        }

        #region ITestObject Members

        public ITestProperty[] Properties {
            get { return NakedObject.Spec.Properties.Select(x => factory.CreateTestProperty(x, this)).ToArray(); }
        }

        public object GetDomainObject() {
            return NakedObject.GetDomainObject();
        }

        public string IconName {
            get {
                Assert.IsNotNull(NakedObject, "Cannot get icon for null object");
                return NakedObject.IconName();
            }
        }

        public override string Title {
            get {
                Assert.IsNotNull(NakedObject, "Cannot get title for null object");
                return NakedObject.TitleString();
            }
        }

        public ITestObject Save() {
            AssertCanBeSaved();

            transactionManager.StartTransaction();
            lifecycleManager.MakePersistent(NakedObject);
            transactionManager.EndTransaction();
            return this;
        }

        public ITestObject Refresh() {
            persistor.Refresh(NakedObject);
            return this;
        }

        public ITestObject AssertTitleEquals(string expectedTitle) {
            Assert.IsTrue(Title.Equals(expectedTitle), "Expected title '" + expectedTitle + "' but got '" + Title + "'");

            return this;
        }

        public ITestObject AssertIsType(Type expected) {
            Type actualType = NakedObject.GetDomainObject().GetType();
            actualType = TypeUtils.IsProxy(actualType) ? actualType.BaseType : actualType;
            Assert.IsTrue(actualType.Equals(expected), "Expected type " + expected + " but got " + actualType);
            return this;
        }

        public ITestProperty GetPropertyByName(string name) {
            var q = Properties.Where(x => x.Name == name);
            if (q.Count() < 1) Assert.Fail("No Property named '" + name + "'");
            if (q.Count() > 1) Assert.Fail("More than one Property named '" + name + "'");
            return q.Single();
        }

        public ITestProperty GetPropertyById(string id) {
            var q = Properties.Where(x => x.Id == id);
            if (q.Count() != 1) Assert.Fail("No Property with Id '" + id + "'");
            return q.Single();
        }

        public ITestObject AssertCanBeSaved() {
            Assert.IsTrue(NakedObject.ResolveState.IsTransient(), "Can only persist a transient object: " + NakedObject);
            Assert.IsTrue(NakedObject.Spec.Persistable == PersistableType.UserPersistable, "Object not persistable by user: " + NakedObject);

            Properties.ForEach(p => p.AssertIsValidToSave());

            var validatorFacet = NakedObject.Spec.GetFacet<IValidateObjectFacet>();

            var result = validatorFacet.Validate(NakedObject);

            if (!string.IsNullOrEmpty(result)) {
                Assert.Fail(result);
            }

            return this;
        }

        public ITestObject AssertCannotBeSaved() {
            try {
                AssertCanBeSaved();
            }
            catch (AssertFailedException) {
                // expected 
                return this;
            }
            Assert.Fail("Object should not be saveable");
            return this; // for compiler 
        }

        public ITestObject AssertIsTransient() {
            Assert.IsTrue(NakedObject.ResolveState.IsTransient(), "Object is not transient");
            return this;
        }

        public ITestObject AssertIsPersistent() {
            Assert.IsTrue(NakedObject.ResolveState.IsPersistent(), "Object is not persistent");
            return this;
        }

        public virtual string GetPropertyOrder() {
            var props = this.Properties;
            var order = new StringBuilder();
            for (int i = 0; i < props.Length; i++) {
                order.Append(props[i].Name);
                order.Append(i < props.Length - 1 ? ", " : "");
            }
            return order.ToString();
        }

        public ITestHasProperties AssertPropertyOrderIs(string order) {
            Assert.AreEqual(order, GetPropertyOrder());
            return this;
        }

        public ITestMenu GetMenu() {
            IMenuImmutable menu = NakedObject.Spec.ObjectMenu;
            return new TestMenu(menu, factory, this);
        }
        #endregion

        public override bool Equals(Object obj) {
            if (obj is TestObject) {
                var testObject = (TestObject) obj;
                return testObject.NakedObject == NakedObject;
            }
            return false;
        }

        public override string ToString() {
            return NakedObject == null ? "" : NakedObject.ToString();
        }

        public override int GetHashCode() {
            return NakedObject.GetHashCode();
        }
    }

    // Copyright (c) INakedObject Objects Group Ltd.
}