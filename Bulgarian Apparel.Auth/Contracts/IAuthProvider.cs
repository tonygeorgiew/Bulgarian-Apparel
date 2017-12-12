using Bulgarian_Apparel.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Bulgarian_Apparel.Auth
{
    public interface IAuthProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }
    
        string CurrentUserUsername { get; }

        IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();

        bool IsInRole(string userId, string roleName);

        IdentityResult AddToRole(string userId, string roleName);

        IdentityResult RemoveFromRole(string userId, string roleName);

        void BanUser(string userId);

        void UnbanUser(string userId);
    }
}