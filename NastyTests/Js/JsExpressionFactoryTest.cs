using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsExpressionFactoryTest {

	    [TestMethod]
	    public void TestSpecialCases() {
		    Check(null, typeof(JsVariable), "null");
		    var e = new JsCall("init");
		    Assert.AreSame(e, JsExpressionFactory.create(e));
	    }
	
	    [TestMethod]
	    public void TestArray() {
		    var data = new object[] {"abc", 123, null};
		    Check(data, typeof(JsList), "[\"abc\", 123, null]");
		    var d2 = new [] {"q", "v"};
		    Check(d2, typeof(JsList), "[\"q\", \"v\"]");
	    }

	    [TestMethod]
	    public void TestList() {
		    var data = new List<object> {"abc", 123, null};
	        Check(data, typeof(JsList), "[\"abc\", 123, null]");
	    }

	    [TestMethod]
	    public void TestMap() {
		    var data = new Dictionary<String, Object> {{"abc", 123}};
	        Check(data, typeof(JsMap), "{\"abc\":123}");
	    }

	    [TestMethod]
	    public void TestNumber() {
		    Check((byte)12, typeof(JsNumber), "12");
		    Check((short)12, typeof(JsNumber), "12");
		    Check(12, typeof(JsNumber), "12");
		    Check((long)12, typeof(JsNumber), "12");
		    Check(12.45, typeof(JsNumber), "12.45");
		    Check(12.345m, typeof(JsNumber), "12.345");
	    }
	
	    [TestMethod]
	    public void TestString() {
		    Check("abcd", typeof(JsString), "\"abcd\"");
		    Check('\n', typeof(JsString), "\"\\n\"");
	    }
	
	    private static void Check(object value, Type clazz, String encoded) {
		    var expr = JsExpressionFactory.create(value);
		    Assert.AreSame(clazz, expr.GetType());
		    Assert.AreEqual(encoded, expr.Encode());
		
	    }

    }
}