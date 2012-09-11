using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestGameOfLife
{
    
    
    /// <summary>
    ///This is a test class for BoardTest and is intended
    ///to contain all BoardTest Unit Tests
    ///</summary>
  [TestClass()]
  public class BoardTest {


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
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest0() {
      uint size = 0; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest1() {
      uint size = 1; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest8() {
      uint size = 8; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest200() {
      uint size = 200; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTestMax() {
      uint size = uint.MaxValue; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTestMin() {
      uint size = uint.MinValue; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(target.Size, size);
    }

    /// <summary>
    ///A test for ChangeStatus
    ///</summary>
    [TestMethod()]
    public void ChangeStatusTest() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      Board.BoardEdit[] edits = null; // TODO: Initialize to an appropriate value
      target.ChangeStatus(edits);
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for GenerateRandom
    ///</summary>
    [TestMethod()]
    [DeploymentItem("GameOfLife.exe")]
    public void GenerateRandomTest() {
      PrivateObject param0 = null; // TODO: Initialize to an appropriate value
      Board_Accessor target = new Board_Accessor(param0); // TODO: Initialize to an appropriate value
      Random random = null; // TODO: Initialize to an appropriate value
      target.GenerateRandom(random);
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for GenerateRandomBoard
    ///</summary>
    [TestMethod()]
    public void GenerateRandomBoardTest() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      target.GenerateRandomBoard();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for GenerateRandomBoard
    ///</summary>
    [TestMethod()]
    public void GenerateRandomBoardTest1() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      int Seed = 0; // TODO: Initialize to an appropriate value
      target.GenerateRandomBoard(Seed);
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for NextDay
    ///</summary>
    [TestMethod()]
    public void NextDayTest() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      target.NextDay();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for getNeighbors
    ///</summary>
    [TestMethod()]
    [DeploymentItem("GameOfLife.exe")]
    public void getNeighborsTest() {
      PrivateObject param0 = null; // TODO: Initialize to an appropriate value
      Board_Accessor target = new Board_Accessor(param0); // TODO: Initialize to an appropriate value
      uint col = 0; // TODO: Initialize to an appropriate value
      uint row = 0; // TODO: Initialize to an appropriate value
      bool zombie = false; // TODO: Initialize to an appropriate value
      bool zombieExpected = false; // TODO: Initialize to an appropriate value
      uint expected = 0; // TODO: Initialize to an appropriate value
      uint actual;
      actual = target.getNeighbors(col, row, out zombie);
      Assert.AreEqual(zombieExpected, zombie);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for withInBoard
    ///</summary>
    [TestMethod()]
    [DeploymentItem("GameOfLife.exe")]
    public void withInBoardTest() {
      PrivateObject param0 = null; // TODO: Initialize to an appropriate value
      Board_Accessor target = new Board_Accessor(param0); // TODO: Initialize to an appropriate value
      uint col = 0; // TODO: Initialize to an appropriate value
      uint row = 0; // TODO: Initialize to an appropriate value
      bool expected = false; // TODO: Initialize to an appropriate value
      bool actual;
      actual = target.withInBoard(col, row);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for Item
    ///</summary>
    [TestMethod()]
    public void ItemTest() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      uint col = 0; // TODO: Initialize to an appropriate value
      uint row = 0; // TODO: Initialize to an appropriate value
      Nullable<int> actual;
      actual = target[col, row];
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for Size
    ///</summary>
    [TestMethod()]
    public void SizeTest() {
      uint size = 0; // TODO: Initialize to an appropriate value
      Board target = new Board(size); // TODO: Initialize to an appropriate value
      uint actual;
      actual = target.Size;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
