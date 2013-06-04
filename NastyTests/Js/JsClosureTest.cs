using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Js;

namespace NastyTests.Js
{
    [TestClass]
    public class JsClosureTest {

	    [TestMethod]
	    public void TestExpressionNoArgs() {
		    var cl = new JsClosure(new JsCall("init"));
		    Assert.AreEqual("function(){init()}", cl.Encode());
	    }

	    [TestMethod]
	    public void TestExpressionOneArg() {
		    var v1 = new JsVariable("var1");
		    var cl = new JsClosure(new JsCall("init"), v1);
		    Assert.AreEqual("function(var1){init()}", cl.Encode());
	    }
	
	    [TestMethod]
	    public void TestExpressionManyArgs() {
		    var v1 = new JsVariable("var1");
		    var v2 = new JsVariable("var2");
		    var cl = new JsClosure(new JsCall("alert", v1, v2), v2, v1);
		    Assert.AreEqual("function(var2, var1){alert(var1, var2)}", cl.Encode());
	    }

	    [TestMethod]
	    public void TestRunnable() {
		    var v1 = new JsVariable("var1");
		    var v2 = new JsVariable("var2");
		    var cl = new JsClosure(() => JsContext.Add(new JsCall("alert", v1, v2)), v2, v1);
		    Assert.AreEqual("function(var2, var1){alert(var1, var2);}", cl.Encode());
	    }

	    [TestMethod]
        public void TestEncodeOnLoadAsHtml() {
            string result = new JsClosure(() => {
                    JsContext.Add(new JsCall("init"));
                    JsContext.Add(new JsCall("init2", 123));
            }).EncodeOnLoadAsHtml();
            Assert.AreEqual("<script>jQuery(function(){init();init2(123);})</script>", result);
        }
    }
}