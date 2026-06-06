// ═══════════════════════════════════════════════════════════
// CitasApp — UI Interactions
// ═══════════════════════════════════════════════════════════

document.addEventListener('DOMContentLoaded', function () {

    // ── Sidebar toggle (mobile) ────────────────────────────
    const sidebar = document.querySelector('.sidebar');
    const overlay = document.querySelector('.sidebar-overlay');
    const menuBtn = document.querySelector('.btn-mobile-menu');

    if (menuBtn) {
        menuBtn.addEventListener('click', function () {
            sidebar.classList.toggle('open');
            overlay?.classList.toggle('show');
        });
    }

    if (overlay) {
        overlay.addEventListener('click', function () {
            sidebar.classList.remove('open');
            overlay.classList.remove('show');
        });
    }

    // ── Modals ─────────────────────────────────────────────
    document.querySelectorAll('[data-modal-open]').forEach(function (btn) {
        btn.addEventListener('click', function () {
            const target = document.getElementById(btn.dataset.modalOpen);
            if (target) target.classList.add('show');
        });
    });

    document.querySelectorAll('[data-modal-close]').forEach(function (btn) {
        btn.addEventListener('click', function () {
            const modal = btn.closest('.modal-backdrop');
            if (modal) modal.classList.remove('show');
        });
    });

    document.querySelectorAll('.modal-backdrop').forEach(function (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target === modal) modal.classList.remove('show');
        });
    });

    // ── Toast auto-dismiss ─────────────────────────────────
    document.querySelectorAll('.toast').forEach(function (toast) {
        setTimeout(function () {
            toast.style.opacity = '0';
            toast.style.transform = 'translateX(20px)';
            toast.style.transition = 'all 300ms ease';
            setTimeout(function () { toast.remove(); }, 300);
        }, 4000);
    });

    // ── Delete confirmations ───────────────────────────────
    document.querySelectorAll('.btn-delete-confirm').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            if (!confirm('¿Estás seguro de que deseas eliminar este registro?')) {
                e.preventDefault();
            }
        });
    });

    // ── Keyboard shortcut: Cmd+K for search ────────────────
    document.addEventListener('keydown', function (e) {
        if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
            e.preventDefault();
            const searchInput = document.querySelector('.header-search input');
            if (searchInput) searchInput.focus();
        }
    });
});