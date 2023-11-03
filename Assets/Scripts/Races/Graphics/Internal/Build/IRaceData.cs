using System;
using System.Collections.Generic;

internal interface IRaceData
{
    IMiscRaceData MiscRaceData { get; }

    FullSpriteProcessOut NewUpdate(Actor_Unit actor);

    void RandomCustomCall(Unit unit);
    
    string SingularName(Unit unit);
    string PluralName(Unit unit);
    
    string SingularName(Gender gender);
    string PluralName(Gender gender);
    
    
    WallType WallType();

    List<BoneInfo> BoneInfo(Unit unit);

    FlavorText FlavorText();
    RaceTraits RaceTraits();
    void CustomizeButtons(Unit unit, EnumIndexedArray<ButtonType, CustomizerButton> buttons);

    ExtraRaceInfo ExtraRaceInfo();
}