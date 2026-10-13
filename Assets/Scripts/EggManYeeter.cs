using NaughtyAttributes;
using UnityEngine;

public class EggManYeeter : MonoBehaviour
{
    [SerializeField,Tag] private string _collisionTag;
    [SerializeField] private float _yeetStrengh;
    [SerializeField] private Collider2D _collider;
    private bool isActive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            if (other.CompareTag(_collisionTag) && other.attachedRigidbody)
            {
                Vector2 moveDir = other.transform.position - transform.position;
                moveDir.Normalize();
                other.attachedRigidbody.AddForce(moveDir * _yeetStrengh, ForceMode2D.Impulse);
            }
        }
    }
    
    public void SetActive(bool active)
    {
        isActive = active;
        _collider.enabled = !active;
    }
}
