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
        $.each(result.data, function (i, field) {
          
          if(i>=50) {document.getElementById("loading").style.display = "none";return;}
          if(field.donusturulenFormat !="jp2" && field.donusturulenFormat !="tiff"){
            let image =
            '<div class="box">'
            +'<a href="/converted/'+field.foto +'" target="_blank">'+
            '<img src="/converted/'+field.foto +'" alt=""></a>'+
            '<a href="/converted/'+field.foto +'" target="_blank" download>'+
            '<div class="options"><img class="down-btn" src="./img/direct-download.svg" alt=""></a>'+
            '<a href="#" onclick=\'Delete('+field.id+',"'+field.foto+'","'+field.ipAdresi+'","'+field.response+'","'+field.istekResponseSure+'","'+field.yuklenenFormat+'","'+field.donusturulenFormat+'")\'><img class="down-btn" src="./img/delete.svg" alt=""></a></div><div class="options">'+field.yuklenenFormat.toUpperCase()+'-'+field.donusturulenFormat.toUpperCase()+'</div></div>';
          document.getElementById("content").innerHTML += image;
          }
          
        });
        document.getElementById("loading").style.display = "none";
      });
    });
    function Delete(id,foto,ipAdresi,response,istekResponseSure,yuklenenFormat,donusturulenFormat){
      document.getElementById("loading").style.display = "flex";
      var formdata = {};
      formdata.id = id;
      formdata.foto = foto;
      formdata.ipAdresi = ipAdresi;
      formdata.response = response;
      formdata.istekResponseSure = istekResponseSure;
      formdata.yuklenenFormat = yuklenenFormat;
      formdata.donusturulenFormat = donusturulenFormat;
      $.ajax({
    url: "/api/operations/delete",
    type: "post",
    headers: {
      Authorization: 'Bearer '+sessionStorage.getItem("Token")
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
  });
    }
    function logout(){
      sessionStorage.setItem("Login", "False");
      window.location.reload();
    }