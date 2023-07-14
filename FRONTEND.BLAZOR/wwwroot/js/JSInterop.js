/* Begin: Initialize Bootstrap Carousel */
function InitializeCarousel() {
    $(".mim-banner-carousel").carousel(
        {
            interval: 5000
        }
    );
}

var Coursel = {
    initializeHomePageCarousel: () => {
        $('.banner-img-4').owlCarousel({
            rtl: false,
            loop: true,
            margin: 10,
            nav: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 2
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 4
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
                    items: 1
                },
                1000: {
                    items: 2
                }
            }
        });
    }
}
/* End: Initialize Bootstrap Carousel */