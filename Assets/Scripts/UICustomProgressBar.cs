using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UICustomProgressBar : MonoBehaviour
{
    [SerializeField] private Vector2 _progressRange;
    [SerializeField] private Image _targetImage;
    [SerializeField] private float _tweenDuration;


    
    public void SetProgress(float progress, bool tween = true)
    {
        progress = Mathf.Clamp01(progress);
        float value = 0;
        if (_progressRange.x > _progressRange.y)
        {
            value = ChipironUtility.EvaluateVector2(new Vector2(_progressRange.y,_progressRange.x ), progress);
        }
        else
        {
            value = ChipironUtility.EvaluateVector2(_progressRange, progress);
        }
        
        if (tween)
        {
            
            //_targetImage.rectTransform.DOKill();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_targetImage.rectTransform.DOSizeDelta(
                new Vector2(value, _targetImage.rectTransform.offsetMax.y), _tweenDuration,true));
            sequence.SetEase(Ease.OutCirc);
        }
        else
        {
            _targetImage.rectTransform.offsetMax = 
                new Vector2(value, _targetImage.rectTransform.offsetMax.y );
        }
    }
}
