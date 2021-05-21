using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Zenject;

public class CommonBoard : MonoBehaviour, ICommonBoard {
	[SerializeField] private Vector2 _boardSize = new Vector2(5, 5);
	[SerializeField] private List<Sprite> _charactersSprites;
	private Vector2 _tileOffset;

	private Tile.TileFactory _tileFactory;

	private BoardController _boardController;
	private IEnumerator _currentCoroutine;

	private Vector2 _boardPosition;

	public Vector2 BoardSize => _boardSize;

	public static bool IsShifting { get; private set; }

	[Inject]
	private void Construct(Tile.TileFactory tileFactory)
    {
		_tileFactory = tileFactory;
    }

    private void Awake()
    {
		_boardController = new BoardController(this);
    }

	void Start()
	{
		TileController.OnMatchFound += ClearAndRefillBoard;

		_tileOffset = Tile.TileFactory.TileOffset;
		_boardPosition = _boardController.SetBoardPosition(_tileOffset);

		CreateBoard();
	}

	public void CreateBoard()
    {
		_boardController.CreateBoard();
    }

	public ICommonTile GetNewTile(int xPos, int yPos)
    {
		var tile = _tileFactory.Create();
		tile.SetUp(new Vector3(_boardPosition.x + (_tileOffset.x * xPos), _boardPosition.y + (_tileOffset.y * yPos), 0), Quaternion.identity, transform);

		tile.ImageSource = GetNewTileImage(xPos, yPos);
		return tile;
	}

    public Sprite GetNewTileImage(int xIndex, int yIndex)
	{
		var possibleCharacters = new List<Sprite>();
		possibleCharacters.AddRange(_charactersSprites);

        if (xIndex != 0)
        {
            possibleCharacters.Remove(_boardController.GetTile(xIndex - 1, yIndex).ImageSource);
        }

        if (yIndex != 0)
        {
            possibleCharacters.Remove(_boardController.GetTile(xIndex, yIndex - 1).ImageSource);
        }

		return possibleCharacters[UnityEngine.Random.Range(0, possibleCharacters.Count)];
	}

	public void ClearAndRefillBoard()
    {
		if(_currentCoroutine != null)
        {
			StopCoroutine(_currentCoroutine);
        }

		_currentCoroutine = FindNullTiles();
		StartCoroutine(_currentCoroutine);
    }

	private IEnumerator FindNullTiles()
	{
		for (var x = 0; x < (int)BoardSize.x; x++)
		{
			for (var y = 0; y < (int)BoardSize.y; y++)
			{
				if (_boardController.GetTile(x, y).ImageSource == null)
				{
					yield return StartCoroutine(ShiftTilesDown(x, y));
					break;
				}
			}
		}

		for (int x = 0; x < (int)BoardSize.x; x++)
		{
			for (int y = 0; y < (int)BoardSize.y; y++)
			{
				_boardController.GetTile(x, y).ClearAllMatches();
			}
		}

	}

	private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
	{
		IsShifting = true;
		var tiles = new List<ICommonTile>();
		var nullCount = 0;

		for (var y = yStart; y < (int)BoardSize.y; y++)
		{
			var tile = _boardController.GetTile(x, y);
			if (tile.ImageSource == null)
			{
				nullCount++;
			}
			tiles.Add(tile);
		}

		for (var i = 0; i < nullCount; i++)
		{
			yield return new WaitForSeconds(shiftDelay);

			for (var k = 0; k < tiles.Count - 1; k++)
			{
				tiles[k].ImageSource = tiles[k + 1].ImageSource;
				tiles[k + 1].ImageSource = GetNewTileImage(x, ((int)BoardSize.y - 1));
			}
		}

		IsShifting = false;
	}
}
