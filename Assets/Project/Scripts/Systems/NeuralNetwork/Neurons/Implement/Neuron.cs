using Seshihiko.Systems.NeuralNetwork.Base;
using Seshihiko.Systems.NeuralNetwork.Functions;

namespace Seshihiko.Systems.NeuralNetwork
{
    public class Neuron : NeuronBase
    {
        public override void Calculate(NeuronBase[] prevNeurons)
        {
            var sum = 0f;
            for (int i = 0; i < _parameters.Weights.Length; i++)
            {
                sum += (_parameters.Weights[i] * prevNeurons[i].OutputSignal) + _parameters.Bias;
            }

            _outputSignal = ActivationFunctions.Tanh(sum);
        }
    }   
}
