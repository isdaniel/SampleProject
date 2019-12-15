using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiService.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        List<UserViewModel> userInfo = new List<UserViewModel>()
        {
            new UserViewModel() {Id = 1,Age = 20,UserName = "Daniel"},
            new UserViewModel() {Id = 2,Age = 25,UserName = "Kevin"},
        };

        [HttpGet]
        [Route("UserInfo")]
        public UserViewModel GetUser(int id)
        {
            return userInfo.FirstOrDefault(x=>x.Id==id);
        }

        [Route("GetUserPost")]
        [HttpPost]
        public UserViewModel GetUserPost(UserInputModel model)
        {
            return userInfo.FirstOrDefault(x=>x.Id==model.Id);
        }
    }

    public class UserInputModel
    {
        public int Id { get; set; }
    }

    public class UserViewModel
    {
        public int Age { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
    }
}
