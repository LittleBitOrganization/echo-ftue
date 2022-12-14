using LittleBit.Modules.SequenceLogicModule;
using LittleBitGames.QuestsModule.Trackers.Controllers;

namespace LittleBitGames.FTUE.Components
{
    public class TrackerLogic : UnitLogic
    {
        private readonly ITrackerController _trackerController;

        public TrackerLogic(ITrackerController trackerController)
        {
            _trackerController = trackerController;

            IsAvailable = false;
            
            _trackerController.OnGoal += OnGoal;
            _trackerController.StartTracking();
        }

        private void OnGoal() =>
            IsAvailable = true;

        public override void Dispose()
        {
            base.Dispose();
            _trackerController.OnGoal -= OnGoal;
        }
    }
}