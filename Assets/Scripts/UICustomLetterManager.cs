using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICustomLetterManager : MonoBehaviour
{
    [SerializeField] private Vector2 _characterSize;
    [SerializeField] private UICustomLetter _characterPrefab;
    
    private List<UICustomLetter> _customLetters = new List<UICustomLetter>();
    
    
    public void RenderString(string text)
    {
        int textIndex = 0;
        foreach(char c in text)
        {
            if (textIndex + 1 > _customLetters.Count) CreateNewLetter();
            print(textIndex + " : " + _customLetters.Count);
            _customLetters[textIndex].SetCharAs(c);
            textIndex++;
        }

        for (int i = textIndex; i < _customLetters.Count; i++)
        {
            _customLetters[i].Disable();
        }
    }

    public void CreateNewLetter()
    {
        print("out");
        UICustomLetter newLetter = Instantiate(_characterPrefab, transform);
        print("in");
        newLetter.gameObject.name = "Letter n°" + (_customLetters.Count + 1);
        _customLetters.Add(newLetter);
    }
    
    
}
