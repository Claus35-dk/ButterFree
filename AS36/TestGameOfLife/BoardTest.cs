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
    Board.BoardEdit[] bigBoard = {
                                  new Board.BoardEdit(0, 0, 0), 
                                  new Board.BoardEdit(0, 1, 1), 
                                  new Board.BoardEdit(0, 2, 0), 
                                  new Board.BoardEdit(0, 3, null), 
                                  new Board.BoardEdit(1, 0, 1), 
                                  new Board.BoardEdit(1, 1, 1), 
                                  new Board.BoardEdit(1, 2, 1), 
                                  new Board.BoardEdit(1, 3, 1), 
                                  new Board.BoardEdit(2, 0, 0), 
                                  new Board.BoardEdit(2, 1, 1), 
                                  new Board.BoardEdit(2, 2, 0), 
                                  new Board.BoardEdit(2, 3, 0), 
                                  new Board.BoardEdit(3, 0, 0), 
                                  new Board.BoardEdit(3, 1, 1), 
                                  new Board.BoardEdit(3, 2, 0), 
                                  new Board.BoardEdit(3, 3, null), 
                                };
      

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
      Assert.AreEqual(size, target.Size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest1() {
      uint size = 1; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(size, target.Size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest8() {
      uint size = 8; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(size, target.Size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTest200() {
      uint size = 200; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(size, target.Size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTestMax() {
      uint size = uint.MaxValue; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(size, target.Size);
    }
    /// <summary>
    ///A test for Board Constructor
    ///</summary>
    [TestMethod()]
    public void BoardConstructorTestMin() {
      uint size = uint.MinValue; // Initialize board with an appropriate value
      Board target = new Board(size);
      Assert.AreEqual(size, target.Size);
    }

    /// <summary>
    ///A test for ChangeStatus
    ///</summary>
    [TestMethod()]
    public void ChangeStatusTest() {
      uint size = 8; // Initialize to an appropriate size
      Board target = new Board(size); 
      Board.BoardEdit[] edits = {
                                  new Board.BoardEdit(1, 1, 1), // should change state
                                  new Board.BoardEdit(1, 2, 0), // should change state
                                  new Board.BoardEdit(2, 3, 3), // should not change state
                                  new Board.BoardEdit(3, 1, null), // should not change state
                                  new Board.BoardEdit(9, 6, 4), // out of range
                                }; 
      target.ChangeStatus(edits);
      Assert.AreEqual(1, target[1, 1]);
      Assert.AreEqual(0, target[1, 2]);
      Assert.AreEqual(null, target[2, 3]);
      Assert.AreEqual(null, target[3, 1]);
      // The last "test" should not throw an exception, but simply do nothing
    }

    /// <summary>
    ///A test for GenerateRandomBoard
    ///</summary>
    [TestMethod()]
    public void GenerateRandomBoardTest() {
      uint size = 20; // Initialize to an appropriate size
      Board target = new Board(size); 
      target.GenerateRandomBoard();
      
      bool n = false;
      bool l = false;
      bool d = false;
      bool fail = false;
      for(uint i = 0; i<size; i++){
        for(uint j = 0; j<size; j++){
          switch(target[i,j]){
            case null:
              n = true;
              break;
            case 0:
              d = true;
              break;
            case 1:
              l = true;
              break;
            default:
              fail = true;
              break;
          }
          if (fail) break;
          if(n && l && d) break;
        }
      }
      Assert.IsTrue(n && l && d); // all posible values have been reached
      Assert.IsFalse(fail); // no imposible values have been reached
    }

    /// <summary>
    ///A test for NextDay
    ///</summary>
    [TestMethod()]
    public void NextDayTest() {
      uint size = 4; //  Initialize to an appropriate size
      Board target = new Board(size); 
      
      // Set board
      target.ChangeStatus(bigBoard);

      // Next day
      target.NextDay();
      
      // Check
      Assert.AreEqual(1, target[0, 0]);
      Assert.AreEqual(1, target[0, 1]);
      Assert.AreEqual(0, target[0, 2]);
      Assert.AreEqual(null, target[0, 3]);
      Assert.AreEqual(1, target[1, 0]);
      Assert.AreEqual(0, target[1, 1]);
      Assert.AreEqual(0, target[1, 2]);
      Assert.AreEqual(0, target[1, 3]);
      Assert.AreEqual(0, target[2, 0]);
      Assert.AreEqual(0, target[2, 1]);
      Assert.AreEqual(0, target[2, 2]);
      Assert.AreEqual(0, target[2, 3]);
      Assert.AreEqual(0, target[3, 0]);
      Assert.AreEqual(0, target[3, 1]);
      Assert.AreEqual(0, target[3, 2]);
      Assert.AreEqual(null, target[3, 3]);
    }

    /// <summary>
    ///A test for NextDay
    ///</summary>
    [TestMethod()]
    public void NextDayTestZombie() {
      uint testrun = 10;
      uint size = 2; //  Initialize to an appropriate size
      Board target = new Board(size);
      bool zombie = false;

      for (int i = 0; i < testrun; i++) {
        // Set board
        Board.BoardEdit[] edits = {
                                  new Board.BoardEdit(0, 0, 1), 
                                  new Board.BoardEdit(0, 1, 1), 
                                  new Board.BoardEdit(1, 0, 1), 
                                  new Board.BoardEdit(1, 1, null), 
                                };
        target.ChangeStatus(edits);

        // Next day
        target.NextDay();

        // Check
        if (target[0, 0] == null) { zombie = true; break; }
      }
      Assert.AreEqual(true, target[0, 0] == null || target[0, 0] == 1);
      Assert.AreEqual(0, target[0, 1]);
      Assert.AreEqual(0, target[1, 0]);
      Assert.AreEqual(null, target[1, 1]);
      Assert.IsTrue(zombie);
    }

    /// <summary>
    ///A test for getNeighbors
    ///</summary>
    [TestMethod()]
    [DeploymentItem("GameOfLife.exe")]
    public void getNeighborsTest() {
      PrivateObject param0 = new PrivateObject(new Board(4)); // Initialize to an appropriate size
      Board_Accessor target = new Board_Accessor(param0); 
      // Set board
      target.ChangeStatus(bigBoard);
      bool zombie;
      Assert.AreEqual((uint)3, target.getNeighbors(0, 0, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)3, target.getNeighbors(0, 1, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)4, target.getNeighbors(0, 2, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)2, target.getNeighbors(0, 3, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)3, target.getNeighbors(1, 0, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)4, target.getNeighbors(1, 1, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)4, target.getNeighbors(1, 2, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)1, target.getNeighbors(1, 3, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)4, target.getNeighbors(2, 0, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)4, target.getNeighbors(2, 1, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)5, target.getNeighbors(2, 2, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)2, target.getNeighbors(2, 3, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)2, target.getNeighbors(3, 0, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)1, target.getNeighbors(3, 1, out zombie));
      Assert.IsFalse(zombie);
      Assert.AreEqual((uint)2, target.getNeighbors(3, 2, out zombie));
      Assert.IsTrue(zombie);
      Assert.AreEqual((uint)0, target.getNeighbors(3, 3, out zombie));
      Assert.IsFalse(zombie);
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
