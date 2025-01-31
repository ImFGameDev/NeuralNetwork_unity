using Seshihiko.Systems.NeuralNetwork.Parameters;

namespace Seshihiko.Systems.NeuralNetwork.Base
{
    public abstract class NetworkBase
    {
        public abstract void InstallParameters(NetworkModel model);
        
        public abstract NetworkModel GetParameters();
        
        public abstract float[] CalculateTheResult(float[] inputs);
    }
}