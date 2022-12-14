using UnityEngine;
using UnityEngine.UI;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public class HighlightPresenter
    {
        private readonly HighlightView _view;

        public HighlightPresenter(HighlightView view)
        {
            _view = view;
        }

        public void FocusTo(IFocusable focusable)
        {
            GameObject gameObject = focusable.Object;

            if (gameObject.TryGetComponent(out Image image))
            {
                FocusToUi(image);
            }
            else
            {
                FocusToWorldObject(focusable);
            }
        }

        private void FocusToUi(Image image)
        {
            _view.Bind(image);
        }

        private void FocusToWorldObject(IFocusable focusable)
        {
            _view.BindWorldObject(focusable);
        }
        
        public void Unfocus()
        {
            _view.Break();
        }
        
    }
}