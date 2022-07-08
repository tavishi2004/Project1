using CGDriveApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGDriveApplication.RequestModel
{
    public class DocumentRequestModel
    {
        public string DocName { get; set; }
        public string ContentType { get; set; }
        public int? Size { get; set; }
        public int? DCreatedBy { get; set; }
        public DateTime? DCreatedAt { get; set; }
        public int FolDocId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFavourite { get; set; }
       
    }
}

