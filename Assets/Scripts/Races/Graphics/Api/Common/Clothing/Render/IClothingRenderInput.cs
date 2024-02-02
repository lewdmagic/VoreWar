public interface IClothingRenderInput
{
    Actor_Unit Actor { get; }
    
    // Shortcuts 
    Actor_Unit A { get; }
    Unit U { get; }
    
    string Sex { get; }
    string SimpleWeaponSpriteFrontV1 { get; }
    string SimpleWeaponSpriteBackV1 { get; }
    
    SpriteDictionary Sprites { get; }
}