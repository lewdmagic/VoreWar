#region

using System;
using System.Collections.Generic;

#endregion

enum ModdingMode
{
    Before,
    After
}

internal class RaceData : IRaceData
{
    public string SingularName(Unit unit)
    {
        return SingularName(unit.GetGender());
    }    
    
    public string PluralName(Unit unit)
    {
        return PluralName(unit.GetGender());
    }    
    
    public string SingularName(Gender gender)
    {
        INameInput input = new NameInput(gender);
        return ExtraRaceInfo2.SingularName(input);
    }    
    
    public string PluralName(Gender gender)
    {
        INameInput input = new NameInput(gender);
        return ExtraRaceInfo2.PluralName(input);
    }

    public WallType WallType()
    {
        return ExtraRaceInfo2.WallType ?? TacticalMode.DefaultType;
    }

    public List<BoneInfo> BoneInfo(Unit unit)
    {
        return ExtraRaceInfo2.BoneTypesGen?.Invoke(unit);
    }

    public FlavorText FlavorText()
    {
        return ExtraRaceInfo2.FlavorText ?? LogRaceData.DefaultFlavorText;
    }

    public void CustomizeButtons(Unit unit, EnumIndexedArray<ButtonType, CustomizerButton> buttons)
    {
        ExtraRaceInfo2?.CustomizeButtonsAction?.Invoke(unit, buttons);

        if (ExtraRaceInfo2?.CustomizeButtonsAction2 != null)
        {
            ButtonCustomizer buttonCustomizer = new ButtonCustomizer();
            ExtraRaceInfo2.CustomizeButtonsAction2.Invoke(unit, buttonCustomizer);
            buttonCustomizer.ApplyValues(buttons);
        }
    }

    public ExtraRaceInfo ExtraRaceInfo()
    {
        return ExtraRaceInfo2;
    }

    public IRaceTraits RaceTraits() => ExtraRaceInfo2.RaceTraits ?? RaceParameters.Default;


    
    public ExtraRaceInfo ExtraRaceInfo2 { get; private set; }
    public Action<IRandomCustomInput> RandomCustom { get; private set; }
    public Action<IRunInput, IRunOutput> RunBefore { get; private set; }
    public Action<IRunInput, IRaceRenderAllOutput> RenderAllAction { get; private set; }
    public SpriteTypeIndexed<SingleRenderFunc> RaceSpriteSet { get; private set; }
    public SetupOutput SetupOutputRaw { get; private set; }
    public ISetupOutput SetupOutput => SetupOutputRaw;
    
    public RaceData(
        SpriteTypeIndexed<SingleRenderFunc> raceSpriteSet,
        SetupOutput setupOutput,
        Action<IRunInput, IRunOutput> runBefore,
        Action<IRandomCustomInput> randomCustom,
        ExtraRaceInfo extraRaceInfo2,
        Action<IRunInput, IRaceRenderAllOutput> renderAllAction
        )
    {
        RaceSpriteSet = raceSpriteSet;
        SetupOutputRaw = setupOutput;
        RunBefore = runBefore;
        RandomCustom = randomCustom;
        ExtraRaceInfo2 = extraRaceInfo2;
        RenderAllAction = renderAllAction;
    }

    
    internal void ModifySingleRender(SpriteType spriteType, ModdingMode mode, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        SingleRenderFunc current = RaceSpriteSet[spriteType];
        if (current != null)
        {
            if (mode == ModdingMode.Before)
            {
                current.ModBefore(generator);
            }

            if (mode == ModdingMode.After)
            {
                current.ModAfter(generator);
            }
        }
        else
        {
            throw new Exception("Tried to modify " + spriteType + " which does not exist. Use ReplaceSingleRender instead");
        }
    }

    internal void ReplaceSingleRender(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc(layer, generator);
    }

    
    public void RandomCustomCall(Unit unit)
    {
        RandomCustom(new RandomCustomInput(unit, SetupOutputRaw));
    }
    

    private class RandomCustomInput : IRandomCustomInput
    {
        internal RandomCustomInput(Unit unit, ISetupOutput setupOutput)
        {
            Unit = unit;
            SetupOutput = setupOutput;
        }

        public IUnitRead Unit { get; }
        public ISetupOutput SetupOutput { get; }
    }
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ///////////////////////// API IMPLEMENTATIONS
}