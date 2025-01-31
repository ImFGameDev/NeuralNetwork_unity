using Seshihiko.Systems.NeuralNetwork.Parameters;
using UnityEngine;

namespace Project.Scripts.Core.Debuger
{
    public static class DebugConsole
    {
        public static void Log(string text)
        {
            Debug.Log(text);
        }

        public static void ErrorLog(string text)
        {
            Debug.LogError(text);
        }
        
        public static void DebudWeight(NetworkModel model, string text)
        {
            var weight = "";
            var bias = "";
            
            for (int i = 1; i < model.Parameters.Length; i++)
            {
                for (int j = 0; j < model.Parameters[i].Length; j++)
                {
                    for (int k = 0; k < model.Parameters[i][j].Weights.Length; k++)
                    {
                        weight += $"\n{i}_{j}_{k}: {model.Parameters[i][j].Weights[k]}";
                    }
                    bias += $"\n{i}_{j}: {model.Parameters[i][j].Bias}";
                }
            }
            
            Log($"{text}\n Weights: {weight}" +
                             $"\nBias: {bias}");
        }
    }
}