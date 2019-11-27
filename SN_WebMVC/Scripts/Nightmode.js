var btn_nightmode = document.getElementById("btn-nightmode")
var btn_lightmode = document.getElementById("btn-lightmode")

var bodyContent = document.querySelector(".body-content")
var container = document.querySelector(".container")
var sidebar = document.querySelector(".wa-sidebar")
var navbar = document.querySelector(".navbar")

var i = 0


btn_nightmode.addEventListener('click', function () {

    bodyContent.classList.add('nightmode')

    btn_nightmode.classList.add('btn-nightmode-none')
    btn_lightmode.classList.remove('btn-nightmode-none')

    sidebar.classList.add("nightmode")
    navbar.classList.add("navbar-nightmode")

    while (i == 0) {
        alert('eu sei q ta incompleto cala a boca')
        i++;
    }

})

btn_lightmode.addEventListener('click', function () {

    bodyContent.classList.remove('nightmode')

    btn_lightmode.classList.add('btn-nightmode-none')
    btn_nightmode.classList.remove('btn-nightmode-none')

    sidebar.classList.remove("nightmode")
    navbar.classList.remove("navbar-nightmode")

})