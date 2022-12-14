using LittleBit.Modules.Description;
using LittleBitGames.QuestsModule.Trackers.Metadata;
using LittleBitGames.QuestsModule.Trackers.ProgressCalculators;

namespace LittleBitGames.FTUE.Components
{
    public class AchievementTrackingData : IAchievementTrackingData
    {
        public string TrackerKey { get; set; }

        public double TargetValue { get; set; }

        public TrackRelativity TrackRelativity { get; set; }

        public ProgressUpdateMethod UpdateMethod { get; set; }

        public IKeyHolder AchievementKeyHolder { get; set; }
        
    }
}