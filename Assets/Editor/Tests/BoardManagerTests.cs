using System.Collections;
using System.Collections.Generic;
//using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests
{
    public class BoardManagerTests
    {
        [Test]
        public void WhenCreateBoard_AndSizeNotSetInEditor_ThenCreateBoardWithNonZeroSize()
        {
            //Arrange
            //TODO redo using Create and Setup
            //ICommonBoard board = Substitute.For<ICommonBoard>();
            
            //board.CreateBoard(1f, 1f);
            //Assert.AreEqual(25, board.BoardSize);

            //Act

            //Assert

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
