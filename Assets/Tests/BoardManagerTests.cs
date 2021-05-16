using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BoardManagerTests
    {
        [Test]
        public void WhenCreateBoard_AndSizeNotSetInEditor_ThenCreateBoardWithNonZeroSize()
        {
            ////Arrange
            ////TODO redo using Create and Setup
            //ICommonBoard board = Substitute.For<ICommonBoard>();
            //BoardController boardController = Substitute.For<BoardController>();
            //boardController.SetBoardController(board);

            ////Act
            //boardController.CreateBoard(1f, 1f);

            ////Assert
            //Assert.AreEqual(25, board.BoardSize);
        }

        [Test]
        public void WhenGetNewTileImage_AndTileIndexesAreEqualZero_ThenReturnImageFromExistingTiles()
        {
            //Arrange
            //BoardManager boardManager = Create.Board();
            //Act

            //Assert

        }
    }
}
