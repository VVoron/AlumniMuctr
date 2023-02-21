let btns = document.querySelectorAll("*[data-modal-btn]");

for (let i = 0; i < btns.length; i++) {
    btns[i].addEventListener('click', function () {
        let name = btns[i].getAttribute('data-modal-btn');
        let modal = document.querySelector("[data-modal-window='" + name + "']");
        modal.style.display = "block";
        document.getElementById("temp").classList.add("body-stop-scroll");
        let close = modal.querySelector(".close_modal_window");
        close.addEventListener('click', function () {
            modal.style.display = "none";
            document.getElementById("temp").classList.remove("body-stop-scroll");
        });
    });
}

window.onclick = function (event) {
    if (event.target.hasAttribute('data-modal-window')) {
        let modals = document.querySelectorAll('*[data-modal-window]');
        for (let i = 0; i < modals.length; i++) {
            modals[i].style.display = "none";
            document.getElementById("temp").classList.remove("body-stop-scroll");
        }
    }
}

const imgChecked = document.querySelector('.icon_show');
const imgNotChecked = document.querySelector('.icon_close');

var category = 0; //0 - all, 1 - events, 2 - jobs, 3 - anouncments

function filterFunSaturdayTable() {
    var filter = document.querySelector("select").value;
    var table = document.querySelector("table");
    var tr = table.getElementsByTagName("tr");
    for (var i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[2];
        if (td.classList == filter || filter == "Любой") {
            tr[i].classList.remove("hidden");
        }
        else if (!tr[i].classList.contains("hidden"))
            tr[i].classList.add("hidden");
    }
}

/*function openRegForm() {
    document.getElementById("funreg").classList.remove("hidden");
    document.getElementById("overlay").classList.add("active");
    document.getElementById("temp").classList.add("body-stop-scroll");
    setTimeout(() => { document.getElementById("back").addEventListener("click", closeRegForm); }, 200);
}

function closeRegForm() {
    document.getElementById("funreg").classList.add("hidden");
    document.getElementById("overlay").classList.remove("active");
    document.getElementById("temp").classList.remove("body-stop-scroll");
    document.getElementById("back").removeEventListener("click", closeRegForm);
}*/

function filterRegs(filter) {
    document.querySelector(".filter.active").classList.remove("active");
    document.querySelector("." + filter).classList.add("active");

    var btns = document.querySelectorAll(".btn-secondary");
    btns.forEach(element => {
        if (!element.classList.contains("hidden"))
            element.classList.add("hidden")
    });
    document.querySelector(".btn." + filter).classList.remove("hidden");

    var table = document.querySelector("table");
    var tr = table.getElementsByTagName("tr");
    for (var i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td.className == "false" && filter == "verify") {
            if (!tr[i].classList.contains("hidden"))
                tr[i].classList.add("hidden");
        } else if (td.className == "true" && filter == "not-verify") {
            if (!tr[i].classList.contains("hidden"))
                tr[i].classList.add("hidden");
        } else {
            tr[i].classList.remove("hidden");
        }
    }
}

function reveal() {
    var reveals = document.querySelectorAll(".main-block");
    for (var i = 0; i < reveals.length; i++) {
        var windowHeight = window.innerHeight;
        var elementTop = reveals[i].getBoundingClientRect().top;
        var elementVisible = 100;
        if (elementTop < windowHeight - elementVisible) {
            reveals[i].classList.add("active");
        } else {
            reveals[i].classList.remove("active");
        }
    }
};

function navigationscroll() {
    var nav = document.getElementById("main-nav");
    var footer = document.getElementById("info");
    var windowHeight = window.innerHeight;
    var elementTop = footer.getBoundingClientRect().top;
    if (elementTop < windowHeight) {
        nav.classList.add('hidden');
    } else {
        nav.classList.remove('hidden');
    }
}

function changeActive(element) {
    var ids = ["all", "events", "jobs", "announcements"]

    for (var i = 0; i < ids.length; i++) {
        if (element.id === ids[i]) {
            element.classList.add("active-category");
            category = i;
        } else {
            var el = document.getElementById(ids[i]);
            el.classList.remove("active-category");
        }
    }
    newsFilter();
}

function newsFilter() {
    var elList = document.querySelectorAll(".news-block");
    for (var i = 0; i < elList.length; i++) {
        if (!elList[i].classList.contains("hidden"))
            elList[i].classList.add("hidden");
    }
    if (category == 0) {
        for (var i = 0; i < 3; i++) {
            elList[i].classList.remove("hidden");
        }
    } else {
        var numel = 0;
        for (var i = 0; i < elList.length; i++) {
            if (elList[i].classList.contains(category)) {
                elList[i].classList.remove("hidden");
                numel++;
            }
            if (numel == 3) {
                break;
            }
        }
    }
    document.getElementById("more").classList.remove("hidden");
}

function getYears(id) {
    for (let year = 1920; year <= Date().getFullYear(); year++) {
        let options = document.createElement("OPTION");
        document.getElementById(id).appendChild(options).innerHTML = year;
    }
}

/*function showNews(element) {
    var newsBlock = element.nextElementSibling;
    var bg = document.getElementById("dark-background")
    var body = document.getElementById("temp");
    var overlay = document.getElementById("overlay");
    newsBlock.classList.add("active");
    newsBlock.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
    bg.classList.add("backgr");
    body.classList.add("body-stop-scroll");
    overlay.classList.add("active");
}

function closeNews(element) {
    var newsBlock = element.parentElement;
    var body = document.getElementById("temp");
    var overlay = document.getElementById("overlay");
    newsBlock.classList.remove("active");
    body.classList.remove("body-stop-scroll");
    overlay.classList.remove("active");
}*/

function showProgramm(element) {
    var programmBlock = element.nextElementSibling;
    var body = document.getElementById("temp");
    var overlay = document.getElementById("overlay2");

    programmBlock.classList.add("active");
    programmBlock.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
    body.classList.add("body-stop-scroll");
    overlay.classList.add("active");
}

function closeProgramm(element) {
    var programmBlock = element.parentElement;
    var body = document.getElementById("temp");
    var overlay = document.getElementById("overlay2");
    programmBlock.classList.remove("active");
    body.classList.remove("body-stop-scroll");
    overlay.classList.remove("active");
}

window.addEventListener("scroll", reveal);
window.addEventListener("scroll", navigationscroll);

window.onload = function () {
    $(document).ready(function () {
        $(".owl-carousel").owlCarousel({
            loop: true,
            margin: 50,
            autoplay: true,
            smartSpeed: 1000,
            autoplayTimeout: 3000,
            autoplayHoverPause: true
        });
        document.querySelector(".loader").classList.add("hidden");
    });

    var $button = document.querySelector('.btn_support'),
        $button_close = document.querySelector('.top_btn_support'),
        $container = document.querySelector('.contact_form');
    $show = document.querySelector('.icon_show');
    $close = document.querySelector('.icon_close');

    $button.addEventListener('click', function (e) {
        var isVisible = $container.style.display == 'block';

        $container.style.display = isVisible ? 'none' : 'block';
        $close.style.display = isVisible ? 'none' : 'block';
        $show.style.display = isVisible ? 'block' : 'none';
        $button.style.backgroundColor = isVisible ? "#27a0f0" : "#fff";

    });
    $button_close.addEventListener('click', function (e) {
        var isVisible = $container.style.display == 'block';
        $container.style.display = isVisible ? 'none' : 'block';
        $close.style.display = isVisible ? 'none' : 'block';
        $show.style.display = isVisible ? 'block' : 'none';
        $button.style.backgroundColor = isVisible ? "#27a0f0" : "#fff";
    });

   /* document.getElementById("reg-btn").onclick = function () {
        var el = document.querySelectorAll(".reg-form")[0];
        var body = document.getElementById("temp");
        el.classList.remove("reg-close-animation");
        el.classList.add("display-flex");
        body.classList.add("body-stop-scroll");
    };

    document.getElementById("close-btn").addEventListener("click", function () {
        var el = document.querySelectorAll(".reg-form")[0];
        var body = document.getElementById("temp");
        el.classList.add("reg-close-animation");
        body.classList.remove("body-stop-scroll");
        setTimeout(() => { el.classList.remove("display-flex"); }, 1300);

display: flex !important;
    position: fixed !important;
    z-index: 100;
    overflow-y: scroll;
    background-color: rgba(102, 102, 102, 0.5);
    width: 100%;
    height: 100%;
    justify-content: space-around;
    animation: regStart 1.3s ease;




    });*/








    /*$(document).ready(function ($) {
        $('.popup-open-reg').click(function () {
            $('.popup-fade').fadeIn();
            document.getElementById("temp").classList.add("body-stop-scroll");
            return false;
        });

        $('.reg-close').click(function () {
            $(this).parents('.popup-fade').fadeOut();
            document.getElementById("temp").classList.remove("body-stop-scroll");
            return false;
        });

        $(document).keydown(function (e) {
            if (e.keyCode === 27) {
                e.stopPropagation();
                $('.popup-fade').fadeOut
            }
        });

        $('.popup-fade').click(function (e) {
            if ($(e.target).closest('.reg-container').length == 0) {
                $(this).fadeOut();
                document.getElementById("temp").classList.remove("body-stop-scroll");
            }
        });



    });*/

    document.getElementById('more').onclick = function () {
        var showPerClick = 3;

        var hidden = this.parentNode.querySelectorAll('.hidden');

        var countOfNextEl = 0;
        for (var i = 0; i < hidden.length; i++) {
            if (!hidden[i]) {
                this.classList.add("hidden");
                break;
            }

            if (category == 0 || hidden[i].classList.contains(category)) {
                if (showPerClick != 0) {
                    hidden[i].classList.add('news-block');
                    hidden[i].classList.remove('hidden');
                    showPerClick--;
                }
                else {
                    countOfNextEl++;
                }
            }
        }

        if (countOfNextEl == 0) {
            this.classList.add("hidden");
        }
    };
}




document.addEventListener('DOMContentLoaded', function () {
    const modalController = ({ modal, btnOpen, btnClose, time = 300 }) => {
        const buttonElems = document.querySelectorAll(btnOpen);
        const modalElem = document.querySelector(modal);

        modalElem.style.cssText = `
    display: flex;
    visibility: hidden;
    opacity: 0;
    transition: opacity ${time}ms ease-in-out;
  `;

        const closeModal = event => {
            const target = event.target;

            if (
                target === modalElem ||
                (btnClose && target.closest(btnClose)) ||
                event.code === 'Escape'
            ) {

                modalElem.style.opacity = 0;
                document.getElementById("temp").classList.remove("body-stop-scroll");
                setTimeout(() => {
                    modalElem.style.visibility = 'hidden';
                }, time);

                window.removeEventListener('keydown', closeModal);
            }
        }

        const openModal = () => {
            document.getElementById("temp").classList.add("body-stop-scroll");
            modalElem.style.visibility = 'visible';
            modalElem.style.opacity = 1;
            window.addEventListener('keydown', closeModal)
        };

        buttonElems.forEach(btn => {
            btn.addEventListener('click', openModal);
        });

        modalElem.addEventListener('click', closeModal);
    };

    modalController({
        modal: '.modal_reg',
        btnOpen: '.section__button_reg',
        btnClose: '.modal__close',
    });

    modalController({
        modal: '.modal_reg_anons',
        btnOpen: '.modal_reg_anons_open',
        btnClose: '.modal_reg__close'
    });
}, false);




