// Simple Zoom
// Version: 1.0.1
// Author: Daniel Lochner

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DanielLochner.Assets.SimpleZoom
{
    [AddComponentMenu("UI/Simple Zoom")]
    [RequireComponent(typeof(ScrollRect))]
    public class SimpleZoom : MonoBehaviour, IPointerClickHandler
    {
        #region Fields
        // Basic.
        public float currentZoom = 1f;
        public float minZoom = 1f;
        public float maxZoom = 3f;
        public ZoomTarget zoomTarget = ZoomTarget.Pointer;
        public Vector2 customPosition = new Vector2(0.5f, 0.5f);
        public ZoomType zoomType = ZoomType.Clamped;
        public float elasticLimit = 2f;
        public float elasticDamping = 0.1f;
        public ZoomMode zoomMode = ZoomMode.Scale;
        // Control.
        public Button zoomIn = null;
        public Vector2 zoomInPosition = new Vector2(0.5f, 0.5f);
        public float zoomInIncrement = 0.5f;
        public float zoomInSmoothing = 0.1f;
        public Button zoomOut = null;
        public Vector2 zoomOutPosition = new Vector2(0.5f, 0.5f);
        public float zoomOutIncrement = 0.5f;
        public float zoomOutSmoothing = 0.1f;
        public Slider zoomSlider = null;
        public GameObject zoomView = null;
        // Other.
        public bool zoomMovement = true;
        public bool doubleTap = true;
        public float doubleTapTargetTime = 0.25f;
        public float scrollWheelIncrement = 0.5f;
        public float scrollWheelSmoothing = 0.1f;

        private float initialZoom = 1f, currentDistance = 1f, initialDistance = 1f, doubleTapTime, targetSmoothing, targetZoom = 1f, zoomViewScale;
        private bool isInitialTouch = true;
        private int taps = 0;
        private Vector2 mouseLocalPosition, originalSize, originalScale;
        private RectTransform zoomViewViewport;
        private Canvas canvas;
        #endregion

        #region Properties
        public float CurrentZoom
        {
            get { return currentZoom; }
        }
        public float TargetZoom
        {
            get { return targetZoom; }
        }
        public float ZoomProgress
        {
            get
            {
                return (currentZoom - minZoom) / (maxZoom - minZoom);
            }
        }
        public Vector4 ZoomMargin
        {
            get
            {
                Vector2 viewportPivot = Viewport.pivot;
                Vector2 contentPivot = Content.pivot;

                Vector2 viewportSize = Viewport.rect.size * Viewport.localScale;
                Vector2 contentSize = Content.rect.size * Content.localScale;

                float left = (Viewport.position.x - viewportSize.x * viewportPivot.x) - (Content.position.x - contentSize.x * contentPivot.x);
                float bottom = (Viewport.position.y - viewportSize.y * viewportPivot.y) - (Content.position.y - contentSize.y * contentPivot.y);

                float right = (Content.position.x + contentSize.x * (1 - contentPivot.x)) - (Viewport.position.x + viewportSize.x * (1 - viewportPivot.x));
                float top = (Content.position.y + contentSize.y * (1 - contentPivot.y)) - (Viewport.position.y + viewportSize.y * (1 - viewportPivot.y));

                return new Vector4(left, right, bottom, top);
            }
        }
        public ScrollRect ScrollRect
        {
            get { return GetComponent<ScrollRect>(); }
        }
        public RectTransform Content
        {
            get { return ScrollRect.content; }
        }
        public RectTransform Viewport
        {
            get { return ScrollRect.viewport; }
        }

        public static bool UsingUnityRemote
        {
            get
            {
                #if (UNITY_EDITOR)
                    return UnityEditor.EditorApplication.isRemoteConnected;
                #else
                    return false;
                #endif
            }
        }
        #endregion

        #region Enumerators
        public enum ZoomTarget
        {
            Pointer,
            Custom
        }
        public enum ZoomType
        {
            Clamped,
            Elastic
        }
        public enum ZoomMode
        {
            Scale,
            Size
        }
        #endregion

        #region Methods
        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
        private void Start()
        {
            if (Validate())
            {
                Setup();
            }
            else
            {
                throw new Exception("Invalid configuration.");
            }
        }
        private void Update()
        {
            OnPivotZoomUpdate();

            OnZoomSlider();
            OnZoomView();
            OnDoubleTap();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (taps == 0) doubleTapTime = doubleTapTargetTime;
            if (Input.touchCount <= 1) taps++;
        }

        private void Setup()
        {
            originalSize = Content.rect.size;
            originalScale = Content.localScale;

            // Canvas & Camera
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                canvas.planeDistance = (canvas.GetComponent<RectTransform>().rect.height / 2f) / Mathf.Tan((canvas.worldCamera.fieldOfView / 2f) * Mathf.Deg2Rad);
                if (canvas.worldCamera.farClipPlane < canvas.planeDistance)
                {
                    canvas.worldCamera.farClipPlane = Mathf.Ceil(canvas.planeDistance);
                }
            }

            // Content.
            Content.anchorMin = new Vector2(0.5f, 0.5f);
            Content.anchorMax = new Vector2(0.5f, 0.5f);

            // Zoom Buttons.
            if (zoomIn != null)
                zoomIn.onClick.AddListener(delegate { ZoomIn(zoomInPosition, zoomInIncrement, zoomInSmoothing); });
            if (zoomOut != null)
                zoomOut.onClick.AddListener(delegate { ZoomOut(zoomOutPosition, zoomOutIncrement, zoomOutSmoothing); });
        }
        private bool Validate()
        {
            bool valid = true;

            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.LogError("<b>[SimpleZoom]</b> The canvas render mode cannot be set to \"World Space\".", gameObject);
                valid = false;
            }

            if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
            {
                Debug.LogError("<b>[SimpleZoom]</b> The world camera must be assigned if the render mode has been set to \"Screen Space - Camera\".", gameObject);
                valid = false;
            }

            if (zoomView)
            {
                Vector2 viewportDimensions = Viewport.rect.size;
                zoomViewViewport = zoomView.transform.GetChild(0).GetComponent<RectTransform>();
                Vector2 zoomViewViewportDimensions = zoomViewViewport.rect.size;

                Vector2 contentDimensions = Content.rect.size;
                Vector2 zoomViewContentDimensions = zoomView.GetComponent<RectTransform>().rect.size;

                if ((viewportDimensions / zoomViewViewportDimensions) != (contentDimensions / zoomViewContentDimensions))
                {
                    Debug.LogError("<b>[SimpleZoom]</b> The Zoom View's dimensions must be SIMILAR (i.e. the ratios of the lengths of their corresponding sides are equal) to that of the Simple Zoom's dimensions.", gameObject);
                    valid = false;
                }
            }

            return valid;
        }

        private void OnPivotZoomUpdate()
        {
            #region Set
            if (SystemInfo.deviceType == DeviceType.Handheld || UsingUnityRemote){
                OnHandheldUpdate();
            }else if (SystemInfo.deviceType == DeviceType.Desktop){
                OnDesktopUpdate();
            }
            #endregion

            #region Update
            if (Math.Round(currentZoom, 4) != targetZoom)
            {
                if (targetSmoothing != 0)
                {
                    currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.unscaledDeltaTime * (1f / targetSmoothing));
                }
                else
                {
                    currentZoom = targetZoom;
                }
            }
            else
            {
                currentZoom = targetZoom;
            }

            if (zoomMode == ZoomMode.Scale)
            {
                Content.localScale = originalScale * currentZoom;
            }
            else if (zoomMode == ZoomMode.Size)
            {
                Content.sizeDelta = originalSize * currentZoom;
            }
            #endregion
        }
        private void OnHandheldUpdate()
        {
            if (Input.touchCount >= 2)
            {
                #region Set Pivot
                Vector2 pos1 = Input.touches[0].position;
                Vector2 pos2 = Input.touches[1].position;

                Vector2 inputPosition = Vector2.zero;
                switch (zoomTarget)
                {
                    case ZoomTarget.Pointer:
                        inputPosition = (pos1 + pos2) / 2;
                        break;
                    case ZoomTarget.Custom:
                        inputPosition = customPosition * new Vector2(Screen.width, Screen.height);
                        break;
                }
                currentDistance = Vector2.Distance(pos1, pos2);

                if (isInitialTouch)
                {
                    Vector2 pivot = Vector2.zero;
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, inputPosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out mouseLocalPosition))
                    {
                        float x = Content.pivot.x + (mouseLocalPosition.x / Content.rect.width);
                        float y = Content.pivot.y + (mouseLocalPosition.y / Content.rect.height);
                        pivot = new Vector2(x, y);
                    }
                    SetPivot(pivot);

                    initialDistance = currentDistance;
                    isInitialTouch = false;
                }
                #endregion

                #region Set Zoom
                if (zoomType == ZoomType.Clamped){
                    SetZoom(Mathf.Clamp(initialZoom * (currentDistance / initialDistance), minZoom, maxZoom));
                }else if (zoomType == ZoomType.Elastic){
                    SetZoom(ElasticClamp(initialZoom * (currentDistance / initialDistance), minZoom, maxZoom, elasticLimit, elasticDamping));
                }
                ScrollRect.horizontal = ScrollRect.vertical = zoomMovement;
                #endregion
            }
            else
            {
                #region Reset Zoom
                initialZoom = currentZoom;

                if (zoomType == ZoomType.Elastic)
                {
                    if (currentZoom > maxZoom){
                        SetZoom(maxZoom, 0.1f);
                    }else if (currentZoom < minZoom){
                        SetZoom(minZoom, 0.1f);
                    }
                }

                ScrollRect.horizontal = ScrollRect.vertical = true;
                isInitialTouch = true;
                #endregion
            }
        }
        private void OnDesktopUpdate()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel != 0)
            {
                #region Set Pivot
                Vector2 inputPosition = Vector2.zero;
                switch (zoomTarget)
                {
                    case ZoomTarget.Pointer:
                        inputPosition = Input.mousePosition;
                        break;
                    case ZoomTarget.Custom:
                        inputPosition = customPosition * new Vector2(Screen.width, Screen.height);
                        break;
                }
                Vector2 pivot = Vector2.zero;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, inputPosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out mouseLocalPosition))
                {
                    float x = Content.pivot.x + (mouseLocalPosition.x / Content.rect.width);
                    float y = Content.pivot.y + (mouseLocalPosition.y / Content.rect.height);
                    pivot = new Vector2(x, y);
                }
                SetPivot(pivot);
                #endregion

                #region Set Zoom
                SetZoom(Mathf.Clamp(currentZoom + ((scrollWheel * 10) * scrollWheelIncrement), minZoom, maxZoom), scrollWheelSmoothing);
                #endregion
            }
        }
        private void OnDoubleTap()
        {
            if (doubleTap)
            {
                doubleTapTime -= Time.unscaledDeltaTime;
                if (doubleTapTime <= 0f)
                {
                    taps = 0; // Reset.
                }
                else if (taps >= 2)
                {
                    if (Math.Round(currentZoom, 2) > minZoom)
                    {
                        SetPivot(new Vector2(ZoomMargin.x / (ZoomMargin.x + ZoomMargin.y), ZoomMargin.z / (ZoomMargin.z + ZoomMargin.w)));
                        SetZoom(minZoom, 0.1f);
                    }
                    else
                    {
                        Vector2 pivot = Vector2.zero;
                        Vector2 outputPosition = Vector2.zero;
                        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, Input.mousePosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out outputPosition))
                        {
                            float x = Content.pivot.x + (outputPosition.x / Content.rect.width);
                            float y = Content.pivot.y + (outputPosition.y / Content.rect.height);
                            pivot = new Vector2(x, y);
                        }
                        SetPivot(pivot);
                        SetZoom(maxZoom, 0.1f);
                    }

                    taps = 0; // Reset.
                }
            }
        }
        private void OnZoomSlider()
        {
            if (zoomSlider != null)
            {
                zoomSlider.value = ZoomProgress;
            }
        }
        private void OnZoomView()
        {
            if (zoomView != null)
            {
                zoomViewScale = zoomView.GetComponent<RectTransform>().rect.width / (Content.rect.width * Content.localScale.x);

                zoomViewViewport.offsetMin = zoomViewScale * new Vector2(ZoomMargin.x, ZoomMargin.z);
                zoomViewViewport.offsetMax = -zoomViewScale * new Vector2(ZoomMargin.y, ZoomMargin.w);
            }
        }

        private float ElasticClamp(float value, float minValue, float maxValue, float limit, float damping)
        {
            if (value > maxValue)
            {
                float y = limit * Mathf.Sin(damping * (value - maxValue)) + maxValue;

                if (value > (maxValue + ((2 * Mathf.PI / damping) / 4f)))
                {
                    return maxValue + limit;
                }
                else
                {
                    return y;
                }
            }
            else if (value < minValue)
            {
                float y = limit * Mathf.Sin(damping * (value - minValue)) + minValue;

                if (value < (minValue - ((2 * Mathf.PI / damping) / 4f)))
                {
                    return minValue - limit;
                }
                else
                {
                    return y;
                }
            }
            else
            {
                return value;
            }
        }

        private void SetZoom(float targetZoom, float targetSmoothing = 0)
        {
            this.targetSmoothing = targetSmoothing;
            this.targetZoom = targetZoom;
        }
        private void SetPivot(Vector2 pivot)
        {
            Vector3 displacement = Content.pivot - pivot;
            displacement.Scale(Content.rect.size);
            displacement.Scale(Content.localScale);

            Content.pivot = pivot;
            Content.anchoredPosition -= (Vector2)displacement;
        }

        public void ZoomIn(Vector2 pivot, float increment, float smoothing)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, pivot * new Vector2(Screen.width, Screen.height), canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out mouseLocalPosition))
            {
                float x = Content.pivot.x + (mouseLocalPosition.x / Content.rect.width);
                float y = Content.pivot.y + (mouseLocalPosition.y / Content.rect.height);
                pivot = new Vector2(x, y);
            }
            SetPivot(pivot);
            SetZoom(Mathf.Clamp(currentZoom + increment, minZoom, maxZoom), smoothing);
        }
        public void ZoomOut(Vector2 pivot, float increment, float smoothing)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Content, pivot * new Vector2(Screen.width, Screen.height), canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out mouseLocalPosition))
            {
                float x = Content.pivot.x + (mouseLocalPosition.x / Content.rect.width);
                float y = Content.pivot.y + (mouseLocalPosition.y / Content.rect.height);
                pivot = new Vector2(x, y);
            }
            SetPivot(pivot);
            SetZoom(Mathf.Clamp(currentZoom - increment, minZoom, maxZoom), smoothing);
        }
        #endregion
    }
}