# 🏷️ LoyaltyProgram - API REST .NET Core + PostgreSQL

API de gestion d'un programme de fidélité client.
Stack moderne : .NET 9, Docker, PostgreSQL.

[![.NET](https://github.com/delitamakanda/LoyaltyProgram/actions/workflows/ci.yml/badge.svg?branch=main&event=push)](https://github.com/delitamakanda/LoyaltyProgram/actions/workflows/ci.yml)
![version](https://img.shields.io/badge/dotnet%20version-net9.0-blue)


## 📦 Fonctionnalités
- ✅ Gestion des clients et des boutiques
- ✅ Programme de fidélité (points, carte, transactions)
- ✅ Documentation Swagger intégrée

## 🚀 Prérequis
- .NET 9 SDK
- Docker

## ⚙️ Installation
Clone du projet :
```bash
git clone git@github.com:delitamakanda/LoyaltyProgram.git
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
dotnet ef database update --project LoyaltyProgram.Api
```

## 📁 Structure du Projet
```rust
src/
├─ LoyaltyProgram.Api/           --> Projet API principal
│    └─ appsettings.json         --> Configurations (connexions, logs)
├─ LoyaltyProgram.Domain/        --> Entitées
├─ LoyaltyProgram.Application/   --> Logique Métier
├─ LoyaltyProgram.Infrastructure/--> Gestion des migrations
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
- Notifications ou alertes lors du franchissement de seuils de points
- Validité limitée dans le temps des récompenses
- Système de statuts (VIP, Silver, Gold...)
- Sécurisation par Authentification (JWT)
