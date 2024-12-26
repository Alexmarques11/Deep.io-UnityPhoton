using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel;  // Referência ao painel da leaderboard
    public GameObject leaderboardEntryPrefab;  // Referência ao prefab do template de texto
    public Transform leaderboardContainer;  // Contêiner onde os textos serão instanciados
    private List<PlayerController> players;  // Lista de jogadores

    void Start()
    {
        players = new List<PlayerController>();
    }

    // Atualiza a leaderboard com os dados mais recentes
    public void UpdateLeaderboard()
    {
        // Limpa qualquer instância anterior da leaderboard
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }

        // Ordena os jogadores pela pontuação (decrescente)
        players.Sort((player1, player2) => player2.score.CompareTo(player1.score));

        // Cria uma entrada de texto para cada jogador na leaderboard
        foreach (var player in players)
        {
            // Instancia um novo objeto de texto baseado no template
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);

            // Obtém o componente TMP_Text do novo objeto
            TMP_Text text = entry.GetComponent<TMP_Text>();

            // Atualiza o texto com o nome e a pontuação do jogador
            text.text = $"{player.playerName}: {player.score}";
        }
    }

    // Função chamada quando um jogador entra na sala
    public void OnPlayerJoined(PlayerController newPlayer)
    {
        players.Add(newPlayer);
        UpdateLeaderboard();  // Atualiza a leaderboard
    }

    // Função chamada quando um jogador sai da sala
    public void OnPlayerLeft(PlayerController leavingPlayer)
    {
        players.Remove(leavingPlayer);
        UpdateLeaderboard();  // Atualiza a leaderboard
    }
}
