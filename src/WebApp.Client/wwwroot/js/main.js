import { initTabHandler } from './handlers/tabHandler.js';
// Utilisation de l'alias pour pointer vers la RCL
//import { initRclTablePersistence } from 'DataTable.Rcl/Scripts/src/tablePersistence';
const bootstrap = () => {
    console.log("🚀 Frontend Initialized");
    initTabHandler();
    //   initRclTablePersistence();
};
if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", bootstrap);
}
else {
    bootstrap();
}
