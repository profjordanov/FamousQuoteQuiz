using System.Threading.Tasks;
using FamousQuoteQuiz.Core.Models;
using Optional;

namespace FamousQuoteQuiz.Core.Services
{
    public interface IUsersService
    {
        Task<Option<JwtModel, Error>> Login(LoginUserModel model);

        Task<Option<UserModel, Error>> Register(RegisterUserModel model);
    }
}
