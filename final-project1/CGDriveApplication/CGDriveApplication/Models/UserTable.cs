using System;
using System.Collections.Generic;

namespace CGDriveApplication.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            Documents = new HashSet<Documents>();
            Folder = new HashSet<Folder>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<Documents> Documents { get; set; }
        public ICollection<Folder> Folder { get; set; }
    }
}
