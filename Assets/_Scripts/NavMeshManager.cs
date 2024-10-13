using UnityEngine;
using Unity.AI.Navigation;
public class NavMeshManager : MonoBehaviour
{
    public static bool isInitialize = false;

    void Update()
    {        
        if (!isInitialize) {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            isInitialize = true;
        }        
    }
}
