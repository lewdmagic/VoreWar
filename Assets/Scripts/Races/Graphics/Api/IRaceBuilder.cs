#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion


internal interface IRaceBuilder<T> where T : IParameters
{
    void Setup(Action<MiscRaceDataWritableReadable<T>> setupFunc);
    void RandomCustom(Action<IRandomCustomInput> value);
    void RunBefore(Action<IRunInput, IRunOutput<T>> value);
    
    void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator);

    void RenderSingle(SpriteType spriteType, SingleRenderFunc<T> render);
    
    void Names(string singularName, string pluralName);
    void Names(string singularName, Func<INameInput, string> pluralName);
    void Names(Func<INameInput, string> singularName, string pluralName);
    void Names(Func<INameInput, string> singularName, Func<INameInput, string> pluralName);

    void TownNames(List<string> nameList);
    void PreyTownNames(List<string> nameList);
    void IndividualNames(List<string> nameList);
    
    void WallType(WallType wallType);

    void BonesInfo(Func<Unit, List<BoneInfo>> boneTypesGen);

    void FlavorText(FlavorText flavorText);
    void RaceTraits(RaceTraits raceTraits);
    void SetRaceTraits(Action<RaceTraits> setRaceTraits);

    //void CustomizeButtons(Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> action);
    void CustomizeButtons(Action<Unit, ButtonCustomizer> action);
}