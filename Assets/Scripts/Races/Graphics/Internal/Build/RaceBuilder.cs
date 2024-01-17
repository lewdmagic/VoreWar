#region

using System;
using System.Collections.Generic;

#endregion

internal static class RaceBuilder
{
    internal static IRaceData CreateV2(Func<MiscRaceData> template, Action<IRaceBuilder> builderUser)
    {
        Func<IParameters> basic = () => new EmptyParameters();
        RaceBuilder<IParameters> builder = new RaceBuilder<IParameters>(template);
        builderUser.Invoke(builder);
        return builder.Build(basic);
    }

    internal static IRaceData CreateV2<T>(Func<MiscRaceData> template, Action<IRaceBuilder> builderUser) where T : IParameters, new()
    {
        Func<T> makeTempState = () => new T();
        RaceBuilder<T> builder = new RaceBuilder<T>(template);
        builderUser.Invoke(builder);
        return builder.Build(makeTempState);
    }

    private class EmptyParameters : IParameters
    {
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

internal class RaceBuilder<T> : IRaceBuilder where T : IParameters
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

    public IRaceData Build(Func<T> makeTempState)
    {
        MiscRaceData data = _template();
        _setupFunc?.Invoke(data);
        
        return new RaceData<T>(RaceSpriteSet, data, _runBefore, _randomCustom, makeTempState, data._extraRaceInfo, _renderAllAction);
    }
}