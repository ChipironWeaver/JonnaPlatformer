using UnityEngine;
using UnityEngine.Serialization;

public class HinoDeath : MonoBehaviour
{
    [SerializeField] private float _yPositionDeath;
    [SerializeField] private int _value;
    void Update()
    {
        if(transform.position.y < _yPositionDeath)
        {
            HinoCounter.Instance.AddHinoCount(_value);
            Destroy(transform.parent.gameObject);
        }
    }
}