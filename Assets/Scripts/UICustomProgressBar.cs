using UnityEngine;
using UnityEngine.UI;

public class UICustomProgressBar : MonoBehaviour
{
    [SerializeField] private Vector2 _progressRange;
    [SerializeField] private Image _targetImage;

    public void SetProgress(float progress)
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
        _targetImage.rectTransform.offsetMax = 
            new Vector2(value, _targetImage.rectTransform.offsetMax.y );
    }
}
