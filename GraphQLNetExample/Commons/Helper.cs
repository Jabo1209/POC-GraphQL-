using System.Text.RegularExpressions;

namespace GraphQLNetExample.Commons
{
    public class Helper
    {
        public static bool IsNotValidEmail(string email)
        {
            // Return true if strIn is in valid e-mail format.
            return !Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

    }
}
