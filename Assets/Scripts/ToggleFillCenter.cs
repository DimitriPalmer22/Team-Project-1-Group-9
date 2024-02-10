using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFillCenter : MonoBehaviour
{
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ToggleFill()
    {
        if (buttonImage != null)
        {
            buttonImage.fillCenter = !buttonImage.fillCenter;
        }
        else
        {
            Debug.LogError("ToggleFillCenter: No Image component found");
        }
    }

}
