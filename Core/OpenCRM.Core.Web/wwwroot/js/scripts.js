/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});

//var menuItem = document.querySelectorAll(".menu-item");

//for (var i = 0; i < menuItem.length; i++) {
//    var subMenu = menuItem[i];

//    subMenu.addEventListener('click', (e) => {
//        var name = e.target.innerHTML;
//        var getsubmenu = document.getElementsByClassName(name);
//        var copygetsubmenu = getsubmenu[0].cloneNode(true);
//        copygetsubmenu.lastChild.classList.remove("show");

//        var showsubmenu = document.getElementById("dropdown-submenu");
//        showsubmenu.innerHTML = copygetsubmenu.innerHTML;
//    });
//}