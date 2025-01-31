using UnityEngine;
using UnityEngine.AI;

namespace Venera.Architecture
{
    public static class ExtentionFunctions
    {
        public static Vector3 GetRandomPointInPath(this NavMeshAgent agent)
        {
            Debug.Log(agent.path.corners.Length);
            
            var path = agent.path;
            var index = Random.Range(0, path.corners.Length);
            return path.corners[index];
        }

        public static Vector3 GetNoizePosition(this Vector3 vector, float intensity)
        {
            return vector + new Vector3(Random.Range(0, intensity),0,Random.Range(0, intensity));
        }

        public static Transform GetRandomPoint(this Transform[] transforms)
        {
            var value = Random.Range(0, transforms.Length);
            var transform = transforms[value];
           
            return transform;
        }
    }
}