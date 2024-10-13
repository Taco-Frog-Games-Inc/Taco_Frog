using UnityEngine;
using Unity.AI.Navigation;
public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;


    // Update is called once per frame
    void Update()
    {
        surface.BuildNavMesh();
    }
}
