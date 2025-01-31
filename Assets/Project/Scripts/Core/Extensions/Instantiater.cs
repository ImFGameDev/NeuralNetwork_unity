using UnityEngine;

namespace Project.Scripts.Game.Spawner
{
    public class Instantiater : MonoBehaviour
    {
        public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            return Instantiate(prefab, position, rotation);
        }
    }
}