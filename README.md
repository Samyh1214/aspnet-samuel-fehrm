# GymPortal – ASP.NET Core MVC

## Om projektet
GymPortal är en webbportal för ett gym byggd med ASP.NET Core MVC. UI:t är designat att efterlikna en färdig design från Figma. Projektet demonstrerar en fullstack-implementation med fokus på ren arkitektur, säkerhet och skalbarhet från databasmodellering till presentation.

Projektet följer principerna för Clean Architecture och Domain-Driven Design (DDD) med tydlig separation mellan domän, applikation, infrastruktur och presentation. Grunden är lagd för vidare utveckling av membership-hantering, gymklasser och bokning.

## Teknisk stack
- .NET 10
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core (Code First)
- InMemory-databas (development) / SQL Server (production)
- xUnit (enhetstester)

## Arkitektur
Lösningen är uppdelad i fyra projekt:

| Projekt | Ansvar |
|---|---|
| Domain | Entiteter och affärslogik |
| Application | Interfaces, DTOs och Result Pattern |
| Infrastructure | Identity, EF Core och databasimplementationer |
| Presentation | Controllers, Views och Partial Views |

## Kom igång
Projektet kräver ingen databaskonfiguration i development-läge. En InMemory-databas används automatiskt.

```bash
git clone <repo-url>
cd GymPortal
dotnet run --project Presentation
```

Ett admin-konto skapas automatiskt vid uppstart:

| **Email** | admin@domain.local |
| **Lösenord** | Admin123! |

## Designmönster
- **Service Pattern** - `IAccountService`, `IAuthService`
- **Factory Pattern** - `ApplicationUser.Create()`, `GymClass.Create()`
- **Result Pattern** - `AuthResult`, `AccountResult`
- **DTO Pattern** - `AccountDetails`, `UpdateAccountDetails`

## Notering
`SignInController` kommunicerar direkt med `SignInManager` från Infrastructure, vilket tekniskt bryter mot Clean Architecture. Detta är medvetet noterat och hade i en produktionsmiljö refaktorerats till att använda `IAuthService`.

## Tester
Projektet innehåller enhetstester för `AuthResult` och `AccountResult` i testprojektet `GymPortal.Tests`.
