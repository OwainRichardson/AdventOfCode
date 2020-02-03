using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_21_1
    {
        private static Fighter _boss = new Fighter { Name = "BOSS" };
        private static Fighter _player = new Fighter { Name = "PLAYER", HitPoints = 100, Damage = 0, Armor = 0 };
        private static List<Result> _results = new List<Result>();

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day21_full.txt");

            ParseBoss(inputs);

            foreach (var weapon in _weapons)
            {
                foreach (var armor in _armor)
                {
                    foreach (var ring in _rings)
                    {
                        foreach (var ring2 in _rings.Where(x => x.Name != ring.Name))
                        {
                            _player.Damage = weapon.Damage + ring.Damage + ring2.Damage;
                            _player.Armor = armor.Armor + ring.Armor + ring2.Armor;
                            _player.HitPoints = 100;
                            _player.Weapon = weapon.Name;
                            _player.ArmorName = armor.Name;
                            _player.Ring1 = ring.Name;
                            _player.Ring2 = ring2.Name;

                            Result result = new Result
                            {
                                Cost = weapon.Cost + armor.Cost + ring.Cost + ring2.Cost
                            };

                            SimulateFight(result);
                        }
                    }
                }
            }

            //Console.WriteLine($"Player wins {_results.Count} fights");
            Console.Write($"Least amount required to win is ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(_results.Min(x => x.Cost));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void SimulateFight(Result result)
        {
            _boss.HitPoints = 103;

            int index = 1;
            while (_boss.HitPoints > 0 && _player.HitPoints > 0)
            {
                if (index % 2 == 1)
                {
                    PlayerTurn();
                }
                else
                {
                    BossTurn();
                }

                index++;
            }

            if (_player.HitPoints > 0)
            {
                //Console.WriteLine($"Player has won");
                result.PlayerWon = true;

                result.Weapon = _player.Weapon;
                result.ArmorName = _player.ArmorName;
                result.Ring1 = _player.Ring1;
                result.Ring2 = _player.Ring2;

                _results.Add(result);
            }
            else
            {
                //Console.WriteLine($"Boss has won");
            }
        }

        private static void BossTurn()
        {
            var damageDone = CalculateDamage(_boss, _player);
            _player.HitPoints -= damageDone;

            //Console.WriteLine($"Boss deals {damageDone} damage; The player goes down to {_player.HitPoints} hit points");
        }

        private static void PlayerTurn()
        {
            var damageDone = CalculateDamage(_player, _boss);
            _boss.HitPoints -= damageDone;

            //Console.WriteLine($"Player deals {damageDone} damage; The boss goes down to {_boss.HitPoints} hit points");
        }

        private static int CalculateDamage(Fighter attacker, Fighter defender)
        {
            var damageDone = attacker.Damage - defender.Armor;

            if (damageDone <= 0)
            {
                damageDone = 1;
            }

            return damageDone;
        }

        private static void ParseBoss(string[] inputs)
        {
            foreach (var input in inputs)
            {
                if (input.StartsWith("Hit Points"))
                {
                    _boss.HitPoints = int.Parse(Regex.Match(input, @"([0-9]+)").Value);
                }
                else if (input.StartsWith("Damage"))
                {
                    _boss.Damage = int.Parse(Regex.Match(input, @"([0-9]+)").Value);
                }
                else if (input.StartsWith("Armor"))
                {
                    _boss.Armor = int.Parse(Regex.Match(input, @"([0-9]+)").Value);
                }
            }
        }

        private static readonly List<Weapon> _weapons = new List<Weapon>
        {
            new Weapon
            {
                Name = "Dagger",
                Cost = 8,
                Damage = 4,
                Armor = 0
            },
            new Weapon
            {
                Name = "Shortsword",
                Cost = 10,
                Damage = 5,
                Armor = 0
            },
            new Weapon
            {
                Name = "Warhammer",
                Cost = 25,
                Damage = 6,
                Armor = 0
            },
            new Weapon
            {
                Name = "Longsword",
                Cost = 40,
                Damage = 7,
                Armor = 0
            },
            new Weapon
            {
                Name = "Greataxe",
                Cost = 74,
                Damage = 8,
                Armor = 0
            }
        };

        private static readonly List<Armor> _armor = new List<Armor>
        {
            new Armor
            {
                Name = "None",
                Cost = 0,
                Damage = 0,
                Armor = 0
            },
            new Armor
            {
                Name = "Leather",
                Cost = 13,
                Damage = 0,
                Armor = 1
            },
            new Armor
            {
                Name = "Chainmail",
                Cost = 31,
                Damage = 0,
                Armor = 2
            },
            new Armor
            {
                Name = "Splitmail",
                Cost = 53,
                Damage = 0,
                Armor = 3
            },
            new Armor
            {
                Name = "Bandedmail",
                Cost = 75,
                Damage = 0,
                Armor = 4
            },
            new Armor
            {
                Name = "Platemail",
                Cost = 102,
                Damage = 0,
                Armor = 5
            }
        };

        private static readonly List<Ring> _rings = new List<Ring>
        {
            new Ring
            {
                Name = "NoneL",
                Cost = 0,
                Damage = 0,
                Armor = 0
            },
            new Ring
            {
                Name = "NoneR",
                Cost = 0,
                Damage = 0,
                Armor = 0
            },
            new Ring
            {
                Name = "Damage +1",
                Cost = 25,
                Damage = 1,
                Armor = 0
            },
            new Ring
            {
                Name = "Damage +2",
                Cost = 50,
                Damage = 2,
                Armor = 0
            },
            new Ring
            {
                Name = "Damage +3",
                Cost = 100,
                Damage = 3,
                Armor = 0
            },
            new Ring
            {
                Name = "Armor +1",
                Cost = 20,
                Damage = 0,
                Armor = 1
            },
            new Ring
            {
                Name = "Armor +2",
                Cost = 40,
                Damage = 0,
                Armor = 2
            },
            new Ring
            {
                Name = "Armor +3",
                Cost = 80,
                Damage = 0,
                Armor = 3
            },
        };
    }
}