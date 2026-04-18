document.addEventListener('DOMContentLoaded', function () {
    const mobileBtn = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');

    mobileBtn?.addEventListener('click', function () {
        mobileMenu?.classList.toggle('open');
    });
});


document.getElementById('profileImage')?.addEventListener('change', function () {
    const placeholder = document.querySelector('.file-input-placeholder');
    if (placeholder) {
        placeholder.textContent = this.files[0]?.name ?? 'Upload Profile Image';
    }
});