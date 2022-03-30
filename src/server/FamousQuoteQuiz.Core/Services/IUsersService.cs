using FamousQuoteQuiz.Core.Models;
using Optional;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Core.Services
{
    public interface IUsersService
    {
        Task<Option<JwtModel, Error>> Login(LoginUserModel model);

        Task<Option<UserModel, Error>> Register(RegisterUserModel model);
    }
}
