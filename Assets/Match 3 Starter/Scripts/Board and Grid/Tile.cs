using UnityEngine;
using System;
using System.Collections.Generic;

public class Tile : MonoBehaviour, ICommonTile
{
	[SerializeField] private SpriteRenderer _renderer;

	public static event Action<Clip> OnSoundPlay;
	public static event Action OnMatchFound;

	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private bool _isSelected;
	private bool _matchFound;

	private Vector2[] _adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	public Vector2 Offset => _renderer.bounds.size;

	//TODO Get rid of this field since there is already public Sprite property
	public SpriteRenderer SpriteRenderer => _renderer;

	public Sprite ImageSource 
	{ 
		get { return _renderer.sprite; } 
		set { _renderer.sprite = value; } 
	}

    private void OnMouseDown()
    {
		if (_renderer.sprite == null || BoardManager.IsShifting)
		{
			return;
		}

		if (_isSelected)
		{
			Deselect();
		}
		else
		{
			if (previousSelected == null)
			{
				Select();
			}
			else
			{
				previousSelected.ClearAllMatches();

				if (GetAllAdjacentTiles().Contains(previousSelected))
				{
					SwapSprite(previousSelected.SpriteRenderer);
					previousSelected.Deselect();
					ClearAllMatches();
				}
				else
				{
					previousSelected.Deselect();
					ClearAllMatches();
					Select();
				}
			}
		}
	}

	public void SwapSprite(SpriteRenderer other)
	{
		if (ImageSource == other.sprite)
		{
			return;
		}

		var tempSprite = other.sprite;
		other.sprite = ImageSource;
		ImageSource = tempSprite;

		OnSoundPlay?.Invoke(Clip.Swap);
	}

	public List<Tile> GetAllAdjacentTiles()
	{
		var adjacentTiles = new List<Tile>();

		for (var i = 0; i < _adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(_adjacentDirections[i]));
		}

		return adjacentTiles;
	}

	public List<Tile> FindMatch(Vector2 castDir)
	{
		var matchingTiles = new List<Tile>();
		var hit = Physics2D.Raycast(transform.position, castDir);

		Tile tile;

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

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });

		if (_matchFound)
		{
			ImageSource = null;
			_matchFound = false;
			OnMatchFound?.Invoke();
			OnSoundPlay?.Invoke(Clip.Clear);
		}
	}

	private void Select() {
		_isSelected = true;
		_renderer.color = selectedColor;
		previousSelected = this;
		OnSoundPlay?.Invoke(Clip.Select);
	}

	private void Deselect() {
		_isSelected = false;
		_renderer.color = Color.white;
		previousSelected = null;
	}

	private Tile GetAdjacent(Vector2 castDir)
	{
		var hit = Physics2D.Raycast(transform.position, castDir);

		if (hit.collider != null)
		{
			return hit.collider.GetComponent<Tile>();
		}

		return null;
	}

	private bool CheckTilesForMatch(Collider2D collider, Sprite otherToCompare, out Tile tile)
    {
		tile = collider.GetComponent<Tile>();
		return tile.ImageSource.Equals(otherToCompare);
    }

	private void ClearMatch(Vector2[] paths)
	{
		var matchingTiles = new List<Tile>();
		for (var i = 0; i < paths.Length; i++)
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 2)
		{
			for (var i = 0; i < matchingTiles.Count; i++)
			{
				matchingTiles[i].ImageSource = null;
			}
			_matchFound = true;
		}
	}
}