using System;
using System.Collections;
using System.Linq;
using Godot;
using Utils;
using static DcmAttack;
using static DcmProjectile;

public partial class MedAttack : Node2D, ICollide
{
    [Export]
    private Timer timer;

    [Export]
    private DcmProjectile DcmProjectile;

    [Export]
    private DcmObject DcmObject;

    [Export]
    private Player Player;
    int i = 0;

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
    // private ArrayList weaponList = new ArrayList() { 6 };

    public override void _Ready()
    {
        base._Ready();

        // À remplacer par une fonction permettant le choix d'arme et utiliser pour les spawns de monstres
        // timer.EnsureValid().Timeout += () =>
        // {
        //     if (i < 6)
        //     {
        //         DcmProjectile.ActivateProjectile((EProjectileType)i);
        //         i++;
        //     }
        // };
        object[] list = weaponList.ToArray();
        Random.Shared.Shuffle(list);
        weaponList.Clear();
        weaponList.AddRange(list);
        CallDeferred(MethodName._ActivateWeapon);
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

                    // À remplacer Die pour TakeDamage + Spawn seulement si TakeDamage return True
                    bool spawnGem = enemy.TakeDamage(1);
                    if (spawnGem)
                        DcmObject.SpawnGem(enemy.GlobalPosition);
                }
                break;
            case EAlgoSelectionDetection.eEnemyOnPlayer:
                {
                    // à coder
                    int damage = 0;
                    if (InEntered is Player player)
                    {
                        if (InEntering != null)
                        {
                            damage = 1;
                        }
                        player.TakeDamage(damage);
                    }
                }
                break;
            case EAlgoSelectionDetection.eMeleeOnEnemy:
                {
                    BaseEnemy enemy = (BaseEnemy)InEntering;
                    // À remplacer Die pour TakeDamage + Spawn seulement si TakeDamage return True
                    bool spawnGem = enemy.TakeDamage(1);
                    if (spawnGem)
                        DcmObject.SpawnGem(enemy.GlobalPosition);
                }
                break;
            case EAlgoSelectionDetection.eMapOnPlayer:
                {
                    Player player = (Player)InEntered;
                    player.Die();
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
                    // à coder le return de AddXp pour le choix d'arme
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
                    character.TakeDamage(1);
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
}
