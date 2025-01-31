using UnityEngine;

namespace Seshihiko.Systems.Entity.Objects
{
    public struct Data
    {
        public TypeEnvironmentObject TypeObject;
        public int LocalIndex;
    }
    
    public interface IEnvironmentObject
    {
        public Data GetData();
        public Transform GetTransform();
    }
}