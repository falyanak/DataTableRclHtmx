# Mon Projet .NET 8 + HTMX + TypeScript

## 🚀 Démarrage rapide
1. Clonez le dépôt.
2. Ouvrez dans **VS Code**.
3. Cliquez sur **"Reopen in Container"** (nécessite Docker).
4. Appuyez sur **F5** pour lancer l'application.

## 🛠 Architecture
- **src/WebApp.Client** : Application principale ASP.NET Core.
- **DataTable.Rcl** : Bibliothèque de composants UI agnostique.
- **Scripts/src** : Code source TypeScript compilé automatiquement dans `wwwroot/js`.

## 📦 Commandes utiles
- `Ctrl+Shift+B` : Build complet (TS + .NET).
- La tâche **TS: Watch** tourne en arrière-plan pour compiler le JS à la volée.