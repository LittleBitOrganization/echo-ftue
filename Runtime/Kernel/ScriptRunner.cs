using System;
using System.Collections;
using System.Linq;
using LittleBit.Modules.CoreModule;
using LittleBitGames.FTUE.Kernel.ScenarioSystem;
using UnityEngine;

namespace LittleBitGames.FTUE.Kernel
{
    public class ScriptRunner : IRunner
    {
        public event Action OnComplete;

        private readonly LQueue<Scenario> _scenariosMainThread;

        private readonly LQueue<Scenario> _scenariosSecondaryThread;

        private readonly ICoroutineRunner _coroutineRunner;

        public ScriptRunner(LQueue<Scenario> scenarios, ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _scenariosMainThread = scenarios;
            _scenariosSecondaryThread = new LQueue<Scenario>();
        }

        public void Run() => _coroutineRunner.StartCoroutine(ThreadDispatching());

        private IEnumerator ThreadDispatching()
        {
            while (true)
            {
                var scenario = GetScenario();
                yield return null;
                
                if(scenario == null) continue;
                if(scenario.IsForceSkip) continue;
                if(scenario.CanBeSkipped) continue;

                if (scenario.Available == false)
                {
                    _scenariosSecondaryThread.Enqueue(scenario);
                    continue;
                }
                scenario.Run();

                yield return new WaitUntil(() => scenario.IsCompleted);
            }

            OnComplete?.Invoke();
        }
        
        private bool IsCompleted => _scenariosMainThread.Count == 0 && _scenariosSecondaryThread.Count == 0;
        
        /// <summary>
        /// Ожидаем выполнения одного сценария, который во втором контейнере
        /// </summary>
        /// <returns></returns>
        private Scenario GetScenario()
        {
            if (_scenariosSecondaryThread.Count == 0)
            {
                if (_scenariosMainThread.Count > 0)
                    return _scenariosMainThread.Dequeue();
                return null;
            }
            else
            {
                var availableScenario = _scenariosSecondaryThread.FirstOrDefault(s => s.Available);
                if (availableScenario != null)
                {
                    _scenariosSecondaryThread.Remove(availableScenario);
                    return availableScenario;
                }
                return null;

            }
            
        }
    }
}