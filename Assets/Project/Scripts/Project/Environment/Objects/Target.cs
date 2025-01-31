using System;
using Seshihiko.Systems.Entity.Objects;
using UnityEngine;

namespace Seshihiko.Project.Envirovement.Object
{
    public class Target : MonoBehaviour, IEnvironmentObject
    {
        private static int _index;
        private Data _data;

        private void Awake()
        {
            _index++;
            _data.LocalIndex = _index;
            _data.TypeObject = TypeEnvironmentObject.Target;
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