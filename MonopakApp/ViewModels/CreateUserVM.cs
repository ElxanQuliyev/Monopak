using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class CreateUserVM
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }
        public string DeliveredAddress { get; set; }

    }
}