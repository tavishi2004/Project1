var dashid = sessionStorage.getItem("Id");
const Docid = sessionStorage.getItem("Documentid");
console.log(Docid);
onLoad();
function onLoad() {
  listFolders();
  listonlyFiles();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "    Hi" + " " + sessionStorage.getItem("UserName");
}

function listFolders() {
  try {
    var foldercreate = document.getElementById("main");

    foldercreate.innerHTML = "";
    fetch("http://localhost:58659/api/Folder/Trash?id=" + dashid, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((folders) => {
        console.log(folders);
        folders.forEach((folder) => {
          var foldercreate = document.getElementById("main");
          var fol = document.createElement("div");

          fol.style.width = "180px";
          fol.style.height = "106px";
          (fol.style.margin = "18px"), "18px", "18px", "18px";
          fol.style.background = "white";
          fol.style.display = "inline-grid";
          fol.style.padding = "20px";
          fol.style.borderRadius = "20px";
          fol.style.color = "#618f61";
          fol.style.boxShadow = "0px 10px lightgrey";

          const folname = folder.folderName;
          const foldid = folder.folderId;
          console.log(folname);
          fol.innerHTML = `<i class="fa fa-folder fa-3x" style="color:lightblue; aria-hidden="true;cursor:pointer">
      <a onclick="trashedfile(${foldid})" style="color:black;font-weight:normal;font-size:20px;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${folname}</a> 
      </i>
      <i class="fa fa-undo" onclick="restore(${foldid})" style="position:relative;bottom:-15px;"></i>
    <i class="fa fa-trash fa-1.5px" onclick="deletefolder(${foldid})" style="position:relative;left: 130px;bottom: 1px;">
    </i>`;
          foldercreate.appendChild(fol);
        });
      });
  } catch (err) {
    console.log(err);
  }
}

// function to delete folder
function deletefolder(folder) {
  var requestOptions = {
    method: "DELETE",
    body: "raw",
    redirect: "follow",
  };

  fetch("http://localhost:58659/api/Folder/" + folder, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
  location.reload();
}

//function to restore folder from trash
function restore(resfol) {
  var myHeaders = new Headers();
  myHeaders.append("accept", "*/*");

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(
    "http://localhost:58659/api/Folder/Restore?id=" + resfol,
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
  location.reload();
}

function listonlyFiles() {
  var requestOptions = {
    method: "GET",
    redirect: "follow",
  };

  fetch(
    "http://localhost:58659/api/Documents/onlyfile?id=" + Docid,
    requestOptions
  )
    .then((response) => response.json())
    .then((result) => {
      console.log(result);
      var filecreate = document.getElementById("main");
      var fil = document.createElement("div");

      fil.style.width = "260px";
      fil.style.height = "116px";
      (fil.style.margin = "20px"), "20px", "20px", "20px";
      fil.style.background = "white";
      fil.style.display = "inline-grid";
      fil.style.padding = "20px";
      fil.style.borderRadius = "12px";
      fil.style.color = "#618f61";

      docname = result.docName;
      console.log(docname);
      fil.innerHTML = `<i class="fa fa-file fa-3x" aria-hidden="true">
            <a onclick="trashedfile(${Docid})" style="font-size:20px;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${docname}</a> 
            </i>
        
          <i class="fa fa-trash fa-1.5px" onclick="deletefolder(${Docid})" style="position:relative;left: 200px;bottom: 1px;">
          </i> `;
      filecreate.appendChild(fil);
    })
    .catch((error) => console.log("error", error));
}

//function for trash file
function trashedfile(foId) {
  sessionStorage.setItem("FolderId", foId);
  window.location.href = "trashfile.html";
}
var header = document.getElementById("side");
var list = header.getElementsByClassName("List");
for (var i = 0; i < list.length; i++) {
  list[i].addEventListener("click", function () {
    var current = document.getElementsByClassName("active");
    if (current.length > 0) {
      current[0].className = current[0].className.replace(" active", "");
    }
    this.className += " active";
  });
}
