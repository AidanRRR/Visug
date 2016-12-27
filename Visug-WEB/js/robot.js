/**
 * Created by aidan on 27/12/2016.
 */

var blnClick=false;
var timeInterval=2500;

function onHover() {}
function offHover() {}

function startRobot() {
    window.setInterval(function(){
        switchImage();
    }, timeInterval);

}

function switchImage() {
    if (blnClick==false) {
        blnClick=true;
        var animation = 3;
        while (animation==3) {
            var animation = 1 + Math.floor(Math.random() * 5);
        }
        $('#robotImg').attr('src', './img/robot/ROBOT_PNG_0' + animation + '.png');
    } else {
        blnClick=false;
        $('#robotImg').attr('src', './img/robot/ROBOT_PNG_03.png');
    }
}
