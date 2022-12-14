// using System;
// using LittleBit.Modules.Scenarios.Components.Base;
// using LittleBitGames.QuestsModule.Common;
// using LittleBitGames.QuestsModule.Factories;
// using LittleBitGames.QuestsModule.Trackers.Controllers;
// using LittleBitGames.QuestsModule.Trackers.Metadata;
// using LittleBitGames.QuestsModule.Trackers.ProgressCalculators;
//
// namespace LittleBit.Modules.Scenarios.Components
// {
//     public class TriggerScenarioComponent : ScenarioComponent
//     {
//         private readonly ITrackerProvider _trackerProvider;
//         private readonly string _key;
//         private ITrackerController _trackerController;
//         private readonly double _targetValue;
//
//         public TriggerScenarioComponent(ITrackerProvider trackerProvider, string key, double targetValue)
//         {
//             _targetValue = targetValue;
//             _key = key;
//             _trackerProvider = trackerProvider;
//         }
//
//         protected override void OnExecute()
//         {
//             var trackingData = new AchievementTrackingData
//             {
//                 TrackerKey = $"tracker/{_key}",
//                 TargetValue = _targetValue,
//                 TrackRelativity = TrackRelativity.Absolute,
//                 UpdateMethod = 0,
//                 AchievementKeyHolder = new KeyHolder(_key)
//             };
//
//             _trackerController = _trackerProvider.Create(trackingData);
//             
//             _trackerController.OnGoal += Complete;
//         }
//
//         protected override void OnComplete()
//         {
//         }
//
//         protected override void OnDispose()
//         {
//             _trackerController.OnGoal -= Complete;
//         }
//         
//         
//     }
// }