# 🏷️ LoyaltyProgram - API REST .NET Core + PostgreSQL

API de gestion d'un programme de fidélité client.
Stack moderne : .NET 8, Docker, PostgreSQL.

[![.NET](https://github.com/delitamakanda/LoyaltyProgram/actions/workflows/ci.yml/badge.svg?branch=main&event=push)](https://github.com/delitamakanda/LoyaltyProgram/actions/workflows/ci.yml)
![version](https://img.shields.io/badge/dotnet%20version-net8.0-blue)

## Screenshots

![Image 1](extras/localhost_5237_swagger_index.html_2.png)
![Image 2](extras/localhost_5237_swagger_index.html.png)

## 📦 Fonctionnalités

- ✅ Gestion des clients et des boutiques
- ✅ Programme de fidélité (points, carte, transactions)
- ✅ Notifications ou alertes lors du franchissement de seuils de points
- ✅ Validité limitée dans le temps des récompenses
- ✅ Système de statuts (VIP, Silver, Gold...)
- ✅ Documentation Swagger intégrée
- ✅ Sécurisation par Authentification (JWT)
- ✅ Historique des transactions et points
- ✅ Gestion des récompenses (modèle + endpoints)

## 🚀 Prérequis

- .NET 8 SDK
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

API accessible sur : http://localhost:5237/swagger/index.html

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

- Support multilingue
- Optimisation multi-devices
- Export des données (transactions, points, récompenses) pour reporting ou analyses
- Développement d'un Dashboard
