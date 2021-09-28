public class LeaderboardData
{
    private string playerUsername;
    private int playerScore;

    public string PlayerUsername { get => playerUsername; set => playerUsername = value; }
    public int PlayerScore { get => playerScore; set => playerScore = value; }

    public LeaderboardData(string _playerUsername, int _playerScore)
    {
        this.playerUsername = _playerUsername;
        this.playerScore = _playerScore;
    }
}