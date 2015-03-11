// Copyright � Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using NakedObjects.Architecture.Facets;
using NakedObjects.Architecture.Facets.Actions.Executed;
using NakedObjects.Architecture.Facets.AutoComplete;
using NakedObjects.Architecture.Facets.Disable;
using NakedObjects.Architecture.Facets.Hide;
using NakedObjects.Architecture.Facets.Propcoll.NotPersisted;
using NakedObjects.Architecture.Facets.Properties.Access;
using NakedObjects.Architecture.Facets.Properties.Choices;
using NakedObjects.Architecture.Facets.Properties.Defaults;
using NakedObjects.Architecture.Facets.Properties.Modify;
using NakedObjects.Architecture.Facets.Properties.Validate;
using NakedObjects.Architecture.Facets.Propparam.Validate.Mandatory;
using NakedObjects.Architecture.Reflect;
using NakedObjects.Reflector.DotNet.Facets.Actions.Executed;
using NakedObjects.Reflector.DotNet.Facets.AutoComplete;
using NakedObjects.Reflector.DotNet.Facets.Disable;
using NakedObjects.Reflector.DotNet.Facets.Hide;
using NakedObjects.Reflector.DotNet.Facets.Propcoll.Access;
using NakedObjects.Reflector.DotNet.Facets.Propcoll.NotPersisted;
using NakedObjects.Reflector.DotNet.Facets.Properties.Choices;
using NakedObjects.Reflector.DotNet.Facets.Properties.Defaults;
using NakedObjects.Reflector.DotNet.Facets.Properties.Modify;
using NakedObjects.Reflector.DotNet.Facets.Properties.Validate;
using NUnit.Framework;

namespace NakedObjects.Reflector.DotNet.Facets.Properties {
    [TestFixture]
    public class PropertyFieldMethodsFacetFactoryTest : AbstractFacetFactoryTest {
        #region Setup/Teardown

        [SetUp]
        public override void SetUp() {
            base.SetUp();
            facetFactory = new PropertyMethodsFacetFactory {Reflector = reflector};
        }

        [TearDown]
        public override void TearDown() {
            facetFactory = null;
            base.TearDown();
        }

        #endregion

        private PropertyMethodsFacetFactory facetFactory;

        protected override Type[] SupportedTypes {
            get {
                return new[] {
                                 typeof (IMandatoryFacet),
                                 typeof (IPropertyAccessorFacet),
                                 typeof (IPropertyValidateFacet),
                                 typeof (IPropertyDefaultFacet),
                                 typeof (IPropertyChoicesFacet),
                                 typeof (IPropertySetterFacet),
                                 typeof (IPropertyClearFacet),
                                 typeof (INotPersistedFacet),
                                 typeof (IDisabledFacet)
                             };
            }
        }

        protected override IFacetFactory FacetFactory {
            get { return facetFactory; }
        }

        private class Customer {
            public string FirstName {
                get { return null; }
            }
        }

        private class Customer1 {
            public string FirstName {
                get { return null; }
                set { }
            }
        }

        private class Customer10 {
            public string FirstName {
                get { return null; }
            }

            public string[] ChoicesFirstName() {
                return null;
            }
        }

        private class Customer10r {
            public string FirstName {
                get { return null; }
            }

            [Executed(Where.Remotely)]
            public string[] ChoicesFirstName() {
                return null;
            }
        }

        private class Customer10l {
            public string FirstName {
                get { return null; }
            }

            [Executed(Where.Locally)]
            public string[] ChoicesFirstName() {
                return null;
            }
        }

        private class Customer11 {
            public string FirstName {
                get { return null; }
            }

            public string DefaultFirstName() {
                return null;
            }
        }

        private class Customer11r {
            public string FirstName {
                get { return null; }
            }

            [Executed(Where.Remotely)]
            public string DefaultFirstName() {
                return null;
            }
        }

        private class Customer11l {
            public string FirstName {
                get { return null; }
            }

            [Executed(Where.Locally)]
            public string DefaultFirstName() {
                return null;
            }
        }

        private class Customer12 {
            public string FirstName {
                get { return null; }
            }

            public string ValidateFirstName(string firstName) {
                return null;
            }
        }

        private class Customer13 {
            public string FirstName {
                get { return null; }
            }

            public string SecondName {
                get { return null; }
            }

            public bool HideFirstName() {
                return false;
            }

            public bool HideSecondName(string name) {
                return false;
            }
        }

        private class Customer14 {
            public string FirstName {
                get { return null; }
            }

            public string SecondName {
                get { return null; }
            }

            public bool HidePropertyDefault() {
                return false;
            }

            public bool HideSecondName(string name) {
                return false;
            }
        }

        private class Customer15 {
            public string FirstName {
                get { return null; }
            }

            public string SecondName {
                get { return null; }
            }

            public string DisableFirstName(string firstName) {
                return null;
            }

            public string DisableSecondName() {
                return null;
            }
        }

        private class Customer16 {
            public string FirstName {
                get { return null; }
            }

            public string SecondName {
                get { return null; }
            }

            public string DisablePropertyDefault() {
                return null;
            }

            public string DisableSecondName() {
                return null;
            }
        }

        private class Customer17 {
            public string FirstName {
                get { return null; }
            }

            public string LastName {
                get { return null; }
            }

            public string[] ChoicesFirstName(string lastName) {
                return null;
            }
        }

        private class Customer18 {
            public string FirstName {
                get { return null; }
            }

            public string LastName {
                get { return null; }
            }

            public string[] ChoicesFirstName() {
                return null;
            }

            public string[] ChoicesFirstName(string lastName) {
                return null;
            }
        }

        private class Customer19 {
            public string FirstName {
                get { return null; }
            }

            [Executed(Ajax.Disabled)]
            public string ValidateFirstName(string firstName) {
                return null;
            }
        }

        private class Customer20 {
            public string FirstName {
                get { return null; }
            }

            [Executed(Ajax.Enabled)]
            public string ValidateFirstName(string firstName) {
                return null;
            }
        }


        private class Customer2 {
            public string FirstName {
                get { return null; }
                set { }
            }
        }

        private class Customer3 {
            public string FirstName {
                get { return null; }
                set { }
            }
        }

        private class Customer4 {
            public string FirstName {
                get { return null; }
            }

            public void ModifyFirstName(string firstName) {}
        }

        private class Customer6 {
            public string FirstName {
                get { return null; }
            }

            public void ModifyFirstName(string firstName) {}
        }

        private class Customer7 {
            public string FirstName {
                get { return null; }
                set { }
            }

            public void ModifyFirstName(string firstName) {}
        }

        private class Customer8 {
            public string FirstName {
                get { return null; }
            }

            public void ClearFirstName() {}
        }

        private class Customer9 {
            public string FirstName {
                get { return null; }
                set { }
            }
        }

        public class CustomerStatic {
            public string FirstName {
                get { return null; }
                set { }
            }

            public string LastName {
                get { return null; }
                set { }
            }

            // set required otherwise marked as DisabledFacetAlways          
            public static string NameFirstName() {
                return "Given name";
            }

            public static string DescriptionFirstName() {
                return "Some old description";
            }

            public static bool AlwaysHideFirstName() {
                return true;
            }

            public static bool ProtectFirstName() {
                return true;
            }

            public static bool HideFirstName(IPrincipal principal) {
                return true;
            }

            public static string DisableFirstName(IPrincipal principal) {
                return "disabled for this user";
            }

            // set required otherwise marked as DisabledFacetAlways    
            public static bool AlwaysHideLastName() {
                return false;
            }

            public static bool ProtectLastName() {
                return false;
            }
        }

        private class Customer21 {
            public string FirstName {
                get { return null; }
            }

            public IQueryable<string> AutoCompleteFirstName(string name) {
                return null;
            }
        }

        private class Customer22 {
            public string FirstName {
                get { return null; }
            }

            public IEnumerable<string> AutoCompleteFirstName(string name) {
                return null;
            }
        }

        private class Customer23 {
            public string FirstName {
                get { return null; }
            }

            public IQueryable<string> AutoCompleteFirstName() {
                return null;
            }
        }

        private class Customer24 {
            public string FirstName {
                get { return null; }
            }

            public IQueryable<string> AutoCompleteFirstName(int name) {
                return null;
            }
        }

        private class Customer25 {
            public string FirstName {
                get { return null; }
            }

            public IQueryable<string> AutoCompletFirstName(string name) {
                return null;
            }
        }

        private class Customer26 {
            public string FirstName {
                get { return null; }
            }

            [PageSize(33)]
            public IQueryable<string> AutoCompleteFirstName([MinLength(3)] string name) {
                return null;
            }
        }

        public interface NameInterface {
             string Name { get; set; }
        }

        private class Customer27 {
            public NameInterface FirstName {
                get { return null; }
            }

            public IQueryable<NameInterface> AutoCompleteFirstName(string name) {
                return null;
            }
        }


        [Test]
        public void TestChoicesFacetFoundAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer10), "FirstName");
            MethodInfo propertyChoicesMethod = FindMethod(typeof (Customer10), "ChoicesFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyChoicesFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyChoicesFacetViaMethod);
            var propertyChoicesFacet = (PropertyChoicesFacetViaMethod) facet;
            Assert.AreEqual(propertyChoicesMethod, propertyChoicesFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod));

            IFacet facetExecuted = facetHolder.GetFacet(typeof (IExecutedControlMethodFacet));
            Assert.IsNull(facetExecuted);
        }

        [Test]
        public void TestChoicesFacetFoundAndMethodRemovedWithParms() {
            PropertyInfo property = FindProperty(typeof(Customer17), "FirstName");
            MethodInfo propertyChoicesMethod = FindMethod(typeof(Customer17), "ChoicesFirstName", new[] { typeof(string) });
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IPropertyChoicesFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyChoicesFacetViaMethod);
            var propertyChoicesFacet = (PropertyChoicesFacetViaMethod)facet;
            Assert.AreEqual(propertyChoicesMethod, propertyChoicesFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod));

            IFacet facetExecuted = facetHolder.GetFacet(typeof(IExecutedControlMethodFacet));
            Assert.IsNull(facetExecuted);
        }

        [Test]
        public void TestChoicesFacetFoundAndMethodRemovedDuplicate() {
            PropertyInfo property = FindProperty(typeof(Customer18), "FirstName");
            MethodInfo propertyChoicesMethod1 = FindMethod(typeof(Customer18), "ChoicesFirstName", new Type[] {});
            MethodInfo propertyChoicesMethod2 = FindMethod(typeof(Customer18), "ChoicesFirstName", new [] {typeof(string) });
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IPropertyChoicesFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyChoicesFacetViaMethod);
            var propertyChoicesFacet = (PropertyChoicesFacetViaMethod)facet;
            Assert.AreEqual(propertyChoicesMethod1, propertyChoicesFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod1));
            Assert.IsFalse(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod2));

            IFacet facetExecuted = facetHolder.GetFacet(typeof(IExecutedControlMethodFacet));
            Assert.IsNull(facetExecuted);
        }


        [Test]
        public void TestChoicesFacetFoundAndMethodRemovedLocal() {
            PropertyInfo property = FindProperty(typeof (Customer10l), "FirstName");
            MethodInfo propertyChoicesMethod = FindMethod(typeof (Customer10l), "ChoicesFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyChoicesFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyChoicesFacetViaMethod);
            var propertyChoicesFacet = (PropertyChoicesFacetViaMethod) facet;
            Assert.AreEqual(propertyChoicesMethod, propertyChoicesFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod));

            var facetExecuted = facetHolder.GetFacet<IExecutedControlMethodFacet>();
            Assert.IsNotNull(facetExecuted);

            Assert.AreEqual(facetExecuted.ExecutedWhere(propertyChoicesMethod), Architecture.Facets.Where.Locally);
        }

        [Test]
        public void TestChoicesFacetFoundAndMethodRemovedRemote() {
            PropertyInfo property = FindProperty(typeof (Customer10r), "FirstName");
            MethodInfo propertyChoicesMethod = FindMethod(typeof (Customer10r), "ChoicesFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyChoicesFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyChoicesFacetViaMethod);
            var propertyChoicesFacet = (PropertyChoicesFacetViaMethod) facet;
            Assert.AreEqual(propertyChoicesMethod, propertyChoicesFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyChoicesMethod));

            var facetExecuted = facetHolder.GetFacet<IExecutedControlMethodFacet>();
            Assert.IsNotNull(facetExecuted);

            Assert.AreEqual(facetExecuted.ExecutedWhere(propertyChoicesMethod), Architecture.Facets.Where.Remotely);
        }

        [Test]
        public void TestAutoCompleteFacetFoundAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer21), "FirstName");
            MethodInfo propertyAutoCompleteMethod = FindMethodIgnoreParms(typeof (Customer21), "AutoCompleteFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IAutoCompleteFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is AutoCompleteFacetViaMethod);
            var propertyAutoCompleteFacet = (AutoCompleteFacetViaMethod) facet;
            Assert.AreEqual(propertyAutoCompleteMethod, propertyAutoCompleteFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyAutoCompleteMethod));

            Assert.AreEqual(50, propertyAutoCompleteFacet.PageSize);
            Assert.AreEqual(0, propertyAutoCompleteFacet.MinLength);
        }

        [Test]
        public void TestAutoCompleteFacetFoundAndMethodRemovedForInterface() {
            PropertyInfo property = FindProperty(typeof(Customer27), "FirstName");
            MethodInfo propertyAutoCompleteMethod = FindMethodIgnoreParms(typeof(Customer27), "AutoCompleteFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAutoCompleteFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is AutoCompleteFacetViaMethod);
            var propertyAutoCompleteFacet = (AutoCompleteFacetViaMethod)facet;
            Assert.AreEqual(propertyAutoCompleteMethod, propertyAutoCompleteFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyAutoCompleteMethod));

            Assert.AreEqual(50, propertyAutoCompleteFacet.PageSize);
            Assert.AreEqual(0, propertyAutoCompleteFacet.MinLength);
        }

        [Test]
        public void TestAutoCompleteFacetAttributes() {
            PropertyInfo property = FindProperty(typeof(Customer26), "FirstName");
            MethodInfo propertyAutoCompleteMethod = FindMethodIgnoreParms(typeof(Customer26), "AutoCompleteFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAutoCompleteFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is AutoCompleteFacetViaMethod);
            var propertyAutoCompleteFacet = (AutoCompleteFacetViaMethod)facet;
            Assert.AreEqual(propertyAutoCompleteMethod, propertyAutoCompleteFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyAutoCompleteMethod));
            Assert.AreEqual(33, propertyAutoCompleteFacet.PageSize);
            Assert.AreEqual(3, propertyAutoCompleteFacet.MinLength);
        }


        [Test]
        public void TestAutoCompleteFacetIgnored() {
            PropertyInfo property = FindProperty(typeof (Customer22), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            Assert.IsNull(facetHolder.GetFacet(typeof (IAutoCompleteFacet)));

            property = FindProperty(typeof (Customer23), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            Assert.IsNull(facetHolder.GetFacet(typeof (IAutoCompleteFacet)));

            property = FindProperty(typeof (Customer24), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            Assert.IsNull(facetHolder.GetFacet(typeof (IAutoCompleteFacet)));

            property = FindProperty(typeof (Customer25), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            Assert.IsNull(facetHolder.GetFacet(typeof (IAutoCompleteFacet)));
        }

        [Test]
        public void TestClearFacet() {
            PropertyInfo property = FindProperty(typeof (Customer8), "FirstName");
            MethodInfo propertyClearMethod = FindMethod(typeof (Customer8), "ClearFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyClearFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyClearFacetViaClearMethod);
            var propertyClearFacet = (PropertyClearFacetViaClearMethod) facet;
            Assert.AreEqual(propertyClearMethod, propertyClearFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyClearMethod));
        }

        [Test]
        public void TestClearFacetViaSetterIfNoExplicitClearMethod() {
            PropertyInfo property = FindProperty(typeof (Customer9), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyClearFacet));
            Assert.IsNull(facet);
        }

        [Test]
        public void TestDefaultFacetFoundAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer11), "FirstName");
            MethodInfo propertyDefaultMethod = FindMethod(typeof (Customer11), "DefaultFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyDefaultFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyDefaultFacetViaMethod);
            var propertyDefaultFacet = (PropertyDefaultFacetViaMethod) facet;
            Assert.AreEqual(propertyDefaultMethod, propertyDefaultFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyDefaultMethod));

            IFacet facetExecuted = facetHolder.GetFacet(typeof (IExecutedControlMethodFacet));
            Assert.IsNull(facetExecuted);
        }

        [Test]
        public void TestDefaultFacetFoundAndMethodRemovedLocal() {
            PropertyInfo property = FindProperty(typeof (Customer11l), "FirstName");
            MethodInfo propertyDefaultMethod = FindMethod(typeof (Customer11l), "DefaultFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyDefaultFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyDefaultFacetViaMethod);
            var propertyDefaultFacet = (PropertyDefaultFacetViaMethod) facet;
            Assert.AreEqual(propertyDefaultMethod, propertyDefaultFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyDefaultMethod));

            var facetExecuted = facetHolder.GetFacet<IExecutedControlMethodFacet>();
            Assert.IsNotNull(facetExecuted);

            Assert.AreEqual(facetExecuted.ExecutedWhere(propertyDefaultMethod), Architecture.Facets.Where.Locally);
        }

        [Test]
        public void TestDefaultFacetFoundAndMethodRemovedRemote() {
            PropertyInfo property = FindProperty(typeof (Customer11r), "FirstName");
            MethodInfo propertyDefaultMethod = FindMethod(typeof (Customer11r), "DefaultFirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyDefaultFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyDefaultFacetViaMethod);
            var propertyDefaultFacet = (PropertyDefaultFacetViaMethod) facet;
            Assert.AreEqual(propertyDefaultMethod, propertyDefaultFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyDefaultMethod));


            var facetExecuted = facetHolder.GetFacet<IExecutedControlMethodFacet>();
            Assert.IsNotNull(facetExecuted);

            Assert.AreEqual(facetExecuted.ExecutedWhere(propertyDefaultMethod), Architecture.Facets.Where.Remotely);
        }

        [Test]
        public void TestDisableDefaultMethodFacet() {
            PropertyInfo property = FindProperty(typeof (Customer16), "FirstName");
            MethodInfo hideMethod = FindMethod(typeof (Customer16), "DisablePropertyDefault", new Type[0]);
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IDisableForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisableForContextFacetViaMethod);
            var disableFacet = (DisableForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, disableFacet.GetMethod());
            Assert.IsFalse(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestDisableMethodOverridsDefault() {
            PropertyInfo property = FindProperty(typeof (Customer16), "SecondName");
            MethodInfo hideMethod = FindMethod(typeof (Customer16), "DisableSecondName", new Type[0]);
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IDisableForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisableForContextFacetViaMethod);
            var disableFacet = (DisableForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, disableFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestDisableMethodWithParameterFacet() {
            PropertyInfo property = FindProperty(typeof (Customer15), "FirstName");
            MethodInfo hideMethod = FindMethod(typeof (Customer15), "DisableFirstName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IDisableForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisableForContextFacetViaMethod);
            var propertyValidateFacet = (DisableForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestDisableMethodWithoutParameterFacet() {
            PropertyInfo property = FindProperty(typeof (Customer15), "SecondName");
            MethodInfo hideMethod = FindMethod(typeof (Customer15), "DisableSecondName", new Type[0]);
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IDisableForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisableForContextFacetViaMethod);
            var propertyValidateFacet = (DisableForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public override void TestFeatureTypes() {
            NakedObjectFeatureType[] featureTypes = facetFactory.FeatureTypes;
            Assert.IsFalse(Contains(featureTypes, NakedObjectFeatureType.Objects));
            Assert.IsTrue(Contains(featureTypes, NakedObjectFeatureType.Property));
            Assert.IsFalse(Contains(featureTypes, NakedObjectFeatureType.Collection));
            Assert.IsFalse(Contains(featureTypes, NakedObjectFeatureType.Action));
            Assert.IsFalse(Contains(featureTypes, NakedObjectFeatureType.ActionParameter));
        }

        [Test]
        public void TestHideDefaultMethodFacet() {
            PropertyInfo property = FindProperty(typeof (Customer14), "FirstName");
            MethodInfo hideMethod = FindMethod(typeof (Customer14), "HidePropertyDefault", new Type[0]);
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IHideForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is HideForContextFacetViaMethod);
            var propertyValidateFacet = (HideForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
            Assert.IsFalse(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestHideMethodOverridesDefault() {
            PropertyInfo property = FindProperty(typeof (Customer14), "SecondName");
            MethodInfo hideMethod = FindMethod(typeof (Customer14), "HideSecondName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IHideForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is HideForContextFacetViaMethod);
            var propertyValidateFacet = (HideForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
        }

        [Test]
        public void TestHideMethodWithParameterFacet() {
            PropertyInfo property = FindProperty(typeof (Customer13), "SecondName");
            MethodInfo hideMethod = FindMethod(typeof (Customer13), "HideSecondName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IHideForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is HideForContextFacetViaMethod);
            var propertyValidateFacet = (HideForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestHideMethodWithoutParameterFacet() {
            PropertyInfo property = FindProperty(typeof (Customer13), "FirstName");
            MethodInfo hideMethod = FindMethod(typeof (Customer13), "HideFirstName", new Type[0]);
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IHideForContextFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is HideForContextFacetViaMethod);
            var propertyValidateFacet = (HideForContextFacetViaMethod) facet;
            Assert.AreEqual(hideMethod, propertyValidateFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(hideMethod));
        }

        [Test]
        public void TestIfHaveSetterAndModifyFacetThenTheModifyFacetWinsOut() {
            PropertyInfo property = FindProperty(typeof (Customer7), "FirstName");
            MethodInfo propertyModifyMethod = FindMethod(typeof (Customer7), "ModifyFirstName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertySetterFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertySetterFacetViaModifyMethod);
            var propertySetterFacet = (PropertySetterFacetViaModifyMethod) facet;
            Assert.AreEqual(propertyModifyMethod, propertySetterFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyModifyMethod));
        }

        [Test]
        public void TestInitializationFacetIsInstalledForSetterMethodAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer2), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyInitializationFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is IPropertyInitializationFacet);
            var propertySetterFacet = (PropertyInitializationFacetViaSetterMethod) facet;
            Assert.AreEqual(property.GetSetMethod(), propertySetterFacet.GetMethod());
        }

        [Test]
        public void TestInstallsDisabledForSessionFacetAndRemovesMethod() {
            PropertyInfo property = FindProperty(typeof (CustomerStatic), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IDisableForSessionFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisableForSessionFacetNone);
        }

        [Test]
        public void TestInstallsHiddenForSessionFacetAndRemovesMethod() {
            PropertyInfo property = FindProperty(typeof (CustomerStatic), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IHideForSessionFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is HideForSessionFacetNone);
        }

        [Test]
        public void TestModifyMethodWithNoSetterStillInstallsDisabledAndDerivedFacets() {
            PropertyInfo property = FindProperty(typeof (Customer6), "FirstName");
            MethodInfo propertyModifyMethod = FindMethod(typeof (Customer6), "ModifyFirstName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (INotPersistedFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is NotPersistedFacetAnnotation);
            facet = facetHolder.GetFacet(typeof (IDisabledFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is DisabledFacetAlways);
        }

        [Test]
        public void TestPropertyAccessorFacetIsInstalledAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyAccessorFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyAccessorFacetViaAccessor);
            var propertyAccessorFacetViaAccessor = (PropertyAccessorFacetViaAccessor) facet;
            Assert.AreEqual(property.GetGetMethod(), propertyAccessorFacetViaAccessor.GetMethod());
        }

        [Test]
        public void TestSetterFacetIsInstalledForModifyMethodAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer4), "FirstName");
            MethodInfo propertyModifyMethod = FindMethod(typeof (Customer4), "ModifyFirstName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertySetterFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertySetterFacetViaModifyMethod);
            var propertySetterFacet = (PropertySetterFacetViaModifyMethod) facet;
            Assert.AreEqual(propertyModifyMethod, propertySetterFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyModifyMethod));
        }

        [Test]
        public void TestSetterFacetIsInstalledForSetterMethodAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer1), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertySetterFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertySetterFacetViaSetterMethod);
            var propertySetterFacet = (PropertySetterFacetViaSetterMethod) facet;
            Assert.AreEqual(property.GetSetMethod(), propertySetterFacet.GetMethod());
        }

        [Test]
        public void TestSetterFacetIsInstalledMeansNoDisabledOrDerivedFacetsInstalled() {
            PropertyInfo property = FindProperty(typeof (Customer3), "FirstName");
            facetFactory.Process(property, methodRemover, facetHolder);
            Assert.IsNull(facetHolder.GetFacet(typeof (INotPersistedFacet)));
            Assert.IsNull(facetHolder.GetFacet(typeof (IDisabledFacet)));
        }

        [Test]
        public void TestValidateFacetFoundAndMethodRemoved() {
            PropertyInfo property = FindProperty(typeof (Customer12), "FirstName");
            MethodInfo propertyValidateMethod = FindMethod(typeof (Customer12), "ValidateFirstName", new[] {typeof (string)});
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof (IPropertyValidateFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is PropertyValidateFacetViaMethod);
            var propertyValidateFacet = (PropertyValidateFacetViaMethod) facet;
            Assert.AreEqual(propertyValidateMethod, propertyValidateFacet.GetMethod());
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyValidateMethod));
        }

        [Test]
        public void TestAjaxFacetFoundAndMethodRemovedEnabled() {
            PropertyInfo property = FindProperty(typeof(Customer20), "FirstName");
            MethodInfo propertyValidateMethod = FindMethod(typeof(Customer20), "ValidateFirstName", new[] { typeof(string) });
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAjaxFacet));
            Assert.IsNull(facet);
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyValidateMethod));
        }

        [Test]
        public void TestAjaxFacetFoundAndMethodRemovedDisabled() {
            PropertyInfo property = FindProperty(typeof(Customer19), "FirstName");
            MethodInfo propertyValidateMethod = FindMethod(typeof(Customer19), "ValidateFirstName", new[] { typeof(string) });
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAjaxFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is AjaxFacetAnnotation);
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyValidateMethod));
        }

        [Test]
        public void TestAjaxFacetNotAddedByDefault() {
            PropertyInfo property = FindProperty(typeof(Customer12), "FirstName");
            MethodInfo propertyValidateMethod = FindMethod(typeof(Customer12), "ValidateFirstName", new[] { typeof(string) });
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAjaxFacet));
            Assert.IsNull(facet);
            Assert.IsTrue(methodRemover.GetRemoveMethodMethodCalls().Contains(propertyValidateMethod));
        }

        [Test]
        public void TestAjaxFacetAddedIfNoValidate() {
            PropertyInfo property = FindProperty(typeof(Customer2), "FirstName");      
            facetFactory.Process(property, methodRemover, facetHolder);
            IFacet facet = facetHolder.GetFacet(typeof(IAjaxFacet));
            Assert.IsNotNull(facet);
            Assert.IsTrue(facet is AjaxFacetAnnotation);
        }

       

    }

    // Copyright (c) Naked Objects Group Ltd.
}