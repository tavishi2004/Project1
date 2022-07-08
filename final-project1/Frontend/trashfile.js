function onLoad() {
  listFiles();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "    Hi" + " " + sessionStorage.getItem("UserName");
}
onLoad();

function listFiles() {
  try {
    var filecreate = document.getElementById("filemain");
    filecreate.innerHTML = "";
    fetch(
      "http://localhost:58659/api/Documents/TrashDocId?id=" +
        sessionStorage.getItem("FolderId"),
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
      <a  style="font-size:15px; color:black;font-weight:normal;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${filname}</a> 
      </i>
      <i class="fa fa-trash fa-1.5px" onclick="trashfile(${docid})" style="position:relative;left:130px;bottom:-15px;cursor:pointer;">
  </i>`;

          filecreate.appendChild(fil);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
