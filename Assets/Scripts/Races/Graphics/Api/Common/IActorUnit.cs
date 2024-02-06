using System.Collections.Generic;
using UnityEngine;

public interface IActorUnit
{
    PredatorComponent PredatorComponent { get; }


    IUnitRead Unit { get; }
    Vec2i Position { get; }
    AnimationController AnimationController { get; }
    bool HasBelly { get; }
    bool HasPreyInBreasts { get; }
    bool HasBodyWeight { get; }
    bool IsAttacking { get; }

    /// <summary>
    /// This one Covers all forms of consuming
    /// </summary>
    bool IsEating { get; }

    bool IsOralVoring { get; }
    bool IsOralVoringHalfOver { get; }
    bool IsCockVoring { get; }
    bool IsBreastVoring { get; }
    bool IsUnbirthing { get; }
    bool IsTailVoring { get; }
    bool IsAnalVoring { get; }
    bool IsPouncingFrog { get; }
    bool HasJustVored { get; }
    bool HasJustFailedToVore { get; }
    bool IsDigesting { get; }
    bool IsAbsorbing { get; }
    bool IsBirthing { get; }
    bool IsSuckling { get; }
    bool IsBeingSuckled { get; }
    bool IsRubbing { get; }
    bool IsBeingRubbed { get; }
    List<Side> SidesAttackedThisBattle { get; }
    bool SquishedBreasts { get; set; }
    bool Surrendered { get; }
    bool Targetable { get; }
    Weapon BestRanged { get; }
    bool HasAttackedThisCombat { get; }
    bool Visible { get; }
    bool DamagedColors { get; }
    int TurnUsedShun { get; }

    // void ClearMovement();
    // void DrainExp(int damage);
    // void SetPos(Vec2i p);
    // int MaxMovement();
    // int CurrentMaxMovement();
    // void ReloadSpellTraits();
    // void GenerateSpritePrefab(Transform folder);
    // void UpdateBestWeapons();
    // void SetPredMode(PreyLocation preyType);
    // void SetBurpMode();

    /// <summary>
    /// Used for idle Animations - ignored if the unit is already animating something
    /// </summary>
    /// <param name="frameNum">The Number of Frames of animation, starts at this and counts down to 0</param>
    /// <param name="time">The seconds per step</param>
    void SetAnimationMode(int frameNum, float time);

    // void SetVoreSuccessMode();
    // void SetVoreFailMode();
    // void SetAbsorbtionMode();
    // void SetDigestionMode();
    // void SetBirthMode();
    // void SetSuckleMode();
    // void SetSuckledMode();
    // void SetRubMode();
    // void SetRubbedMode();
    int CheckAnimationFrame();

    /// <summary>
    /// Splits the chain of sprites evenly based on the ball size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetBallSize(int highestSprite, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the tail size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetTailSize(int highestSprite, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetStomachSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetExclusiveStomachSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetWombSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites unevenly based on the stomach size (Oral + Unbirth)
    /// This one causes the low sprites to be passed through quicker, and the high sprites to be passed through slower.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetRootedStomachSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the left breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetLeftBreastSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the right breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetRightBreastSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the second stomach's size.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetStomach2Size(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// Splits the chain of sprites evenly based on the combined size of bellies 1 & 2. For units with DualStomach trait.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetCombinedStomachSize(int highestSprite = 15, float multiplier = 1);

    /// <summary>
    /// An alternate version of GetStomachSize used for combining all vore types into a single value
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4</param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    int GetUniversalSize(int highestSprite = 15, float multiplier = 1);

    int GetBodyWeight();

    /// <summary>
    /// 0: Melee 1 hold
    /// 1: Melee 1 attack
    /// 2: Melee 2 hold
    /// 3: Melee 2 attack
    /// 4: Ranged 1 hold
    /// 5: Ranged 1 attack
    /// 6: Ranged 2 hold
    /// 7: Ranged 2 attack
    /// </summary>
    /// <returns></returns>
    int GetWeaponSprite();
    int GetSimpleBodySprite();
    // float GetSpecialChance(SpecialAction action);
    // float GetPureStatClashChance(int attackStat, int defenseStat, float shift); // generic AF
    // float GetAttackChance(Actor_Unit attacker, bool ranged, bool includeSecondaries = false, float mod = 0);
    // int WeaponDamageAgainstTarget(Actor_Unit target, bool ranged, float multiplier = 1, bool forceBite = false);
    // Vec2i PounceAt(Actor_Unit target);
    // bool MeleePounce(Actor_Unit target);
    // bool VorePounce(Actor_Unit target, SpecialAction voreType = SpecialAction.None, bool AIAutoPick = false);
    // bool ShunGokuSatsu(Actor_Unit target);
    // bool Regurgitate(Actor_Unit a, Vec2i l);
    // bool TailStrike(Actor_Unit target);
    // bool Attack(Actor_Unit target, bool ranged, bool forceBite = false, float damageMultiplier = 1, bool canKill = true);
    // bool Defend(Actor_Unit attacker, ref int damage, bool ranged, out float chance, bool canKill = true);
    // float GetDevourChance(Actor_Unit attacker, bool includeSecondaries = false, int skillBoost = 0, bool force = false);
    // bool BellyRub(Actor_Unit target);
    // float BodySize();
    // float Bulk(int count = 0);
    // bool Move(int direction, TacticalTileType[,] tiles);
    // void Update(float dt);
    // void NewTurn();
    // void SubtractHealth(int damage);
    // int CalculateDamageWithResistance(int damage, DamageType damageType);
    // bool Damage(int damage, bool spellDamage = false, bool canKill = true, DamageType damageType = DamageType.Generic);
    // void DigestCheck(string feedtype = "");
    // Vec2i GetPos(int i);
    bool IsErect();
    // float WillCheckOdds(Actor_Unit actor, Actor_Unit target);
    // float CritCheck(Actor_Unit actor, Actor_Unit target);
    // float GrazeCheck(Actor_Unit actor, Actor_Unit target);
    // bool ChangeRaceAny(Unit template, bool permanent, bool isPrey);
    // void ChangeRacePrey();
    // void RevertRace();
}