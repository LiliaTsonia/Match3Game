using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonBoard
{
    void CreateBoard(float xOffset, float yOffset);
    Sprite GetCurrentTileImage(byte xIndex, byte yIndex, ref List<Sprite> possibleCharacters);
    void ClearAndRefillBoard();
}
