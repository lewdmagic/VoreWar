#region

using System;
using System.Collections.Generic;

#endregion


internal class RaceDataMaker
{
    private readonly Func<ISetupInput, SetupOutput> _template;
    private readonly Action<IRaceBuilder> _builderUser;

    public RaceDataMaker(Func<ISetupInput, SetupOutput> template, Action<IRaceBuilder> builderUser)
    {
        _template = template;
        _builderUser = builderUser;
    }

    internal IRaceData Create()
    {
        RaceBuilder builder = new RaceBuilder(_template);
        _builderUser.Invoke(builder);
        return builder.Build();
    }
}

internal static class RaceBuilderStatic
{
    internal static RaceDataMaker CreateV2(Func<ISetupInput, SetupOutput> template, Action<IRaceBuilder> builderUser)
    {
        return new RaceDataMaker(template, builderUser);
    }
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
                new BoneInfo(BoneType.FurryBones, unit.Name)
            };
        }
        else
        {
            return new List<BoneInfo>
            {
                new BoneInfo(BoneType.GenericBonePile, unit.Name),
                new BoneInfo(BoneType.HumanoidSkull, "")
            };
        }
    };

    internal Func<INameInput, string> SingularName;
    internal Func<INameInput, string> PluralName;
    internal WallType? WallType;

    internal Func<Unit, List<BoneInfo>> BoneTypesGen = DefaultBoneInfo;
    internal FlavorText FlavorText = new FlavorText();
    internal IRaceTraits RaceTraits;

    internal Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> CustomizeButtonsAction;
    internal Action<Unit, ButtonCustomizer> CustomizeButtonsAction2;

    internal List<string> IndividualNames;
    internal List<string> TownNames;
    internal List<string> PreyTownNames;
}

internal class RaceBuilder : IRaceBuilder
{
    private readonly SpriteTypeIndexed<SingleRenderFunc> _raceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc>();

    private Action<IRandomCustomInput, IRandomCustomOutput> _randomCustom;

    private Action<IRunInput, IRunOutput> _runBefore;
    private Action<ISetupInput, SetupOutput> _setupFunc;
    private Action<IRunInput, IRaceRenderAllOutput> _renderAllAction;
    private readonly Func<ISetupInput, SetupOutput> _template;

    internal RaceBuilder(Func<ISetupInput, SetupOutput> template)
    {
        _template = template;
    }

    public void Setup(Action<ISetupInput, ISetupOutput> setupFunc)
    {
        _setupFunc = setupFunc;
    }

    public void RandomCustom(Action<IRandomCustomInput, IRandomCustomOutput> value)
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
        _raceSpriteSet[spriteType] = new SingleRenderFunc(layer, generator);
    }

    [Obsolete("RenderSingle is deprecated, use RenderAll when writing new races.")]
    public void RenderSingle(SpriteType spriteType, SingleRenderFunc render)
    {
        _raceSpriteSet[spriteType] = render;
    }

    public void RenderAll(Action<IRunInput, IRaceRenderAllOutput> generator)
    {
        _renderAllAction = generator;
    }

    public IRaceData Build()
    {
        SetupInput input = new SetupInput();
        SetupOutput data = _template(input);
        _setupFunc?.Invoke(input, data);

        return new RaceData(_raceSpriteSet, data, _runBefore, _randomCustom, data.ExtraRaceInfo, _renderAllAction);
    }
}