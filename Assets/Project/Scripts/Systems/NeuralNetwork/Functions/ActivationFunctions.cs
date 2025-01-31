using System;

namespace Seshihiko.Systems.NeuralNetwork.Functions
{
    public static class ActivationFunctions
    {
        public static float Tanh(float x)
        {
            return (float)Math.Tanh(x);
        }
    
        public static float Sigmoid(float x)
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }
   
        public static float Relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
    
        public static float Leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
    
        public static float SigmoidDer(float x)
        {
            return x * (1 - x);
        }
    
        public static float TanhDer(float x)
        {
            return 1 - (x * x);
        }
    
        public static float ReluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
    
        public static float LeakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }
    }   
}
