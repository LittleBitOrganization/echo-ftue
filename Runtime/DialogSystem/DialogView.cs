using System;
using DG.Tweening;
using LittleBit.Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleBitGames.FTUE.DialogSystem
{
    public class DialogView : Layout, IDialogView
    {
        public event Action OnClick;

        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Button button;
        [SerializeField] private GameObject downIcon;

        private Tween _textTween;
        private string _phrase;

        private void Start() =>
            button.onClick.AddListener(OnButtonClick);

        private void OnButtonClick()
        {
            if (_textTween.active)
            {
                dialogText.text = _phrase;
                _textTween?.Kill(true);
                downIcon.SetActive(true);
                return;
            }
            
            OnClick?.Invoke();
        }

        public void NextPhrase(string phrase)
        {
            _phrase = phrase;
            _textTween?.Kill(true);
            dialogText.text = "";
            downIcon.SetActive(false);
            _textTween = dialogText.DOText(phrase, 0.03f * phrase.Length).SetEase(Ease.Linear).OnComplete(() =>
            {
                downIcon.SetActive(true);
            });
        }

        public void Dispose()
        {
            _textTween?.Kill(true);
            Destroy(gameObject);
        }
    }
}