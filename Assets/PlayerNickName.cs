using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNickname : MonoBehaviourPun
{
    public TMP_Text nicknameText;  // Texto que será exibido acima do jogador

    void Start()
    {
        if (nicknameText != null)
        {
            nicknameText.text = PhotonNetwork.NickName;  // Define o nome do jogador

            // Caso o jogador não seja local, apenas sincroniza o nome
            if (!photonView.IsMine)
            {
                nicknameText.gameObject.SetActive(false);  // O texto não deve ser exibido para outros jogadores
            }
        }
    }
}
