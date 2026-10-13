using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UICustomLetter : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private float _targetHeight;
    [SerializeField] private Dictionary<char,VideoClip> _characters;

    private RenderTexture renderTexture;
    private Material material;

    public void Start()
    {
        renderTexture = new RenderTexture((int)_targetHeight, (int)_targetHeight, 24);
        renderTexture.name = gameObject.name + "'s render texture";
        
        material = new Material(_baseMaterial);
        material.name = gameObject.name + "'s material";
        material.SetTexture("_mainTex", renderTexture);
        
        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        _videoPlayer.targetTexture = renderTexture;
        _rawImage.material = material;
    }
    public bool SetCharAs(char character)
    {
        if (_characters.TryGetValue(character, out VideoClip letter))
        {
            
            _rawImage.color = Color.white;
            _rawImage.rectTransform.sizeDelta = 
                new Vector2(letter.width / (float)letter.height * _targetHeight
                    , _targetHeight);
            _videoPlayer.clip = letter;
            return true;
        }

        _rawImage.rectTransform.sizeDelta = new Vector2( _targetHeight, _targetHeight);
        _rawImage.color = Color.magenta;
        _videoPlayer.clip = null;
        return false;
    }

    public void Disable()
    {
        _rawImage.rectTransform.sizeDelta = new Vector2( _targetHeight, _targetHeight);
        _rawImage.color = Color.clear;
        _videoPlayer.clip = null;
    }
}
