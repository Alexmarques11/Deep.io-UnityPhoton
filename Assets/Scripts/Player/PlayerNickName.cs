using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNickName : MonoBehaviour
{
    public TMP_Text nicknameText;  // Referência ao componente TextMeshPro que exibirá o nome

    void Start()
    {
        // Define o texto do nickname para o jogador atual
        if (nicknameText != null)
        {
            nicknameText.text = PhotonNetwork.NickName;  // Defina o nome do jogador no texto
        }
    }
}
