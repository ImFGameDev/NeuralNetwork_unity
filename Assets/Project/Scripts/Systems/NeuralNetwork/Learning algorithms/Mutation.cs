using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Seshihiko.Systems.NeuralNetwork.Learning
{
    public sealed class Mutation
    {
        private MutationConfig _config; 
            
        public Mutation(MutationConfig config)
        {
            _config = config;
        }

        public NetworkModel Mutate(NetworkModel network)
        {
            for (ushort i = 1; i < network.Parameters.Length; i++)
            {
                for (ushort j = 0; j < network.Parameters[i].Length; j++)
                {
                    for (ushort k = 0; k < network.Parameters[i][j].Weights.Length; k++)
                    {
                        var chanceNeuron = Random.Range(0, 100);
                        var chanceBias = Random.Range(0, 100);
                        
                        if (chanceNeuron <= _config.MutationChance)
                        {
                            network.Parameters[i][j].Weights[k] += Random.Range(-_config.MutationStrength, _config.MutationStrength);
                        }

                        if (chanceBias <= _config.MutationChance)
                        {
                            network.Parameters[i][j].Bias += Random.Range(-_config.MutationStrength, _config.MutationStrength);
                        }
                    }
                }
            }
            
            return network;
        }
    }   
}
