$(document).ready(function () {
    
    if(sessionStorage.getItem("Login") == "True" )
    {
        var currentDate = new Date();
        var expirationDate = sessionStorage.getItem("Expiration");
        if(Date.parse(currentDate) > Date.parse(expirationDate))
        {
            window.location.href = "/admin";
            return 1;
        }
        
    }
    else{
        window.location.href = "/admin";
        return 1;
    }

    $.getJSON("/api/operations/getall", function (result) {
      document.getElementById("title").innerHTML = result.data.length + " işlem listelendi";
        $.each(result.data.reverse(), function (i, field) {
          
          if(i>=50) {document.getElementById("loading").style.display = "none";return;}
          if(field.donusturulenFormat !="jp2" && field.donusturulenFormat !="tiff"){
            let image =
            '<div class="box">'
            +'<a href="/converted/'+field.foto +'" target="_blank">'+
            '<img src="/converted/'+field.foto +'" alt=""></a>'+
            '<a href="/converted/'+field.foto +'" target="_blank" download>'+
            '<div class="options"><img class="down-btn" src="./img/direct-download.svg" alt=""></a>'+
            '<a href="#" onclick=\'Delete('+field.id+')\'><img class="down-btn" src="./img/delete.svg" alt=""></a></div><div class="options">'+field.yuklenenFormat.toUpperCase()+'-'+field.donusturulenFormat.toUpperCase()+'</div></div>';
          document.getElementById("content").innerHTML += image;
          }
          
        });
        document.getElementById("loading").style.display = "none";
      });
    });
    function Delete(id){
      document.getElementById("loading").style.display = "flex";
      
      var url = "/api/operations/delete";

      var xhr = new XMLHttpRequest();
      xhr.open("POST", url);
      xhr.setRequestHeader("Accept", "application/json");
      xhr.setRequestHeader("Content-Type", "application/json");
      xhr.setRequestHeader("Authorization", "Bearer "+sessionStorage.getItem("Token"));
      xhr.onreadystatechange = function () {
      document.getElementById("loading").style.display = "none";
        if (xhr.readyState === 4) {
            
            var response = JSON.parse(xhr.response);
            
            if(response.success)
            {
              window.location.reload();
            }
            else{
              console.log(xhr.status);
              console.log(xhr.responseText);
              alert(xhr.responseText);
            }
            
            
        }};

      xhr.send('{"Id": '+id+'}');
      

      /*$.ajax({
    url: "/api/operations/delete",
    type: "post",
    beforeSend: function (xhr) {
      xhr.setRequestHeader('Authorization', 'Bearer '+sessionStorage.getItem("Token"));
  },
    
    data: JSON.stringify(formdata),
    contentType: "application/json",
    processData: false,
    success: function (response) {
       console.log(response)
    },
    error: function(XMLHttpRequest, textStatus, errorThrown) {
      console.log(XMLHttpRequest);
      alert(XMLHttpRequest.responseText);
      document.getElementById("loading").style.display = "none";
   }
  });*/
    }
    function logout(){
      sessionStorage.setItem("Login", "False");
      window.location.reload();
    }
    function navbar(){
      document.getElementById("navbarSupportedContent").classList.toggle("collapse");
    }