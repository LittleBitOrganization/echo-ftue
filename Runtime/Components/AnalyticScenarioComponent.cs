using LittleBitGames.FTUE.Components.Base;

namespace LittleBitGames.FTUE.Components
{
    public class AnalyticScenarioComponent : ScenarioComponent
    {
        private readonly IEventsService _eventsService;
        private readonly string _eventKey;

        public AnalyticScenarioComponent(IEventsService eventsService, string eventKey)
        {
            _eventsService = eventsService;
            _eventKey = eventKey;
        }
        protected override void OnExecute()
        {
            _eventsService.DesignEvent(new DataEventDesign(_eventKey));
            Complete();
        }

        protected override void OnComplete()
        {
            
        }

        protected override void OnDispose()
        {
            
        }
    }
}