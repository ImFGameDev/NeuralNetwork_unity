using System.Collections.Generic;
using Project.Scripts.Core.Extensions;
using Project.Scripts.Core.Interfase;
using Project.Scripts.Systems.Tank;
using Seshihiko.Systems.Entity.Components;
using Seshihiko.Systems.Entity.Objects;
using Seshihiko.Systems.NeuralNetwork.Base;
using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Project.Scripts.Characters
{
    public class AgentTank : AgentBase
    {
        [SerializeField] private Transform _rayTransform;
        [SerializeField] private TriggerListener _trigger;
        [SerializeField] private Tank _tank;
        
        private List<int> _targetsIndexes;
        private bool _isItActive;
        private float[] _inputSignals;

        private float[] CalculateNetwork()
        {
            var result = _rayScanner.Scan(_rayTransform);

            for (int i = 0; i < _inputSignals.Length; i++) //Обнуление входных сигналов
                _inputSignals[i] = 1;

            for (int i = 0; i < result.RaysData.Length; i++) //Заполнение входных сигналов
            {
                if (result.RaysData[i].Distance != 0 &&
                    result.RaysData[i].EnvironmentObject.TypeObject == TypeEnvironmentObject.Wall)
                {
                    var normalizebleDistance = result.RaysData[i].Distance;
                     _inputSignals[i] = normalizebleDistance;
                }
            }

            _inputSignals[_inputSignals.Length - 1] = _tank.Speed / _tank.MaxSpeed;

            return _network.CalculateTheResult(_inputSignals);
        }

        private void TakeAction(float[] output)
        {
            _tank.DriveLeft(output[0]);
            _tank.DriveLeft(output[1]);
        }
        
        private void Collision(IEnvironmentObject obj)
        {
            var data = obj.GetData();
            
            switch (data.TypeObject)
            {
                case TypeEnvironmentObject.Wall:
                    _tank.DisableMovement();
                    _isItActive = false;
                    Dead();
                    break;
                case TypeEnvironmentObject.Target:
                    if (CheckTarget(data.LocalIndex))
                    {
                        _perfomance++;   
                    }
                    break;
            }
        }

        private bool CheckTarget(int index)
        {
            foreach (var variableIndex in _targetsIndexes)
            {
                if (variableIndex == index)
                    return false;
            }

            _targetsIndexes.Add(index);
            return true;
        }
        

        public override void Tick()
        {
            if (_isItActive) 
                TakeAction(CalculateNetwork());
        }

        public override void InstallComponents(NetworkBase network, RayScanner rayScanner, int inputs)
        {
            _targetsIndexes = new List<int>();
            _network = network;
            _rayScanner = rayScanner;
            
            _inputSignals = new float[inputs];
            _trigger.Collision.AddListener(Collision);
        }
        
        public override NetworkModel GetParameters()
        {
            return _network.GetParameters();
        }

        public override void Reset()
        {
            _targetsIndexes = new List<int>();
            _perfomance = 0;
            _tank.EnableMovement();
            _isItActive = true;
        }
    }
}