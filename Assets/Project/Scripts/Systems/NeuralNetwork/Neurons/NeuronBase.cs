using Seshihiko.Systems.NeuralNetwork.Parameters;

namespace Seshihiko.Systems.NeuralNetwork.Base
{
    public abstract class NeuronBase
    {
        protected NeuronParameters _parameters;
        protected float _inputSignal;
        protected float _outputSignal;
        
        public float OutputSignal => _outputSignal;

        public abstract void Calculate(NeuronBase[] prevNeurons);

        public void SetParameters(NeuronParameters parameters)
        {
            _parameters = parameters;
        }

        public NeuronParameters GetParameters()
        {
            return _parameters;
        }

        public void GiveInputSignal(float value)
        {
            _inputSignal = value;
            _outputSignal = _inputSignal;
        }
    }
}