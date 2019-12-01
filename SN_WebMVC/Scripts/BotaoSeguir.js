var btnSeguir = document.getElementById('btnSeguir')
var btnDesseguir = document.getElementById('btnDesseguir')


btnSeguir.addEventListener('click', function () {
    btnDesseguir.classList.remove('wa-input-none')
    btnSeguir.classList.add('wa-input-none')
})

btnDesseguir.addEventListener('click', function () {
    btnSeguir.classList.remove('wa-input-none')
    btnDesseguir.classList.add('wa-input-none')
})