using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EggManManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _eggMans;
    [SerializeField] private float _requiredDistance;
    [SerializeField] private Camera _camera;

    private Transform followerTarget;

    public void Update()
    {
        if (followerTarget)
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
            followerTarget.position = mousePosition;
        }
    }
    
    public Transform GetEggMan(bool bypassDistance = false)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
        
        float currentClosestEggManDistance = float.MaxValue;
        Transform closestEggMan = null;
        foreach (Transform eggMan in _eggMans)
        {
            float distance = Vector2.Distance(eggMan.position, mousePosition);
            if (distance < currentClosestEggManDistance)
            {
                currentClosestEggManDistance = distance;
                closestEggMan = eggMan;
            }
        }

        if (!closestEggMan) return null;

        if (currentClosestEggManDistance > _requiredDistance && !bypassDistance) return null;
        print(closestEggMan.gameObject.name);
        return closestEggMan;
    }

    public void ClickPerformed()
    {
        Transform eggMan = GetEggMan();
        if(eggMan) followerTarget = eggMan;
    }
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            followerTarget = null;
        }
        else if (context.performed)
        {
            ClickPerformed();
        }
    }
}
