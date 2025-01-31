using System.Collections.Generic;
using Project.Scripts.Core.Debuger;
using Seshihiko.Systems.NeuralNetwork.Base;
using Seshihiko.Systems.NeuralNetwork.Parameters;

namespace Seshihiko.Systems.NeuralNetwork.Model
{
    public sealed class Perceptron : NetworkBase
    {
        private NeuronBase[][] _neurons; //Двумерный массив, первое знаение слой, второе количестов нееронов в слое

        public Perceptron(int[] config)
        {
            CreateLayers(config);
        }

        private void CreateLayers(int[] config)
        {
            var layers = new List<Neuron[]>();
           
            for (int i = 0; i < config.Length; i++) //Слои
            {
                var layer = new Neuron[config[i]];
                
                for (int j = 0; j < layer.Length; j++) // Нейроны
                {
                    layer[j] = new Neuron();
                }
                
                layers.Add(layer);
            }

            _neurons = layers.ToArray();
        }

        public override void InstallParameters(NetworkModel model)
        {
            for (int i = 0; i < _neurons.Length; i++)
                for (int j = 0; j < _neurons[i].Length; j++)
                    _neurons[i][j].SetParameters(model.Parameters[i][j]);
        }

        public override NetworkModel GetParameters()
        {
            var parameters = new List<NeuronParameters[]>();

            for (int i = 0; i < _neurons.Length; i++)
            {
                parameters.Add(new NeuronParameters[_neurons[i].Length]);
                
                if (i > 0)
                {
                    for (int j = 0; j < _neurons[i].Length; j++)
                    {
                        parameters[i][j] = new NeuronParameters();
                        parameters[i][j] = _neurons[i][j].GetParameters();
                    }   
                }
            }

            var model = new NetworkModel
            {
                Parameters = parameters.ToArray() 
            };
            
            return model;
        }

        public override float[] CalculateTheResult(float[] inputs)
        {
            var outputs = new float[_neurons[_neurons.Length-1].Length];
            
            for (int i = 0; i < _neurons[0].Length; i++)
            {
                _neurons[0][i].GiveInputSignal(inputs[i]);
            }
            
            for (int i = 1; i < _neurons.Length; i++)
            {
                var prevLayer = _neurons[i - 1];
                for (int j = 0; j < _neurons[i].Length; j++)
                {
                    _neurons[i][j].Calculate(prevLayer);
                }
            }

            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = _neurons[_neurons.Length - 1][i].OutputSignal;
            }
            
            return outputs;
        }
    }
}
