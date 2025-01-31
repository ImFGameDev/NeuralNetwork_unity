using System.Collections.Generic;
using Project.Scripts.Core.Extensions;
using Project.Scripts.Core.Interfase;
using Seshihiko.Systems.Automobile;
using Seshihiko.Systems.Entity.Components;
using Seshihiko.Systems.Entity.Objects;
using Seshihiko.Systems.NeuralNetwork.Base;
using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Project.Scripts.Characters
{
    public class AgentCar : AgentBase
    {
        [SerializeField] private Transform _rayTransform;
        [SerializeField] private TriggerListener _trigger;
        [SerializeField] private Car _car;
        
        private List<int> _targetsIndexes;
        private bool _isItActive;
        private float[] _inputSignals;

        private float[] CalculateNetwork()
        {
            var result = _rayScanner.Scan(_rayTransform);
            
            for (int i = 0; i < result.RaysData.Length; i++) //Заполнение входных сигналов
                if (result.RaysData[i].Distance != 0 && result.RaysData[i].EnvironmentObject.TypeObject == TypeEnvironmentObject.Wall)
                    _inputSignals[i] = result.RaysData[i].Distance;
                else
                    _inputSignals[i] = 1;

            _inputSignals[_inputSignals.Length - 1] = _car.Speed / _car.MaxSpeed;

            return _network.CalculateTheResult(_inputSignals);
        }

        private void TakeAction(float[] output)
        {
            _car.Drive(output.ArrayToVector2());
        }
        
        private void Collision(IEnvironmentObject obj)
        {
            var data = obj.GetData();
            
            switch (data.TypeObject)
            {
                case TypeEnvironmentObject.Wall:
                    _car.DisableMovement();
                    _isItActive = false;
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
            {
                TakeAction(CalculateNetwork());   
            }
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
            _car.EnableMovement();
            _isItActive = true;
        }
    }
}