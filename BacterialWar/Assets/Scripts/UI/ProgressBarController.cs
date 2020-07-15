using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public EventHandler<float> TrackProgress;

    [SerializeField]
    private Vector3 _offsetProgressBar;

    [SerializeField]
    private Vector2 _sizeRelation;

    [SerializeField]
    private RectTransform _progressCanvasTransform;

    [SerializeField]
    private Image _frameCanvasImage;

    [SerializeField]
    private Image _progressCanvasImage;

    private bool _isCalculatedSizes;
    private float _progressBarWidth;
    private Vector3 _offsetProgressBarCalculted;
    private Vector2 _progressBarSizeCalculated;

    private void Awake()
    {
        TrackProgress += OnTrackProgress;
    }    

    private void OnDestroy()
    {
        TrackProgress -= OnTrackProgress;    
    }

    private void CalculateSizes(GameObject hexObject)
    {        
        // Calculate position offset
        var sizeObject = hexObject.GetComponent<Renderer>().bounds.size;
        _offsetProgressBarCalculted = Vector3.Scale(sizeObject, _offsetProgressBar);

        // Calculate progress bar size
        var hexRectangle = VectorHelper.GUIRectWithObject(hexObject);
        _progressBarSizeCalculated = Vector2.Scale(hexRectangle.size, _sizeRelation);

        _progressBarWidth = _progressBarSizeCalculated.x;

        _isCalculatedSizes = true;
    }

    public void SetPosition(GameObject hexObject)
    {
        if (!_isCalculatedSizes)
        {
            CalculateSizes(hexObject);
        }

        var rectTransform = gameObject.GetComponent<RectTransform>();        

        Vector2 positionScreenPoint = RectTransformUtility.WorldToScreenPoint(
            Camera.main, hexObject.transform.position + _offsetProgressBarCalculted);
        
        rectTransform.anchoredPosition = positionScreenPoint -
            UICollection.Instance.ProgressBarsCanvas.GetComponent<RectTransform>().sizeDelta / 2f;

        rectTransform.sizeDelta = _progressBarSizeCalculated;
    }

    public void SetColor(Color color)
    {
        _frameCanvasImage.color = new Color(color.r, color.g, color.b, _frameCanvasImage.color.a);
        _progressCanvasImage.color = new Color(color.r, color.g, color.b, _progressCanvasImage.color.a);
    }

    private void OnTrackProgress(object sender, float progress)
    {
        if (progress < 0)
        {
            progress = 0;
        }
        else if (progress > 1)
        {
            progress = 1;
        }

        _progressCanvasTransform.sizeDelta = new Vector2(_progressBarWidth * progress, _progressCanvasTransform.sizeDelta.y);
    }
}
