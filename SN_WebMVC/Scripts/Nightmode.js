var btn_nightmode = document.getElementById("btn-nightmode")
var btn_lightmode = document.getElementById("btn-lightmode")

var html = document.querySelector("html")
var body = document.querySelector("body")
var bodyContent = document.querySelector(".body-content")
var container = document.querySelector(".container")
var sidebar = document.querySelector(".wa-sidebar")
var navbar = document.querySelector(".navbar")

var i = 0


btn_nightmode.addEventListener('click', function () {

    html.classList.add('nightmode')
    body.classList.add('nightmode')
    bodyContent.classList.add('nightmode')

    btn_nightmode.classList.add('btn-nightmode-none')
    btn_lightmode.classList.remove('btn-nightmode-none')

    sidebar.classList.add("nightmode")
    navbar.classList.add("navbar-nightmode")

    while (i == 0) {
        alert('em desenvolvimento')
        i++;
    }

})

btn_lightmode.addEventListener('click', function () {

    html.classList.remove('nightmode')
    body.classList.remove('nightmode')
    bodyContent.classList.remove('nightmode')

    btn_lightmode.classList.add('btn-nightmode-none')
    btn_nightmode.classList.remove('btn-nightmode-none')

    sidebar.classList.remove("nightmode")
    navbar.classList.remove("navbar-nightmode")

})