using UnityEngine;

namespace LittleBitGames.FTUE.View
{
    public class FocusUiObjectDisabled : FocusObject
    {
        protected override void OnDestroyed()
        {
            
        }

        protected override void OnDisabled()
        {
            Click();
            Debug.LogError("ClickFocusUiObjectDisabled");
            _container.Remove(this);
        }

        private void OnDisable()
        {
            
        }
    }
}