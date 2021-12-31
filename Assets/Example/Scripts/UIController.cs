using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform settingsWindow;
    [SerializeField] private TMP_InputField inputField;
    public event Action<GameData> OnClickGenerateGame;
    public event Action OnClickReset;
    
    public void ShowSettingsWindow()
    {
        settingsWindow.gameObject.SetActive(true);
    }
    
    public void HideSettingsWindow()
    {
        settingsWindow.gameObject.SetActive(false);
    }

    public void GenerateGame()
    {
        int objectCount = Convert.ToInt32(inputField.text);
        OnClickGenerateGame?.Invoke(new GameData(objectCount));
        HideSettingsWindow();
    }
    
    public void ResetGame()
    {
        OnClickReset?.Invoke();
    }
}
