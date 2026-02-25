export const initTabHandler = (): void => {
    document.body.addEventListener('htmx:beforeSend', (evt: any) => {
        const target = evt.target as HTMLElement;
        if (target.classList.contains('tab-btn')) {
            // Désactiver tous les onglets du groupe
            const container = target.closest('.nav-tabs') || target.closest('.nav-pills');
            if (container) {
                container.querySelectorAll('.tab-btn').forEach(btn => 
                    btn.classList.remove('active')
                );
            }
            // Activer l'onglet cliqué
            target.classList.add('active');
        }
    });
};