##  Builder l'image
```bash
docker build -t loyalty-api .
```bash

## Lancer un conteneur
```bash
docker run -p 5000:5000 loyalty-api
```

## Lancer par docker-compose
```bash
docker-compose up --build
```

Tu peux ensuite accéder à ton API via http://localhost:5000

## Créer une nouvelle migration
```bash
dotnet ef migrations add InitialCreate --project src/LoyaltyProgram.Api --startup-project src/LoyaltyProgram.Api
```

## Appliquer les migrations à la base de donnée
```base
dotnet ef database update --project src/LoyaltyProgram.Api --startup-project src/LoyaltyProgram.Api
```

## Lister les migrations
```bash
dotnet ef migrations list --project src/LoyaltyProgram.Api --startup-project src/LoyaltyProgram.Api
```

## Supprimer la dernière migration
```bash
dotnet ef migrations remove --project src/LoyaltyProgram.Api --startup-project src/LoyaltyProgram.Api
```

## Générer script SQL pour les migrations
```bash
dotnet ef migrations script --project src/LoyaltyProgram.Api --startup-project src/LoyaltyProgram.Api
```


## Reference package
```bash
dotnet add src/LoyaltyProgram.Application/LoyaltyProgram.Application.csproj reference src/LoyaltyProgram.Domain/LoyaltyProgram.Domain.csproj
```