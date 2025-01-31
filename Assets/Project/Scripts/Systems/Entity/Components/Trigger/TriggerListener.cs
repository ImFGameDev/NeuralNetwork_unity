using Seshihiko.Systems.Entity.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Seshihiko.Systems.Entity.Components
{
    public class TriggerListener : MonoBehaviour
    {
        public UnityEvent<IEnvironmentObject> Collision;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IEnvironmentObject>(out var environment))
            {
                Collision?.Invoke(environment);
            }
        }
    }
}