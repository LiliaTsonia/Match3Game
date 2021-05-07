using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public int XSize = 5, YSize = 5;
    public List<Sprite> CharactersSprites;
    public GameObject TilePrefab;

    private ICommonBoard _commonBoard;

    public void SetBoardController(ICommonBoard commonBoard)
    {
        _commonBoard = commonBoard;
    }

    public void CreateBoard(float xOffset, float yOffset)
    {
        _commonBoard.CreateBoard(xOffset, yOffset);
    }

    public Sprite GetNewTileImage(byte xIndex, byte yIndex)
    {
        return _commonBoard.GetNewTileImage(xIndex, yIndex);
    }
}
