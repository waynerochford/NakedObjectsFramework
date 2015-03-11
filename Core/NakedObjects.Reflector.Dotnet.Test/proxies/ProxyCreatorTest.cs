﻿// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using NakedObjects.Boot;
using NakedObjects.Core.NakedObjectsSystem;
using NakedObjects.Reflector.DotNet.Reflect.Proxies;
using NakedObjects.Services;
using NakedObjects.Xat;
using NUnit.Framework;

namespace NakedObjects.Reflector.DotNet.Proxies {
    public class HasProperty {
        public virtual string Prop { get; set; }
    }


    public class HasCollectionWithVirtualAccessors {
        private ICollection<HasProperty> aCollection = new List<HasProperty>();

        public virtual ICollection<HasProperty> ACollection {
            get { return aCollection; }
            set { aCollection = value; }
        }

        public virtual void AddToACollection(HasProperty val) {
            aCollection.Add(val);
        }

        public virtual void RemoveFromACollection(HasProperty val) {
            aCollection.Remove(val);
        }

        public virtual void ClearACollection() {
            aCollection.Clear();
        }
    }

    public class HasCollectionWithNonVirtualAccessors {
        private ICollection<HasProperty> aCollection = new List<HasProperty>();

        public virtual ICollection<HasProperty> ACollection {
            get { return aCollection; }
            set { aCollection = value; }
        }

        public void AddToACollection(HasProperty val) {
            aCollection.Add(val);
        }

        public  void RemoveFromACollection(HasProperty val) {
            aCollection.Remove(val);
        }

        public  void ClearACollection() {
            aCollection.Clear();
        }
    }


    [TestFixture]
    public class ProxyCreatorTest : AcceptanceTestCase {
        #region Setup/Teardown

        [SetUp]
        public void SetupTest() {
            InitializeNakedObjectsFramework();
        }

        [TearDown]
        public void TearDownTest() {
            CleanupNakedObjectsFramework();
        }

        #endregion

        protected override IServicesInstaller MenuServices {
            get { return new ServicesInstaller(new[] {new SimpleRepository<HasProperty>()}); }
        }

        private readonly DotNetDomainObjectContainerInjector injector = new DotNetDomainObjectContainerInjector(new[] { new SimpleRepository<HasProperty>() });


        [Test]
        public void CreateProxyWithVirtualAddTo() {
            Type proxyType = ProxyCreator.CreateProxyType(typeof (HasCollectionWithVirtualAccessors));
            var proxy = (HasCollectionWithVirtualAccessors) Activator.CreateInstance(proxyType);
            injector.InitDomainObject(proxy);

            var hasProperty = new HasProperty();

            proxy.AddToACollection(hasProperty);

            Assert.IsTrue(proxy.ACollection.Contains(hasProperty));
        }

        [Test]
        public void CreateProxyWithVirtualRemoveFrom() {
            Type proxyType = ProxyCreator.CreateProxyType(typeof (HasCollectionWithVirtualAccessors));
            var proxy = (HasCollectionWithVirtualAccessors) Activator.CreateInstance(proxyType);
            injector.InitDomainObject(proxy);
            var hasProperty = new HasProperty();

            proxy.ACollection.Add(hasProperty);

            Assert.IsTrue(proxy.ACollection.Contains(hasProperty));

            proxy.RemoveFromACollection(hasProperty);

            Assert.IsFalse(proxy.ACollection.Contains(hasProperty));
        }

        [Test]
        public void CreateProxyWithVirtualClear() {
            Type proxyType = ProxyCreator.CreateProxyType(typeof(HasCollectionWithVirtualAccessors));
            var proxy = (HasCollectionWithVirtualAccessors)Activator.CreateInstance(proxyType);
            injector.InitDomainObject(proxy);
            var hasProperty = new HasProperty();

            proxy.ACollection.Add(hasProperty);

            Assert.IsTrue(proxy.ACollection.Contains(hasProperty));

            proxy.ClearACollection();

            Assert.IsFalse(proxy.ACollection.Contains(hasProperty));
        }

        [Test]
        public void CreateProxyWithNonVirtualAccessors() {
            Type proxyType = ProxyCreator.CreateProxyType(typeof(HasCollectionWithNonVirtualAccessors));
            var proxy = (HasCollectionWithNonVirtualAccessors)Activator.CreateInstance(proxyType);
            injector.InitDomainObject(proxy);

            var hasProperty = new HasProperty();

            proxy.AddToACollection(hasProperty);

            Assert.IsTrue(proxy.ACollection.Contains(hasProperty));
        }


        [Test]
        public void CreateProxyWithVirtualProperty() {
            Type proxyType = ProxyCreator.CreateProxyType(typeof (HasProperty));
            var proxy = (HasProperty) Activator.CreateInstance(proxyType);
            injector.InitDomainObject(proxy);
            const string testValue = "A Test Value";

            proxy.Prop = testValue;

            Assert.AreEqual(testValue, proxy.Prop);
        }
    }
}