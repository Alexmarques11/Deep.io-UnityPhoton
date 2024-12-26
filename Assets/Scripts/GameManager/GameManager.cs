using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_InputField nicknameInputField;
    public string playerNickname = "";

    private void Awake()
    {
        // Implementação de Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerNickname()
    {

        if (!string.IsNullOrEmpty(nicknameInputField.text))
        {
            playerNickname = nicknameInputField.text;
        }
    }
}