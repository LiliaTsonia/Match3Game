using UnityEngine;
using System;
using System.Collections.Generic;

public class Tile : MonoBehaviour, ICommonTile
{
	[SerializeField] private SpriteRenderer _renderer;

	private TileController _tileController;

	public Vector2 Offset => _renderer.bounds.size;

	public Sprite ImageSource 
	{ 
		get { return _renderer.sprite; } 
		set { _renderer.sprite = value; } 
	}

    private void Awake()
    {
		_tileController = new TileController(this);
    }

    private void OnMouseDown()
    {
		if (_renderer.sprite == null || CommonBoard.IsShifting)
		{
			return;
		}

		_tileController.Test();
	}

	public void SwapSprite(ICommonTile other)
	{
		if (ImageSource == other.ImageSource)
		{
			return;
		}

		var tempSprite = other.ImageSource;
		other.ImageSource = ImageSource;
		ImageSource = tempSprite;
	}


	public List<ICommonTile> FindMatch(Vector2 castDir)
	{
		var matchingTiles = new List<ICommonTile>();
		var hit = Physics2D.Raycast(transform.position, castDir);

		ICommonTile tile;

		while (hit.collider != null && CheckTilesForMatch(hit.collider, ImageSource, out tile))
		{
			matchingTiles.Add(tile);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles;
	}

	public void ClearAllMatches()
	{
		if (ImageSource == null)
		{
			return;
		}

		_tileController.ClearAllMatches();
	}


	public ICommonTile GetAdjacent(Vector2 castDir)
	{
		var hit = Physics2D.Raycast(transform.position, castDir);

		if (hit.collider != null)
		{
			return hit.collider.GetComponent<ICommonTile>();
		}

		return null;
	}

	private bool CheckTilesForMatch(Collider2D collider, Sprite otherToCompare, out ICommonTile tile)
    {
		tile = collider.GetComponent<ICommonTile>();
		return tile.ImageSource.Equals(otherToCompare);
    }

    public void SetColor(Color color)
    {
		_renderer.color = color;
    }
}