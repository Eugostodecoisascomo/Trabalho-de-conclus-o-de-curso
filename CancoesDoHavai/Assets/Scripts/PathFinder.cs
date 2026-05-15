/*using System.Numerics;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    IDictionary<Vector3, Vector3> branchParents = new Dictionary<Vector3, Vector3>();
    public IList<Vector3> walkableBranchs = new IList<Vector3>();

    Vector3 FindShortestPath(Vector3 start, Vector3 finish)
    {
        uint nodeVisitCount = 0;

        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> exploredBranchs = new HashSet<Vector3>();
        queue.Enqueue (start);
        while(queue.Count != 0)
        {
            Vector3 currentNode = queue.Dequeue();
            nodeVisitCount++;
        }
    }
}
*/