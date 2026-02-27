import { initRclTablePersistence } from "./table-persistence.js";
const bootstrap = () => {
    console.log("🚀 DataTable RCL start initialization");
    initRclTablePersistence();
     console.log("🚀 DataTable RCL  Initialized");
}
if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", bootstrap);
}
else {
    bootstrap();
}
