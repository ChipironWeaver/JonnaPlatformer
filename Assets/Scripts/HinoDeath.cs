using UnityEngine;

public class HinoDeath : MonoBehaviour
{
    [SerializeField] private float yPositionDeath;
    void Update()
    {
        if(transform.position.y < yPositionDeath) Destroy(transform.parent.gameObject);
    }
}
