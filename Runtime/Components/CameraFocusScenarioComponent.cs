using System;
using LittleBitGames.FTUE.Components.Base;
using LittleBitGames.FTUE.HighlightSystem;
using UnityEngine;

namespace LittleBitGames.FTUE.Components
{
    public class CameraFocusScenarioComponent : ScenarioComponent
    {
        private readonly string _key;
        private readonly ContainerHighlighterObjects _containerHighlighterObjects;
        private readonly CameraPresenter _cameraPresenter;
        private readonly float _time;
        private IHighlighter _highlighter;

        public CameraFocusScenarioComponent(string key,
                                            CameraPresenter cameraPresenter,
                                            ContainerHighlighterObjects containerHighlighterObjects,
                                            float time = 0)
        {
            _key = key;
            _cameraPresenter = cameraPresenter;
            _containerHighlighterObjects = containerHighlighterObjects;
            _time = time;
        }
        
        protected override void OnExecute()
        {
            if (_containerHighlighterObjects.Contains(_key) == false) 
                throw new Exception("Focusable object with key: " + _key + " not found");
            _highlighter = _containerHighlighterObjects.Get(_key);
            _cameraPresenter.FocusTo(_highlighter, Complete, _time);
        }

        protected override void OnComplete()
        {
            Debug.LogError(_key + " Complete");
        }

        protected override void OnDispose()
        {
            
        }
    }
}