using UnityEngine;
using UnityEngine.Serialization;

public class HinoCounter : MonoBehaviourSingleton<HinoCounter>
{
    [SerializeField] private UICustomProgressBar _progressBar;
    [SerializeField] private int _pointStep;
    [SerializeField] private UICustomLetterManager _pointRenderer;
    private int _hinoCount;
    private int _currentPoints;
    
    
    public void AddHinoCount(int count)
    {
        _hinoCount += count;
        while ((_hinoCount - _pointStep * _currentPoints) >= _pointStep)
        {
            _currentPoints++;
            _pointRenderer.RenderString(_currentPoints.ToString());
        }
        Render();
    }

    public void Render()
    {
        _progressBar.SetProgress((_hinoCount - _pointStep * _currentPoints)/(float)_pointStep);
    }
}
 