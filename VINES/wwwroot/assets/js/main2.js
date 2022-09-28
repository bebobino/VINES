/**
* Template Name: eBusiness - v4.3.0
* Template URL: https://bootstrapmade.com/ebusiness-bootstrap-corporate-template/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/


(function () {
    "use strict";

    /**
     * Easy selector helper function
     */
    const select = (el, all = false) => {
        el = el.trim()
        if (all) {
            return [...document.querySelectorAll(el)]
        } else {
            return document.querySelector(el)
        }
    }

    /**
     * Easy event listener function
     */
    const on = (type, el, listener, all = false) => {
        let selectEl = select(el, all)
        if (selectEl) {
            if (all) {
                selectEl.forEach(e => e.addEventListener(type, listener))
            } else {
                selectEl.addEventListener(type, listener)
            }
        }
    }

    /**
     * Easy on scroll event listener 
     */
    const onscroll = (el, listener) => {
        el.addEventListener('scroll', listener)
    }



    /**
     * Navbar links active state on scroll
     */
    let navbarlinks = select('#navbar .scrollto', true)
    const navbarlinksActive = () => {
        let position = window.scrollY + 200
        navbarlinks.forEach(navbarlink => {
            if (!navbarlink.hash) return
            let section = select(navbarlink.hash)
            if (!section) return
            if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
                navbarlink.classList.add('active')
            } else {
                navbarlink.classList.remove('active')
            }
        })
    }
    window.addEventListener('load', navbarlinksActive)
    onscroll(document, navbarlinksActive)

    /**
     * Scrolls to an element with header offset
     */
    const scrollto = (el) => {
        let header = select('#header')
        let offset = header.offsetHeight

        if (!header.classList.contains('header-scrolled')) {
            offset -= 16
        }

        let elementPos = select(el).offsetTop
        window.scrollTo({
            top: elementPos - offset,
            behavior: 'smooth'
        })
    }

    /**
     * Toggle .header-scrolled class to #header when page is scrolled
     */
    let selectHeader = select('#header')
    if (selectHeader) {
        const headerScrolled = () => {
            if (window.scrollY > 100) {
                selectHeader.classList.add('header-scrolled')
            } else {
                selectHeader.classList.remove('header-scrolled')
            }
        }
        window.addEventListener('load', headerScrolled)
        onscroll(document, headerScrolled)
    }

    /**
     * Back to top button
     */
    let backtotop = select('.back-to-top')
    if (backtotop) {
        const toggleBacktotop = () => {
            if (window.scrollY > 100) {
                backtotop.classList.add('active')
            } else {
                backtotop.classList.remove('active')
            }
        }
        window.addEventListener('load', toggleBacktotop)
        onscroll(document, toggleBacktotop)
    }

    /**
     * Mobile nav toggle
     */
    on('click', '.mobile-nav-toggle', function (e) {
        select('#navbar').classList.toggle('navbar-mobile')
        this.classList.toggle('bi-list')
        this.classList.toggle('bi-x')
    })

    /**
     * Mobile nav dropdowns activate
     */
    on('click', '.navbar .dropdown > a', function (e) {
        if (select('#navbar').classList.contains('navbar-mobile')) {
            e.preventDefault()
            this.nextElementSibling.classList.toggle('dropdown-active')
        }
    }, true)

    /**
     * Scrool with ofset on links with a class name .scrollto
     */
    on('click', '.scrollto', function (e) {
        if (select(this.hash)) {
            e.preventDefault()

            let navbar = select('#navbar')
            if (navbar.classList.contains('navbar-mobile')) {
                navbar.classList.remove('navbar-mobile')
                let navbarToggle = select('.mobile-nav-toggle')
                navbarToggle.classList.toggle('bi-list')
                navbarToggle.classList.toggle('bi-x')
            }
            scrollto(this.hash)
        }
    }, true)

    /**
     * Scroll with ofset on page load with hash links in the url
     */
    window.addEventListener('load', () => {
        if (window.location.hash) {
            if (select(window.location.hash)) {
                scrollto(window.location.hash)
            }
        }
    });

    /**
     * Preloader
     */
    let preloader = select('#preloader');
    if (preloader) {
        window.addEventListener('load', () => {
            preloader.remove()
        });
    }

    /**
     * Hero carousel indicators
     */
    let heroCarouselIndicators = select("#hero-carousel-indicators")
    let heroCarouselItems = select('#heroCarousel .carousel-item', true)

    heroCarouselItems.forEach((item, index) => {
        (index === 0) ?
            heroCarouselIndicators.innerHTML += "<li data-bs-target='#heroCarousel' data-bs-slide-to='" + index + "' class='active'></li>" :
            heroCarouselIndicators.innerHTML += "<li data-bs-target='#heroCarousel' data-bs-slide-to='" + index + "'></li>"
    });

    /**
     * Porfolio isotope and filter
     */
    window.addEventListener('load', () => {
        let portfolioContainer = select('.portfolio-container');
        if (portfolioContainer) {
            let portfolioIsotope = new Isotope(portfolioContainer, {
                itemSelector: '.portfolio-item',
                layoutMode: 'fitRows'
            });

            let portfolioFilters = select('#portfolio-flters li', true);

            on('click', '#portfolio-flters li', function (e) {
                e.preventDefault();
                portfolioFilters.forEach(function (el) {
                    el.classList.remove('filter-active');
                });
                this.classList.add('filter-active');

                portfolioIsotope.arrange({
                    filter: this.getAttribute('data-filter')
                });
            }, true);
        }
    });

    /**
     * Initiate portfolio lightbox 
     */
    const portfolioLightbox = GLightbox({
        selector: '.portfolio-lightbox'
    });

    /**
  * Initiate glightbox 
  */
    const glightbox = GLightbox({
        selector: '.glightbox'
    });


    /**
     * Testimonials slider
     */
    new Swiper('.testimonials-slider', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.swiper-pagination',
            type: 'bullets',
            clickable: true
        }
    });

    $(window).on('load', function () {
        $('#cookies').modal('show');
    });


});


//Cueto Code
var mymap;

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        alert("Geolocation is not supported!");
    }
}
function showPosition(position) {
    mymap.setView([position.coords.latitude, position.coords.longitude], 15)
    var marker = L.marker([position.coords.latitude, position.coords.longitude]).addTo(mymap).bindPopup("<center><p>You</p></center>");

}

function addMarker(x, y, z, i) {
    var greenIcon = new L.Icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });
    var url = "https://maps.google.com?q="+y+","+x;
    var marker = L.marker([y, x], { icon: greenIcon }).addTo(mymap).bindPopup("<center><p>" + z + '</p></center><p></p><center><a href="'+url+'">'+i+"</p></center>");

}




function initializeMap() {

    
    mymap = L.map('vaxMap').setView([0, 0], 15);
    L.tileLayer('https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=993xdhHU78iIxlykv1dm'
        , { attribution: '<a href="https://www.maptiler.com/copyright/" target="_blank">&copy; MapTiler</a> <a href="https://www.openstreetmap.org/copyright" target="_blank">&copy; OpenStreetMap contributors</a>', }).addTo(mymap);
    
}



function showNotification(title, notifbody) {
    const notification = new Notification(title, {
        body: notifbody
    });

    notification.onclick = (e) => {
        window.location.href = "https://google.com"
    }
}

$(document).ready(function ()
{
    
    $("#newVac").click(function () {
        $("#newVaccine").modal('show');
    });
    $("#hideVac").click(function () {
        $("#newVaccine").modal('hide');
    });
    $("#forgot").click(function () {
        $("#forgotPass").modal('show');
    });
    $("#hideForgot").click(function () {
        $("#forgotPass").modal('hide');
    });
    $("#showSources").click(function () {
      $("#newsSources").modal('show');
    });
    $("#hideSources").click(function () {
      $("#newsSources").modal('hide');
    });
    $("#showInstitutions").click(function () {
      $("#institutions").modal('show');
    });
    $("#hideInstitutions").click(function () {
      $("#institutions").modal('hide');
    });
    $("#showVaccines").click(function () {
      $("#vaccines").modal('show');
    });
    $("#hideVaccines").click(function () {
      $("#vaccines").modal('hide');
    });
    $("#showPatients").click(function () {
        $("#patients").modal('show');
    });
    $("#hidePatients").click(function () {
        $("#patients").modal('hide');
    });
    $("#showAdvertisers").click(function () {
        $("#advertisers").modal('show');
    });
    $("#hideAdvertisers").click(function () {
        $("#advertisers").modal('hide');
    });

});

