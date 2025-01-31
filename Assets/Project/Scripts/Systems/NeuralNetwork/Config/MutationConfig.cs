using UnityEngine;

namespace Seshihiko.Systems.NeuralNetwork.Learning
{
    [CreateAssetMenu(menuName = "MutationConfig", fileName = "new MutationConfig")]
    public class MutationConfig : ScriptableObject
    {
        [SerializeField] private int _mutationChance = 20;
        [SerializeField] private float _mutationStrenth = 0.5f;
        
        public int MutationChance => _mutationChance;
        public float MutationStrength => _mutationStrenth;
    }
}