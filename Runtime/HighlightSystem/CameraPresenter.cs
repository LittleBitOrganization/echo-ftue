using System;
using LittleBit.Modules.CameraModule;
using LittleBit.Modules.CoreModule;
using UnityEngine;
using Zenject;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public class CameraPresenter : ITickable
    {
        private readonly CameraService _cameraService;
        private readonly ICameraDisabler _cameraDisabler;
        private float _timer;
        private IFocusable _focusable;
        private bool _processing;
        private Action _onFocused;

        public CameraPresenter(CameraService cameraService,
                               ICameraDisabler cameraDisabler,
                               ICoroutineRunner coroutineRunner)
        {
            _cameraService = cameraService;
            _cameraDisabler = cameraDisabler;
        }

        public void DisableCamera() => 
            _cameraDisabler.DisableCamera();

        public void EnableCamera() => 
            _cameraDisabler.EnableCamera();

        public void FocusTo(IFocusable focusable, Action onFocused)
        {
            if (focusable.Object.TryGetComponent(out RectTransform _))
            {
                onFocused?.Invoke();
            }
            else
            {
                _cameraService.MoveToPosition(focusable.Object.transform.position);
                onFocused?.Invoke();
            }
        }       
        
        public void FocusTo(IFocusable focusable, Action onFocused, float followTime)
        {
            _processing = false;
            _onFocused = null;
            
            _focusable = focusable;
            _timer = followTime;

            if (_focusable.Object.TryGetComponent(out RectTransform _)) throw new Exception();

            _cameraService.MoveToPosition(_focusable.Object.transform.position);
            _cameraService.SetZoom(0.1f);

            
            if (followTime <= 0)
            {
                onFocused?.Invoke();
            }
            else
            {
                _onFocused = onFocused;
                _processing = true;
                DisableCamera();
            }
        }

        public void Tick()
        {
            if (_processing == false) return;
            
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _cameraService.MoveToPosition(_focusable.Object.transform.position);
            }
            else
            {
                EnableCamera();
                _processing = false;
                _onFocused.Invoke();
            }
        }
    }
}