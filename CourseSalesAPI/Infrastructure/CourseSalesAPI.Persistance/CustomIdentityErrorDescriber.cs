using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance
{
    using Microsoft.AspNetCore.Identity;

    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Description = $"'{userName}' kullanıcı adı zaten alınmış. Lütfen başka bir kullanıcı adı deneyin."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Description = $"Şifre en az {length} karakter olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Description = "Şifre en az bir alfanümerik olmayan karakter içermelidir. (örn. @, #, !)"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Description = "Şifre en az bir rakam içermelidir. (örn. 0-9)"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Description = "Şifre en az bir küçük harf içermelidir. (örn. a-z)"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Description = "Şifre en az bir büyük harf içermelidir. (örn. A-Z)"
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Description = $"'{email}' geçerli bir e-posta adresi değil."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Description = $"'{email}' e-posta adresi zaten kullanılmakta. Lütfen farklı bir e-posta adresi deneyin."
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Description = "Bu kullanıcı için zaten bir şifre tanımlanmış."
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Description = "Girdiğiniz şifreler uyuşmuyor."
            };
        }
    }
}