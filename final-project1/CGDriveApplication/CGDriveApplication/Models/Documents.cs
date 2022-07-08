using System;
using System.Collections.Generic;

namespace CGDriveApplication.Models
{
    public partial class Documents
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string ContentType { get; set; }
        public int? Size { get; set; }
        public int? DCreatedBy { get; set; }
        public DateTime? DCreatedAt { get; set; }
        public int FolDocId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFavourite { get; set; }
        public UserTable DCreatedByNavigation { get; set; }
        public Folder FolDoc { get; set; }
    }
}
