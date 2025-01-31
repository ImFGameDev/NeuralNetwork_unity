using System.Collections.Generic;
using Project.Scripts.Agents.Manager;
using Project.Scripts.Core.Interfase;
using Seshihiko.Project.Agents.Learning;
using UnityEngine;
using Zenject;
using ITickable = Project.Scripts.Core.Interfase.ITickable;

namespace Project.Scripts.Core.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private AgentManager _agentManager;
        [SerializeField] private AgentsStorage _agentsStorage;

        private List<ITickable> _tickables;
        private bool _isInit;

        public override void InstallBindings()
        {
            BindAgentStorage();
            BindAgentManager();
            BindNaturalSelection();
            InitializeComponents();
            InstallTickableObjects();
        }

        private void BindAgentStorage()
        {
            Container.BindInterfacesAndSelfTo<AgentsStorage>()
                .FromInstance(_agentsStorage)
                .AsSingle()
                .NonLazy();
        }

        private void BindAgentManager()
        {
            Container.BindInterfacesAndSelfTo<AgentManager>()
                .FromInstance(_agentManager)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindNaturalSelection()
        {
            Container.BindInterfacesAndSelfTo<NaturalSelection>()
                .AsSingle()
                .NonLazy();
        }

        private void InitializeComponents()
        {
            var components = Container.ResolveAll<IInitialize>();

            foreach (var component in components)
            {
               component.Init();
            }
            
            _isInit = true;
        }

        private void InstallTickableObjects()
        {
            _tickables = Container.ResolveAll<ITickable>();
        }

        private void FixedUpdate()
        {
            if (_isInit)
                foreach (var variabTickable in _tickables)
                {
                    variabTickable.Tick(Time.deltaTime);
                }
        }
    }
}