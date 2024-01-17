#region

using System;
using System.Collections.Generic;

#endregion


internal class RaceDataMaker
{

    private readonly RaceBuilder _builder;

    public RaceDataMaker(RaceBuilder builder)
    {
        _builder = builder;
    }

    internal IRaceData Create(Race race)
    {
        return _builder.Build(race);
    }
}

internal static class RaceBuilderStatic
{
    internal static RaceDataMaker CreateV2(Func<MiscRaceData> template, Action<IRaceBuilder> builderUser)
    {
        RaceBuilder builder = new RaceBuilder(template);
        builderUser.Invoke(builder);
        return new RaceDataMaker(builder);
    }
}

public interface INameInput
{
    Gender GetGender();
}

internal class NameInput : INameInput
{
    public Gender GetGender() => _gender;
    private readonly Gender _gender;

    public NameInput(Gender gender)
    {
        _gender = gender;
    }
}

// TODO find a better name probably
internal class ExtraRaceInfo
{
    private static readonly Func<Unit, List<BoneInfo>> DefaultBoneInfo = (unit) =>
    {
        if (unit.Furry)
        {
            return new List<BoneInfo>
            {
                new BoneInfo(BoneTypes.FurryBones, unit.Name)
            };
        }
        else
        {
            return new List<BoneInfo>
            {
                new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                new BoneInfo(BoneTypes.HumanoidSkull, "")
            };
        }
    };
    
    internal Func<INameInput, string> SingularName;
    internal Func<INameInput, string> PluralName;
    internal WallType? WallType;

    internal Func<Unit, List<BoneInfo>> BoneTypesGen = DefaultBoneInfo;
    internal FlavorText FlavorText;
    internal RaceTraits RaceTraits;

    internal Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> CustomizeButtonsAction;
    internal Action<Unit, ButtonCustomizer> CustomizeButtonsAction2;

    internal List<string> IndividualNames;
    internal List<string> TownNames;
    internal List<string> PreyTownNames;
}

internal class RaceBuilder : IRaceBuilder
{
    private readonly SpriteTypeIndexed<SingleRenderFunc> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc>();

    private Action<IRandomCustomInput> _randomCustom;

    private Action<IRunInput, IRunOutput> _runBefore;
    private Action<MiscRaceData> _setupFunc;
    private Action<IRunInput, IRaceRenderAllOutput> _renderAllAction;
    private readonly Func<MiscRaceData> _template;

    internal RaceBuilder(Func<MiscRaceData> template)
    {
        _template = template;
    }
    
    public void Setup(Action<MiscRaceData> setupFunc)
    {
        _setupFunc = setupFunc;
    }

    public void RandomCustom(Action<IRandomCustomInput> value)
    {
        _randomCustom = value;
    }

    public void RunBefore(Action<IRunInput, IRunOutput> value)
    {
        _runBefore = value;
    }

    [Obsolete("RenderSingle is deprecated, use RenderAll when writing new races.")]
    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc(layer, generator);
    }

    [Obsolete("RenderSingle is deprecated, use RenderAll when writing new races.")]
    public void RenderSingle(SpriteType spriteType, SingleRenderFunc render)
    {
        RaceSpriteSet[spriteType] = render;
    }

    public void RenderAll(Action<IRunInput, IRaceRenderAllOutput> generator)
    {
        _renderAllAction = generator;
    }

    public IRaceData Build(Race race)
    {
        MiscRaceData data = _template();
        _setupFunc?.Invoke(data);
        
        return new RaceData(RaceSpriteSet, data, _runBefore, _randomCustom, data._extraRaceInfo, _renderAllAction, race);
    }
}