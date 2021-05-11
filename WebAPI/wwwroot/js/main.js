var ServerIp = "https://localhost:44331";
var slideIndex = 1;


function plusSlides(n) {
  showSlides(slideIndex += n);
}

function currentSlide(n) {
  showSlides(slideIndex = n);
}

function showSlides(n) {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("dot");
  if (n > slides.length) {slideIndex = 1}    
  if (n < 1) {slideIndex = slides.length}
  for (i = 0; i < slides.length; i++) {
      slides[i].style.display = "none";  
  }
  for (i = 0; i < dots.length; i++) {
      dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "flex";  
  
}
$(document).ready(function () {
  
  var counter = 0;
  $.getJSON(ServerIp + "/api/operations/Getall", function (result) {
    $.each(result.data.reverse(), function (i, field) {
      if(field.donusturulenFormat !="jp2" && field.donusturulenFormat !="tiff"  ){

        if(counter >=4)
        {
          document.getElementById("loading").style.display = "none";
          document.getElementsByClassName("controls")[0].style.display = "block";
          document.getElementsByClassName("controls")[1].style.display = "block";
          showSlides(slideIndex);
          return;
        }
        counter++;
        //document.getElementById("loading").style.display = "none";
        let slide =
          '<div class="mySlides fade"><img src="/converted/'+field.foto+'" style="width:100%"></div>';
  
        document.getElementById("light-slider").innerHTML = slide+ document.getElementById("light-slider").innerHTML;
        
      }
      
    });
    document.getElementById("loading").style.display = "none";
    document.getElementsByClassName("controls")[0].style.display = "block";
    document.getElementsByClassName("controls")[1].style.display = "block";
    showSlides(slideIndex);
  });

  });

function upload() {
  if(document.getElementById("extension").value != 0)
  {
    
    document.getElementById("image").click();
  }
  else{
    alert("Önce dönüştürülecek türü seçmelisiniz!")
  }
}

function submitForm() {
  document.getElementsByClassName("download")[0].classList.add("hidden");
  var formdata = new FormData();
  var files = $("#image")[0].files;
  let extension = document.getElementById("extension").value
  // Check file selected or not
  if (files.length > 0 && document.getElementById("extension").value != 0) {
    document.getElementById("loading").style.display = "flex";
    formdata.append("file", files[0]);
    formdata.append("DonusturulenFormat",extension);
    $.ajax({
      url: "/api/operations/upload",
      data: formdata,
      cache: false,
      contentType: false,
      processData: false,
      method: 'POST',
      type: 'POST',
      success: function (response) {
        console.log(response)
        if (response.success) {
          document.getElementById("download").href=response.data;
          document.getElementById("converted").innerHTML = "&nbsp;"+extension+"&nbsp;uzantısına&nbsp;";
          
          setTimeout(function(){ document.getElementsByClassName("download")[0].classList.remove("hidden"); document.getElementById("loading").style.display = "none"; }, 1000);
        } else {
          alert("Dosya yüklenemedi.");
          document.getElementById("loading").style.display = "none";
        }
      },
      error: function(XMLHttpRequest, textStatus, errorThrown) {
        alert(XMLHttpRequest.responseText);
        document.getElementById("loading").style.display = "none";
     }
    });
  } else {
    alert("Lütfen bir dosya seçin.");
    document.getElementById("loading").style.display = "none";
  }
}


