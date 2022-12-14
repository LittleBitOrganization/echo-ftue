using System;
using LittleBitGames.FTUE.HighlightSystem;
using UnityEngine;
using Zenject;

namespace LittleBitGames.FTUE.View
{
    public abstract class FocusObject : MonoBehaviour, IHighlighter, IRaycastable
    {
        [SerializeField] private string _key;

        protected ContainerHighlighterObjects _container;
        private DiContainer _diContainer;
        protected event Action OnClick;

        [Inject]
        public void Construct(DiContainer ftueContainer)
        {
            _diContainer = ftueContainer;
            AddSelfToContainer();
        }

        public void RemoveSelfFromContainer()
        {
            _container = _diContainer.Resolve<ContainerHighlighterObjects>();
            _container.Remove(this);
            SetKey(string.Empty);
        }

        public void AddSelfToContainer()
        {
            _container = _diContainer.Resolve<ContainerHighlighterObjects>();
            _container.Remove(this);
            
            if (string.IsNullOrEmpty(_key)) return;

            if (_container.Contains(_key) == false)
            {
                _container.Add(this);
            }
        }

        public void SetKey(string key) =>
            _key = key;

        public string GetKey() => _key;

        public GameObject Object => gameObject;

        public void AddClickListener(Action onClick)
        {
            OnClick += onClick;
        }

        public void RemoveClickListener(Action onClick)
        {
            OnClick -= onClick;
        }


        private void OnDestroy()
        {
            _container?.Remove(this);
            OnDestroyed();
        }

        private void OnDisable()
        {
            OnDisabled();
        }

        protected abstract void OnDestroyed();
        protected abstract void OnDisabled();

        protected void Click()
        {
            Debug.Log(_key + "  clicked");
            OnClick?.Invoke();
        }
    }

    public class FocusGameObject : FocusObject
    {
        private IRaycastService _raycastService;
        [SerializeField] private GameObject _meshHightlighter;

        [Inject]
        private void OnInjectRaycastService(IRaycastService raycastService)
        {
            SetActiveHighlighter(false);
            _raycastService = raycastService;
            _raycastService.AddOnRaycastHitListener(OnRaycastHit);
        }

        protected override void OnDestroyed()
        {
            _raycastService.RemoveOnRaycastHitListener(OnRaycastHit);
        }

        protected override void OnDisabled() { }

        private void OnRaycastHit(GameObject obj)
        {
            if (obj == Object)
            {
                Click();
            }
        }

        public void SetActiveHighlighter(bool isActive)
        {
            if(_meshHightlighter != null)
                _meshHightlighter.gameObject.SetActive(isActive);
        }
    }
}