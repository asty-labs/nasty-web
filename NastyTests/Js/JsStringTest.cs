using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{

    [TestClass]
    public class JsStringTest {

        [TestMethod]
        public void TestEncode()
        {
            Assert.AreEqual("\"\"", new JsString("").Encode());
            Assert.AreEqual("\"abc\"", new JsString("abc").Encode());
            Assert.AreEqual("\"'a' \\\"b\\\" \\nc\\n\"", new JsString("'a' \"b\" \nc\n").Encode());
	    }
    }
}