import { initTabHandler } from './handlers/tabHandler.js';

const bootstrap = () => {
    console.log("🚀 Frontend Initialized");
    initTabHandler();
};

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", bootstrap);
} else {
    bootstrap();
}