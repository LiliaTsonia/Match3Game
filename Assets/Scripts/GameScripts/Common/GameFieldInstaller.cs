using Zenject;
using UnityEngine;

public class GameFieldInstaller : MonoInstaller
{
    public GameObject TilePrefab;

    public override void InstallBindings()
    {
        BindTileFactory();
    }

    private void BindTileFactory()
    {
        Tile.TileFactory.TileOffset = TilePrefab.GetComponent<SpriteRenderer>().bounds.size;

        Container
            .BindFactory<ICommonTile, Tile.TileFactory>().FromComponentInNewPrefab(TilePrefab)
            .AsSingle();
    }
}
