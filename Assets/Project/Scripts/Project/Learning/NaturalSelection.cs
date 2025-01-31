using Project.Scripts.Core.Debuger;
using Project.Scripts.Core.Interfase;
using Project.Scripts.Game.Environment.Configuration_learning;
using Seshihiko.Systems.NeuralNetwork.Learning;

namespace Seshihiko.Project.Agents.Learning
{
    public sealed class NaturalSelection
    {
        private MutationConfig _mutationConfig;
        private TrainingConfig _trainingConfig;
        private AgentsStorage _agentsStorage;
        private Mutation _mutation;
        private int _teacherSize;
        private int _bestResult;

        public NaturalSelection(MutationConfig mutationConfig, TrainingConfig trainingConfig, AgentsStorage agentsStorage)
        {
            _mutationConfig = mutationConfig;
            _trainingConfig = trainingConfig;
            _agentsStorage = agentsStorage;
        }
        
        public void Teach()
        {
            var agents = _agentsStorage.GetListAgents();
            
            agents.Sort();

            if (agents[agents.Count - 1].Perfomance > _bestResult)
            {
                _bestResult = agents[agents.Count - 1].Perfomance;
                DebugConsole.Log($"New best result: {_bestResult}");
            }

            for (int i = 0; i < _teacherSize; i++)
            {
                agents[i].SetEditedModel(
                    _mutation.Mutate(agents[i + _teacherSize].GetParameters()));
            }
        }

        public void Init()
        {
            _teacherSize = _trainingConfig.PopulationSize / 2;
            _mutation = new Mutation(_mutationConfig);
        }
    }
}
