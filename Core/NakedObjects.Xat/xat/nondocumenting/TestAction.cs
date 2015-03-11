﻿// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NakedObjects.Architecture.Adapter;
using NakedObjects.Architecture.Facets.Actions.Invoke;
using NakedObjects.Architecture.Facets.Objects.Parseable;
using NakedObjects.Architecture.Reflect;
using NakedObjects.Core.Context;

namespace NakedObjects.Xat {
    internal class TestAction : ITestAction {
        private readonly INakedObjectAction action;
        private readonly ITestObjectFactory factory;
        private readonly ITestHasActions owningObject;

        public TestAction(INakedObjectAction action, ITestHasActions owningObject, ITestObjectFactory factory)
            : this(string.Empty, action, owningObject, factory) {}

        public TestAction(string contributor, INakedObjectAction action, ITestHasActions owningObject, ITestObjectFactory factory) {
            SubMenu = contributor;
            this.owningObject = owningObject;
            this.factory = factory;
            this.action = action;
        }

        #region ITestAction Members

        public string Name {
            get { return action.Name; }
        }

        public string SubMenu { get; private set; }

        public string LastMessage { get; private set; }

        public ITestParameter[] Parameters {
            get { return action.Parameters.Select(x => factory.CreateTestParameter(action, x, owningObject)).ToArray(); }
        }

        public bool MatchParameters(Type[] typestoMatch) {
            if (action.Parameters.Count() == typestoMatch.Length) {
                int i = 0;
                return action.Parameters.All(x => x.Specification.IsOfType(NakedObjectsContext.Reflector.LoadSpecification(typestoMatch[i++])));
            }
            return false;
        }

        public ITestObject InvokeReturnObject(params object[] parameters) {
            return (ITestObject) DoInvoke(ParsedParameters(parameters));
        }

        public ITestCollection InvokeReturnCollection(params object[] parameters) {
            return (ITestCollection) DoInvoke(ParsedParameters(parameters));
        }

        public void Invoke(params object[] parameters) {
            DoInvoke(ParsedParameters(parameters));
        }

        public ITestCollection InvokeReturnPagedCollection(int page, params object[] parameters) {
            return (ITestCollection) DoInvoke(page, ParsedParameters(parameters));
        }

        #endregion

        private ITestNaked DoInvoke(int page, params object[] parameters) {
            ResetLastMessage();
            AssertIsValidWithParms(parameters);
            INakedObject[] parameterObjects = parameters.AsTestNakedArray().Select(x => x.NakedObject).ToArray();

            INakedObject[] parms = action.RealParameters(owningObject.NakedObject, parameterObjects);
            INakedObject target = action.RealTarget(owningObject.NakedObject);
            INakedObject result = action.GetFacet<IActionInvocationFacet>().Invoke(target, parms, page);

            if (result == null) {
                return null;
            }
            if (result.Specification.IsCollection) {
                return factory.CreateTestCollection(result);
            }
            return factory.CreateTestObject(result);
        }


        private ITestNaked DoInvoke(params object[] parameters) {
            ResetLastMessage();
            AssertIsValidWithParms(parameters);
            INakedObject[] parameterObjects = parameters.AsTestNakedArray().Select(x => x.NakedObject).ToArray();
            INakedObject result = null;
            try {
                result = action.Execute(owningObject.NakedObject, parameterObjects);
            }
            catch (ArgumentException e) {
                Assert.IsInstanceOfType(e, typeof (ArgumentException));
                Assert.Fail("Invalid Argument(s)");
            }

            if (result == null) {
                return null;
            }
            if (result.Specification.IsCollection && !result.Specification.IsParseable) {
                return factory.CreateTestCollection(result);
            }
            return factory.CreateTestObject(result);
        }

        private void ResetLastMessage() {
            LastMessage = string.Empty;
        }

        #region Asserts

        public ITestAction AssertIsDisabled() {
            ResetLastMessage();
            if (action.IsVisible(NakedObjectsContext.Session, owningObject.NakedObject)) {
                IConsent canUse = action.IsUsable(NakedObjectsContext.Session, owningObject.NakedObject);
                LastMessage = canUse.Reason;
                Assert.IsFalse(canUse.IsAllowed, "Action '" + Name + "' is usable: " + canUse.Reason);
            }
            return this;
        }

        public ITestAction AssertIsEnabled() {
            ResetLastMessage();
            AssertIsVisible();
            IConsent canUse = action.IsUsable(NakedObjectsContext.Session, owningObject.NakedObject);
            LastMessage = canUse.Reason;
            Assert.IsTrue(canUse.IsAllowed, "Action '" + Name + "' is disabled: " + canUse.Reason);
            return this;
        }


        public ITestAction AssertIsInvalidWithParms(params object[] parameters) {
            ResetLastMessage();

            object[] parsedParameters = ParsedParameters(parameters);

            if (action.IsVisible(NakedObjectsContext.Session, owningObject.NakedObject)) {
                IConsent canUse = action.IsUsable(NakedObjectsContext.Session, owningObject.NakedObject);
                LastMessage = canUse.Reason;
                if (canUse.IsAllowed) {
                    INakedObject[] parameterObjects = parsedParameters.AsTestNakedArray().Select(x => x == null ? null : x.NakedObject).ToArray();
                    IConsent canExecute = action.IsParameterSetValid(owningObject.NakedObject, parameterObjects);
                    LastMessage = canExecute.Reason;
                    Assert.IsFalse(canExecute.IsAllowed, "Action '" + Name + "' is usable and executable");
                }
            }
            return this;
        }

        public ITestAction AssertIsValidWithParms(params object[] parameters) {
            ResetLastMessage();
            AssertIsVisible();
            AssertIsEnabled();

            object[] parsedParameters = ParsedParameters(parameters);

           

            INakedObject[] parameterObjects = parsedParameters.AsTestNakedArray().Select(x => x == null ? null : x.NakedObject).ToArray();
            IConsent canExecute = action.IsParameterSetValid(owningObject.NakedObject, parameterObjects);
            Assert.IsTrue(canExecute.IsAllowed, "Action '" + Name + "' is unusable: " + canExecute.Reason);
            return this;
        }

        public ITestAction AssertIsVisible() {
            ResetLastMessage();
            Assert.IsTrue(action.IsVisible(NakedObjectsContext.Session, owningObject.NakedObject), "Action '" + Name + "' is hidden");
            return this;
        }

        public ITestAction AssertIsInvisible() {
            ResetLastMessage();
            Assert.IsFalse(action.IsVisible(NakedObjectsContext.Session, owningObject.NakedObject), "Action '" + Name + "' is visible");
            return this;
        }

        public ITestAction AssertIsDescribedAs(string expected) {
            Assert.IsTrue(expected.Equals(action.Description), "Description expected: '" + expected + "' actual: '" + action.Description + "'");
            return this;
        }

        public ITestAction AssertLastMessageIs(string message) {
            Assert.IsTrue(message.Equals(LastMessage), "Last message expected: '" + message + "' actual: '" + LastMessage + "'");
            return this;
        }

        public ITestAction AssertLastMessageContains(string message) {
            Assert.IsTrue(LastMessage.Contains(message), "Last message expected to contain: '" + message + "' actual: '" + LastMessage + "'");
            return this;
        }

        private object[] ParsedParameters(params object[] parameters) {
            var parsedParameters = new List<object>();

            Assert.IsTrue(parameters.Count() == action.Parameters.Count(), String.Format("Action '{0}' is unusable: wrong number of parameters, got {1}, expect {2}", Name, parameters.Count(), action.Parameters.Count()));

            int i = 0;
            foreach (INakedObjectActionParameter parm in action.Parameters) {
                object value = parameters[i++];

                if (value is string && parm.Specification.IsParseable) {
                    parsedParameters.Add(parm.Specification.GetFacet<IParseableFacet>().ParseTextEntry((string) value).Object);
                }
                else {
                    parsedParameters.Add(value);
                }
            }

            return parsedParameters.ToArray();
        }

        #endregion
    }
}