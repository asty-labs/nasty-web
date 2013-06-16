using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Nasty.Core;
using Nasty.Js;
using Nasty.Utils;

namespace NastyTests.Core
{
    [TestClass]
    public class FormEngineTest {

        [TestMethod]
        public void TestProcess() {
            var parameterProvider = new TestParameterProvider();
            parameterProvider.Add("eventHandler", "SuccessfulEvent");
            var formEngine = new FormEngine(parameterProvider, null, ClientSideFormPersister.Instance);
            var form = new MyForm {Id = "formId"};
            parameterProvider.Add("state", Convert.ToBase64String(SerializationUtils.SerializeObject(form)));
            parameterProvider.Add("EVT.srcId", "formId.testSrcId");
            parameterProvider.Add("EVT.someParameter", "testParameter");
            var expr = formEngine.DoProcess();
            form.Output = "testSrcId/testParameter";
            Assert.AreEqual("$$('formId').jasty(\"Form\", \"update\", [\"" +
                    Convert.ToBase64String(SerializationUtils.SerializeObject(form)) + "\"]);", expr.Encode());
        }

        [TestMethod]
        public void TestProcessWithError() {
            var parameterProvider = new TestParameterProvider();
            var formEngine = new FormEngine(parameterProvider, null, ClientSideFormPersister.Instance);
            parameterProvider.Add("eventHandler", "ErroneousEvent");
            var form = new MyForm {Id = "formId"};
            parameterProvider.Add("state", Convert.ToBase64String(SerializationUtils.SerializeObject(form)));
            parameterProvider.Add("EVT.srcId", "formId.testSrcId");
            parameterProvider.Add("EVT.someParameter", "testParameter");

            var link = new TestErrorHandlingLink {Next = formEngine};
            var expr = link.DoProcess();
            Assert.AreEqual("alert(\"some error\")", expr.Encode());
        }

        public class TestErrorHandlingLink : AbstractProcessingLink
        {
            public override IJsExpression DoProcess()
            {
                try
                {
                    return base.DoProcess();
                }
                catch (Exception e)
                {
                    return new JsCall("alert", e.Message);
                }
            }
        }

        public class TestParameterProvider : IParameterProvider {

            private readonly IDictionary<string, string[]> _parameters = new Dictionary<string, string[]>();
        
            public void Add(string key, string value) {
                _parameters.Add(key, new [] {value});
            }

            public string GetParameter(string name) {
                return _parameters[name][0];
            }

            public IDictionary<string, string[]> GetParameterMap() {
                return _parameters;
            }
        }
    }
}