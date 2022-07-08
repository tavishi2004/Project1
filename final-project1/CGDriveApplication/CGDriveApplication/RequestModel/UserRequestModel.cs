using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGDriveApplication.RequestModel
{
    public class UserRequestModel
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
