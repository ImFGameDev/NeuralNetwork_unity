using System.Collections.Generic;
using Project.Scripts.Core.Debuger;
using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Seshihiko.Systems.NeuralNetwork.Functions
{
    public static class NetworkCreater
    {
        public static NetworkModel CreateParameters(int[] config)
        {
            var networkParameters = new NetworkModel();
            var parameters = new List<NeuronParameters[]>();

            for (int i = 0; i < config.Length; i++)
            {
                parameters.Add(new NeuronParameters[config[i]]);
                
                if (i > 0)
                {
                    for (int j = 0; j < config[i]; j++)
                    {
                        parameters[i][j] = new NeuronParameters();
                        parameters[i][j].Weights = new float[config[i-1]];
                        
                        for (int k = 0; k < config[i-1]; k++)
                        {
                            parameters[i][j].Weights[k] = Random.Range(-0.5f, 0.5f);
                        }
                        parameters[i][j].Bias = Random.Range(-0.5f, 0.5f);
                    }   
                }
            }

            networkParameters.Parameters = parameters.ToArray();
            return networkParameters;
        }

        public static NetworkModel CopyParameters(NetworkModel model)
        {
            var copiedParameters = new NetworkModel();
            var numberOfLayers = model.Parameters.Length;
        
            copiedParameters.Parameters = new NeuronParameters[model.Parameters.Length][];
        
            for (int i = 1; i < numberOfLayers; i++)
            {
                var numberOfNeurons = model.Parameters[i].Length;
                
                copiedParameters.Parameters[i] = new NeuronParameters[numberOfNeurons];
        
                for (int j = 0; j < numberOfNeurons; j++)
                {
                    var numberOfWeights = model.Parameters[i][j].Weights.Length;
                    
                    copiedParameters.Parameters[i][j] = new NeuronParameters();
                    copiedParameters.Parameters[i][j].Bias = model.Parameters[i][j].Bias;
        
                    for (int k = 0; k < numberOfWeights; k++)
                    {
                        copiedParameters.Parameters[i][j].Weights = new float[numberOfWeights];
                        copiedParameters.Parameters[i][j].Weights[k] = model.Parameters[i][j].Weights[k];   
                    }
                }
            }
            
            return copiedParameters;
        }
    }
}