using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardController
{
    public static event Action<int> OnBoardPositionSet;

    private readonly ICommonBoard _board;
    private ICommonTile[,] _tiles;

    public ICommonTile GetTile(int x, int y) => _tiles[x, y];

    public BoardController(ICommonBoard board)
    {
        _board = board;
    }

    public Vector2 SetBoardPosition(Vector2 tileOffset)
    {
        var xOffset = (_board.BoardSize.x * tileOffset.x - 1) / 2f;
        var yOffset = (_board.BoardSize.y * tileOffset.y - 1) / 2f;

        var holderOffset = new Vector2(xOffset, yOffset);
        holderOffset *= -1f;

        OnBoardPositionSet?.Invoke((int)_board.BoardSize.x + 1);

        return holderOffset;
    }

    public void CreateBoard()
    {
        var xSize = (int)_board.BoardSize.x;
        var ySize = (int)_board.BoardSize.y;

        _tiles = new ICommonTile[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                _tiles[x, y] = _board.GetNewTile(x, y);
            }
        }
    }
}
