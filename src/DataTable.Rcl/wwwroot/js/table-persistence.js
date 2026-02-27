/**
 * Gestion de la persistance de sélection pour la DataTable RCL
 */
export const initRclTablePersistence = () => {
    const STORAGE_KEY = `rcl_select_${window.location.pathname}`;
    // 1. Gestion du clic (Délégation sur le body pour survivre aux swaps HTMX)
    document.body.addEventListener('click', (evt) => {
        const target = evt.target;
        const row = target.closest('tr[data-id]');
        if (row) {
            const id = row.getAttribute('data-id');
            if (id) {
                localStorage.setItem(STORAGE_KEY, id);
                applyHighlight(id);
            }
        }
    });
    // 2. Restauration après mise à jour HTMX
    document.body.addEventListener('htmx:afterOnLoad', (evt) => {
        if (evt.detail.target.classList.contains('rcl-table-wrapper')) {
            const savedId = localStorage.getItem(STORAGE_KEY);
            if (savedId)
                applyHighlight(savedId);
        }
    });
    // 3. Initialisation au premier chargement
    const initialId = localStorage.getItem(STORAGE_KEY);
    if (initialId)
        applyHighlight(initialId);
};
const applyHighlight = (id) => {
    document.querySelectorAll('.rcl-table tr.table-active').forEach(r => r.classList.remove('table-active'));
    const row = document.querySelector(`.rcl-table tr[data-id="${id}"]`);
    if (row)
        row.classList.add('table-active');
};
