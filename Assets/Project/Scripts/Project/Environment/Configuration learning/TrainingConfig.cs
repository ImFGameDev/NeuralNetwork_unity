using UnityEngine;

namespace Project.Scripts.Game.Environment.Configuration_learning
{
    [CreateAssetMenu(menuName = "TrainingConfig", fileName = "new TrainingConfig")]
    public class TrainingConfig : ScriptableObject
    {
        [Header("Population")]
        [SerializeField] private int _populationSize = 50;
        [SerializeField] private int timeIntervalBetweenSaves = 300;
        [SerializeField] private int timeIntervalBetweenIterations = 25;
        [Header("Environment")]
        [SerializeField] private float _speedOfTime = 1f;
        
        public int PopulationSize => _populationSize;
        public int TimeIntervalBetweenSaves => timeIntervalBetweenSaves;
        public int TimeIntervalBetweenIterations => timeIntervalBetweenIterations;
        public float SpeedOfTime => _speedOfTime;
    }
}