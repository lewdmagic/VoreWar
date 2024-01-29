using System;
using System.Collections.Generic;

internal interface IRaceData
{
    ISetupOutput SetupOutput { get; }

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
    
    
    
    ExtraRaceInfo ExtraRaceInfo2 { get; }
    Action<IRandomCustomInput> RandomCustom { get; }
    Action<IRunInput, IRunOutput> RunBefore { get; }
    Action<IRunInput, IRaceRenderAllOutput> RenderAllAction { get; }
    SpriteTypeIndexed<SingleRenderFunc> RaceSpriteSet { get; }
    SetupOutput SetupOutputRaw { get; }
    
}