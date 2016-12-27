/**
 * Created by aidan on 27/12/2016.
 */

$( document ).ready(function() {
    $('#chatwindow').hide();
});

function scrollToChatWindow() {
    $('#chatwindow').fadeIn(2000);

    $('html, body').animate({
        scrollTop: $('#chatwindow').offset().top
    }, 2000);
}
