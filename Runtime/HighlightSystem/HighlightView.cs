using Coffee.UISoftMask;
using DG.Tweening;
using LittleBitGames.FTUE.View;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace LittleBitGames.FTUE.HighlightSystem
{
    public class HighlightView : MonoBehaviour
    {
        [field: SerializeField] private RectTransform _finger;
        [field: SerializeField] private Image _holde;
        [field: SerializeField] private RawImage _holeRaw;
        [field: SerializeField] private Image _background;

        [field: SerializeField] private Camera _camera;

        private BindImage _bindImage;
        private BindGameObject _bindGameObject;
        
        private RenderTexture _renderTexture;


        private void Awake()
        {
            InitRenderTexture();
        }

        private void InitRenderTexture()
        {
            if (_renderTexture == null)
            {
                _renderTexture = new RenderTexture(
                    Screen.width, 
                    Screen.height, 
                    GraphicsFormat.R8G8B8A8_UNorm,
                    GraphicsFormat.D24_UNorm_S8_UInt);
                _renderTexture.antiAliasing = 1;

                _camera.targetTexture = _renderTexture;
                _holeRaw.texture = _renderTexture;
            }
        }

        public void Bind(Image targetImage)
        {
            Dispose();
    
            _bindImage = new BindImage(targetImage, _holde, _finger, _background);
            SetActive(true);
        }

        
        public void BindWorldObject(IFocusable worldObject)
        {
            InitRenderTexture();

            if (_renderTexture.height != Screen.height || _renderTexture.width != Screen.width)
            {
                _renderTexture.width = Screen.width;
                _renderTexture.height = Screen.height;
            }

            Dispose();
            _bindGameObject = new BindGameObject(worldObject, _holeRaw, _camera, _finger, _background);
            
            SetActive(true);
        }
        
        public void Break()
        {
            SetActive(false);
            Dispose();
        }

        private void Dispose()
        {
            _bindImage?.Dispose();
            _bindGameObject?.Dispose();
            _bindImage = null;
            _bindGameObject = null;
        }

        private void LateUpdate()
        {
            _bindImage?.Update();
            _bindGameObject?.Update();
        }
        
        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private class BindGameObject
        {
            private const float DistanceToScale = 2;
            
            private readonly RawImage _holeRaw;
            private readonly FocusGameObject _focusable;
            private readonly Camera _camera;
            private readonly RectTransform _finger;
            private Tweener _tweener;
            private SoftMask _softMask;

            Vector3 TargetPosition => _camera.WorldToScreenPoint(_focusable.Object.transform.position);
            
            internal BindGameObject(IFocusable focusable, RawImage holeRaw, Camera camera, RectTransform finger, Image background)
            {
              
                Color color = background.color;
                color.a = 0f;
                background.color = color;
                
                _focusable = (FocusGameObject)focusable;
                _focusable.SetActiveHighlighter(true);
                _camera = camera;
                _finger = finger;
                _holeRaw = holeRaw;
                _softMask = _holeRaw.GetComponent<SoftMask>();
                
                _finger.localScale = Vector3.one;
                _tweener = _finger.DOScale(0.8f, 0.5f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo);
                
                _finger.gameObject.SetActive(true);
                _holeRaw.gameObject.SetActive(false);
                _softMaskTweener = DOVirtual.Float(1, 0.8f, 1, value =>
                    {
                        _softMask.alpha = value;
                    }).SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo);

            }

            private Sprite _sprite;
            private readonly Tweener _softMaskTweener;

            internal void Update()
            {
                if (_holeRaw.gameObject.activeSelf == false)
                {
                    _holeRaw.gameObject.SetActive(true);
                }
                _finger.position = Vector3.Lerp(_finger.position, TargetPosition, Time.deltaTime * 5);
                _holeRaw.gameObject.SetActive(true);
            }
            
            
            internal void Dispose()
            {
                _tweener?.Kill();
                _softMaskTweener?.Kill();
                _holeRaw.gameObject.SetActive(false);
                _finger.gameObject.SetActive(false);
                _focusable.SetActiveHighlighter(false);
            }
        }
        private class BindImage
        {
            private readonly RectTransform _finger;
            private readonly RectTransform _target;
            private readonly Canvas _targetCanvas;
            private readonly Canvas _holeCanvas;
            private readonly RectTransform _hole;

            private float TargetCanvasScale => _targetCanvas.transform.localScale.x;
            private float HoleCanvasScale => _holeCanvas.transform.localScale.x;
            private float CanvasMultiplyScale =>  TargetCanvasScale / HoleCanvasScale;
            
            private Tweener _tweener;
            private readonly Image _targetGO;

            internal BindImage(Image target, Image hole, RectTransform finger, Image background)
            {
                Color color = background.color;
                color.a = 0;
                background.color = color;
                // Color color = background.color;
                // color.a = 163.0f / 255;
                // background.color = color;
                
                _target = target.GetComponent<RectTransform>();
                
                _targetCanvas = target.canvas;
                _holeCanvas = hole.canvas;
                
                _targetGO = target;
                if (_target == null)
                    Debug.LogError("err");
                _hole = hole.GetComponent<RectTransform>();
                _finger = finger;
                
                var anchoredPosition = _target.anchoredPosition;
                hole.sprite = target.sprite;
                hole.type = target.type;
                hole.pixelsPerUnitMultiplier = target.pixelsPerUnitMultiplier;

                _hole.gameObject.SetActive(true);
                _finger.gameObject.SetActive(true);
                
                _finger.localScale = Vector3.one;
                _tweener = _finger.DOScale(0.8f, 1f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo);

                _hole.pivot = _target.pivot;
                _hole.anchoredPosition = anchoredPosition;
                
                _hole.sizeDelta = _target.sizeDelta * CanvasMultiplyScale;
            }
            
            internal void Update()
            {
                if (_target == null) return;

                _hole.position = _target.position;
                _hole.sizeDelta = new Vector2(_target.rect.width-2, _target.rect.height-2);

                _hole.transform.localScale = _target.localScale * CanvasMultiplyScale;
                _finger.position = Vector3.Lerp(_finger.position, _target.position, Time.deltaTime * 5);
            }

            internal void Dispose()
            {
                _tweener?.Kill(false);
                _hole.gameObject.SetActive(false);
                _finger.gameObject.SetActive(false);
                
            }
        }
    }
}