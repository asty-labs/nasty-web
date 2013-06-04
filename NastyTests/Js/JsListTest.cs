using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsListTest {

	    [TestMethod]
	    public void TestEmptyArray() {
            Assert.AreEqual("[]", new JsList(new object[] { }).Encode());
	    }
	
	    [TestMethod]
	    public void TestOneElementArray() {
            Assert.AreEqual("[\"123\"]", new JsList(new object[] { "123" }).Encode());
	    }
	
	    [TestMethod]
	    public void TestManyElementsArray() {
		    var data = new object[] {123, "345", null};
            Assert.AreEqual("[123, \"345\", null]", new JsList(data).Encode());
	    }

	    [TestMethod]
	    public void TestEmpty() {
            Assert.AreEqual("[]", new JsList().Encode());
	    }

	    [TestMethod]
	    public void TestOneElement() {
		    var data = new List<object> {123};
	        Assert.AreEqual("[123]", new JsList(data).Encode());
	    }

	    [TestMethod]
	    public void TestManyElements() {
		    var data = new List<object> {123, "345", null};
	        Assert.AreEqual("[123, \"345\", null]", new JsList(data).Encode());
	    }

        [TestMethod]
        public void TestFrom()
        {
            Assert.AreEqual("[123, \"345\", null]", JsList.From(123, "345", null).Encode());
	    }
    }
}