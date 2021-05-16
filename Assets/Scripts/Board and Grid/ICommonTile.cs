using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonTile
{
    Sprite ImageSource { get; set; }
    void SwapSprite(ICommonTile other);
    ICommonTile GetAdjacent(Vector2 castDir);
    List<ICommonTile> FindMatch(Vector2 castDir);
    void ClearAllMatches();
    void SetColor(Color color);
}
