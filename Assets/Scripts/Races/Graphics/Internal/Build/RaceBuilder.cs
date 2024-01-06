#region

using System;
using System.Collections.Generic;

#endregion

internal static class RaceBuilder
{
    internal static IRaceData Create(Func<MiscRaceDataWritableReadable<IParameters>> template, Action<IRaceBuilder<IParameters>> builderUser)
    {
        Func<IParameters> basic = () => new EmptyParameters();
        RaceBuilder<IParameters> builder = new RaceBuilder<IParameters>(template);
        builderUser.Invoke(builder);
        return builder.Build(basic);
    }

    internal static IRaceData Create<T>(Func<MiscRaceDataWritableReadable<T>> template, Action<IRaceBuilder<T>> builderUser) where T : IParameters, new()
    {
        Func<T> makeTempState = () => new T();
        RaceBuilder<T> builder = new RaceBuilder<T>(template);
        builderUser.Invoke(builder);
        return builder.Build(makeTempState);
    }
    internal static IRaceData CreateV2(Func<MiscRaceDataWritableReadable<IParameters>> template, Action<IRaceBuilderV2<IParameters>> builderUser)
    {
        Func<IParameters> basic = () => new EmptyParameters();
        RaceBuilderV2<IParameters> builder = new RaceBuilderV2<IParameters>(template);
        builderUser.Invoke(builder);
        return builder.Build(basic);
    }

    internal static IRaceData CreateV2<T>(Func<MiscRaceDataWritableReadable<T>> template, Action<IRaceBuilderV2<T>> builderUser) where T : IParameters, new()
    {
        Func<T> makeTempState = () => new T();
        RaceBuilderV2<T> builder = new RaceBuilderV2<T>(template);
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

    
    private readonly ExtraRaceInfo _extraRaceInfo = new ExtraRaceInfo();

    internal RaceBuilder(Func<MiscRaceDataWritableReadable<T>> template)
    {
        _template = template;
    }

    public void Names(string singularName, string pluralName)
    {
        _extraRaceInfo.SingularName = (input) => singularName;
        _extraRaceInfo.PluralName = (input) => pluralName;
    }

    public void Names(string singularName, Func<INameInput, string> pluralName)
    {
        _extraRaceInfo.SingularName = (input) => singularName;
        _extraRaceInfo.PluralName = pluralName;
    }

    public void Names(Func<INameInput, string> singularName, string pluralName)
    {
        _extraRaceInfo.SingularName = singularName;
        _extraRaceInfo.PluralName = (input) => pluralName;
    }
    
    public void Names(Func<INameInput, string> singularName, Func<INameInput, string> pluralName)
    {
        _extraRaceInfo.SingularName = singularName;
        _extraRaceInfo.PluralName = pluralName;
    }    
    
    public void WallType(WallType wallType)
    {
        _extraRaceInfo.WallType = wallType;
    }

    public void BonesInfo(Func<Unit, List<BoneInfo>> boneTypesGen)
    {
        _extraRaceInfo.BoneTypesGen = boneTypesGen;
    }

    public void FlavorText(FlavorText flavorText)
    {
        _extraRaceInfo.FlavorText = flavorText;
    }

    public void RaceTraits(RaceTraits raceTraits)
    {
        _extraRaceInfo.RaceTraits = raceTraits;
    }

    public void SetRaceTraits(Action<RaceTraits> setRaceTraits)
    {
        RaceTraits traits = new RaceTraits();
        setRaceTraits.Invoke(traits);
        _extraRaceInfo.RaceTraits = traits;
    }

    public void CustomizeButtons(Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> action)
    {
        _extraRaceInfo.CustomizeButtonsAction = action;
    }

    public void CustomizeButtons(Action<Unit, ButtonCustomizer> action)
    {
        _extraRaceInfo.CustomizeButtonsAction2 = action;
    }
    
    public void TownNames(List<string> nameList)
    {
        _extraRaceInfo.TownNames = nameList;
    }

    public void PreyTownNames(List<string> nameList)
    {
        _extraRaceInfo.PreyTownNames = nameList;
    }

    public void IndividualNames(List<string> nameList)
    {
        _extraRaceInfo.IndividualNames = nameList;
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

    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc<T>(layer, generator);
    }

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
        return new RaceData<T>(RaceSpriteSet, dataWritableReadable, _runBefore, _randomCustom, makeTempState, _extraRaceInfo, _renderAllAction);
    }
}

internal class RaceBuilderV2<T> : IRaceBuilderV2<T> where T : IParameters
{
    private readonly SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<T>>();

    private Action<IRandomCustomInput> _randomCustom;

    private Action<IRunInput, IRunOutput<T>> _runBefore;
    private Action<MiscRaceDataWritableReadable<T>> _setupFunc;
    private Action<IRunInput, IRaceRenderAllOutput<T>> _renderAllAction;
    private readonly Func<MiscRaceDataWritableReadable<T>> _template;

    internal RaceBuilderV2(Func<MiscRaceDataWritableReadable<T>> template)
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

    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc<T>(layer, generator);
    }

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