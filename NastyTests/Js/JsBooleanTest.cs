using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{

    [TestClass]
    public class JsBooleanTest {

        [TestMethod]
	    public void TestEncode() {
            Assert.AreEqual("true", new JsBoolean(true).Encode());
            Assert.AreEqual("false", new JsBoolean(false).Encode());
	    }
    }
}