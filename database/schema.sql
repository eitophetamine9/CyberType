-- database/schema.sql
-- Application Development | CIT-U Computer Studies Department
-- Project Milestone: Supabase Database Schema for Profiles Table

-- 1. Create table with soft delete column
create table public.profiles (
  id bigint generated always as identity primary key,
  name text not null,
  callsign text not null,
  difficulty text not null default 'Medium',
  best_wpm integer not null default 0 check (best_wpm >= 0),
  accuracy numeric(5,2) not null default 0 check (accuracy >= 0 and accuracy <= 100),
  high_score integer not null default 0 check (high_score >= 0),
  deleted_at timestamptz,  -- for soft delete
  created_at timestamptz not null default now()
);

-- 2. Performance index on commonly searched identifier
create index on public.profiles (name);
create index on public.profiles (callsign);

-- 3. Permissions & Row Level Security (Open policies for class milestone)
grant select, insert, update, delete on public.profiles to anon;
alter table public.profiles enable row level security;

create policy "read_all" on public.profiles for select to anon using (true);
create policy "insert_all" on public.profiles for insert to anon with check (true);
create policy "update_all" on public.profiles for update to anon using (true) with check (true);
create policy "delete_all" on public.profiles for delete to anon using (true);

-- 4. Initial seed rows for testing
insert into public.profiles (name, callsign, difficulty, best_wpm, accuracy, high_score)
values 
  ('Alex Mercer', 'CYBER-VIPER', 'Hard', 84, 98.50, 42500),
  ('Elena Vance', 'NEO-RAVEN', 'Medium', 68, 96.20, 28900),
  ('Marcus Vance', 'GHOST-STRIKER', 'Easy', 52, 94.00, 16400);
