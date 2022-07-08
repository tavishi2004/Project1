using System;
using System.Collections.Generic;

namespace CGDriveApplication.Models
{
    public partial class Folder
    {
        public Folder()
        {
            Documents = new HashSet<Documents>();
        }

        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public int? FCreatedBy { get; set; }
        public DateTime? FCreatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFavourite { get; set; }

        public UserTable FCreatedByNavigation { get; set; }
        public ICollection<Documents> Documents { get; set; }
    }
}
