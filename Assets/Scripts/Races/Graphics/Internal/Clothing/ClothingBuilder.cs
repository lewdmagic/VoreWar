#region

using System;

#endregion

internal abstract class ClothingBuilderShared
{
    private protected ClothingMiscData Misc;

    public void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc)
    {
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = template.ShallowCopy();
        setMisc(input, copy);
        Misc = copy;
    }

    private class ClothingSetupInput : IClothingSetupInput
    {
        public SpriteDictionary Sprites => State.GameManager.SpriteDictionary;
    }

    private protected class ClothingRenderInputImpl : IClothingRenderInput
    {
        public ClothingRenderInputImpl(Actor_Unit actor)
        {
            Actor = actor;
        }

        public Actor_Unit Actor { get; private set; }
        public SpriteDictionary Sprites => State.GameManager.SpriteDictionary;
    }

    private protected abstract class ClothingDataShared : IClothingDataSimple
    {
        private protected readonly ClothingMiscData Misc;
        public IClothingDataFixed FixedData { get; set; }

        protected ClothingDataShared(ClothingMiscData fixedData)
        {
            Misc = fixedData;
            FixedData = fixedData;
        }

        public bool CanWear(Unit unit)
        {
            if (FixedData.MaleOnly && (unit.HasBreasts || unit.HasDick == false))
            {
                return false;
            }

            if (FixedData.FemaleOnly && unit.HasDick && unit.HasBreasts == false)
            {
                return false;
            }

            if (FixedData.LeaderOnly && unit.Type != UnitType.Leader)
            {
                return false;
            }

            if (FixedData.ReqWinterHoliday && Config.WinterActive() == false)
            {
                return false;
            }

            return true;
        }
    }
}


internal class ClothingBuilder : ClothingBuilderShared, IClothingBuilder
{
    internal static readonly ClothingMiscData DefaultMisc = new ClothingMiscData();
    private Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

    
    internal static ClothingBuilder New()
    {
        return new ClothingBuilder();
    }

    internal static ClothingBuilder<T> New<T>() where T : IParameters
    {
        return new ClothingBuilder<T>();
    }


    public void RenderAll(Action<IClothingRenderInput, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }

    [Obsolete("Old way of building.")]
    internal IClothing BuildClothing()
    {
        return new Clothing(Misc, _completeGen);
    }

    internal static IClothing Create(Action<IClothingBuilder> builderUser)
    {
        ClothingBuilder builder = new ClothingBuilder();
        builderUser.Invoke(builder);
        return builder.BuildClothing();
    }

    internal static IClothing<T> Create<T>(Action<IClothingBuilder<T>> builderUser) where T : IParameters
    {
        ClothingBuilder<T> builder = new ClothingBuilder<T>();
        builderUser.Invoke(builder);
        return builder.BuildClothing();
    }

    private class Clothing : ClothingDataShared, IClothing
    {
        private readonly Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

        public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput> completeGen) : base(fixedData)
        {
            _completeGen = completeGen;
        }

        public ClothingRenderOutput Configure(Actor_Unit actor, IParameters parameters, SpriteChangeDict changeDict)
        {
            IClothingRenderInput input = new ClothingRenderInputImpl(actor);
            ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
            _completeGen.Invoke(input, renderOutput);
            return renderOutput;
        }
    }
}

internal class ClothingBuilder<T> : ClothingBuilderShared, IClothingBuilder<T> where T : IParameters
{
    private Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;

    public void RenderAll(Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }

    [Obsolete("Old way of building.")]
    internal IClothing<T> BuildClothing()
    {
        return new Clothing<T>(Misc, _completeGen);
    }


    private class Clothing<TS> : ClothingDataShared, IClothing<TS> where TS : IParameters
    {
        private readonly Action<IClothingRenderInput<TS>, IClothingRenderOutput> _completeGen;

        public Clothing(ClothingMiscData misc, Action<IClothingRenderInput<TS>, IClothingRenderOutput> completeGen) : base(misc)
        {
            _completeGen = completeGen;
        }

        public ClothingRenderOutput Configure(Actor_Unit actor, TS state, SpriteChangeDict changeDict)
        {
            IClothingRenderInput<TS> input = new ClothingRenderInputImpl<TS>(actor, state);
            ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);

            _completeGen.Invoke(input, renderOutput);

            return renderOutput;
        }

        private class ClothingRenderInputImpl<TU> : ClothingRenderInputImpl, IClothingRenderInput<TU> where TU : IParameters
        {
            public ClothingRenderInputImpl(Actor_Unit actor, TU state) : base(actor)
            {
                Params = state;
            }

            public TU Params { get; private set; }
        }
    }
}