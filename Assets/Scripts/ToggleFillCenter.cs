using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFillCenter : MonoBehaviour
{
    private Image _buttonImage;
    [SerializeField] private GameSettingType _gameSettingType;

    void Start()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void ToggleFill()
    {
        if (_buttonImage != null)
        {
            _buttonImage.fillCenter = !_buttonImage.fillCenter;

            switch (_gameSettingType)
            {
                case GameSettingType.Hardcore:
                    GameSettings.SetHardcore(_buttonImage.fillCenter);
                    break;
                case GameSettingType.InfiniteHealth:
                    GameSettings.SetInfiniteHealth(_buttonImage.fillCenter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        
        else
            Debug.LogError("ToggleFillCenter: No Image component found");
    }

}
