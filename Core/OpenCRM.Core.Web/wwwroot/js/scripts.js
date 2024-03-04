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

var divBlock = document.getElementById("card-block");
var cardBlock = {
    Title: "Galaxy",
    Subtitle: "Puple Galaxy in Universe",
    Description: "A galaxy is a collection of gases, dust and billions of stars and their solar systems. The galaxy is held together by the force of gravity.",
    ImageUrl: "http://localhost:5005/media/e2a02caa-ff23-429f-86e7-d772c02a8840.jpg"
}
divBlock.innerHTML = `<div class="block-info">
                     <div class="block-text-info">
                        <h1 class="block-title">${cardBlock.Title}</h1>
                        <h2 class="block-subtitle">${cardBlock.Subtitle}</h2>
                        <p class="block-description">${cardBlock.Description}</p>
                     </div>
                    <img class="block-image" src="${cardBlock.ImageUrl}"/>
                  </div>`


console.log(divBlock.innerHTML)