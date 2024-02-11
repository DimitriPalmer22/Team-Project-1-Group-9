using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFillImage : MonoBehaviour
{
    private Image _buttonImage;
    [SerializeField] private GameSettingType _gameSettingType;

    void Start()
    {
        _buttonImage = GetComponent<Image>();
        
        // Match the button's fill to the setting's value
        switch (_gameSettingType)
        {
            case GameSettingType.Hardcore:
                _buttonImage.fillCenter = GameSettings.IsHardcore;
                break;
            case GameSettingType.InfiniteHealth:
                _buttonImage.fillCenter = GameSettings.IsInfiniteHealth;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
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
