using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    private TMP_Text leaderboardText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        leaderboardText = GameObject.FindGameObjectWithTag("Leaderboard")?.GetComponent<TMP_Text>();
        if (leaderboardText == null)
        {
            Debug.LogError("LeaderboardText não encontrado na cena.");
        }
    }

    public void UpdatePlayerScore(string playerName, int score)
    {
        if (playerScores.ContainsKey(playerName))
        {
            playerScores[playerName] += score;
        }
        else
        {
            playerScores[playerName] = score;
        }

        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        var topPlayers = playerScores.OrderByDescending(p => p.Value).Take(6);

        string leaderboardString = "";
        int rank = 1;
        foreach (var player in topPlayers)
        {
            leaderboardString += $"{rank}. {player.Key}: {player.Value} pontos\n";
            rank++;
        }

        if (leaderboardText != null)
        {
            leaderboardText.text = leaderboardString;
        }
    }
}