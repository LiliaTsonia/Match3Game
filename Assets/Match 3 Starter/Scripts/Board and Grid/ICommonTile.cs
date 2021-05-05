using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonTile
{
    Sprite ImageSource { get; set; }
    void SwapSprite(SpriteRenderer other);
    List<Tile> GetAllAdjacentTiles();
    List<Tile> FindMatch(Vector2 castDir);
    void ClearAllMatches();
}
