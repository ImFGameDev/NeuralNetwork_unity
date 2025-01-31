using Seshihiko.Systems.Entity.Objects;
using UnityEngine;

namespace Seshihiko.Project.Envirovement.Object
{
    public class Wall : MonoBehaviour, IEnvironmentObject
    {
        private Data _data;
        
        private void Awake()
        {
            _data.LocalIndex = 0;
            _data.TypeObject = TypeEnvironmentObject.Wall;
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