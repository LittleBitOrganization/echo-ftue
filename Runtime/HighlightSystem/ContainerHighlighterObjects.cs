using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public class ContainerHighlighterObjects
    {
        private Dictionary<string, IHighlighter> _highlighters;
        private Dictionary<string, Action<IHighlighter>> _dictionaryOnAddAction;

        public ContainerHighlighterObjects()
        {
            _highlighters = new Dictionary<string, IHighlighter>();
            _dictionaryOnAddAction = new Dictionary<string, Action<IHighlighter>>();
        }

        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return _highlighters.ContainsKey(key);
        }

        public IHighlighter Get(string key) => _highlighters[key];

        public void Add(IHighlighter focusable)
        {
            var key = focusable.GetKey();

            if (string.IsNullOrEmpty(key))
                throw new Exception($"{focusable.Object.name}");

            if (_highlighters.ContainsKey(key) == false)
            {
                _highlighters.Add(key, focusable);
                OnAdd(focusable);
            }
            else
            {
                throw new Exception($"{key}  {focusable.Object.name}");
            }
        }

        public void Remove(IHighlighter focusable)
        {
            if (focusable == null) return;
            if (string.IsNullOrEmpty(focusable.GetKey())) return;

            if (_highlighters.ContainsKey(focusable.GetKey()))
                _highlighters.Remove(focusable.GetKey());
        }

        private void OnAdd(IHighlighter focusable)
        {
            if (_dictionaryOnAddAction.ContainsKey(focusable.GetKey()))
            {
                Debug.Log(focusable);
                var action = _dictionaryOnAddAction[focusable.GetKey()];
                if (action == null) return;
                action.Invoke(focusable);
            }
        }
        
        
        public void Subscribe(string key, Action<IHighlighter> onAdd)
        {
            if (_dictionaryOnAddAction.ContainsKey(key) == false)
            {
                _dictionaryOnAddAction.Add(key, onAdd);
            }
            else
            {
                _dictionaryOnAddAction[key] += onAdd;
            }
        }

        public void Unsubscribe(string key, Action<IHighlighter> onAdd)
        {
            if (_dictionaryOnAddAction.ContainsKey(key))
            {
                _dictionaryOnAddAction[key] -= onAdd;
                _dictionaryOnAddAction.Remove(key);

            }
        }
    }
}