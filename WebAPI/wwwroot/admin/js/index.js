var ServerIp = "https://localhost:44331";
function Isempty(data)
{
  if(typeof(data) == 'number' || typeof(data) == 'boolean')
  { 
    return false; 
  }
  if(typeof(data) == 'undefined' || data === null)
  {
    return true; 
  }
  if(typeof(data.length) != 'undefined')
  {
    return data.length == 0;
  }
  var count = 0;
  for(var i in data)
  {
    if(data.hasOwnProperty(i))
    {
      count ++;
    }
  }
  return count == 0;
}
function submit() {
  document.getElementById("loading").style.display = "flex";
  var formdata = {};
  formdata.email= document.getElementById("mail").value;
  formdata.password= document.getElementById("pass").value;
  $.ajax({
    url: ServerIp + "/api/auth/login",
    type: "post",
    data: JSON.stringify(formdata),
    contentType: "application/json",
    processData: false,
    success: function (response) {
        
      if(Isempty(response.token) || response == "Kullanıcı bulunamadı")
      {
        alert(response);
      }
      else{
        sessionStorage.setItem("Login", "True");
        sessionStorage.setItem("Expiration", response.expiration);
        sessionStorage.setItem("Token", response.token);
        window.location.reload();
      }
    },
    error: function(XMLHttpRequest, textStatus, errorThrown) {
      alert(XMLHttpRequest.responseText);
      document.getElementById("loading").style.display = "none";
   }
  });
}
function navbar(){
	document.getElementById("navbarSupportedContent").classList.toggle("collapse");
}
$(document).ready(function () {
    
if(sessionStorage.getItem("Login") == "True" )
{
    var currentDate = new Date();
    var expirationDate = sessionStorage.getItem("Expiration");
    if(Date.parse(currentDate) < Date.parse(expirationDate))
    {
        window.location.href = "/admin/main.html"
    }
    //eksik kaldı devam edilecek!
    
}
else{
  document.getElementById("loading").style.display = "none";
}
});