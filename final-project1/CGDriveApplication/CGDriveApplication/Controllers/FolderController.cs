using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGDriveApplication.Models;
using CGDriveApplication.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CGDriveApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly CG_DocsContext _cgfolder;
        private readonly CG_DocsContext _cgDocument;
        public FolderController(CG_DocsContext cgfolder, CG_DocsContext cgDocument)
        {
            _cgfolder = cgfolder;
            _cgDocument = cgDocument;
        }
        // GET: api/Folder
        [HttpGet]
        public IActionResult Get()
        {
            var getFolder = _cgfolder.Folder.ToList();
            return Ok(getFolder);
        }

        // GET: api/Folder/5
        [HttpGet("FolderId")]
        public IActionResult Get(int id)
        {
            var getFid = _cgfolder.Folder.Where(o => o.FCreatedBy == id && o.IsDeleted == false);
            return Ok(getFid);

        }

        [HttpGet("Trash")]
        public IActionResult GetTrash(int id)
        {
            var getfid = _cgfolder.Folder.Where(o => o.FCreatedBy == id && o.IsDeleted == true);
            return Ok(getfid);
        }
        [HttpGet("favorite")]
        public IActionResult Getfav(int id)
        {
            var getfav = _cgfolder.Folder.Where(o => o.FCreatedBy == id && o.IsFavourite == true);
            return Ok(getfav);
        }

        [HttpGet("Recent")]
        public IActionResult GetRecentFolder(int id)
        {
            var createdAt = DateTime.Now.AddMinutes(-30);
            var res = _cgfolder.Folder.Where(o => o.FCreatedAt >= createdAt && o.FCreatedBy == id && o.IsDeleted == false);
            return Ok(res);
        }

        // POST: api/Folder
        [HttpPost]
        public void Post([FromBody] FolderRequestModel value)
        {
            Folder Fd = new Folder()
            {
                FolderName = value.FolderName,
                FCreatedBy = value.FCreatedBy,
                FCreatedAt = DateTime.Now,
                IsDeleted = value.IsDeleted
            };
            _cgfolder.Folder.Add(Fd);
            _cgfolder.SaveChanges();
        }

        // PUT: api/Folder/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
            var upfil = _cgDocument.Documents.Where(o => o.FolDocId == id).ToList();
            foreach(var res in upfil)
            {
                res.IsDeleted = true;
                if(res.IsFavourite==true)
                {
                    res.IsFavourite = false;
                }
                _cgDocument.SaveChanges();
            }
            var upfol = _cgfolder.Folder.FirstOrDefault(o => o.FolderId == id);
            upfol.IsDeleted = true;
            if (upfol.IsFavourite == true)
            {
                upfol.IsFavourite = false;
            }
            _cgfolder.Folder.Update(upfol);
            _cgfolder.SaveChanges();
        }
        [HttpPut("Restore")]
        public void PutRestore(int id)
        {
            var refil = _cgDocument.Documents.Where(o => o.FolDocId == id).ToList();
            foreach (var res in refil)
            {
                res.IsDeleted = false;
                _cgDocument.SaveChanges();
            }
            var refol = _cgfolder.Folder.FirstOrDefault(o => o.FolderId == id);
            refol.IsDeleted = false;
            _cgfolder.Folder.Update(refol);
            _cgfolder.SaveChanges();
        }

        [HttpPut("favourites")]
        public void PutFav(int id)
        {
            var favfil = _cgDocument.Documents.Where(o => o.FolDocId == id).ToList();
            foreach (var res in favfil)
            {
                res.IsFavourite = true;
                _cgDocument.SaveChanges();
            }
            var favfol = _cgfolder.Folder.FirstOrDefault(o => o.FolderId == id);
            favfol.IsFavourite = true;
            _cgfolder.Folder.Update(favfol);
            _cgfolder.SaveChanges();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deldoc = _cgfolder.Documents.Where(o => o.FolDocId == id).ToList();
            deldoc.ForEach(o => _cgfolder.Documents.Remove(o));
            var delfol = _cgfolder.Folder.Where(o => o.FolderId == id).ToList();
            delfol.ForEach(o => _cgfolder.Folder.Remove(o));
            _cgfolder.SaveChanges();
        }
        [HttpGet("folder/{id}/{value}")]
        public IActionResult Get(int id, string value)
        {
            var result = _cgfolder.Folder.Where(o => (o.FolderName.Contains(value) && o.FCreatedBy == id && o.IsDeleted==false));
            return Ok(result);
        }
    }
}
