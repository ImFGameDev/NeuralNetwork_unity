using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core.Interfase
{
    public class AgentsStorage : MonoBehaviour, ITickable
    {
        [SerializeField] private AgentBase _prefab;
        
        private List<AgentBase> _agents;

        public AgentBase Prefab => _prefab;

        public void SetListAgents(List<AgentBase> agents)
        {
            _agents = agents;
        }

        public List<AgentBase> GetListAgents()
        {
            return _agents;
        }

        public void Tick(float delta)
        {
            foreach (var agent in _agents)
            {
                agent.Tick();
            }
        }
    }
}