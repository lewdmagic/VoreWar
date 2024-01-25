using System;

public class RenderInput : IRenderInput
{
    public Actor_Unit Actor { get; }
    
    public Actor_Unit A => Actor;
    public Unit U => Actor.Unit;

            public string Sex => Actor.Unit.HasBreasts ? "female" : "male";

            // TODO 
            public string SimpleWeaponSpriteFrontV1
            {
                get
                {
                    int spriteIndex = Actor.GetWeaponSprite();
                    switch (Actor.GetWeaponSprite())
                    {
                        case 0: return "weapon_melee_front_hold_001";
                        case 1: return "weapon_melee_front_attack_001";
                        case 2: return "weapon_melee_front_hold_002";
                        case 3: return "weapon_melee_front_attack_002";
                        case 4: return "weapon_ranged_front_hold_001";
                        case 5: return "weapon_ranged_front_attack_001";
                        case 6: return "weapon_ranged_front_hold_002";
                        case 7: return "weapon_ranged_front_attack_002";
                        default: throw new Exception($"Unexpected weapon sprite index: {spriteIndex}");
                    }
                }
            }
    
            // TODO 
            public string SimpleWeaponSpriteBackV1
            {
                get
                {
                    int spriteIndex = Actor.GetWeaponSprite();
                    switch (Actor.GetWeaponSprite())
                    {
                        case 0: return "weapon_melee_back_hold_001";
                        case 1: return "weapon_melee_back_attack_001";
                        case 2: return "weapon_melee_back_hold_002";
                        case 3: return "weapon_melee_back_attack_002";
                        case 4: return "weapon_ranged_back_hold_001";
                        case 5: return "weapon_ranged_back_attack_001";
                        case 6: return "weapon_ranged_back_hold_002";
                        case 7: return "weapon_ranged_back_attack_002";
                        default: throw new Exception($"Unexpected weapon sprite index: {spriteIndex}");
                    }
                }
            }
    
    public SpriteDictionary Sprites => State.GameManager.SpriteDictionary;
    
    public RenderInput(Actor_Unit actor)
    {
        Actor = actor;
    }
}