using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EggManManager : MonoBehaviour
{
    [SerializeField] private List<EggManYeeter> _eggMans;
    [SerializeField] private float _requiredDistance;
    [SerializeField] private Camera _camera;

    private EggManYeeter followerTarget;

    public void Update()
    {
        if (followerTarget)
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
            followerTarget.transform.position = mousePosition;
        }
    }
    
    public EggManYeeter GetEggMan(bool bypassDistance = false)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
        
        float currentClosestEggManDistance = float.MaxValue;
        EggManYeeter closestEggMan = null;
        foreach (EggManYeeter eggMan in _eggMans)
        {
            float distance = Vector2.Distance(eggMan.transform.position, mousePosition);
            if (distance < currentClosestEggManDistance)
            {
                currentClosestEggManDistance = distance;
                closestEggMan = eggMan;
            }
        }

        if (!closestEggMan) return null;

        if (currentClosestEggManDistance > _requiredDistance && !bypassDistance) return null;
        return closestEggMan;
    }

    public void ClickPerformed()
    {
        EggManYeeter eggMan = GetEggMan();
        if(eggMan)
        {
            if (followerTarget) followerTarget.SetActive(false);
            followerTarget = eggMan;
            followerTarget.SetActive(true);
        }
    }
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            if (followerTarget) followerTarget.SetActive(false);
            followerTarget = null;
        }
        else if (context.performed)
        {
            ClickPerformed();
        }
    }
}
