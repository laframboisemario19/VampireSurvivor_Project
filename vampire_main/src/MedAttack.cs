using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Utils;
using static DcmAttack;
using static DcmProjectile;

public partial class MedAttack : Node2D, ICollide
{
    [Export]
    private Node2D Game;

    [Export]
    private Timer timer;

    [Export]
    private DcmProjectile DcmProjectile;

    [Export]
    private DcmObject DcmObject;

    [Export]
    private Player Player;

    [Export]
    private DcmEnemi DcmEnemi;

    private bool GameStarted;
    int i = 0;

    private bool _gameOver = false;

    public enum EAlgoSelectionDetection
    {
        eProjectileOnEnemy,
        eEnemyOnPlayer,
        eMeleeOnEnemy,
        eMapOnPlayer,
        eTreasureOnPlayer,
        eTreasureOnAuraPlayer,
        eTrapOnCharacter,
    }

    private ArrayList weaponList = new ArrayList() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    public override void _Ready()
    {
        base._Ready();

        object[] list = weaponList.ToArray();
        Random.Shared.Shuffle(list);
        weaponList.Clear();
        weaponList.AddRange(list);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (GameStarted)
            return;
        if (Input.IsActionPressed("ui_select"))
        {
            DcmEnemi.StartGame();
            Player.IsGameStarted = true;
            CallDeferred(MethodName._ActivateWeapon);
            GameStarted = true;
        }
    }

    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        switch (InAlgoSelectionDetection)
        {
            case EAlgoSelectionDetection.eProjectileOnEnemy:
                {
                    Projectile projectile = (Projectile)InEntering;
                    projectile.Die();
                    BaseEnemy enemy = (BaseEnemy)InEntered;

                    bool spawnGem = enemy.TakeDamage(1);
                    if (spawnGem)
                    {
                        if (enemy is ZombieBoss)
                        {
                            Tween tw = CreateTween();
                            tw.TweenProperty(Game, "modulate", Colors.Green, 2.0f);
                            Player.win();

                            _GameOver();
                        }
                        else if (spawnGem)
                        {
                            DcmObject.SpawnGem(enemy.GlobalPosition);
                        }
                    }
                }
                break;
            case EAlgoSelectionDetection.eEnemyOnPlayer:
                {
                    int damage = 0;
                    if (InEntered is Player player)
                    {
                        if (InEntering != null)
                        {
                            damage = 1;
                        }
                        if (player.TakeDamage(damage))
                        {
                            Tween tw = CreateTween();
                            tw.TweenProperty(Game, "modulate", Colors.Red, 2.0f);
                            _GameOver();
                        }
                    }
                }
                break;
            case EAlgoSelectionDetection.eMeleeOnEnemy:
                {
                    BaseEnemy enemy = (BaseEnemy)InEntering;
                    bool spawnGem = enemy.TakeDamage(1);
                    if (enemy is ZombieBoss)
                    {
                        Tween tw = CreateTween();
                        tw.TweenProperty(Game, "modulate", Colors.Green, 2.0f);
                    }
                    else if (spawnGem)
                    {
                        DcmObject.SpawnGem(enemy.GlobalPosition);
                    }
                }
                break;
            case EAlgoSelectionDetection.eMapOnPlayer:
                {
                    Player player = (Player)InEntered;
                    player.Die();
                    Tween tw = CreateTween();
                    tw.TweenProperty(Game, "modulate", Colors.Red, 2.0f);
                    _GameOver();
                }
                break;

            case EAlgoSelectionDetection.eTreasureOnAuraPlayer:
                {
                    BaseObject baseObject = (BaseObject)InEntering;
                    Player player = (Player)InEntered;
                    baseObject.Animate(player);
                }
                break;
            case EAlgoSelectionDetection.eTreasureOnPlayer:
                {
                    BaseObject baseObject = (BaseObject)InEntering;
                    Player player = (Player)InEntered;
                    baseObject.Die();
                    if (player.AddXp(baseObject.XpValue))
                    {
                        _ActivateWeapon();
                    }
                    ;
                }
                break;
            case EAlgoSelectionDetection.eTrapOnCharacter:
                {
                    Trap trap = (Trap)InEntering;
                    ITakeDamage character = (ITakeDamage)InEntered;

                    if (character.TakeDamage(1))
                    {
                        if (character is Player)
                        {
                            Tween tw = CreateTween();
                            tw.TweenProperty(Game, "modulate", Colors.Red, 2.0f);
                            _GameOver();
                        }
                        else if (character is ZombieBoss)
                        {
                            Tween tw = CreateTween();
                            tw.TweenProperty(Game, "modulate", Colors.Green, 2.0f);
                            Player.win();

                            _GameOver();
                        }
                        else if (character is BaseEnemy)
                        {
                            DcmObject.SpawnGem(((BaseEnemy)character).GlobalPosition);
                        }
                    }

                    trap.TakeDamage(1);
                }
                break;
            default:
                break;
        }
    }

    private void _ActivateWeapon()
    {
        if (weaponList.Count > 0)
        {
            int index = (int)weaponList[0];
            weaponList.RemoveAt(0);

            if (index < 6)
            {
                DcmProjectile.ActivateProjectile((EProjectileType)index);
            }
            else
            {
                Player.DcmAttack.ActivateAttack((eWeaponType)index);
            }
        }
    }

    private void _GameOver()
    {
        IEnumerable<BaseEnemy> allEnemies = DcmEnemi
            .EnsureValid()
            .GatherChildren()
            .OfType<BaseEnemy>();
        foreach (BaseEnemy e in allEnemies)
        {
            e.Die();
        }
        DcmProjectile.GameOver();
        DcmEnemi.GameOver();
        Player.DcmAttack.GameOver();
    }
}
