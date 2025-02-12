﻿using Races.Graphics.Implementations.MainRaces;
using UnityEngine;

internal static class TacticalGraphicalEffects
{
    internal enum SpellEffectIcon
    {
        None,
        Heal,
        Resurrect,
        PurplePlus,
        Poison,
        Buff,
        Debuff,
        Web,
    }

    internal static void CreateProjectile(ActorUnit actor, ActorUnit target)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var arrow = Object.Instantiate(State.GameManager.TacticalMode.ArrowPrefab).GetComponent<ArrowEffect>();
        var sprite = ArrowType(actor, out Material material);
        if (Equals(actor.Unit.Race, Race.Panther)) PantherSetup(arrow, actor);
        if (sprite != null) arrow.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        if (material != null) arrow.GetComponentInChildren<SpriteRenderer>().material = material;
        arrow.Setup(actor.Position, target.Position, target);
    }

    private static void PantherSetup(ArrowEffect obj, ActorUnit actor)
    {
        Weapon weapon = actor.BestRanged;
        if (weapon.Graphic == 4)
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.PantherBase;
            anim.Frame = new Sprite[]
            {
                sprites[36],
                sprites[37],
                sprites[38],
                sprites[39],
            };
            anim.FrameTime = new float[] { .05f, .05f, .05f, .05f };
        }
        else if (weapon.Graphic == 6)
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.PantherBase;
            anim.Frame = new Sprite[]
            {
                sprites[42],
                sprites[43],
                sprites[44],
                sprites[45],
            };
            anim.FrameTime = new float[] { .05f, .05f, .05f, .05f };
        }
    }

    private static Sprite ArrowType(ActorUnit actor, out Material material)
    {
        Weapon weapon = actor.BestRanged;
        material = null;
        if (Equals(actor.Unit.Race, Race.Harpy))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Harpies[27];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Harpies[28];
        }
        else if (Equals(actor.Unit.Race, Race.Imp))
        {
            if (weapon.Graphic == 4) return State.GameManager.SpriteDictionary.NewimpBase[93];
        }

        else if (Equals(actor.Unit.Race, Race.Scylla))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Scylla[22];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Scylla[23];
        }
        else if (Equals(actor.Unit.Race, Race.Slime))
        {
            if (weapon.Graphic == 4)
            {
                material = Slimes.GetSlimeAccentMaterial(actor);
                return State.GameManager.SpriteDictionary.Slimes[16];
            }
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Slimes[17];
        }
        else if (Equals(actor.Unit.Race, Race.Crypter))
        {
            if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Crypters[27];
        }
        else if (Equals(actor.Unit.Race, Race.Kangaroo))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Kangaroos[125];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Kangaroos[126];
        }
        else if (Equals(actor.Unit.Race, Race.Tiger) && actor.Unit.ClothingType == 1)
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (Equals(actor.Unit.Race, Race.Kobold))
            return State.GameManager.SpriteDictionary.Kobolds[20];
        else if (Equals(actor.Unit.Race, Race.Equine) && weapon.Graphic == 6)
            return State.GameManager.SpriteDictionary.EquineClothes[48];
        else if (Equals(actor.Unit.Race, Race.Alraune) && (weapon.Graphic == 4 || weapon.Graphic == 6))
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (Equals(actor.Unit.Race, Race.SpitterSlug))
            return State.GameManager.SpriteDictionary.SpitterSlug[10];
        else if (Equals(actor.Unit.Race, Race.Bat))
            return State.GameManager.SpriteDictionary.Demibats1[132];
        else if (Equals(actor.Unit.Race, Race.Panther))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.PantherBase[36];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.PantherBase[42];
        }
        else if (Equals(actor.Unit.Race, Race.Bee))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Bees1[89];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Bees1[89];
        }
        else if ((Equals(actor.Unit.Race, Race.Merfolk) && weapon.Graphic == 4) || weapon.Graphic == 6)
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (Equals(actor.Unit.Race, Race.Viper))
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Vipers1[20];
            else if (weapon.Graphic == 6) return State.GameManager.SpriteDictionary.Vipers1[20];
        }

        return null;
    }

    internal static void SuccubusSwordEffect(Vector2 location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var obj = Object.Instantiate(State.GameManager.SpriteRendererPrefab);
        obj.transform.position = location;
        obj.AddComponent<Assets.Scripts.Entities.Animations.SuccubusSword>();
    }

    internal static void CreateIceBlast(Vec2 location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.IceBlast;
        Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());

        MiscUtilities.DelayedInvoke(() => EightRing(prefab, location, .5f), .1f);
        MiscUtilities.DelayedInvoke(() => EightRing(prefab, location, 1), .2f);
    }

    internal static void CreateFireBall(Vec2I startLocation, Vec2I endLocation, ActorUnit target)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Fireball;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.X, startLocation.Y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
    }

    internal static void CreateHeartProjectile(Vec2I startLocation, Vec2I endLocation, ActorUnit target)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Charm;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.X, startLocation.Y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
    }

    internal static void CreatePollenCloud(Vec2I location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.PollenCloud;
        Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());
    }

    internal static void CreatePoisonCloud(Vec2I location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.PoisonCloud;
        Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());
    }

    internal static void CreateGasCloud(Vec2I location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.GasCloud;
        Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());
    }

    internal static void CreateSmokeCloud(Vec2I location, float scale)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.SmokeCloud;
        GameObject obj = Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());
        obj.transform.localScale *= scale;
    }

    internal static void CreateGlueBomb(Vec2I startLocation, Vec2I endLocation)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var arrow = Object.Instantiate(State.GameManager.TacticalMode.ArrowPrefab).GetComponent<ArrowEffect>();
        var sprite = State.GameManager.SpriteDictionary.SpitterSlug[10];
        if (sprite != null) arrow.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        System.Action hitEffect = () =>
        {
            var prefab = State.GameManager.TacticalEffectPrefabList.GlueBomb;
            Object.Instantiate(prefab, new Vector3(endLocation.X, endLocation.Y, 0), new Quaternion());
        };
        arrow.Setup(startLocation, endLocation, null, null, hitEffect);
    }

    internal static void CreateSpiderWeb(Vec2I startLocation, Vec2I endLocation, ActorUnit target)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.SpiderWeb;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.X, startLocation.Y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
        effect.TotalTime *= 2;
        effect.ExtraTime = 2.25f;
        var fade = effect.GetComponent<FadeInFadeOut>();
        fade.HoldTime = effect.TotalTime + .75f;
    }

    internal static void CreateHugeMagic(Vec2I startLocation, Vec2I endLocation, ActorUnit target, bool landed)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.HugeMagic;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.X, startLocation.Y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
        effect.TotalTime *= 2;
        System.Action hitEffect = null;
        if (landed)
        {
            hitEffect = () => { CreateMagicExplosion(new Vec2I(endLocation.X, endLocation.Y)); };
        }

        effect.Setup(startLocation, endLocation, target, null, hitEffect);
    }

    internal static void CreateMagicExplosion(Vec2I location)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.MagicExplosion;
        Object.Instantiate(prefab, new Vector3(location.X, location.Y, 0), new Quaternion());
    }

    internal static void CreateGenericMagic(Vec2I startLocation, Vec2I endLocation, ActorUnit target, SpellEffectIcon icon = SpellEffectIcon.None)
    {
        if (State.GameManager.TacticalMode.TurboMode) return;
        var prefab = State.GameManager.TacticalEffectPrefabList.GenericMagic;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.X, startLocation.Y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        System.Action hitEffect = null;
        if (icon != SpellEffectIcon.None)
        {
            //Sprite sprite;
            //switch (icon)
            //{
            //    case SpellEffectIcon.Heal:
            //        break;
            //    case SpellEffectIcon.Resurrect:
            //        break;
            //    case SpellEffectIcon.PurplePlus:
            //        break;
            //    case SpellEffectIcon.Poison:
            //        break;
            //    case SpellEffectIcon.Buff:
            //        break;
            //    case SpellEffectIcon.Debuff:
            //        break;
            //}
            hitEffect = () =>
            {
                var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.FadeInFadeOut).GetComponent<FadeInFadeOut>();
                obj.transform.SetPositionAndRotation(new Vector3(target.Position.X, target.Position.Y, 0), new Quaternion());
                obj.SpriteRenderer.sprite = State.GameManager.SpriteDictionary.SpellIcons[(int)icon - 1];
            };
        }

        effect.Setup(startLocation, endLocation, target, null, hitEffect);
    }


    private static void EightRing(GameObject prefab, Vec2 location, float distance)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0) & (y == 0)) continue;
                Object.Instantiate(prefab, new Vector3(location.X + x * distance, location.Y + y * distance, 0), new Quaternion());
            }
        }
    }
}