# Assessment Civios

Prototype voor het automatisch analyseren, classificeren en opslaan van documenten op basis van hun gevoeligheid.

De applicatie is gebouwd als ASP.NET Core Web API in C# en gebruikt Swagger als interface voor de demo.

## Functionaliteit

De applicatie ondersteunt momenteel:

- uploaden van PDF-documenten met metadata;
- validatie van bestandstype en bestandsgrootte;
- tekstextractie uit PDF-bestanden;
- automatische classificatie van documenten;
- toekennen van een access level en bewaartermijn;
- tijdelijke opslag van geanalyseerde documenten;
- definitieve opslag volgens classificatie;
- audit logging van analyse en opslag;
- SQLite-persistentie;
- geautomatiseerde unit tests.

De mogelijke classificaties zijn:

- `PublicData`
- `InternalData`
- `PersonalData`
- `SensitivePersonalData`

## Technologie

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 9
- SQLite
- PdfPig
- Swagger / OpenAPI
- xUnit

## Projectstructuur

De solution is opgesplitst in vier hoofdprojecten:

```text
Assessment.Presentation
Assessment.Application
Assessment.Core
Assessment.Infrastructure
Assessment.Tests
```

### Assessment.Presentation

Bevat de API-controller, request models, dependency injection en configuratie van de applicatie.

### Assessment.Application

Bevat de `DocumentService`, die de verschillende stappen van de documentverwerking orkestreert.

### Assessment.Core

Bevat de businesslogica en domeinmodellen, waaronder:

- classificatie;
- validatie;
- document policy;
- entities;
- enums;
- interfaces;
- result models.

### Assessment.Infrastructure

Bevat technische implementaties voor:

- SQLite / Entity Framework Core;
- repositories;
- lokale bestandsopslag;
- PDF-tekstextractie;
- database migrations.

## Vereisten

Om het project lokaal uit te voeren is het volgende nodig:

- .NET 8 SDK
- Entity Framework Core CLI tools

Indien `dotnet-ef` nog niet geïnstalleerd is:

```powershell
dotnet tool install --global dotnet-ef --version 9.0.20
```

## Installatie

Open een terminal in de map `AssessmentCivios`, waar `AssessmentCivios.sln` staat.

Restore de NuGet packages:

```powershell
dotnet restore .\AssessmentCivios.sln
```

Build de solution:

```powershell
dotnet build .\AssessmentCivios.sln
```

Maak of update de lokale SQLite-database:

```powershell
dotnet ef database update --project ..\Assessment.Infrastructure\Assessment.Infrastructure.csproj --startup-project .\Assessment.Presentation.csproj
```

## Applicatie starten

Via Visual Studio kan het HTTPS-profiel gestart worden met `F5` of `Ctrl+F5`.

Swagger wordt bij het starten automatisch geopend.

De standaard HTTPS-url is:

```text
https://localhost:7150/swagger
```

De applicatie kan ook vanuit de terminal gestart worden:

```powershell
dotnet run --project .\Assessment.Presentation.csproj
```

## API

### Document analyseren

```http
POST /api/document/analyze
```

De request gebruikt `multipart/form-data` en bevat:

- `File`
- `Author`
- `Title`
- `CreationDate`
- `Description`
- `Visibility`

Het document wordt gevalideerd, de tekst wordt uitgelezen en vervolgens geclassificeerd.

Na een geslaagde analyse wordt het bestand tijdelijk opgeslagen in:

```text
LocalFiles/Pending
```

Het document krijgt de status:

```text
Analyzed
```

### Document definitief opslaan

```http
POST /api/document/{id}/store
```

Het document wordt opnieuw uit de database opgehaald. De eerder bepaalde classificatie wordt server-side gebruikt om de definitieve opslaglocatie te kiezen.

Mogelijke mappen zijn:

```text
LocalFiles/Public
LocalFiles/Internal
LocalFiles/Personal
LocalFiles/Sensitive
```

Na succesvolle opslag krijgt het document de status:

```text
Stored
```

### Auditlog opvragen

```http
GET /api/document/{id}/audit
```

Geeft de auditregels van het document chronologisch terug.

Momenteel worden onder andere deze acties gelogd:

- `Analyzed`
- `Stored`

## Classificatie

De classifier gebruikt een eenvoudige rule-based aanpak met keywords.

Voorbeelden:

| Classificatie | Voorbeelden van signalen |
| --- | --- |
| PublicData | brochure, openbaar, folder |
| InternalData | memo, vergadering, nota |
| PersonalData | naam, adres, e-mailadres, rijksregisternummer |
| SensitivePersonalData | medisch, financiële, strafrechtelijke |

De classifier bekijkt:

- de geëxtraheerde documenttekst;
- de titel;
- de beschrijving.

Wanneer meerdere signalen aanwezig zijn, wordt de hoogste classificatie gekozen:

```text
SensitivePersonalData
> PersonalData
> InternalData
> PublicData
```

`IntendedVisibility.Internal` kan een document verhogen naar `InternalData`, maar verlaagt nooit een hogere classificatie.

Wanneer geen classificatiesignalen gevonden worden, wordt het document als `PublicData` geclassificeerd.

## Access en retention

Na classificatie wordt een eenvoudige document policy toegepast:

| Classificatie | Access level | Bewaartermijn |
| --- | --- | --- |
| PublicData | Public | 1 jaar |
| InternalData | Internal | 3 jaar |
| PersonalData | Restricted | 5 jaar |
| SensitivePersonalData | HighlyRestricted | 10 jaar |

De bewaartermijnen zijn keuzes voor dit prototype en stellen geen officiële wettelijke bewaartermijnen voor.

## Bestandsvalidatie

Momenteel gelden de volgende regels:

- alleen `.pdf`;
- extensiecontrole is case-insensitive;
- maximale bestandsgrootte is 10 MB;
- na tekstextractie moet het document effectieve tekst bevatten.

## Tests

De unit tests kunnen uitgevoerd worden met:

```powershell
dotnet test .\AssessmentCivios.sln
```

De huidige testset bevat 18 tests en controleert onder andere:

- classifier keywords;
- prioriteit tussen classificaties;
- metadata-classificatie;
- intended visibility;
- access levels;
- bewaartermijnen;
- PDF-bestandsvalidatie;
- maximale bestandsgrootte;
- validatie van geëxtraheerde tekst.

## Lokale opslag en database

De applicatie gebruikt een lokale SQLite-database:

```text
assessment.db
```

Bestanden worden lokaal opgeslagen onder:

```text
LocalFiles/
```

Bestandsnamen krijgen een GUID-prefix om overschrijven bij gelijke bestandsnamen te vermijden.

De originele bestandsnaam wordt met `Path.GetFileName` verwerkt voordat het bestand lokaal wordt opgeslagen.

## Beperkingen

Dit is een prototype en geen productieklare documentoplossing.

Belangrijke huidige beperkingen:

- alleen PDF wordt ondersteund;
- gescande PDF's zonder uitleesbare tekst hebben nog geen OCR-ondersteuning;
- classificatie is gebaseerd op eenvoudige keywords en metadata;
- er is nog geen authenticatie of echte role-based authorization;
- access levels worden bepaald en opgeslagen, maar nog niet technisch afgedwongen via gebruikersrollen;
- retention wordt berekend en opgeslagen, maar documenten worden nog niet automatisch verwijderd;
- lokale bestandsopslag en SQLite zijn gekozen voor eenvoud van de prototypeomgeving;
- bestandstypevalidatie controleert momenteel de extensie en PDF-verwerking, maar nog geen uitgebreide file signature-validatie.

## Testdata

Gebruik voor de demo en tests alleen fictieve gegevens. De applicatie is bedoeld als lokaal assessment-prototype en niet voor verwerking van echte persoonsgegevens.
