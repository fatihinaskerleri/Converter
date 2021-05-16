$(document).ready(function(){
	$('#book_pick_date, #book_off_date, #tarih3').datepicker({});
	
	$(window).scroll(function(){
		$('.bolum2, .bolum3, .bolum4, .bolum5, .bolum6, .bolum7, footer').each(function(){
			var ustKenar = $(this).offset().top;
			var pencereAlt = $(window).scrollTop()+$(window).height();
			if(pencereAlt > ustKenar){
				$(this).animate({'opacity':'1'},1000);
			}
		})
	})
	
});
function jpeg(){
    window.localStorage.setItem('currentExtension', 'jpeg');
    window.location.href = "convert.html";
}
function png(){
    window.localStorage.setItem('currentExtension', 'png');
    window.location.href = "convert.html";
}
function gif(){
    window.localStorage.setItem('currentExtension', 'gif');
    window.location.href = "convert.html";
}
function tiff(){
    window.localStorage.setItem('currentExtension', 'tiff');
    window.location.href = "convert.html";
}
function jp2(){
    window.localStorage.setItem('currentExtension', 'jp2');
    window.location.href = "convert.html";
}
function webp(){
    window.localStorage.setItem('currentExtension', 'webp');
    window.location.href = "convert.html";
}