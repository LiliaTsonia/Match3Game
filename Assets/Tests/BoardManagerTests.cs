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
        public void WhenSetBoardPosition_AndSizeIs25OffsetIs1_ThenReturnPositionMinus2()
        {
            //Arrange
            //TODO redo using Create and Setup
            ICommonBoard board = Substitute.For<ICommonBoard>();
            board.BoardSize.Returns<Vector2>(new Vector2(5, 5));
            BoardController boardController = new BoardController(board);

            ////Act
            var position = boardController.SetBoardPosition(Vector2.one);

            ////Assert
            Assert.AreEqual(new Vector2(-2f,-2f), position);
        }

        [Test]
        public void WhenSetBoardPosition_AndSizeIs49OffsetIsZero_ThenReturnPositionZeroDotFive()
        {
            //Arrange
            //TODO redo using Create and Setup
            ICommonBoard board = Substitute.For<ICommonBoard>();
            board.BoardSize.Returns<Vector2>(new Vector2(7, 7));
            BoardController boardController = new BoardController(board);

            ////Act
            var position = boardController.SetBoardPosition(Vector2.zero);

            ////Assert
            Assert.AreEqual(new Vector2(.5f, .5f), position);
        }
    }
}
