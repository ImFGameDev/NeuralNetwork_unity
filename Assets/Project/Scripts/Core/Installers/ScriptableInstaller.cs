using Project.Scripts.Game.Environment.Configuration_learning;
using Seshihiko.Systems.Entity.Config;
using Seshihiko.Systems.NeuralNetwork.Config;
using Seshihiko.Systems.NeuralNetwork.Learning;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    [CreateAssetMenu(menuName = "Scriptable", fileName = "ScriptableInstaller")]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private NetworkConfig _networkConfig;
        [SerializeField] private TrainingConfig _trainingConfig;
        [SerializeField] private MutationConfig _mutationConfig;
        [SerializeField] private RayScannerConfig _rayConfig;
        
        public override void InstallBindings()
        {
            BingConfigs();
        }
        
        private void BingConfigs()
        {
            Container.BindInstance(_networkConfig).AsSingle().NonLazy();
            Container.BindInstance(_trainingConfig).AsSingle().NonLazy();
            Container.BindInstance(_mutationConfig).AsSingle().NonLazy();
            Container.BindInstance(_rayConfig).AsSingle().NonLazy();
        }
    }
}