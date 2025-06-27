# 🏷️ LoyaltyProgram - API REST .NET Core + PostgreSQL

API de gestion d'un programme de fidélité client.
Stack moderne : .NET 8, Docker, PostgreSQL.


## 📦 Fonctionnalités
- ✅ Gestion des clients et des boutiques
- ✅ Programme de fidélité (points, carte, transactions)
- ✅ Documentation Swagger intégrée

## 🚀 Prérequis
- .NET 8 SDK
- Docker

## ⚙️ Installation
Clone du projet :
```bash
git clone <repo-url>
cd LoyaltyProgram.Api
```

## 🐳 Lancer avec Docker Compose
```bash
docker-compose up --build
```

API accessible sur : http://localhost:5000/swagger

Base de données PostgreSQL dispo sur localhost:5432

## 🛠️ Migrations EF Core
Ajouter une migration :

```bash
dotnet ef migrations add NomMigration --project LoyaltyProgram.Api
```

Appliquer la migration :
```bash
dotnet ef database update --project LoyaltyProgram.Api```

## 📁 Structure du Projet
```rust
src/
├─ LoyaltyProgram.Api/           --> Projet API principal
│    ├─ Models/                  --> Entités métier
│    ├─ Data/                    --> DbContext + Migrations
│    ├─ Program.cs               --> Bootstrap API
│    └─ appsettings.json         --> Configurations (connexions, logs)
tests/
└─ LoyaltyProgram.Tests/         --> Tests unitaires et d'intégration
```

## 🧪 Tests
```bash
dotnet test
```

## 📝 TODO Évolutions
- Gestion des récompenses (modèle + endpoints)
- Historique des transactions et points
- Système de statuts (VIP, Silver, Gold...)
- Sécurisation par Authentification (JWT)
