using System.Collections;
using System.Collections.Generic;
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
            //Arrange
            BoardManager boardManager = Create.Board();

            //Act

            //Assert

        }

        [Test]
        public void WhenGetNewTileImage_AndTileIndexesAreEqualZero_ThenReturnImageFromExistingTiles()
        {
            //Arrange
            BoardManager boardManager = Create.Board();
            //Act

            //Assert

        }
    }
}
