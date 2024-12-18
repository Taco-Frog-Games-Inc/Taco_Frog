using System;
using System.Collections;
using UnityEngine;

public class SceneChangeFade : PersistGenericSingleton<SceneChangeFade>
{

    [SerializeField] private CanvasGroup _fadeInOutImage;
    private bool _isSceneChanging = false;
    [SerializeField] private float _sceneFadeInOutTimeMultiplier = 0.25f;
    
    private void Start()
    {
        _fadeInOutImage.alpha = 1f;
        _isSceneChanging = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isSceneChanging)
        {
            _fadeInOutImage.alpha -= _sceneFadeInOutTimeMultiplier * Time.deltaTime;
            if (_fadeInOutImage.alpha == 0)
            {
                _isSceneChanging = false;
            }
        }
    }

    public void AddSceneFade()
    {
        _fadeInOutImage.alpha = 1f;
        _isSceneChanging = true;
    }
}
