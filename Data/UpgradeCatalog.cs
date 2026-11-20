using System;
using System.Collections.Generic;
using System.Linq;
using MidtermAppDev.Models;
using MidtermAppDev.Services;

namespace MidtermAppDev.Data
{
    public static class UpgradeCatalog
    {
        public static List<UpgradeOption> GetUpgradePool()
        {
            return new List<UpgradeOption>
            {
                new UpgradeOption
                {
                    Title = "Overclock Dampeners",
                    Description = "Recalibrates thruster drag. Reduces hostile descent speed by 15%.",
                    Icon = "dampeners",
                    Rarity = "Rare",
                    ApplyAction = s => s.EnemySpeedModifier = Math.Max(0.4, s.EnemySpeedModifier * 0.85)
                },
                new UpgradeOption
                {
                    Title = "Kinetic Typo Matrix",
                    Description = "Generates +1 Typo Shield that replenishes at the start of every combat wave.",
                    Icon = "shield",
                    Rarity = "Epic",
                    ApplyAction = s => s.TypoForgiveness += 1
                },
                new UpgradeOption
                {
                    Title = "Bounty Protocol",
                    Description = "Enhances telemetry algorithms to increase overall score multiplier by +25%.",
                    Icon = "bounty",
                    Rarity = "Rare",
                    ApplyAction = s => s.ScoreMultiplier += 0.25
                },
                new UpgradeOption
                {
                    Title = "Nanite Hull Siphon",
                    Description = "Repairs 35 Base Hull Integrity and expands max hull capacity by +20 HP.",
                    Icon = "hull",
                    Rarity = "Common",
                    ApplyAction = s => {
                        s.MaxHealth += 20;
                        s.RepairHealth(35);
                    }
                },
                new UpgradeOption
                {
                    Title = "Frequency Jammer",
                    Description = "Disrupts hostile coordinates, widening the interval between enemy spawns by 15%.",
                    Icon = "jammer",
                    Rarity = "Common",
                    ApplyAction = s => s.SpawnIntervalMs = (int)(s.SpawnIntervalMs * 1.15)
                },
                new UpgradeOption
                {
                    Title = "Syntax Overdrive",
                    Description = "Instantly awards 500 bonus points and boosts overall score multiplier by +15%.",
                    Icon = "overdrive",
                    Rarity = "Legendary",
                    ApplyAction = s => {
                        s.Score += 500;
                        s.ScoreMultiplier += 0.15;
                    }
                }
            };
        }

        public static List<UpgradeOption> GenerateChoices(int count = 3)
        {
            var rng = new Random();
            return GetUpgradePool().OrderBy(_ => rng.Next()).Take(count).ToList();
        }
    }
}
