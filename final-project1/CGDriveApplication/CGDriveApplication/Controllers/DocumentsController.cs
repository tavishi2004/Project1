using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CGDriveApplication.Models;
using CGDriveApplication.RequestModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CGDriveApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly CG_DocsContext _cgDocument;
        private readonly IHostingEnvironment _environment;
        public DocumentsController(CG_DocsContext cgDocument, IHostingEnvironment environment)
        {
            _cgDocument = cgDocument;
            _environment = environment;
        }
        [HttpGet("onlyfile")]
        public IActionResult GetTrashonlyfile(int id)
        {
            var getDoc = _cgDocument.Documents.Where(obj => obj.DocId == id && obj.IsDeleted==true);
            return Ok(getDoc);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _cgDocument.Documents.Where(obj => obj.FolDocId == id && obj.IsDeleted==false);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // GET: api/Documents
        // [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Documents/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Documents
        [HttpPost]
        public void Post([FromBody] DocumentRequestModel value)
        {
            var Documents = new Documents()
            {
                DocName = value.DocName,
                ContentType = value.ContentType,
                Size = value.Size,
                DCreatedBy = value.DCreatedBy,
                FolDocId = value.FolDocId
            };
            _cgDocument.Documents.Add(Documents);
            _cgDocument.SaveChanges();
        }
        [HttpPost("upload/{folderid}/{createdby}/{createdAt}")]
        public IActionResult Upload(List<IFormFile> files,int folderid,int createdby,DateTime createdAt) 
            
        {

               long fsize = files.Sum(f => f.Length);
            var RootPath = Path.Combine(_environment.ContentRootPath, "Resources", "Documents");

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            foreach (var file in files)
            {
                var filePath = Path.Combine(RootPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                        var Documents = new Documents()
                        {
                            DocName = file.FileName,
                            ContentType = file.ContentType,
                            Size = (int)file.Length,
                            FolDocId = folderid,
                            DCreatedBy=createdby,
                            DCreatedAt=createdAt

                        };
                    file.CopyTo(stream);
                    _cgDocument.Documents.Add(Documents);
                    _cgDocument.SaveChanges();
                }
            }
            return Ok(new { count = files.Count, fsize });
        }
        [HttpGet("TrashDocId")]
        public IActionResult TrashGetById(int id)
        {
            try
            {
                var result = _cgDocument.Documents.Where(obj => obj.FolDocId == id && obj.IsDeleted == true);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("favfolderfile")]
        public IActionResult favfile(int id)

        {
            try
            {
                var favfil = _cgDocument.Documents.Where(obj => obj.FolDocId == id && obj.IsFavourite == true);

                if (favfil == null) return NotFound();

                return Ok(favfil);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //[HttpPost]
        //public IActionResult Download(int id)
        //{
        //    var Provider = new FileExtensionContentTypeProvider();
        //    var document = _cgDocument.Documents.Find(id);
        //    if (document == null)
        //        return NotFound();
        //    var file = Path.Combine(_environment.ContentRootPath, "Resources", "Documents", document.DocName);
        //    if (!Provider.TryGetContentType(file, out string ContentType))
        //    {
        //        ContentType = "application/octet-stream";
        //    }
        //    byte[] fileBytes;
        //    if (System.IO.File.Exists(file))
        //    {
        //        fileBytes = System.IO.File.ReadAllBytes(file);
        //    }
        //    else
        //        return NotFound();
        //    return File(fileBytes, ContentType, document.DocName);
        //}
        // PUT: api/Documents/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
        //[HttpGet("DocId")]
        //public IActionResult GetById(int id)
        //{
        //    try
        //    {
        //        var result = _cgDocument.Documents.Where(obj => obj.DocId == id && obj.IsDeleted==false);

        //        if (result == null) return NotFound();

        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}




        [HttpPut("FileTrash")]
            public void Put(int id)
        {
            var upfile = _cgDocument.Documents.FirstOrDefault(o => o.DocId == id);
            upfile.IsDeleted = true;
            _cgDocument.Documents.Update(upfile);
            _cgDocument.SaveChanges();
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var delDoc = _cgDocument.Documents.Where(o => o.DocId == id).ToList();
            delDoc.ForEach(res => _cgDocument.Documents.Remove(res));
            _cgDocument.SaveChanges();
        }
        [HttpGet("Document/{id}/{value}")]
        public IActionResult Get(int id, string value)
        {
            var result = _cgDocument.Documents.Where(o => (o.DocName.Contains(value) && o.DCreatedBy == id));
            return Ok(result);
        }
    }
}
