const imgChecked = document.querySelector('.icon_show');
const imgNotChecked = document.querySelector('.icon_close');

var category = 0; //0 - all, 1 - events, 2 - jobs, 3 - anouncments

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

window.addEventListener("scroll", reveal);
window.addEventListener("scroll", navigationscroll);

window.onload = function () {
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

    document.getElementById("reg-btn").onclick = function () {
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
    });

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
