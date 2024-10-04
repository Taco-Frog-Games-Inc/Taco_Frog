using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;

    [SerializeField] private TileFactory tileFactory;
    
    void Start()
    {
        for(int i = 0; i < width; i++)        
            for(int j = 0; j < depth; j++)
            {
                Vector3 pos = new(i * 10, 0, j * 10);

                EnumTileType type = (i == 0 || j == 0 || i == width - 1 || j == depth - 1) ? EnumTileType.CONTOUR : EnumTileType.MIDDLE;
                tileFactory.Create(type, pos);
            }        
    }
}
