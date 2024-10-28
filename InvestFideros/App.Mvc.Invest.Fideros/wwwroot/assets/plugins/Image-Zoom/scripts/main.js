

function rtlZoomImage() {

  $('.show').zoomImage();
  $('.show-small-img:first-of-type').css({'border': 'solid 1px #bfcef9', 'padding': '2px', 'border-radius': '5px'})
  $('.show-small-img:first-of-type').attr('alt', 'now').siblings().removeAttr('alt')
  $('.show-small-img').click(function () {
    $('#show-img').attr('src', $(this).attr('src'))
    $('#big-img').attr('src', $(this).attr('src'))
    $(this).attr('alt', 'now').siblings().removeAttr('alt')
    $(this).css({'border': 'solid 1px #bfcef9', 'padding': '2px', 'border-radius': '5px'}).siblings().css({'border': 'none', 'padding': '0', 'border-radius': '0'})
    if ($('#small-img-roll').children().length > 4) {
      if ($(this).index() >= 3 && $(this).index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('right', -($(this).index() - 2) * 76 + 'px')
      } else if ($(this).index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('right', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('right', '0')
      }
    }
  })
  // Click'>' next
  $('#next-img').click(function (){
    $('#show-img').attr('src', $(".show-small-img[alt='now']").next().attr('src'))
    $('#big-img').attr('src', $(".show-small-img[alt='now']").next().attr('src'))
    $(".show-small-img[alt='now']").next().css({'border': 'solid 1px #bfcef9', 'padding': '2px'}).siblings().css({'border': 'none', 'padding': '0'})
    $(".show-small-img[alt='now']").next().attr('alt', 'now').siblings().removeAttr('alt')
    if ($('#small-img-roll').children().length > 4) {
      if ($(".show-small-img[alt='now']").index() >= 3 && $(".show-small-img[alt='now']").index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('right', -($(".show-small-img[alt='now']").index() - 2) * 76 + 'px')
      } else if ($(".show-small-img[alt='now']").index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('right', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('right', '0')
      }
    }
  })
  // Click'<' to previous
  $('#prev-img').click(function (){
    $('#show-img').attr('src', $(".show-small-img[alt='now']").prev().attr('src'))
    $('#big-img').attr('src', $(".show-small-img[alt='now']").prev().attr('src'))
    $(".show-small-img[alt='now']").prev().css({'border': 'solid 1px #bfcef9', 'padding': '2px'}).siblings().css({'border': 'none', 'padding': '0'})
    $(".show-small-img[alt='now']").prev().attr('alt', 'now').siblings().removeAttr('alt')
    if ($('#small-img-roll').children().length > 4) {
      if ($(".show-small-img[alt='now']").index() >= 3 && $(".show-small-img[alt='now']").index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('right', -($(".show-small-img[alt='now']").index() - 2) * 76 + 'px')
      } else if ($(".show-small-img[alt='now']").index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('right', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('right', '0')
      }
    }
  })
  
}

function ltrZoomImage() {

  $('.show').zoomImage();
  $('.show-small-img:first-of-type').css({'border': 'solid 1px #bfcef9', 'padding': '2px', 'border-radius': '5px'})
  $('.show-small-img:first-of-type').attr('alt', 'now').siblings().removeAttr('alt')
  $('.show-small-img').click(function () {
    $('#show-img').attr('src', $(this).attr('src'))
    $('#big-img').attr('src', $(this).attr('src'))
    $(this).attr('alt', 'now').siblings().removeAttr('alt')
    $(this).css({'border': 'solid 1px #bfcef9', 'padding': '2px', 'border-radius': '5px'}).siblings().css({'border': 'none', 'padding': '0', 'border-radius': '0'})
    if ($('#small-img-roll').children().length > 4) {
      if ($(this).index() >= 3 && $(this).index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('left', -($(this).index() - 2) * 76 + 'px')
      } else if ($(this).index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('left', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('left', '0')
      }
    }
  })
  // Click'>' next
  $('#next-img').click(function (){
    $('#show-img').attr('src', $(".show-small-img[alt='now']").next().attr('src'))
    $('#big-img').attr('src', $(".show-small-img[alt='now']").next().attr('src'))
    $(".show-small-img[alt='now']").next().css({'border': 'solid 1px #bfcef9', 'padding': '2px'}).siblings().css({'border': 'none', 'padding': '0'})
    $(".show-small-img[alt='now']").next().attr('alt', 'now').siblings().removeAttr('alt')
    if ($('#small-img-roll').children().length > 4) {
      if ($(".show-small-img[alt='now']").index() >= 3 && $(".show-small-img[alt='now']").index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('left', -($(".show-small-img[alt='now']").index() - 2) * 76 + 'px')
      } else if ($(".show-small-img[alt='now']").index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('left', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('left', '0')
      }
    }
  })
  // Click'<' to previous
  $('#prev-img').click(function (){
    $('#show-img').attr('src', $(".show-small-img[alt='now']").prev().attr('src'))
    $('#big-img').attr('src', $(".show-small-img[alt='now']").prev().attr('src'))
    $(".show-small-img[alt='now']").prev().css({'border': 'solid 1px #bfcef9', 'padding': '2px'}).siblings().css({'border': 'none', 'padding': '0'})
    $(".show-small-img[alt='now']").prev().attr('alt', 'now').siblings().removeAttr('alt')
    if ($('#small-img-roll').children().length > 4) {
      if ($(".show-small-img[alt='now']").index() >= 3 && $(".show-small-img[alt='now']").index() < $('#small-img-roll').children().length - 1){
        $('#small-img-roll').css('left', -($(".show-small-img[alt='now']").index() - 2) * 76 + 'px')
      } else if ($(".show-small-img[alt='now']").index() == $('#small-img-roll').children().length - 1) {
        $('#small-img-roll').css('left', -($('#small-img-roll').children().length - 4) * 76 + 'px')
      } else {
        $('#small-img-roll').css('left', '0')
      }
    }
  })
  
}

// To check and add the corresponding calendar output according to direction(ltr, rtl)
(function checkRtl(){
  let bodyRtl = $('body').hasClass('rtl');
  if (bodyRtl) {
    rtlZoomImage();
  }
  else{
    ltrZoomImage();

  }
})();

$('#myonoffswitch55').click(function() {
	rtlZoomImage();
});

$('#myonoffswitch54').click(function() {
	ltrZoomImage()
});