#region

using System;
using System.Collections.Generic;

#endregion

internal static class RaceBuilder
{
    internal static IRaceData CreateV2(Func<MiscRaceDataWritableReadable<IParameters>> template, Action<IRaceBuilder<IParameters>> builderUser)
    {
        Func<IParameters> basic = () => new EmptyParameters();
        RaceBuilder<IParameters> builder = new RaceBuilder<IParameters>(template);
        builderUser.Invoke(builder);
        return builder.Build(basic);
    }

    internal static IRaceData CreateV2<T>(Func<MiscRaceDataWritableReadable<T>> template, Action<IRaceBuilder<T>> builderUser) where T : IParameters, new()
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


internal class RaceBuilder<T> : IRaceBuilder<T> where T : IParameters
{
    private readonly SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<T>>();

    private Action<IRandomCustomInput> _randomCustom;

    private Action<IRunInput, IRunOutput<T>> _runBefore;
    private Action<MiscRaceDataWritableReadable<T>> _setupFunc;
    private Action<IRunInput, IRaceRenderAllOutput<T>> _renderAllAction;
    private readonly Func<MiscRaceDataWritableReadable<T>> _template;

    internal RaceBuilder(Func<MiscRaceDataWritableReadable<T>> template)
    {
        _template = template;
    }
    
    public void Setup(Action<MiscRaceDataWritableReadable<T>> setupFunc)
    {
        _setupFunc = setupFunc;
    }

    public void RandomCustom(Action<IRandomCustomInput> value)
    {
        _randomCustom = value;
    }

    public void RunBefore(Action<IRunInput, IRunOutput<T>> value)
    {
        _runBefore = value;
    }

    [Obsolete("RenderSingle is deprecated, use RenderAll when writing new races.")]
    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc<T>(layer, generator);
    }

    [Obsolete("RenderSingle is deprecated, use RenderAll when writing new races.")]
    public void RenderSingle(SpriteType spriteType, SingleRenderFunc<T> render)
    {
        RaceSpriteSet[spriteType] = render;
    }

    public void RenderAll(Action<IRunInput, IRaceRenderAllOutput<T>> generator)
    {
        _renderAllAction = generator;
    }

    public IRaceData Build(Func<T> makeTempState)
    {
        MiscRaceDataWritableReadable<T> dataWritableReadable = _template();
        _setupFunc?.Invoke(dataWritableReadable);
        
        return new RaceData<T>(RaceSpriteSet, dataWritableReadable, _runBefore, _randomCustom, makeTempState, dataWritableReadable._extraRaceInfo, _renderAllAction);
    }
}