public interface IRaceRenderInput
{
    IActorUnit Actor { get; }
    
    // Shortcuts 
    IActorUnit A { get; }
    IUnitRead U { get; }
    
    
    
    string Sex { get; }
    string SimpleWeaponSpriteFrontV1 { get; }
    string SimpleWeaponSpriteBackV1 { get; }
    
    SpriteDictionary Sprites { get; }
    
    ISetupOutput RaceData { get; }
    bool BaseBody { get; }
}