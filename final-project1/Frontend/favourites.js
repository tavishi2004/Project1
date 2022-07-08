//favorite marked folders function
var dashid = sessionStorage.getItem("Id");

function listFolders() {
  try {
    var foldercreate = document.getElementById("main");

    foldercreate.innerHTML = "";
    fetch("http://localhost:58659/api/Folder/favorite?id=" + dashid, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((folders) => {
        console.log(folders);
        folders.forEach((folder) => {
          var foldercreate = document.getElementById("main");
          var fol = document.createElement("div");
          fol.setAttribute("class", "foldercs");

          const folname = folder.folderName;
          const foldid = folder.folderId;
          console.log(folname);
          fol.innerHTML = `
      <i class="fa fa-folder fa-3x" aria-hidden="true" style="color:lightgrey;cursor:pointer;color:lightblue">
      <a onclick="favfile(${foldid})" style="color:black;font-weight:normal;font-size:15px;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${folname}</a> 
      </i>
      <i class="fa fa-trash fa-1.5px" onclick="Trashfolder(${foldid})" style="position:relative;left:130px;bottom:-15px;"></i>
      <i class="fa fa-info-circle" style="position:relative;left: 3px;bottom: 1px;"></i>
      <i class="fa fa-star-o" onclick="favorites(${foldid})" style="position:relative;left: 130px;bottom:90px;"></i>
    `;
          foldercreate.appendChild(fol);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
onload();
function onload() {
  listFolders();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "    Hi" + " " + sessionStorage.getItem("UserName");
}
function favfile(folder) {
  sessionStorage.setItem("FolderId", folder);
  window.location.href = "favouritesfile.html";
}

//function to search a specific folder
function search() {
  try {
    var searchcontent = document.getElementById("search").value;
    if (searchcontent == "") location.reload();

    var foldercreate = document.getElementById("main");
    foldercreate.innerHTML = "";
    fetch(
      `http://localhost:58659/api/Folder/folder/${dashid}/${searchcontent}`,
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((folders) => {
        console.log(folders);
        folders.forEach((folder) => {
          var foldercreate = document.getElementById("main");
          var fol = document.createElement("div");
          //styling the div for searched folders
          fol.style.width = "240px";
          fol.style.height = "101px";
          fol.style.margin = "20px", "20px", "20px", "20px";
          fol.style.background = "white";
          fol.style.display = "inline-grid";
          fol.style.padding = "20px";
          fol.style.borderRadius = "12px";
          fol.style.color = "#618f61";

          //styling searched folders
          const folname = folder.folderName;
          const foldid = folder.folderId;
          console.log(folname);
          fol.innerHTML = `
    <i class="fa fa-folder fa-3x" aria-hidden="true" style="color:lightblue;">
    <button class="addfile" onclick="uploadfile()"></button>
    <a onclick="uploadfile()" style="color:black;font-size:15px;text-decoration: none;position: absolute;cursor: pointer; margin:20px;font-weight:normal;">${folname}</a> 
    </i>
    <i class="fa fa-trash fa-1.5x" onclick="deletefolder(${foldid})" style="position:relative;left: 190px;bottom: -6px;">
    </i> 
  `;
          foldercreate.appendChild(fol);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
