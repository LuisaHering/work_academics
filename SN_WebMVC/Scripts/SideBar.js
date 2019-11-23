var btn1 = document.getElementById('btn-resize-to-small')
var btn2 = document.getElementById('btn-resize-to-full')

btn1.addEventListener('click', function () {
    var small = document.querySelectorAll(".wa-sidebar-list-small"); 
    var full = document.querySelectorAll(".wa-sidebar-list-full");

    for (var i = 0; i <= small.length; i++) {
        small[i].style.display = "block";
    }

    for (var i = 0; i <= full.length; i++) {
        full[i].style.display = "none";
    }
})

btn2.addEventListener('click', function () {
    var small = document.querySelectorAll(".wa-sidebar-list-small");
    var full = document.querySelectorAll(".wa-sidebar-list-full");

    for (var i = 0; i <= small.length; i++) {
        small[i].style.display = "none";
    }

    for (var i = 0; i <= full.length; i++) {
        full[i].style.display = "block";
    }
})