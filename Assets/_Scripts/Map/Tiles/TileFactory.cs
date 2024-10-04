
using UnityEngine;

public interface ITile {
    public void Create(EnumTileType type, Vector3 pos);
}

public enum EnumTileType { 
    CONTOUR,
    MIDDLE
}

public class TileFactory: MonoBehaviour, ITile
{
    [SerializeField] private GameObject contourTile;
    [SerializeField] private GameObject middleTile;
    public void Create(EnumTileType type, Vector3 pos)
    {
        switch(type)
        {
            case EnumTileType.CONTOUR:
                Instantiate(contourTile, pos, Quaternion.identity);
                break;

            case EnumTileType.MIDDLE:
                Instantiate(middleTile, pos, Quaternion.identity);
                break;

            default:
                throw new System.Exception("Cannot instantiate a tile from the factory...");
        }        
    }
}
