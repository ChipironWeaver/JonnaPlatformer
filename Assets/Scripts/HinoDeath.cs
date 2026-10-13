using UnityEngine;

public class HinoDeath : MonoBehaviour
{
    [SerializeField] private float yPositionDeath;
    void Update()
    {
        if(transform.position.y < yPositionDeath)
        {
            HinoCounter.Instance.AddHinoCount(1);
            Destroy(transform.parent.gameObject);
        }
    }
}
