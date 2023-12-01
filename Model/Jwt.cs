using System.Security.Claims;

namespace API_CSharp.Model
{
    public class Jwt
    {
        public string Key{ get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic IsValidToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verify if token is valid",
                        result = string.Empty
                    };
                }


                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                User user = User.DB().FirstOrDefault(x => x.Id.ToString() == id);

                return new
                {
                    success = true,
                    message = "User found",
                    result = user
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Message: " + ex.Message,
                    result = string.Empty
                };
            }
        }
    }
}
