import { initTabHandler } from './handlers/tabHandler';

const bootstrap = () => {
    console.log("🚀 Frontend Initialized (TypeScript + HTMX)");
    initTabHandler();

    console.log("Test de compilation automatique");
};

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", bootstrap);
} else {
    bootstrap();
}