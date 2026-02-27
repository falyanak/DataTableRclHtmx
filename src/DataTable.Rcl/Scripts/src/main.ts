import { initRclTablePersistence } from "./table-persistence.js";

const bootstrap = () => {
  console.log("🚀 DataTable RCL  Initialized");
  initRclTablePersistence();
};

if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", bootstrap);
} else {
  bootstrap();
}
