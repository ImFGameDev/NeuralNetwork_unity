using UnityEngine;

namespace Seshihiko.Systems.Entity.Config
{
    public class MovementConfig : ScriptableObject
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float _angularSpeed;

        public float MovementSpeed => movementSpeed;
        public float AngularSpeed => _angularSpeed;
    }
}