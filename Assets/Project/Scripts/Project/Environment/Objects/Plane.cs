using Seshihiko.Systems.Entity.Objects;
using UnityEngine;

namespace Seshihiko.Project.Envirovement.Object
{
    public class Plane : MonoBehaviour, IEnvironmentObject
    {
        private Data _data;

        private void Awake()
        {
            _data.LocalIndex = 0;
            _data.TypeObject = TypeEnvironmentObject.Terrain;
        }

        public Data GetData()
        {
            return _data;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}