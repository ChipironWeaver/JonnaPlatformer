using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HinoScreneTransition : MonoBehaviour
{
    [SerializeField,Tag] private string _tag;
    [SerializeField,Scene] private string _sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("enter");
        if (other.CompareTag(_tag))
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
