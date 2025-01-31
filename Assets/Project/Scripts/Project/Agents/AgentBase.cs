using System;
using Project.Scripts.Agents.Manager;
using Seshihiko.Systems.Entity.Components;
using Seshihiko.Systems.NeuralNetwork.Base;
using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Project.Scripts.Core.Interfase
{
    public abstract class AgentBase : MonoBehaviour, IComparable<AgentBase>
    {
        protected int _perfomance; //Отображает производительность агента 
        protected NetworkBase _network;
        protected RayScanner _rayScanner;
        
        private AgentManager _agentManager;
        
        public int Perfomance => _perfomance;
        
        public abstract void Tick();

        public abstract void InstallComponents(NetworkBase network, RayScanner rayScanner, int inputs);

        public abstract void Reset();

        public abstract NetworkModel GetParameters();

        public void Dead()
        {
            _agentManager.DeadAgent();
        }

        public void SetEditedModel(NetworkModel model) => _network.InstallParameters(model);

        public int CompareTo(AgentBase other)
        {
            if (other == null) return 1;

            if (Perfomance > other.Perfomance)
                return 1;
            else if (Perfomance < other.Perfomance)
                return -1;
            else
                return 0;
        }

        public void Init(AgentManager agentManager) => _agentManager = agentManager;
    }
}