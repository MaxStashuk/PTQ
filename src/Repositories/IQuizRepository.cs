using Models;

namespace Repositories;

public interface IQuizRepository
{
    IEnumerable<Quiz> GetAllQuizzes();
    Quiz? GetQuizById(int quizId);
}