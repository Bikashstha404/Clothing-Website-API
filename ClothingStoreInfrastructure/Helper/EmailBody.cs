using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastructure.Helper
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">
    <div style=""max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1);"">
        <h1 style=""font-size: 24px; color: #333333; text-align: center;"">Reset Your Password</h1>
        <hr style=""border: none; height: 1px; background-color: #dddddd;"">
        <p style=""font-size: 16px; color: #555555;"">You're receiving this email because you requested a password reset for your Clothing Website account.</p>
        <p style=""font-size: 16px; color: #555555;"">Please click the button below to choose a new password:</p>

        <div style=""text-align: center; margin: 20px 0;"">
            <a href=""http://localhost:4200/reset-password?email={email}&code={emailToken}"" style=""background-color: #007bff; color: #ffffff; padding: 12px 20px; text-decoration: none; border-radius: 5px; font-size: 16px;"">Reset Password</a>
        </div>

<p>Email= {email} and Token = {emailToken}</p>
        <p style=""font-size: 16px; color: #555555;"">Kind regards,</p>
        <br>
        <p style=""font-size: 16px; color: #555555;"">Bikash Shrestha</p>
    </div>
</body>
</html>";
        }
    }
}
