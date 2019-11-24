var btn = document.getElementById('btn-resize-to-small')

var small = document.querySelectorAll(".wa-sidebar-list-small");
var full = document.querySelectorAll(".wa-sidebar-list-full");

btn.addEventListener('click', function () {

    // full
    for (var i = 0; i < small.length; i++) {
        if (small[i].style.display == "none") {
            small[i].style.display = "block"
        } else {
            small[i].style.display = "none"
        }
    }

    // small
    for (var i = 0; i < full.length; i++) {
        if (full[i].style.display == "block") {
            full[i].style.display = "none"
        } else {
            full[i].style.display = "block"
        }
    }
})
