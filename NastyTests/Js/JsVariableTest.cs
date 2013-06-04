using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{

    [TestClass]
    public class JsVariableTest {
	
	    [TestMethod]
	    public void TestEncode() {
            Assert.AreEqual("testVar", new JsVariable("testVar").Encode());
	    }
    }
}