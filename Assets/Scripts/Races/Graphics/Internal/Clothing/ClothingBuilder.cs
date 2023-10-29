#region

using System;

#endregion

internal abstract class ClothingBuilderShared
{
    private protected ClothingMiscData _misc;

    public void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc)
    {
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = template.ShallowCopy();
        setMisc(input, copy);
        _misc = copy;
    }

    private protected class ClothingSetupInput : IClothingSetupInput
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
        private protected ClothingMiscData _misc;
        public IClothingDataFixed FixedData { get; set; }

        protected ClothingDataShared(ClothingMiscData fixedData)
        {
            _misc = fixedData;
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
    internal static ClothingMiscData DefaultMisc = new ClothingMiscData();
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
        return new Clothing(_misc, _completeGen);
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
        private Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

        public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput> completeGen) : base(fixedData)
        {
            _completeGen = completeGen;
        }

        public ClothingRenderOutput Configure(Actor_Unit actor, IParameters parameters, SpriteChangeDict changeDict)
        {
            IClothingRenderInput input = new ClothingRenderInputImpl(actor);
            ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, _misc);
            _completeGen.Invoke(input, renderOutput);
            return renderOutput;
        }
    }
}

internal class ClothingBuilder<T> : ClothingBuilderShared, IClothingBuilder<T> where T : IParameters
{
    private protected Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;

    public void RenderAll(Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }

    [Obsolete("Old way of building.")]
    internal IClothing<T> BuildClothing()
    {
        return new Clothing<T>(_misc, _completeGen);
    }


    private class Clothing<S> : ClothingDataShared, IClothing<S> where S : IParameters
    {
        private Action<IClothingRenderInput<S>, IClothingRenderOutput> _completeGen;

        public Clothing(ClothingMiscData misc, Action<IClothingRenderInput<S>, IClothingRenderOutput> completeGen) : base(misc)
        {
            _completeGen = completeGen;
        }

        public ClothingRenderOutput Configure(Actor_Unit actor, S state, SpriteChangeDict changeDict)
        {
            IClothingRenderInput<S> input = new ClothingRenderInputImpl<S>(actor, state);
            ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, _misc);

            _completeGen.Invoke(input, renderOutput);

            return renderOutput;
        }

        private class ClothingRenderInputImpl<U> : ClothingRenderInputImpl, IClothingRenderInput<U> where U : IParameters
        {
            public ClothingRenderInputImpl(Actor_Unit actor, U state) : base(actor)
            {
                Params = state;
            }

            public U Params { get; private set; }
        }
    }
}