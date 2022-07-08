//modal popup for adding a folder
const modal = document.querySelector(".modal");
const trigger = document.getElementById("trigger");
const closeButton = document.querySelector(".close-button");
function toggleModal() {
  modal.classList.toggle("show-modal");
}
function windowOnClick(event) {
  if (event.target === modal) {
    toggleModal();
  }
}
trigger.addEventListener("click", toggleModal);
closeButton.addEventListener("click", toggleModal);
window.addEventListener("click", windowOnClick);

//highlighting list on sidebar
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

//for creating a folder
var foldername = document.getElementById("fname");
var fn = document.getElementById("createbtn");
fn.addEventListener("click", function () {
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");
  fetch("http://localhost:58659/api/Folder", {
    method: "POST",

    // cache: "no-cache",

    // credentials: "same-origin",
    headers: myHeaders,
    body: JSON.stringify({
      FolderName: foldername.value,
      FCreatedBy: sessionStorage.getItem("Id"),
      IsDeleted: 0,
      CreatedAt: new Date().toISOString(),
    }),
  })
    .then((res) => {
      return res.text();
    })
    .then((data) => console.log(data));
  listFolders();
  location.reload();
});

var dashid = sessionStorage.getItem("Id");
console.log(dashid);
function listFolders() {
  try {
    var foldercreate = document.getElementById("main");

    foldercreate.innerHTML = "";
    fetch("http://localhost:58659/api/Folder/FolderId?id=" + dashid, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((folders) => {
        console.log(folders);
        folders.forEach((folder) => {
          //styling folders
          var foldercreate = document.getElementById("main");
          var fol = document.createElement("div");
          fol.setAttribute("class", "foldercs");
          const folname = folder.folderName;
          const foldid = folder.folderId;
          console.log(folname);
          fol.innerHTML = `
    <i class="fa fa-folder fa-3x" aria-hidden="true" style="cursor:pointer;color:skyblue;">
    <a onclick="uploadfile(${foldid})" style="color:black;font-weight:normal;font-size:18px;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${folname}</a> 
    </i>
    <i class="fa fa-trash fa-1.5px" onclick="swalfire(${foldid})" style="position:relative;left:130px;bottom:-20px;cursor:pointer;color:palevioletred;"></i>
    <i class="fa fa-info-circle" style="position:relative;right: 5px;bottom: 0px;top:4px;"></i>
    <i class="fa fa-star-o" onclick="favorites(${foldid})" style="position:relative;left: 130px;bottom:90px;"></i>
  `;
          foldercreate.appendChild(fol);
        });
      });
  } catch (err) {
    console.log(err);
  }
}

// Trashfolder(${foldid})
function onLoad() {
  listFolders();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "    Hi" + " " + sessionStorage.getItem("UserName");
}

onLoad();
//alert box to delete a folder
function swalfire(foldid) {
  Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!",
  }).then((result) => {
    if (result.isConfirmed) {
      Swal.fire(
        Trashfolder(foldid),
        "Deleted!",
        "Your file has been deleted.",
        "success"
      );
    }
  });
}

function Trashfolder(folder) {
  var raw = "";

  var requestOptions = {
    method: "PUT",
    body: raw,
    redirect: "follow",
  };

  fetch("http://localhost:58659/api/Folder/" + folder, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
  location.reload();
}

//function to add favorite folders
function favorites(folderid) {
  var myHeaders = new Headers();
  myHeaders.append("accept", "*/*");

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(
    "http://localhost:58659/api/Folder/favourites?id=" + folderid,
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
}
function uploadfile(foId) {
  sessionStorage.setItem("FolderId", foId);
  window.location.href = "file.html";
}
function logout() {
  sessionStorage.clear();
  window.location.href = "login.html";
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
          fol.innerHTML = `<i class="fa fa-folder fa-3x" aria-hidden="true" style="color:lightblue;">
    <button class="addfile" onclick="uploadfile()"></button>
    <a onclick="uploadfile()" style="color:black;font-size:15px;text-decoration: none;position: absolute;cursor: pointer; margin:20px;font-weight:normal;">${folname}</a> 
    </i>
  <i class="fa fa-trash fa-1.5x" onclick="deletefolder(${foldid})" style="position:relative;left: 190px;bottom: -6px;">
  </i> `;
          foldercreate.appendChild(fol);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
