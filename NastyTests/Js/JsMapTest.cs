using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsMapTest {
	
	    [TestMethod]
	    public void TestEmpty() {
            Assert.AreEqual("{}", new JsMap().Encode());
	    }
	
	    [TestMethod]
	    public void TestOneElement() {
		    var data = new Dictionary<string, object> {{"a1", 11}};
	        Assert.AreEqual("{\"a1\":11}", new JsMap(data).Encode());
	    }

        [TestMethod]
        public void TestManyElement()
        {
            var data = new Dictionary<string, object> {{"a1", 11}, {"b1", "bb"}, {"c1", null}};
            Assert.AreEqual("{\"a1\":11, \"b1\":\"bb\", \"c1\":null}", new JsMap(data).Encode());
	    }
    }
}    