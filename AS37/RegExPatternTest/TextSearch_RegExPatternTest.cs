using TextSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RegExPatternTest
{
    /// <summary>
    ///This is a test class for TextSearch_RegExPatternTest and is intended
    ///to contain all TextSearch_RegExPatternTest Unit Tests
    ///</summary>
  [TestClass()]
  public class TextSearch_RegExPatternTest {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext {
      get {
        return testContextInstance;
      }
      set {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///A test for RegExPattern Constructor
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void TextSearch_RegExPatternConstructorTest() {
      string GroupName = "URL";
      string RegEx = @"\b(?:\S+)://(\S+)\b"; 
      ConsoleColor BgColor = ConsoleColor.Black;
      ConsoleColor FgColor = ConsoleColor.White;
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern(GroupName, RegEx, BgColor, FgColor);
      Assert.AreEqual(GroupName, target.GroupName);
      Assert.AreEqual("(?<" + GroupName + ">" + RegEx + ")", target.RegEx);
      Assert.AreEqual(BgColor, target.BgColor);
      Assert.AreEqual(FgColor, target.FgColor);
    }

    /// <summary>
    ///A test for RegExPattern Constructor
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void TextSearch_RegExPatternConstructorTest1() {
      string GroupName = "URL";
      string RegEx = @"\b(?:\S+)://(\S+)\b";
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern(GroupName, RegEx);
      Assert.AreEqual(GroupName, target.GroupName);
      Assert.AreEqual("(?<" + GroupName + ">" + RegEx + ")", target.RegEx);
    }

    /// <summary>
    ///A test for BgColor
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void BgColorTest() {
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern("", ""); 
      ConsoleColor expected = ConsoleColor.Black;
      ConsoleColor actual;
      target.BgColor = expected;
      actual = target.BgColor;
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for FgColor
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void FgColorTest() {
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern("",""); 
      ConsoleColor expected = ConsoleColor.Black;
      ConsoleColor actual;
      target.FgColor = expected;
      actual = target.FgColor;
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for GroupName
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void GroupNameTest() {
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern("","");
      string expected = "URL";
      string actual;
      target.GroupName = expected;
      actual = target.GroupName;
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for RegEx
    ///</summary>
    [TestMethod()]
    [DeploymentItem("TextSearch.exe")]
    public void RegExTest() {
      TextSearch_Accessor.RegExPattern target = new TextSearch_Accessor.RegExPattern("","");
      string expected = @"\b(?:\S+)://(\S+)\b";
      string actual;
      target.RegEx = expected;
      actual = target.RegEx;
      Assert.AreEqual("(?<>"+expected+")", actual);
    }
  }
}
