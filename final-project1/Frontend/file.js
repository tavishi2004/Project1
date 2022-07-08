//popup function to upload a file
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

function onLoad() {
  listFiles();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "Hi" + " " + sessionStorage.getItem("UserName");
}
onLoad();
function logout() {
  sessionStorage.clear();
  window.location.href = "login.html";
}
var userid = sessionStorage.getItem("Id");
var name = sessionStorage.getItem("UserName");
var folderid = sessionStorage.getItem("FolderId");

//function to upload file
function uploadfile() {
  try {
    var filepath = document.getElementById("myfile").files[0];

    var uploadT = new Date();
    console.log(filepath);
    var myHeaders = new Headers();
    myHeaders.append("accept", "*/*");

    var formdata = new FormData();
    formdata.append("files", filepath);

    var requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: formdata,
      redirect: "follow",
    };
    fetch(
      "http://localhost:58659/api/Documents/upload/" +
        folderid +
        "/" +
        userid +
        "/" +
        uploadT.toISOString(),
      requestOptions
    )
      // .then(response => response.text())
      .then((result) => console.log(result));
  } catch (err) {
    console.log(err);
  }

  listFiles();
  location.reload();
}

function listFiles() {
  try {
    var filecreate = document.getElementById("filemain");
    filecreate.innerHTML = "";
    fetch(
      "http://localhost:58659/api/Documents/" +
        sessionStorage.getItem("FolderId"),
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((files) => {
        console.log(files);

        files.forEach((file) => {
          //creating div for files
          var filecreate = document.getElementById("filemain");
          var fil = document.createElement("div");
          fil.setAttribute("class", "filecs");
          const filname = file.docName;
          const docid = file.docId;
          console.log(filname);
          console.log(docid);
          fil.innerHTML = `<a><i class="fa fa-folder fa-3x" aria-hidden="true" style="color:lightblue;position:absolute;"></i></a><br />
      <p style="color:black;font-weight:normal;font-size:15px;text-decoration: none;position: absolute;cursor: pointer; margin:45px 5px";overflow:hidden;">${filname}</p> 
      
      <i class="fa fa-trash fa-1.5px" onclick='swalfire(${docid})' style="position:relative;left:130px;bottom:-25px;top:45px;cursor:pointer;">
      </i>
      <i class="fa fa-info-circle" style="position:relative;right: 5px;bottom: 0px;top:23px;"></i>
      <i class="fa fa-star-o" onclick="favorites(${docid})" style="position:relative;left: 130px;bottom:90px;"></i>`;
          filecreate.appendChild(fil);
        });
      });
  } catch (err) {
    console.log(err);
  }
}

//alert box to delete a file
function swalfire(docid) {
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
        trashfile(docid),
        "Deleted!",
        "Your file has been deleted.",
        "success"
      );
    }
  });
}

function trashfile(file) {
  var raw = "";

  var requestOptions = {
    method: "PUT",
    body: raw,
    redirect: "follow",
  };

  fetch(
    "http://localhost:58659/api/Documents/FileTrash?id=" + file,
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
  sessionStorage.setItem("Documentid", file);
  location.reload();
}

// function to search folders
function search() {
  try {
    var searchcontent = document.getElementById("search").value;
    if (searchcontent == "") location.reload();
    var filecreate = document.getElementById("filemain");
    filecreate.innerHTML = "";
    fetch(
      `http://localhost:58659/api/Documents/Document/${dashid}/${searchcontent}`,
      {
        method: "GET",
      }
    )
      .then((response) => response.json())
      .then((files) => {
        console.log(files);
        files.forEach((file) => {
          var filecreate = document.getElementById("filemain");
          var fil = document.createElement("div");
          fil.setAttribute("class", "filecs");
          const filname = file.docName;
          const docid = file.docId;
          console.log(filname);
          fil.innerHTML = `<i class="fa fa-folder fa-3x" aria-hidden="true" style="color:lightblue;">
      <a  style="color:black;font-size:15px;text-decoration: none;position: absolute;cursor: pointer; margin:20px;">${filname}</a> 
      </i>
      <i class="fa fa-trash fa-1.5px" onclick="deletefolder(${docid})" style="position:relative;left: 200px;bottom: 1px;">
  </i>`;

          filecreate.appendChild(fil);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
