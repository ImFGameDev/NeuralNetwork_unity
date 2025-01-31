using System.Collections.Generic;
using Project.Scripts.Core.Debuger;
using Project.Scripts.Core.Interfase;
using Project.Scripts.Game.Environment.Configuration_learning;
using Project.Scripts.Game.Spawner;
using Seshihiko.Project.Agents.Learning;
using Seshihiko.Systems.Entity.Components;
using Seshihiko.Systems.Entity.Config;
using Seshihiko.Systems.NeuralNetwork.Config;
using Seshihiko.Systems.NeuralNetwork.Functions;
using Seshihiko.Systems.NeuralNetwork.Model;
using Seshihiko.Systems.NeuralNetwork.Parameters;
using Seshihiko.Systems.Save;
using UnityEngine;
using Venera.Architecture;
using Zenject;

namespace Project.Scripts.Agents.Manager
{
    public class AgentManager : MonoBehaviour, IInitialize, ISave
    {
        private const string NAME_SAVE_PARAMETERS = "/AgentAutomobile.txt";
        
        [SerializeField] private Transform _spawnPoint;

        [Inject] private NetworkConfig _networkConfig;
        [Inject] private TrainingConfig _trainingConfig;
        [Inject] private RayScannerConfig _rayScannerConfig; 
        [Inject] private AgentsStorage _agentsStorage;
        [Inject] private NaturalSelection _naturalSelection;
        
        private int _currentPopulationSize;

        private void CreateAgents()
        {
            var agents = new List<AgentBase>();

            _currentPopulationSize = _trainingConfig.PopulationSize;
                
            for (int i = 0; i < _trainingConfig.PopulationSize; i++)
            {
                var agent = Instantiater.Spawn(_agentsStorage.Prefab, 
                    _spawnPoint.position.GetNoizePosition(2), Quaternion.identity);
                
                agent.transform.forward = _spawnPoint.forward;
                agent.Init(this);
                
                agents.Add(agent);
            }
            
            _agentsStorage.SetListAgents(agents);
        }
        
        private void InitAgents(NetworkModel model)
        {
            var rayScanner = new RayScanner(_rayScannerConfig);
            
            foreach (var agent in _agentsStorage.GetListAgents())
            {
                var network = new Perceptron(_networkConfig.ConfigNetwork);
                
                network.InstallParameters(model);
                
                agent.InstallComponents(network, rayScanner, _networkConfig.ConfigNetwork[0]);
                agent.Reset();
            }
        }
        
        private void Teach()
        {
            _naturalSelection.Teach();
            
            Reset();
        }

        public void DeadAgent()
        {
            _currentPopulationSize--;

            if (_currentPopulationSize <= 0)
                Teach();
        }

        public void Reset()
        {
            var agents = _agentsStorage.GetListAgents();

            _currentPopulationSize = _trainingConfig.PopulationSize;
            
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].transform.position = _spawnPoint.position.GetNoizePosition(2);
                agents[i].transform.forward = _spawnPoint.forward;
                agents[i].Reset();
            }
        }
        
        public void Save()
        {
            var agents = _agentsStorage.GetListAgents();
            
            agents.Sort();
            
            Serializator.SaveData(agents[agents.Count - 1].GetParameters(), NAME_SAVE_PARAMETERS);
        }

        public void Load()
        {
            if(Serializator.LoadData<NetworkModel>(out var parameters, NAME_SAVE_PARAMETERS))
            {
                InitAgents(parameters);
            }
            else
            {
                DebugConsole.Log("Create new model");
                var model = NetworkCreater.CreateParameters(_networkConfig.ConfigNetwork);
                
                InitAgents(model);
            }
        }

        public void Init()
        {
            _naturalSelection.Init();
            
            Time.timeScale = _trainingConfig.SpeedOfTime;

            CreateAgents();
            Load();
            
            InvokeRepeating("Save", 
                _trainingConfig.TimeIntervalBetweenSaves, 
                _trainingConfig.TimeIntervalBetweenSaves);
            
            InvokeRepeating("Teach", 
                _trainingConfig.TimeIntervalBetweenIterations, 
                _trainingConfig.TimeIntervalBetweenIterations);
        }
    }
}