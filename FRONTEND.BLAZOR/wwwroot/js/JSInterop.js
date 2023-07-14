/* Begin: Initialize Bootstrap Carousel */
function InitializeCarousel() {
    $(".mim-banner-carousel").carousel(
        {
            interval: 5000
        }
    );
}

function initializeHomePageCarousel() {
    setTimeout(() => {
        $('.banner-img-1').owlCarousel({
            rtl: false,
            loop: true,
            margin: 10,
            nav: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1
                }
            }
        });

        $('.banner-img-2').owlCarousel({
            rtl: false,
            loop: true,
            margin: 10,
            nav: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                }
            }
        });

        $('.banner-img-3').owlCarousel({
            rtl: false,
            loop: true,
            margin: 10,
            nav: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1
                },
                400: {
                    items: 2
                },
                800: {
                    items: 3
                }
            }
        });

        $('.banner-img-4').owlCarousel({
            rtl: false,
            loop: true,
            margin: 10,
            nav: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1
                },
                300: {
                    items: 2
                },
                600: {
                    items: 3
                },
                900: {
                    items: 4
                }
            }
        });
    }, 200)

}
/* End: Initialize Bootstrap Carousel */