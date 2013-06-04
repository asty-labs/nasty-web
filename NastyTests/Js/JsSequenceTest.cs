using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{

    [TestClass]
    public class JsSequenceTest {
	
	    [TestMethod]
	    public void TestEmpty() {
            Assert.AreEqual("", new JsSequence().Encode());
	    }

	    [TestMethod]
	    public void TestSingle() {
            Assert.AreEqual("init();", new JsSequence(new JsCall("init")).Encode());
	    }

        [TestMethod]
        public void TestMany()
        {
            Assert.AreEqual("init();init2();", new JsSequence(new JsCall("init"), new JsCall("init2")).Encode());
	    }
    }
}