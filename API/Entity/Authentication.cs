using System;
namespace API.Entity
{
    public class Authentication
    {
        public string username { get; set; }
        public string passsword { get; set; }
    }
    public class User
    {
        public string token { get; set; }
        public string name { get; set; }
    }
}
