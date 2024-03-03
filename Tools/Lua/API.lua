---@meta

---@class ButtonType
---@field Skintone ButtonType
---@field HairColor ButtonType
---@field HairStyle ButtonType
---@field BeardStyle ButtonType
---@field BodyAccessoryColor ButtonType
---@field BodyAccessoryType ButtonType
---@field HeadType ButtonType
---@field EyeColor ButtonType
---@field EyeType ButtonType
---@field MouthType ButtonType
---@field BreastSize ButtonType
---@field CockSize ButtonType
---@field BodyWeight ButtonType
---@field ClothingType ButtonType
---@field Clothing2Type ButtonType
---@field ClothingExtraType1 ButtonType
---@field ClothingExtraType2 ButtonType
---@field ClothingExtraType3 ButtonType
---@field ClothingExtraType4 ButtonType
---@field ClothingExtraType5 ButtonType
---@field HatType ButtonType
---@field ClothingAccessoryType ButtonType
---@field ClothingColor ButtonType
---@field ClothingColor2 ButtonType
---@field ClothingColor3 ButtonType
---@field ExtraColor1 ButtonType
---@field ExtraColor2 ButtonType
---@field ExtraColor3 ButtonType
---@field ExtraColor4 ButtonType
---@field Furry ButtonType
---@field TailTypes ButtonType
---@field FurTypes ButtonType
---@field EarTypes ButtonType
---@field BodyAccentTypes1 ButtonType
---@field BodyAccentTypes2 ButtonType
---@field BodyAccentTypes3 ButtonType
---@field BodyAccentTypes4 ButtonType
---@field BodyAccentTypes5 ButtonType
---@field BallsSizes ButtonType
---@field VulvaTypes ButtonType
---@field AltWeaponTypes ButtonType

---@type ButtonType
ButtonType = nil

---@class IClothingRenderInput
---@field Actor IActorUnit Read only
---@field A IActorUnit Read only
---@field U IUnitRead Read only
---@field Sex string Read only
---@field SimpleWeaponSpriteFrontV1 string Read only
---@field SimpleWeaponSpriteBackV1 string Read only
---@field Sprites SpriteDictionary Read only
local IClothingRenderInput = {}



---@class IClothingRenderOutput
---@field RevealsBreasts boolean Write only
---@field BlocksBreasts boolean Write only
---@field RevealsDick boolean Write only
---@field SkipCheck boolean Write only
---@field InFrontOfDick boolean Write only
local IClothingRenderOutput = {}

---@param name string
---@param layer integer
---@return IRaceRenderOutput
function IClothingRenderOutput.NewSprite(name, layer) end

---@param layer integer
---@return IRaceRenderOutput
function IClothingRenderOutput.NewSprite(layer) end

---@param spriteType SpriteType
---@return IRaceRenderOutput
function IClothingRenderOutput.ChangeRaceSprite(spriteType) end

---@return nil
function IClothingRenderOutput.DisableBreasts() end

---@return nil
function IClothingRenderOutput.DisableDick() end

---@class IClothingSetupInput
---@field Sprites SpriteDictionary Read only
local IClothingSetupInput = {}



---@class IClothingSetupOutput
---@field LeaderOnly boolean 
---@field FemaleOnly boolean 
---@field MaleOnly boolean 
---@field ReqWinterHoliday boolean 
---@field DiscardSprite Sprite 
---@field ClothingId ClothingId 
---@field DiscardUsesPalettes boolean 
---@field OccupiesAllSlots boolean 
---@field DiscardUsesColor2 boolean 
---@field FixedColor boolean 
---@field RevealsBreasts boolean Write only
---@field RevealsDick boolean Write only
---@field BlocksBreasts boolean Write only
---@field InFrontOfDick boolean Write only
local IClothingSetupOutput = {}



---@class FlavorType
---@field PreyAdjectives FlavorType
---@field PredAdjectives FlavorType
---@field RaceSingleDescription FlavorType
---@field WeaponClaw FlavorType
---@field WeaponMelee1 FlavorType
---@field WeaponMelee2 FlavorType
---@field WeaponRanged1 FlavorType
---@field WeaponRanged2 FlavorType

---@type FlavorType
FlavorType = nil

---@class Gender
---@field None Gender
---@field Female Gender
---@field Male Gender
---@field Hermaphrodite Gender
---@field Gynomorph Gender
---@field Maleherm Gender
---@field Andromorph Gender
---@field Agenic Gender

---@type Gender
Gender = nil

---@class IActorUnit
---@field PredatorComponent PredatorComponent Read only
---@field Unit IUnitRead Read only
---@field Position Vec2I Read only
---@field AnimationController AnimationController Read only
---@field HasBelly boolean Read only
---@field HasPreyInBreasts boolean Read only
---@field HasBodyWeight boolean Read only
---@field IsAttacking boolean Read only
---@field IsEating boolean Read only
---@field IsOralVoring boolean Read only
---@field IsOralVoringHalfOver boolean Read only
---@field IsCockVoring boolean Read only
---@field IsBreastVoring boolean Read only
---@field IsUnbirthing boolean Read only
---@field IsTailVoring boolean Read only
---@field IsAnalVoring boolean Read only
---@field IsPouncingFrog boolean Read only
---@field HasJustVored boolean Read only
---@field HasJustFailedToVore boolean Read only
---@field IsDigesting boolean Read only
---@field IsAbsorbing boolean Read only
---@field IsBirthing boolean Read only
---@field IsSuckling boolean Read only
---@field IsBeingSuckled boolean Read only
---@field IsRubbing boolean Read only
---@field IsBeingRubbed boolean Read only
---@field SidesAttackedThisBattle ListOfSide Read only
---@field SquishedBreasts boolean 
---@field Surrendered boolean Read only
---@field Targetable boolean Read only
---@field BestRanged Weapon Read only
---@field HasAttackedThisCombat boolean Read only
---@field Visible boolean Read only
---@field DamagedColors boolean Read only
---@field TurnUsedShun integer Read only
local IActorUnit = {}

---@param frameNum integer
---@param time number
---@return nil
function IActorUnit.SetAnimationMode(frameNum, time) end

---@return integer
function IActorUnit.CheckAnimationFrame() end

---@param highestSprite integer
---@param multiplier? number
---@return integer
function IActorUnit.GetBallSize(highestSprite, multiplier) end

---@param highestSprite integer
---@param multiplier? number
---@return integer
function IActorUnit.GetTailSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetStomachSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetExclusiveStomachSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetWombSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetRootedStomachSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetLeftBreastSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetRightBreastSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetStomach2Size(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetCombinedStomachSize(highestSprite, multiplier) end

---@param highestSprite? integer
---@param multiplier? number
---@return integer
function IActorUnit.GetUniversalSize(highestSprite, multiplier) end

---@return integer
function IActorUnit.GetBodyWeight() end

---@return integer
function IActorUnit.GetWeaponSprite() end

---@return integer
function IActorUnit.GetSimpleBodySprite() end

---@return boolean
function IActorUnit.IsErect() end

---@class IButtonCustomizer
local IButtonCustomizer = {}

---@param type ButtonType
---@param text string
---@return nil
function IButtonCustomizer.SetText(type, text) end

---@param type ButtonType
---@param active boolean
---@return nil
function IButtonCustomizer.SetActive(type, active) end

---@class IGlobalLua
local IGlobalLua = {}

---@param message any
---@return nil
function _G.Log(message) end

---@param x number
---@param y number
---@param z number
---@return Vector3
function _G.NewVector3(x, y, z) end

---@param x number
---@param y number
---@return Vector2
function _G.NewVector2(x, y) end

---@param swap SwapType
---@param index integer
---@return ColorSwapPalette
function _G.GetPalette(swap, index) end

---@param swap SwapType
---@return integer
function _G.GetPaletteCount(swap) end

---@param stringId string
---@return IClothing
function _G.MakeClothing(stringId) end

---@param stringId string
---@param paramCalc fun(param1: IClothingRenderInput): table
---@return IClothing
function _G.MakeClothingWithParams(stringId, paramCalc) end

---@param maxValue integer
---@return integer
function _G.RandomInt(maxValue) end

---@param text string
---@return FlavorEntry
function _G.NewFlavorEntry(text) end

---@param min integer
---@param max integer
---@return StatRange
function _G.NewStatRange(min, max) end

---@param text string
---@param gender Gender
---@return FlavorEntry
function _G.NewFlavorEntryGendered(text, gender) end

---@class INameInput
local INameInput = {}

---@return Gender
function INameInput.GetGender() end

---@class IRaceStats
---@field Strength StatRange 
---@field Dexterity StatRange 
---@field Voracity StatRange 
---@field Mind StatRange 
---@field Agility StatRange 
---@field Stomach StatRange 
---@field Endurance StatRange 
---@field Will StatRange 
local IRaceStats = {}



---@class IRaceTraits
---@field BodySize integer 
---@field RaceAI RaceAI 
---@field StomachSize integer 
---@field HasTail boolean 
---@field FavoredStat Stat 
---@field RacialTraits ListOfTraitType 
---@field LeaderTraits ListOfTraitType 
---@field SpawnTraits ListOfTraitType 
---@field AllowedVoreTypes ListOfVoreType 
---@field SpawnRace Race 
---@field ConversionRace Race 
---@field LeaderRace Race 
---@field InnateSpells ListOfSpellType 
---@field RaceStats IRaceStats 
---@field ExpMultiplier number 
---@field PowerAdjustment number 
---@field CanUseRangedWeapons boolean 
---@field RaceDescription string 
local IRaceTraits = {}



---@class IUnitRead
---@field HairColor integer 
---@field HairStyle integer 
---@field BeardStyle integer 
---@field SkinColor integer 
---@field AccessoryColor integer 
---@field EyeColor integer 
---@field ExtraColor1 integer 
---@field ExtraColor2 integer 
---@field ExtraColor3 integer 
---@field ExtraColor4 integer 
---@field EyeType integer 
---@field MouthType integer 
---@field BreastSize integer 
---@field DickSize integer 
---@field HasVagina boolean 
---@field BodySize integer 
---@field SpecialAccessoryType integer 
---@field BodySizeManuallyChanged boolean 
---@field DefaultBreastSize integer 
---@field ClothingType integer 
---@field ClothingType2 integer 
---@field ClothingHatType integer 
---@field ClothingAccessoryType integer 
---@field ClothingExtraType1 integer 
---@field ClothingExtraType2 integer 
---@field ClothingExtraType3 integer 
---@field ClothingExtraType4 integer 
---@field ClothingExtraType5 integer 
---@field ClothingColor integer 
---@field ClothingColor2 integer 
---@field ClothingColor3 integer 
---@field Furry boolean 
---@field HeadType integer 
---@field TailType integer 
---@field FurType integer 
---@field EarType integer 
---@field BodyAccentType1 integer 
---@field BodyAccentType2 integer 
---@field BodyAccentType3 integer 
---@field BodyAccentType4 integer 
---@field BodyAccentType5 integer 
---@field BallsSize integer 
---@field VulvaType integer 
---@field BasicMeleeWeaponType integer 
---@field AdvancedMeleeWeaponType integer 
---@field BasicRangedWeaponType integer 
---@field AdvancedRangedWeaponType integer 
---@field DigestedUnits integer 
---@field KilledUnits integer 
---@field TimesKilled integer 
---@field IsDead boolean Read only
---@field Items Item[] Read only
---@field HasDick boolean Read only
---@field HasBreasts boolean Read only
---@field HasWeapon boolean Read only
---@field Level integer Read only
---@field Type UnitType Read only
---@field Predator boolean Read only
---@field Race Race 
---@field GetRace Race Read only
---@field ImmuneToDefections boolean 
---@field Side Side 
---@field InnateSpells ListOfSpellType 
---@field Name string 
---@field PreferredVoreType VoreType 
local IUnitRead = {}

---@return Gender
function IUnitRead.GetGender() end

---@param size integer
---@param update? boolean
---@return nil
function IUnitRead.SetDefaultBreastSize(size, update) end

---@class IRandomCustomInput
---@field Unit IUnitRead Read only
---@field SetupOutput ISetupOutput Read only
local IRandomCustomInput = {}



---@class IRaceRenderAllOutput
local IRaceRenderAllOutput = {}

---@param spriteType SpriteType
---@param layer integer
---@return IRaceRenderOutput
function IRaceRenderAllOutput.NewSprite(spriteType, layer) end

---@param spriteType SpriteType
---@return IRaceRenderOutput
function IRaceRenderAllOutput.ChangeSprite(spriteType) end

---@class IRaceRenderInput
---@field Actor IActorUnit Read only
---@field A IActorUnit Read only
---@field U IUnitRead Read only
---@field Sex string Read only
---@field SimpleWeaponSpriteFrontV1 string Read only
---@field SimpleWeaponSpriteBackV1 string Read only
---@field Sprites SpriteDictionary Read only
---@field RaceData ISetupOutput Read only
---@field BaseBody boolean Read only
local IRaceRenderInput = {}



---@class IRaceRenderOutput
local IRaceRenderOutput = {}

---@param hide Sprite
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(hide) end

---@param id string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, returnNull) end

---@param id string
---@param word1 string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, word1, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, word1, word2, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, word1, word2, word3, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param word4 string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, word1, word2, word3, word4, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param word4 string
---@param word5 string
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, word1, word2, word3, word4, word5, returnNull) end

---@param id string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite(id, number, returnNull) end

---@param id string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, number, returnNull) end

---@param id string
---@param word1 string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, word1, number, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, word1, word2, number, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, word1, word2, word3, number, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param word4 string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, word1, word2, word3, word4, number, returnNull) end

---@param id string
---@param word1 string
---@param word2 string
---@param word3 string
---@param word4 string
---@param word5 string
---@param number integer
---@param returnNull? boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.Sprite0(id, word1, word2, word3, word4, word5, number, returnNull) end

---@param layer integer
---@return IRaceRenderOutput
function IRaceRenderOutput.Layer(layer) end

---@param x number
---@param y number
---@return IRaceRenderOutput
function IRaceRenderOutput.AddOffset(x, y) end

---@param offset Vector2
---@return IRaceRenderOutput
function IRaceRenderOutput.AddOffset(offset) end

---@param x number
---@param y number
---@return IRaceRenderOutput
function IRaceRenderOutput.SetOffset(x, y) end

---@param offset Vector2
---@return IRaceRenderOutput
function IRaceRenderOutput.SetOffset(offset) end

---@param colorFunc Color?
---@return IRaceRenderOutput
function IRaceRenderOutput.Coloring(colorFunc) end

---@param paletteFunc ColorSwapPalette
---@return IRaceRenderOutput
function IRaceRenderOutput.Coloring(paletteFunc) end

---@param swap SwapType
---@param index integer
---@return IRaceRenderOutput
function IRaceRenderOutput.Coloring(swap, index) end

---@param paletteName string
---@param index integer
---@return IRaceRenderOutput
function IRaceRenderOutput.Palette(paletteName, index) end

---@param hide boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.SetHide(hide) end

---@param parent SpriteType
---@param worldPositionStays boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.SetTransformParent(parent, worldPositionStays) end

---@param active boolean
---@return IRaceRenderOutput
function IRaceRenderOutput.SetActive(active) end

---@param localScale Vector3
---@return IRaceRenderOutput
function IRaceRenderOutput.SetLocalScale(localScale) end

---@class ISetupOutput
---@field BreastSizes fun(): integer 
---@field DickSizes fun(): integer 
---@field FurCapable boolean 
---@field CanBeGender ListOfGender 
---@field ExtendedBreastSprites boolean 
---@field GentleAnimation boolean 
---@field BaseBody boolean 
---@field WeightGainDisabled boolean 
---@field HairColors integer 
---@field HairStyles integer 
---@field SkinColors integer 
---@field AccessoryColors integer 
---@field EyeTypes integer 
---@field AvoidedEyeTypes integer 
---@field EyeColors integer 
---@field SecondaryEyeColors integer 
---@field BodySizes integer 
---@field SpecialAccessoryCount integer 
---@field BeardStyles integer 
---@field MouthTypes integer 
---@field AvoidedMouthTypes integer 
---@field ExtraColors1 integer 
---@field ExtraColors2 integer 
---@field ExtraColors3 integer 
---@field ExtraColors4 integer 
---@field HeadTypes integer 
---@field TailTypes integer 
---@field FurTypes integer 
---@field EarTypes integer 
---@field BodyAccentTypes1 integer 
---@field BodyAccentTypes2 integer 
---@field BodyAccentTypes3 integer 
---@field BodyAccentTypes4 integer 
---@field BodyAccentTypes5 integer 
---@field BallsSizes integer 
---@field VulvaTypes integer 
---@field BasicMeleeWeaponTypes integer 
---@field AdvancedMeleeWeaponTypes integer 
---@field BasicRangedWeaponTypes integer 
---@field AdvancedRangedWeaponTypes integer 
---@field MainClothingTypesCount integer Read only
---@field AvoidedMainClothingTypes integer 
---@field ClothingColors integer 
---@field WholeBodyOffset Vector2 
---@field ClothingShift Vector3 
---@field WaistClothingTypesCount integer Read only
---@field ClothingHatTypesCount integer Read only
---@field ClothingAccessoryTypesCount integer Read only
---@field ExtraMainClothing1Count integer Read only
---@field ExtraMainClothing2Count integer Read only
---@field ExtraMainClothing3Count integer Read only
---@field ExtraMainClothing4Count integer Read only
---@field ExtraMainClothing5Count integer Read only
---@field AllowedMainClothingTypes ListOfIClothing Read only
---@field AllowedWaistTypes ListOfIClothing Read only
---@field AllowedClothingHatTypes ListOfIClothing Read only
---@field AllowedClothingAccessoryTypes ListOfIClothing Read only
---@field ExtraMainClothing1Types ListOfIClothing Read only
---@field ExtraMainClothing2Types ListOfIClothing Read only
---@field ExtraMainClothing3Types ListOfIClothing Read only
---@field ExtraMainClothing4Types ListOfIClothing Read only
---@field ExtraMainClothing5Types ListOfIClothing Read only
local ISetupOutput = {}

---@param singularName string
---@param pluralName string
---@return nil
function ISetupOutput.Names(singularName, pluralName) end

---@param singularName string
---@param pluralName fun(param1: INameInput): string
---@return nil
function ISetupOutput.Names(singularName, pluralName) end

---@param singularName fun(param1: INameInput): string
---@param pluralName string
---@return nil
function ISetupOutput.Names(singularName, pluralName) end

---@param singularName fun(param1: INameInput): string
---@param pluralName fun(param1: INameInput): string
---@return nil
function ISetupOutput.Names(singularName, pluralName) end

---@param nameList ListOfstring
---@return nil
function ISetupOutput.TownNames(nameList) end

---@param nameList ListOfstring
---@return nil
function ISetupOutput.PreyTownNames(nameList) end

---@param nameList ListOfstring
---@return nil
function ISetupOutput.IndividualNames(nameList) end

---@param wallType WallType
---@return nil
function ISetupOutput.WallType(wallType) end

---@param boneTypesGen fun(param1: IUnitRead): ListOfBoneInfo
---@return nil
function ISetupOutput.BonesInfo(boneTypesGen) end

---@param flavorText FlavorText
---@return nil
function ISetupOutput.FlavorText(flavorText) end

---@param type FlavorType
---@param ... FlavorEntry
---@return nil
function ISetupOutput.SetFlavorText(type, ...) end

---@param type FlavorType
---@param ... FlavorEntry
---@return nil
function ISetupOutput.AddFlavorText(type, ...) end

---@param raceTraits IRaceTraits
---@return nil
function ISetupOutput.RaceTraits(raceTraits) end

---@param setRaceTraits fun(param1: IRaceTraits): nil
---@return nil
function ISetupOutput.SetRaceTraits(setRaceTraits) end

---@param action fun(param1: IUnitRead,param2: IButtonCustomizer): nil
---@return nil
function ISetupOutput.CustomizeButtons(action) end

---@class RaceAI
---@field Standard RaceAI
---@field Hedonist RaceAI
---@field ServantRace RaceAI

---@type RaceAI
RaceAI = nil

---@class SpellType
---@field None SpellType
---@field Fireball SpellType
---@field PowerBolt SpellType
---@field LightningBolt SpellType
---@field Shield SpellType
---@field Mending SpellType
---@field Speed SpellType
---@field Valor SpellType
---@field Predation SpellType
---@field IceBlast SpellType
---@field Pyre SpellType
---@field Poison SpellType
---@field PreysCurse SpellType
---@field Maw SpellType
---@field Charm SpellType
---@field Summon SpellType
---@field Reanimate SpellType
---@field Enlarge SpellType
---@field Diminishment SpellType
---@field GateMaw SpellType
---@field ViralInfection SpellType
---@field DivinitysEmbrace SpellType
---@field Resurrection SpellType
---@field AmplifyMagic SpellType
---@field Evocation SpellType
---@field ManaFlux SpellType
---@field UnstableMana SpellType
---@field AlraunePuff SpellType
---@field Web SpellType
---@field GlueBomb SpellType
---@field ViperPoison SpellType
---@field Petrify SpellType
---@field HypnoGas SpellType
---@field Bind SpellType
---@field Whispers SpellType
---@field ViperDamage SpellType
---@field ForceFeed SpellType
---@field AssumeForm SpellType
---@field RevertForm SpellType
---@field ManaExpolsion SpellType

---@type SpellType
SpellType = nil

---@class SpriteType
---@field Body SpriteType
---@field Head SpriteType
---@field BodyAccent SpriteType
---@field BodyAccent2 SpriteType
---@field BodyAccent3 SpriteType
---@field BodyAccent4 SpriteType
---@field BodyAccent5 SpriteType
---@field BodyAccent6 SpriteType
---@field BodyAccent7 SpriteType
---@field BodyAccent8 SpriteType
---@field BodyAccent9 SpriteType
---@field BodyAccent10 SpriteType
---@field Hair SpriteType
---@field Hair2 SpriteType
---@field Hair3 SpriteType
---@field Beard SpriteType
---@field BodyAccessory SpriteType
---@field SecondaryAccessory SpriteType
---@field Belly SpriteType
---@field Weapon SpriteType
---@field BackWeapon SpriteType
---@field BodySize SpriteType
---@field Eyes SpriteType
---@field SecondaryEyes SpriteType
---@field Breasts SpriteType
---@field SecondaryBreasts SpriteType
---@field BreastShadow SpriteType
---@field Dick SpriteType
---@field Balls SpriteType
---@field Pussy SpriteType
---@field PussyIn SpriteType
---@field Anus SpriteType
---@field AnusIn SpriteType
---@field Mouth SpriteType

---@type SpriteType
SpriteType = nil

---@class Stat
---@field Strength Stat
---@field Dexterity Stat
---@field Voracity Stat
---@field Agility Stat
---@field Will Stat
---@field Mind Stat
---@field Endurance Stat
---@field Stomach Stat
---@field Leadership Stat
---@field None Stat

---@type Stat
Stat = nil

---@class SwapType
---@field NormalHair SwapType
---@field HairRedKeyStrict SwapType
---@field WildHair SwapType
---@field UniversalHair SwapType
---@field Fur SwapType
---@field FurStrict SwapType
---@field Skin SwapType
---@field RedSkin SwapType
---@field RedFur SwapType
---@field Mouth SwapType
---@field EyeColor SwapType
---@field LizardMain SwapType
---@field LizardLight SwapType
---@field SlimeMain SwapType
---@field SlimeSub SwapType
---@field Imp SwapType
---@field ImpDark SwapType
---@field ImpRedKey SwapType
---@field OldImp SwapType
---@field OldImpDark SwapType
---@field Goblins SwapType
---@field CrypterWeapon SwapType
---@field Clothing SwapType
---@field ClothingStrict SwapType
---@field ClothingStrictRedKey SwapType
---@field Clothing50Spaced SwapType
---@field SkinToClothing SwapType
---@field Kangaroo SwapType
---@field FeralWolfMane SwapType
---@field FeralWolfFur SwapType
---@field Alligator SwapType
---@field Crux SwapType
---@field BeeNewSkin SwapType
---@field DriderSkin SwapType
---@field DriderEyes SwapType
---@field AlrauneSkin SwapType
---@field AlrauneHair SwapType
---@field AlrauneFoliage SwapType
---@field DemibatSkin SwapType
---@field DemibatHumanSkin SwapType
---@field MermenSkin SwapType
---@field MermenHair SwapType
---@field AviansSkin SwapType
---@field DemiantSkin SwapType
---@field DemifrogSkin SwapType
---@field SharkSkin SwapType
---@field DeerSkin SwapType
---@field DeerLeaf SwapType
---@field SharkReversed SwapType
---@field Puca SwapType
---@field PucaBalls SwapType
---@field HippoSkin SwapType
---@field ViperSkin SwapType
---@field KomodosSkin SwapType
---@field KomodosReversed SwapType
---@field CockatriceSkin SwapType
---@field Harvester SwapType
---@field Bat SwapType
---@field Kobold SwapType
---@field Frog SwapType
---@field Dragon SwapType
---@field Dragonfly SwapType
---@field FairySpringSkin SwapType
---@field FairySpringClothes SwapType
---@field FairySummerSkin SwapType
---@field FairySummerClothes SwapType
---@field FairyFallSkin SwapType
---@field FairyFallClothes SwapType
---@field FairyWinterSkin SwapType
---@field FairyWinterClothes SwapType
---@field Ant SwapType
---@field GryphonSkin SwapType
---@field SlugSkin SwapType
---@field PantherSkin SwapType
---@field PantherHair SwapType
---@field PantherBodyPaint SwapType
---@field PantherClothes SwapType
---@field SalamanderSkin SwapType
---@field MantisSkin SwapType
---@field EasternDragon SwapType
---@field CatfishSkin SwapType
---@field GazelleSkin SwapType
---@field EarthwormSkin SwapType
---@field HorseSkin SwapType
---@field TerrorbirdSkin SwapType
---@field VargulSkin SwapType
---@field FeralLionsFur SwapType
---@field FeralLionsEyes SwapType
---@field FeralLionsMane SwapType
---@field GoodraSkin SwapType
---@field AabayxSkin SwapType

---@type SwapType
SwapType = nilundefined

---@class TraitType
---@field DoubleAttack TraitType
---@field Clever TraitType
---@field Foolish TraitType
---@field Resilient TraitType
---@field StrongMelee TraitType
---@field WeakAttack TraitType
---@field EasyToVore TraitType
---@field PackStrength TraitType
---@field PackDexterity TraitType
---@field PackVoracity TraitType
---@field PackDefense TraitType
---@field PackWill TraitType
---@field PackMind TraitType
---@field PackStomach TraitType
---@field PackTactics TraitType
---@field EscapeArtist TraitType
---@field FastDigestion TraitType
---@field SlowDigestion TraitType
---@field DefensiveStance TraitType
---@field Intimidating TraitType
---@field ThrillSeeker TraitType
---@field Ravenous TraitType
---@field Frenzy TraitType
---@field ArtfulDodge TraitType
---@field AdeptLearner TraitType
---@field Tempered TraitType
---@field Nauseous TraitType
---@field Tasty TraitType
---@field Disgusting TraitType
---@field EvasiveBattler TraitType
---@field SlowBreeder TraitType
---@field ProlificBreeder TraitType
---@field Flight TraitType
---@field Pounce TraitType
---@field BoggingSlime TraitType
---@field GelatinousBody TraitType
---@field Maul TraitType
---@field Biter TraitType
---@field Prey TraitType
---@field Paralyzer TraitType
---@field Pathfinder TraitType
---@field AstralCall TraitType
---@field MetalBody TraitType
---@field TentacleHarassment TraitType
---@field BornToMove TraitType
---@field Resourceful TraitType
---@field ForcefulBlow TraitType
---@field AcidResistant TraitType
---@field SoftBody TraitType
---@field KeenReflexes TraitType
---@field NimbleClimber TraitType
---@field StrongGullet TraitType
---@field Feral TraitType
---@field DualStomach TraitType
---@field Dazzle TraitType
---@field Charge TraitType
---@field MadScience TraitType
---@field Lethargic TraitType
---@field ShunGokuSatsu TraitType
---@field Eternal TraitType
---@field AcidImmunity TraitType
---@field Replaceable TraitType
---@field Greedy TraitType
---@field RangedVore TraitType
---@field HeavyPounce TraitType
---@field Cruel TraitType
---@field Large TraitType
---@field Small TraitType
---@field MagicResistance TraitType
---@field MagicProwess TraitType
---@field MetabolicSurge TraitType
---@field FastAbsorption TraitType
---@field SlowAbsorption TraitType
---@field EnthrallingDepths TraitType
---@field FearsomeAppetite TraitType
---@field TailStrike TraitType
---@field Endosoma TraitType
---@field IronGut TraitType
---@field SteadyStomach TraitType
---@field Stinger TraitType
---@field WillingRace TraitType
---@field Bulky TraitType
---@field PollenProjector TraitType
---@field Webber TraitType
---@field Camaraderie TraitType
---@field RaceLoyal TraitType
---@field SlowMovement TraitType
---@field VerySlowMovement TraitType
---@field Toxic TraitType
---@field GlueBomb TraitType
---@field HardSkin TraitType
---@field Vampirism TraitType
---@field TasteForBlood TraitType
---@field PleasurableTouch TraitType
---@field StretchyInsides TraitType
---@field QuickShooter TraitType
---@field FastCaster TraitType
---@field RangedIneptitude TraitType
---@field KeenShot TraitType
---@field HotBlooded TraitType
---@field FocusedDevelopment TraitType
---@field ManaRich TraitType
---@field ManaDrain TraitType
---@field CursedMark TraitType
---@field Slippery TraitType
---@field HealingBlood TraitType
---@field PoisonSpit TraitType
---@field SlowMetabolism TraitType
---@field LuckySurvival TraitType
---@field SenseWeakness TraitType
---@field BladeDance TraitType
---@field LightFrame TraitType
---@field Featherweight TraitType
---@field PeakCondition TraitType
---@field Fit TraitType
---@field Illness TraitType
---@field Diseased TraitType
---@field AntPheromones TraitType
---@field Fearless TraitType
---@field Clumsy TraitType
---@field Reformer TraitType
---@field Revenant TraitType
---@field AllOutFirstStrike TraitType
---@field VenomousBite TraitType
---@field Petrifier TraitType
---@field VenomShock TraitType
---@field Submissive TraitType
---@field Defenseless TraitType
---@field Tenacious TraitType
---@field PredGusher TraitType
---@field Honeymaker TraitType
---@field WetNurse TraitType
---@field TheGreatEscape TraitType
---@field Berserk TraitType
---@field HighlyAbsorbable TraitType
---@field IdealSustenance TraitType
---@field EfficientGuts TraitType
---@field WastefulProcessing TraitType
---@field Charmer TraitType
---@field ForceFeeder TraitType
---@field Reanimator TraitType
---@field Binder TraitType
---@field BookWormI TraitType
---@field BookWormIi TraitType
---@field BookWormIii TraitType
---@field Temptation TraitType
---@field Infertile TraitType
---@field HillImpedence TraitType
---@field LavaWalker TraitType
---@field SnowImpedence TraitType
---@field MountainWalker TraitType
---@field VolcanicImpedence TraitType
---@field DesertImpedence TraitType
---@field WaterWalker TraitType
---@field ForestImpedence TraitType
---@field SwampImpedence TraitType
---@field GrassImpedence TraitType
---@field Donor TraitType
---@field BookEater TraitType
---@field Whispers TraitType
---@field UnpleasantDigestion TraitType
---@field TraitBorrower TraitType
---@field Perseverance TraitType
---@field ManaAttuned TraitType
---@field NightEye TraitType
---@field KeenEye TraitType
---@field AccuteDodge TraitType
---@field SpellBlade TraitType
---@field ArcaneMagistrate TraitType
---@field ViralDigestion TraitType
---@field AwkwardShape TraitType
---@field Infiltrator TraitType
---@field Untamable TraitType
---@field Reincarnation TraitType
---@field Transmigration TraitType
---@field Metamorphosis TraitType
---@field Changeling TraitType
---@field GreaterChangeling TraitType
---@field ForcedMetamorphosis TraitType
---@field MetamorphicConversion TraitType
---@field Corruption TraitType
---@field InfiniteReincarnation TraitType
---@field InfiniteTransmigration TraitType
---@field Possession TraitType
---@field Parasite TraitType
---@field SpiritPossession TraitType
---@field TightNethers TraitType
---@field LightningSpeed TraitType
---@field DivineBloodline TraitType
---@field GeneEater TraitType
---@field InstantDigestion TraitType
---@field InstantAbsorption TraitType
---@field Inescapable TraitType
---@field Irresistable TraitType
---@field Titanic TraitType
---@field Tiny TraitType
---@field HealingBelly TraitType
---@field Assimilate TraitType
---@field AdaptiveBiology TraitType
---@field Huge TraitType
---@field Colossal TraitType
---@field AdaptiveTactics TraitType
---@field KillerKnowledge TraitType
---@field InfiniteAssimilation TraitType
---@field SynchronizedEvolution TraitType
---@field DigestionConversion TraitType
---@field DigestionRebirth TraitType
---@field EssenceAbsorption TraitType
---@field UnlimitedCapacity TraitType
---@field Inedible TraitType
---@field PredConverter TraitType
---@field PredRebirther TraitType
---@field SeductiveTouch TraitType
---@field HypnoticGas TraitType
---@field Extraction TraitType
---@field Annihilation TraitType
---@field Symbiote TraitType
---@field CreateSpawn TraitType
---@field Growth TraitType
---@field IncreasedGrowth TraitType
---@field DoubleGrowth TraitType
---@field PersistentGrowth TraitType
---@field PermanentGrowth TraitType
---@field MinorGrowth TraitType
---@field SlowedGrowth TraitType
---@field FleetingGrowth TraitType
---@field ProteinRich TraitType

---@type TraitType
TraitType = nil

---@class UnitType
---@field Soldier UnitType
---@field Leader UnitType
---@field Mercenary UnitType
---@field Summon UnitType
---@field SpecialMercenary UnitType
---@field Adventurer UnitType
---@field Spawn UnitType

---@type UnitType
UnitType = nil

---@class VoreType
---@field All VoreType
---@field Oral VoreType
---@field Unbirth VoreType
---@field CockVore VoreType
---@field BreastVore VoreType
---@field TailVore VoreType
---@field Anal VoreType

---@type VoreType
VoreType = nil

---@class WallType
---@field Fence WallType
---@field Stone WallType
---@field WoodenPallisade WallType
---@field Cat WallType
---@field Lizard WallType
---@field Scylla WallType
---@field Bunny WallType
---@field Crux WallType
---@field Crypter WallType
---@field Lamia WallType
---@field Fox WallType
---@field Imp WallType
---@field Slime WallType
---@field SlimeExtra WallType

---@type WallType
WallType = nil

---@generic T
---@param condition boolean
---@param result1 T
---@param result2 T
---@return T
function _G.ternary(condition, result1, result2) end

---@class ListOfstring
local ListOfstring = {}

---@param range table<integer, string>
function ListOfstring.AddRange(range) end

---@class ListOfIClothing
local ListOfIClothing = {}

---@param range table<integer, IClothing>
function ListOfIClothing.AddRange(range) end

---@class ListOfBoneInfo
local ListOfBoneInfo = {}

---@param range table<integer, BoneInfo>
function ListOfBoneInfo.AddRange(range) end

---@class ListOfTraitType
local ListOfTraitType = {}

---@param range table<integer, TraitType>
function ListOfTraitType.AddRange(range) end

---@class ListOfVoreType
local ListOfVoreType = {}

---@param range table<integer, VoreType>
function ListOfVoreType.AddRange(range) end

---@class ListOfSpellType
local ListOfSpellType = {}

---@param range table<integer, SpellType>
function ListOfSpellType.AddRange(range) end

---@class ListOfGender
local ListOfGender = {}

---@param range table<integer, Gender>
function ListOfGender.AddRange(range) end

---@class ListOfSide
local ListOfSide = {}

---@param range table<integer, Side>
function ListOfSide.AddRange(range) end


---@alias Vector2 userdata

---@alias Vector3 userdata

---@alias SpriteDictionary userdata

---@alias Sprite userdata

---@alias ClothingId userdata

---@alias PredatorComponent userdata

---@alias Vec2i userdata

---@alias Vec2I userdata

---@alias AnimationController userdata

---@alias Side userdata

---@alias Weapon userdata

---@alias Race userdata

---@alias StatRange userdata

---@alias IClothing userdata

---@alias ColorSwapPalette userdata

---@alias Item userdata

---@alias Color userdata

---@alias BoneInfo userdata

---@alias FlavorText userdata

---@alias FlavorEntry userdata