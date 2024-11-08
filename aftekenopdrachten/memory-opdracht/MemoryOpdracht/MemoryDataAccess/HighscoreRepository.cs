using MemoryLibrary.Models;
using Microsoft.Data.SqlClient;
using MemoryLibrary.Interfaces;

namespace MemoryDataAccess
{
    public class HighscoreRepository: IHighscoreRepository
    {
        private string _connectionstring;

        public HighscoreRepository(string connectionstring) { 
            _connectionstring = connectionstring;
        }

        public void AddHighscore(MemoryGameScore highscore, int maxHighscoreCount)
        {
            List<MemoryGameScore> highscores = GetHighscores();
            if(highscores.Count >= maxHighscoreCount)
            {
                highscores.Add(highscore);
                highscores = highscores.OrderByDescending(h => h.Score).ThenBy(h => h.AmountOfTurns).ToList();
                highscores = highscores.Take(maxHighscoreCount).ToList();
            }
            else
            {
                highscores.Add(highscore);
            }

            DeleteHighscores();
            AddHighscores(highscores);
        }

        public List<MemoryGameScore> GetHighscores()
        {
            List<MemoryGameScore> highscores = new List<MemoryGameScore>();

            var connectionstring = _connectionstring;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string selectHighscores = "SELECT * FROM Highscores ORDER BY score DESC, amount_of_turns ASC";

                var selectHighscoresCommand = new SqlCommand(selectHighscores, connection);
                using (SqlDataReader reader = selectHighscoresCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MemoryGameScore highscore = new MemoryGameScore
                        {
                            GameId = Guid.Parse(reader["game_id"].ToString()),
                            PlayerName = reader["player_name"].ToString(),
                            Score = int.Parse(reader["score"].ToString()),
                            AmountOfTurns = int.Parse(reader["amount_of_turns"].ToString())
                        };
                        highscores.Add(highscore);
                    }
                }
                connection.Close();
            }

            return highscores;
            
        }

        private void DeleteHighscores()
        {
            var connectionstring = _connectionstring;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string deleteHighscores = "DELETE FROM Highscores";

                var deleteHighscoresCommand = new SqlCommand(deleteHighscores, connection);
                deleteHighscoresCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void AddHighscores(List<MemoryGameScore> highscores)
        {
            var connectionstring = _connectionstring;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                foreach (var score in highscores)
                {
                    string insertHighscore = $"INSERT INTO Highscores (game_id, player_name, score, amount_of_turns) VALUES ('{score.GameId}', '{score.PlayerName}', '{score.Score}', '{score.AmountOfTurns}')";
                    var insertHighscoreCommand = new SqlCommand(insertHighscore, connection);
                    insertHighscoreCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
