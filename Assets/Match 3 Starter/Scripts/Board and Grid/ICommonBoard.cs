using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonBoard
{
    void CreateBoard(float xOffset, float yOffset);
    Sprite GetNewTileImage(byte xIndex, byte yIndex);
    void ClearAndRefillBoard();
}
