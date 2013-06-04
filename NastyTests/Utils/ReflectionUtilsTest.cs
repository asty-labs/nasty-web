using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasty.Core;
using Nasty.Utils;

namespace NastyTests.Utils
{
    [TestClass]
    public class ReflectionUtilsTest {
	
	    [TestMethod]
	    public void TestGetAllFields() {
            var fields = ReflectionUtils.GetAllInitProperties(typeof(SubTest1), typeof(InitPropertyAttribute));
		    Assert.AreEqual(3, fields.Count);
	    }
	
	    [TestMethod]
	    public void TestFindField() {
            Assert.IsNotNull(ReflectionUtils.FindField(typeof(SubTest1), "TestString"));
            Assert.IsNotNull(ReflectionUtils.FindField(typeof(SubTest1), "TestString1"));
            Assert.IsNotNull(ReflectionUtils.FindField(typeof(Test1), "TestString"));
            Assert.IsNotNull(ReflectionUtils.FindField(typeof(Test1), "TestNumber"));
            Assert.IsNull(ReflectionUtils.FindField(typeof(SubTest1), "TestNumber1"));
            Assert.IsNull(ReflectionUtils.FindField(typeof(Test1), "TestNumber1"));
	    }
	
	    [TestMethod]
	    public void TestCopyFields() {
		    var src = new Test1 {TestString = "bla-bla", TestNumber = 123};
	        var dest = new SubTest1();
            ReflectionUtils.CopyInitProperties(src, dest, typeof(InitPropertyAttribute));
            Assert.AreEqual("bla-bla", dest.TestString);
            Assert.AreEqual("bla-bla", dest.TestString);
		    Assert.IsNull(dest.TestString1);
            Assert.AreEqual(123, dest.TestNumber);
	    }

	    [TestMethod]
	    public void TestCopyFieldsOtherClass() {
		    var src = new SubTest1 {TestString1 = "bla-bla", TestNumber = 123};
	        var dest = new DestClass();
            ReflectionUtils.CopyInitProperties(src, dest, typeof(InitPropertyAttribute));
		    Assert.AreEqual("bla-bla", dest.TestString1);
            Assert.AreEqual(123, dest.TestNumber);
	    }
	
	    public class Test1 {
            [InitProperty]
            public virtual string TestString { get; set; }
            [InitProperty]
            public int TestNumber { get; set; }
		
	    }
	
	    public class SubTest1 : Test1 {
            [InitProperty]
            public override string TestString { get; set; }
            [InitProperty]
            public string TestString1 { get; set; }
	    }
	
	    public class DestClass {
            [InitProperty]
            public string TestString1 { get; set; }
            [InitProperty]
            public int TestNumber { get; set; }
	    }
    }
}