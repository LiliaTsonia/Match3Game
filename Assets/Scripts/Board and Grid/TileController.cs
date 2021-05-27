using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController
{
    public static event Action<SoundClip, AudioType> OnSoundPlay;
    public static event Action OnMatchFound;

    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static TileController previousSelected = null;

    private bool _isSelected;
    private bool _matchFound;

    private Vector2[] _adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private readonly ICommonTile _tile;

    public TileController(ICommonTile tile)
    {
        _tile = tile;
    }

    public void OnTilePressed()
    {
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

				if (GetAllAdjacentTiles().Contains(previousSelected._tile))
				{
					_tile.SwapSprite(previousSelected._tile);
					OnSoundPlay?.Invoke(SoundClip.Swap, AudioType.SFX_Master);
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

	public List<ICommonTile> GetAllAdjacentTiles()
	{
		var adjacentTiles = new List<ICommonTile>();

		for (var i = 0; i < _adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(_tile.GetAdjacent(_adjacentDirections[i]));
		}

		return adjacentTiles;
	}

	public void ClearMatch(Vector2[] paths)
	{
		var matchingTiles = new List<ICommonTile>();
		for (var i = 0; i < paths.Length; i++)
		{
			matchingTiles.AddRange(_tile.FindMatch(paths[i]));
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

	public void ClearAllMatches()
    {
		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });

		CheckIfMatchFound();
	}

	public void CheckIfMatchFound()
    {
		if (_matchFound)
		{
			_tile.ImageSource = null;
			_matchFound = false;
			OnMatchFound?.Invoke();
			OnSoundPlay?.Invoke(SoundClip.Clear, AudioType.SFX_Master);
		}
	}

	private void Select()
	{
		_isSelected = true;
		_tile.SetColor(selectedColor);
		previousSelected = this;
		OnSoundPlay?.Invoke(SoundClip.Select, AudioType.SFX_Master);
	}

	private void Deselect()
	{
		_isSelected = false;
		_tile.SetColor(Color.white);
		previousSelected = null;
	}
}
