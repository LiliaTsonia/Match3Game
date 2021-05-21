using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonBoard
{
    Vector2 BoardSize { get; }
    void CreateBoard();
    ICommonTile GetNewTile(int xPos, int yPos);
    Sprite GetNewTileImage(int xIndex, int yIndex);
    void ClearAndRefillBoard();
}
