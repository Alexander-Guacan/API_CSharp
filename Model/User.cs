namespace API_CSharp.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public static List<User> DB()
        {
            var Users = new List<User>
            {
                new User {
                    Id = 1,
                    UserName = "adguacan",
                    Password = "hola123",
                    Rol = "artist"
                },
                new User {
                    Id = 2,
                    UserName = "msbarriga",
                    Password = "password780",
                    Rol = "admin"
                },
                new User {
                    Id = 3,
                    UserName = "klmacas",
                    Password = "password",
                    Rol = "client"
                }
            };

            return Users;
        }
    }
}
