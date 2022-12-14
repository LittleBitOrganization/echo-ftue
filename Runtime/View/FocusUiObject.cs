using System;

namespace LittleBitGames.FTUE.View
{
    public class FocusUiObject : FocusObject
    {
        private void Start()
        {
            Button.AddOnClickListener(Click);
        }
        
        private IClickableLayout Button {
            get
            {
                var buttonLayout = GetComponent<IClickableLayout>();
                if (buttonLayout == null)
                    throw new Exception("FocusUiObject with key " + GetKey() + " dont contain ButtonLayout");
                return buttonLayout;
            }
        }

        protected override void OnDestroyed()
        {
            
        }

        protected override void OnDisabled()
        {
            
        }
    }
}