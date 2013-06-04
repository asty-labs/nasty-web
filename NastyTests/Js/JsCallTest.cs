using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsCallTest {

	    [TestMethod]
	    public void TestNoParameters() {
            Assert.AreEqual("init()", new JsCall("init").Encode());
	    }

	    [TestMethod]
	    public void TestOneParameter() {
		    var call = new JsCall("init", 1);
            Assert.AreEqual("init(1)", call.Encode());
	    }

        [TestMethod]
        public void TestManyParameters()
        {
		    var call = new JsCall("init", null, 1, "abc", 1.23m);
            Assert.AreEqual("init(null, 1, \"abc\", 1.23)", call.Encode());
	    }
    }
}