// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// ============================================================
// Dark mode toggle
// Saves preference to localStorage so it persists on reload
// ============================================================

(function () {
    const html = document.documentElement;

    // Apply saved theme immediately on page load (avoids flash)
    const saved = localStorage.getItem('dsv-theme');
    if (saved === 'dark') {
        html.setAttribute('data-theme', 'dark');
    }

    function updateToggleUI(isDark) {
        // Desktop
        const icon = document.getElementById('theme-icon');
        const label = document.getElementById('theme-label');
        // Mobile
        const iconMobile = document.getElementById('theme-icon-mobile');

        if (icon) {
            icon.className = isDark ? 'bi bi-sun' : 'bi bi-moon-stars';
        }
        if (label) {
            label.textContent = isDark ? 'Light' : 'Dark';
        }
        if (iconMobile) {
            iconMobile.className = isDark ? 'bi bi-sun' : 'bi bi-moon-stars';
        }
    }

    function toggleTheme() {
        const isDark = html.getAttribute('data-theme') === 'dark';
        if (isDark) {
            html.removeAttribute('data-theme');
            localStorage.setItem('dsv-theme', 'light');
        } else {
            html.setAttribute('data-theme', 'dark');
            localStorage.setItem('dsv-theme', 'dark');
        }
        updateToggleUI(!isDark);
    }

    document.addEventListener('DOMContentLoaded', function () {
        // Set correct icon on load
        updateToggleUI(html.getAttribute('data-theme') === 'dark');

        // Desktop toggle
        const btn = document.getElementById('theme-toggle');
        if (btn) btn.addEventListener('click', toggleTheme);

        // Mobile toggle
        const btnMobile = document.getElementById('theme-toggle-mobile');
        if (btnMobile) btnMobile.addEventListener('click', toggleTheme);
    });
})();

document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('hamburger-btn');
    const menu = document.getElementById('mobileMenu');

    if (!btn || !menu) return;

    btn.addEventListener('click', function () {
        const isOpen = menu.classList.toggle('open');

        btn.setAttribute('aria-expanded', isOpen);
        menu.setAttribute('aria-hidden', !isOpen);
    });
});