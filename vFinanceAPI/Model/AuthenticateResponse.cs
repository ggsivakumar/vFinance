using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }      
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            UserName = user.UserName;
            UserId = user.UserId;
            Token = token;
        }
    }
}
