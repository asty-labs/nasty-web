using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Components;
using Nasty.Js;

namespace NastyTests.Components
{
    [TestClass]
    public class ButtonTest {
    
        [TestMethod]
        public void TestInit() {
            var btn = new Button();
            JsContext.Execute(() => {
                    btn.Id = "btn1";
                    btn.Text = "my text";
            });
            var script = JsContext.Execute(btn.Init).Encode();
            Assert.AreEqual("$$('btn1').jasty(\"Button\", \"init\", [{\"id\":\"btn1\", \"text\":\"my text\", \"visible\":true}]);", script);
        }
    }
}