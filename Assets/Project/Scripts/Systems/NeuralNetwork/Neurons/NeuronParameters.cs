using System;

namespace Seshihiko.Systems.NeuralNetwork.Parameters
{
    [Serializable]
    public struct NeuronParameters
    {
        public float[] Weights;
        public float Bias;
    }
}