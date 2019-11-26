var btn = document.getElementById('btn-resize')

var small = document.querySelectorAll(".wa-sidebar-list-small")
var full = document.querySelectorAll(".wa-sidebar-list-full")
var waSideBar = document.querySelector(".wa-sidebar")
var miniLogo = document.getElementById("mini-logo-sidebar")


btn.addEventListener('click', function () {

    // cresce
    for (var i = 0; i < small.length; i++) {
        if (small[i].style.display == "none") {
            small[i].style.display = "block"
            waSideBar.style.width = "6em"
            miniLogo.style.margin = "0"
            btn.style.marginBottom = "20px"
        } else {
            small[i].style.display = "none"
            waSideBar.style.width = "12em"
            miniLogo.style.margin = "20px 35px"
            btn.style.marginBottom = "0"
        }
    }

    // diminui
    for (var i = 0; i < full.length; i++) {
        if (full[i].style.display == "block") {
            full[i].style.display = "none"
        } else {
            full[i].style.display = "block"
        }
    }

    
})
