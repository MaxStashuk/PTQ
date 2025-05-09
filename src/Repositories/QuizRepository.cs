using Microsoft.Data.SqlClient;
using Models;

namespace Repositories;

public class QuizRepository : IQuizRepository
{
    private string _connectionString;

    public QuizRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IEnumerable<Quiz> GetAllQuizzes()
    {
        List <Quiz> quizzes = [];

        String query = "SELECT * FROM Quiz";
        
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var quiz = new Quiz
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PotatoTeacherId = reader.GetInt32(2),
                            PathFile = reader.GetString(3),
                        };
                        quizzes.Add(quiz);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return quizzes;
        }
    }

    public Quiz GetQuizById(int quizId)
    {
        string Query = "SELECT * FROM Quiz WHERE Id = @quizId";
        
        SqlCommand command = new SqlCommand(Query);
        command.Parameters.AddWithValue("@quizId", quizId);
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        Quiz q = command.ExecuteScalar();
        
    }
}