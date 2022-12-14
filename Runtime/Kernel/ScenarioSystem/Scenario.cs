using System;
using LittleBit.Modules.CoreModule;
using LittleBit.Modules.Description;
using LittleBit.Modules.SequenceLogicModule;
using LittleBitGames.FTUE.Components;
using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.Kernel.Factory;
using LittleBitGames.QuestsModule.Common;
using LittleBitGames.QuestsModule.Factories;
using LittleBitGames.QuestsModule.Trackers.Metadata;
using LittleBitGames.QuestsModule.Trackers.ProgressCalculators;

namespace LittleBitGames.FTUE.Kernel.ScenarioSystem
{
    public class Scenario : IRunner, IKeyHolder, IOriginator<ScenarioData>
    {
        private string _key;
        public event Action OnComplete;

        private LQueue<ScenarioComponent> _queueComponents;
        private readonly ICreator _creator;
        private ComponentFactory _componentFactory;
        private IRunner _scenarioRunner;

        private SequenceLogic _availabilityLogic;
        private SequenceLogic _skipLogic;
        private ITrackerProvider _trackerProvider;

        public bool CanBeSkipped { get; private set; } = false;

        public bool IsForceSkip { get; private set; } = false;
        public bool IsCompleted { get; private set; } = false;

        public bool Available { get; private set; } = true;

        public Scenario(ICreator creator, ITrackerProvider trackerProvider, string key)
        {
            _creator = creator;
            _key = key;
            _queueComponents = new LQueue<ScenarioComponent>();
            _componentFactory = new ComponentFactory(_queueComponents, _creator, this);
            _scenarioRunner = new ScenarioRunner(_queueComponents, _componentFactory);
            _availabilityLogic = new SequenceLogic();
            _skipLogic = new SequenceLogic();
            _trackerProvider = trackerProvider;
            
            _availabilityLogic.OnChangeAvailable += OnChangeAvailable;
        }

        private void OnChangeAvailable(bool available) =>
            Available = available;

        private void OnChangeSkip(bool isSkip)
        {
            IsForceSkip = isSkip;
        }

        public ComponentFactory Add()
        {
            return _componentFactory;
        }
        
        public Scenario AddSkipLogic(UnitLogic cellarChecker)
        {
            IsForceSkip = false;
            _skipLogic.Add(cellarChecker);
            _skipLogic.OnChangeAvailable += OnChangeSkip;
            _skipLogic.Check(true);
            return this;
        }

        public Scenario AddSkipByAchievement(string key, double targetValue,
            TrackRelativity relativity = TrackRelativity.Absolute)
        {
            var trackingData = new AchievementTrackingData
            {
                TrackerKey = key,
                TargetValue = targetValue,
                TrackRelativity = relativity,
                UpdateMethod = ProgressUpdateMethod.IncrementValue,
                AchievementKeyHolder = new KeyHolder(key)
            };

            AddSkipLogic(new TrackerLogic(_trackerProvider.Create(trackingData)));
            return this;
        }
        
        public Scenario AddAchievementTrigger(string key, double targetValue, TrackRelativity relativity = TrackRelativity.Absolute)
        {
            Available = false;

            var trackingData = new AchievementTrackingData
            {
                TrackerKey = key,
                TargetValue = targetValue,
                TrackRelativity = relativity,
                UpdateMethod = ProgressUpdateMethod.IncrementValue,
                AchievementKeyHolder = new KeyHolder(key)
            };

            _availabilityLogic.Add(new TrackerLogic(_trackerProvider.Create(trackingData)));
            _availabilityLogic.Check(true);
            return this;
        }

        public Scenario AddUnitLogicTrigger(UnitLogic unitLogic)
        {
            _availabilityLogic.Add(unitLogic);
            _availabilityLogic.Check(true);
            return this;
        }

        public string GetKey() => _key;

        public void Run()
        {
            _scenarioRunner.OnComplete += Complete;
            _scenarioRunner.Run();
        }

        private void Complete()
        {
            _availabilityLogic.Dispose();
            _scenarioRunner.OnComplete -= Complete;
            IsCompleted = true;
           
            OnComplete?.Invoke();
        }

        public ScenarioData Backup()
        {
            return new ScenarioData()
            {
                CanBeSkipped = true
            };
        }

        public void Restore(ScenarioData data)
        {
            CanBeSkipped = data.CanBeSkipped;
        }

       
    }
}