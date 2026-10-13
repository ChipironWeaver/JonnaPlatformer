using UnityEngine;

public class HinoCounter : MonoBehaviourSingleton<HinoCounter>
{
    [SerializeField] private UICustomProgressBar progressBar;
    private int _hinoCount;
    
    public void AddHinoCount(int count)
    {
        _hinoCount += count;
        Render();
    }

    public void Render()
    {
        progressBar.SetProgress(_hinoCount/50f);
    }
}
