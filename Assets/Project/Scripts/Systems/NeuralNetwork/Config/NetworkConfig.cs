using UnityEngine;

namespace Seshihiko.Systems.NeuralNetwork.Config
{
    [CreateAssetMenu(menuName = "NetworkConfig", fileName = "new NetworkConfig")]
    public class NetworkConfig : ScriptableObject
    {
        [SerializeField] private int[] _config;
        
        public int[] ConfigNetwork => _config;
    }
}