using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestGameOfLife
{
    
    
    /// <summary>
    ///This is a test class for Board_BoardEditTest and is intended
    ///to contain all Board_BoardEditTest Unit Tests
    ///</summary>
  [TestClass()]
  public class Board_BoardEditTest {


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
    ///A test for BoardEdit Constructor
    ///</summary>
    [TestMethod()]
    public void Board_BoardEditConstructorTest() {
      uint col = 0; // TODO: Initialize to an appropriate value
      uint row = 0; // TODO: Initialize to an appropriate value
      Nullable<int> status = new Nullable<int>(); // TODO: Initialize to an appropriate value
      Board.BoardEdit target = new Board.BoardEdit(col, row, status);
      Assert.Inconclusive("TODO: Implement code to verify target");
    }
  }
}
