using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{

    [TestClass]
    public class JsNumberTest {
	
	    [TestMethod]
	    public void TestInt() {
            Assert.AreEqual("42", new JsNumber(42).Encode());
	    }

	    [TestMethod]
	    public void TestFloat() {
            Assert.AreEqual("42.56", new JsNumber(42.56).Encode());
	    }

        [TestMethod]
        public void TestBigDecimal()
        {
            Assert.AreEqual("42.56", new JsNumber(42.56m).Encode());
	    }

    }
}