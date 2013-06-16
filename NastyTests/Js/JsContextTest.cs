using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsContextTest {

	    [TestMethod]
	    public void TestErrorNoContext() {
		    try {
			    JsContext.Add(new JsCall("init"));
			    Assert.Fail();
		    }
		    catch(Exception) {}
	    }
	
	    [TestMethod]
	    public void TestExecute() {
		    var expr = JsContext.Execute(() => {
				    JsContext.Add(new JsCall("init"));
				    JsContext.Add(new JsCall("init2", 123));
		    });
		    Assert.AreEqual("init();init2(123);", expr.Encode());
	    }
    }
}