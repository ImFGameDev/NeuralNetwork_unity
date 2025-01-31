using System;

namespace Seshihiko.Systems.NeuralNetwork.Parameters
{
    [Serializable]
    public struct NetworkModel
    {
        public NeuronParameters[][] Parameters;
    }
}