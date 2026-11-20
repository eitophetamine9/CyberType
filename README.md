# CyberType // Defense Grid

> A browser-based, run-based survival typing game built with Blazor WebAssembly (.NET 10), pure C# game loops, Supabase PostgreSQL persistence, and Tailwind CSS v4 styling.

![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white) ![Supabase](https://img.shields.io/badge/Supabase-Database-3ECF8E?logo=supabase&logoColor=white) ![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-v4.x-06B6D4?logo=tailwindcss&logoColor=white) ![Architecture](https://img.shields.io/badge/Architecture-Pure_C%23_State-4B5563) ![Assets](https://img.shields.io/badge/Assets-Kenney_CC0-22C55E)

---

## Storage Pattern & Database Integration (Project Milestone)

### 1. Database Table (`public.profiles`)
The application persists Operator Profiles to Supabase PostgreSQL using the schema defined in [`database/schema.sql`](file:///c:/Users/Erick/MidtermAppDev/database/schema.sql).

- `id`: `bigint generated always as identity primary key`
- `name`: `text not null` (Operator identity)
- `callsign`: `text not null` (Tactical combat call-sign)
- `difficulty`: `text not null default 'Medium'`
- `best_wpm`: `integer not null default 0`
- `accuracy`: `numeric(5,2) not null default 0`
- `high_score`: `integer not null default 0`
- `deleted_at`: `timestamptz` (**Soft delete timestamp**)
- `created_at`: `timestamptz not null default now()`

### 2. Storage Pattern: Soft Delete (`deleted_at`) & Why
This application implements the **Soft Delete Pattern** via a nullable `deleted_at timestamptz` column rather than executing destructive hard SQL `DELETE` queries:

1. **Telemetry & Historical Integrity**: In gaming and telemetry systems, permanently dropping player records causes referential integrity breakdown with combat logs, match records, and statistical trends. Soft deletion preserves analytical data.
2. **Safe Deletion & Immediate Recovery**: Operators deleted by accident can be instantly restored by setting `deleted_at = NULL` without needing database backups or table restores.
3. **Optimized Query Filter**: Active player queries uniformly filter with `WHERE deleted_at IS NULL` (`.Where(p => p.DeletedAt == null)`), ensuring inactive/decommissioned operators are excluded from normal views without data destruction.
4. **Audit Trail**: The exact timestamp when an operator was decommissioned remains available for auditability and grading verification.

### 3. Setup & Connecting Supabase
1. Run [`database/schema.sql`](file:///c:/Users/Erick/MidtermAppDev/database/schema.sql) in your Supabase Dashboard **SQL Editor**.
2. Copy your **Project URL** and **Publishable Key** (`sb_publishable_...`) from **Settings → API Keys**.
3. Place them into `wwwroot/appsettings.json`:
   ```json
   {
     "Supabase": {
       "Url": "https://YOUR-PROJECT-REF.supabase.co",
       "Key": "sb_publishable_YOUR_KEY"
     }
   }
   ```
4. Run the project: `dotnet run --launch-profile http`. Navigate to `/profile` to view, add, update, and soft-delete operator profiles.

---

## Project Structure & Decoupled Domains

```text
├── database/
│   └── schema.sql                # Supabase table DDL, RLS policies, and test seed data
├── Models/
│   ├── OperatorProfile.cs        # Supabase BaseModel mapping public.profiles
│   ├── GameEnums.cs              # GameDifficulty & LexiconMode enums
│   ├── EnemyWord.cs              # Enemy entity with coordinate & substring properties
│   ├── UpgradeOption.cs          # Upgrade definition model
│   ├── RunRecord.cs              # Run telemetry record model
│   └── FloatingCombatText.cs     # Real-time floating damage & score text model
├── Data/
│   ├── WordLexicon.cs            # Word pools (Syntax & English) and difficulty filters
│   └── UpgradeCatalog.cs         # Upgrade card definitions & randomizer
├── Services/
│   └── GameSessionState.cs       # Scoped session manager (vitals, combo, stats, timers)
├── Components/
│   ├── WaveCompleteModal.razor   # Sector secured victory modal with high-contrast actions
│   └── GameOverModal.razor       # Base destroyed defeat modal with retry/abort actions
├── Pages/
│   ├── Home.razor                # Briefing Room dashboard with Lexicon & Difficulty selectors
│   ├── DefenseGrid.razor         # 60 FPS typing combat engine & target-locking state machine
│   ├── UpgradeHub.razor          # Centered upgrade cards & run progression
│   ├── Profile.razor             # Operator Dossier with full Supabase CRUD & Soft Delete
│   └── NotFound.razor            # 404 fallback page
├── Styles/
│   └── app.css                   # Tailwind CSS v4 source with tactical HUD design tokens
└── wwwroot/
    ├── appsettings.json          # Supabase URL & publishable key configuration
    ├── css/app.css               # Compiled & minified Tailwind CSS bundle
    ├── images/sprites/           # Kenney CC0 2D space shooter sprites
    │   ├── player_ship.png       # Operator battlecruiser avatar
    │   ├── enemy_ship.png        # Hostile typing target sprite
    │   ├── laser_beam.png        # Attack visual projectile
    │   └── space_bg.png          # High-density starfield backdrop
    └── index.html                # CyberType WebAssembly host page with importmap & preload
```

---

## Game Modes & Features

1. **Vocabulary / Lexicon Modes**:
   - **Mixed Hybrid**: Blends programming syntax keywords and regular English words.
   - **Code Syntax**: Pure C# and programming tokens (`async`, `virtual`, `interface`, `middleware`, `reflection`, etc.).
   - **Regular Words**: Everyday English vocabulary (`star`, `galaxy`, `shadow`, `thunder`, `whisper`, `metamorphosis`, etc.).

2. **Difficulty Tiers**:
   - **Easy**: 3–5 letter words, 0.65x falling speed, starts with +1 Typo Shield.
   - **Medium**: 5–8 letter words, 1.0x balanced speed, 1.4x score multiplier.
   - **Hard**: 8–14 letter words, 1.4x aggressive speed, 2.0x score multiplier.

3. **Precision Mechanics**:
   - Real-time 60 FPS loop with strict laser target-locking.
   - 2D sprite rendering powered by Kenney CC0 Space Shooter assets.
   - Clean tactical HUD with health gauge, combo counter, and live WPM calculator.

---

## Running Locally

Development server: **`http://localhost:5223`**

```bash
dotnet run --launch-profile http
```
