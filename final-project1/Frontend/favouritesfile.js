function onLoad() {
  listfavFiles();
  var admin = document.getElementById("usertext");
  admin.innerHTML = "    Hi" + " " + sessionStorage.getItem("UserName");
}
onLoad();

function listfavFiles() {
  try {
    var filecreate = document.getElementById("filemain");
    filecreate.innerHTML = "";
    fetch(
      "http://localhost:58659/api/Documents/favfolderfile?id=" +
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
          fil.innerHTML = `<i class="fa fa-folder fa-3x" aria-hidden="true">
     <a  style="font-size:20px;text-decoration: none;position: absolute;cursor: pointer; margin:20px">${filname}</a> 
     </i>
     <i class="fa fa-trash fa-1.5px" onclick="trashfile(${docid})" style="position:relative;left: 200px;bottom: 1px;">
 </i>`;

          filecreate.appendChild(fil);
        });
      });
  } catch (err) {
    console.log(err);
  }
}
