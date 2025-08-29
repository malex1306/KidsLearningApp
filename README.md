# KidsLearningApp

Dies ist eine **Full-Stack-Lernanwendung für Kinder** bestehend aus:

- **Backend:** .NET  
- **Frontend:** Angular

---

## 📋 Voraussetzungen

Stelle sicher, dass folgende Tools installiert sind:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js & npm](https://nodejs.org/)
- Angular CLI (global installieren, falls noch nicht vorhanden)
  npm install -g @angular/cli



1️⃣ Repository klonen
git clone https://github.com/malex1306/KidsLearningApp.git
cd KidsLearningApp

2️⃣ Backend einrichten & starten

# Ins Backend wechseln

cd KidsLearning.Backend

# Datenbank-Migrationen anwenden
dotnet ef database update

# Backend starten
dotnet run

3️⃣ Frontend einrichten & starten

# Neues Terminal öffnen oder ins Hauptverzeichnis zurück
cd ../KidsLearning.Frontend/ClientApp

# Abhängigkeiten installieren
npm install

# Angular-Entwicklungsserver starten
ng serve

