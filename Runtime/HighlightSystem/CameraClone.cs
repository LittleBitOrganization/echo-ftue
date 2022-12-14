using UnityEngine;
using Zenject;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public class CameraClone : MonoBehaviour
    {
        private Camera _cameraTarget;
        private Camera _cameraSelf;
        
        [Inject]
        private void Init(Camera cameraTarget)
        {
            _cameraSelf = GetComponent<Camera>();
            _cameraTarget = cameraTarget;
        }
        private void LateUpdate()
        {
            if(_cameraTarget == null) return;
            _cameraSelf.transform.position = _cameraTarget.transform.position;
            _cameraSelf.transform.rotation = _cameraTarget.transform.rotation;
            _cameraSelf.orthographicSize = _cameraTarget.orthographicSize;
        }
    }
}