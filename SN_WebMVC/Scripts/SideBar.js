// botoes de açao
var btnDiminui = document.getElementById('btn-resize1')
var btnCresce = document.getElementById('btn-resize2')

// elementos da sidebar
var waSideBar = document.getElementById("wa-sidebar-id")
var profilePic = document.querySelector(".wa-profile-pic")
var perfilBtn = document.querySelector(".wa-perfil-btn")
var listaTexto = document.querySelector(".wa-sidebar-list-grande")
var listaIcones = document.querySelector(".wa-sidebar-list-pequeno")
var waHr = document.querySelector(".wa-hr")
var miniLogo = document.getElementById("mini-logo-sidebar")

// body 
var bodyContent = document.querySelector(".body-content")

btnDiminui.addEventListener('click', function (event) {
    event.preventDefault();

    // diminui
    waSideBar.classList.remove("wa-sidebar-grande")
    waSideBar.classList.add("wa-sidebar-pequeno")

    profilePic.classList.remove("wa-profile-pic-grande")
    profilePic.classList.add("wa-profile-pic-pequeno")

    perfilBtn.classList.add("wa-perfil-btn-none")

    listaTexto.style.display = "none"
    listaIcones.style.display = "block"

    waHr.classList.remove("wa-hr-grande")
    waHr.classList.add("wa-hr-pequeno")

    miniLogo.classList.remove("wa-mini-logo-grande")
    miniLogo.classList.add("wa-mini-logo-pequeno")

    bodyContent.classList.remove("wa-body-content-margin12")
    bodyContent.classList.add("wa-body-content-margin6")

    btnDiminui.classList.add("wa-resize-btn-none")

    btnCresce.classList.remove("wa-resize-btn-none")

})


btnCresce.addEventListener('click', function (event) {
    event.preventDefault();

    // cresce
    waSideBar.classList.remove("wa-sidebar-pequeno")
    waSideBar.classList.add("wa-sidebar-grande")

    profilePic.classList.remove("wa-profile-pic-pequeno")
    profilePic.classList.add("wa-profile-pic-grande")

    perfilBtn.classList.remove("wa-perfil-btn-none")

    listaTexto.style.display = "block"
    listaIcones.style.display = "none"

    waHr.classList.remove("wa-hr-pequeno")
    waHr.classList.add("wa-hr-grande")

    miniLogo.classList.remove("wa-mini-logo-pequeno")
    miniLogo.classList.add("wa-mini-logo-grande")

    bodyContent.classList.remove("wa-body-content-margin6")
    bodyContent.classList.add("wa-body-content-margin12")

    btnCresce.classList.add("wa-resize-btn-none")

    btnDiminui.classList.remove("wa-resize-btn-none")

})
