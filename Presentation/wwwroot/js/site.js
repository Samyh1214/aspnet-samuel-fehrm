document.addEventListener('DOMContentLoaded', function () {
    const mobileBtn = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');

    mobileBtn?.addEventListener('click', function () {
        mobileMenu?.classList.toggle('open');
    });
});