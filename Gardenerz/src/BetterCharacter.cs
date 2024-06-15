using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace Gardenerz;

[HarmonyPatch]
public class BetterCharacter
{
    public static List<CharacterClass> CharacterClasses = new()
    {
        new CharacterClass()
        {
            Code = "gardener",
            Gear = new JsonItemStack[]
            {
                new()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-head-straw-hat"),
                },
                new()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-upperbody-linen-tunic"),
                },
                new()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-upperbodyover-nomad-mantle"),
                },
                new()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-waist-linen-rope"),
                },
                new()
                {   
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-lowerbody-dirty-linen-trousers"),
                },
                new()
                {   
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game", "clothes-foot-nomad-boots"),
                }
            },
            Traits = new []
            {
                "green-fingers", "gardener", "healthy", "kind", "nearsighted", "nervous"
            }
        },
    };

    public static List<Trait> traits = new()
    {
        new Trait
        {
            Code = "green-fingers",  
            Attributes = new Dictionary<string, double>() {
                {"cropMultiplier", 1.1},
                {"wildCropMultiplier", 1.4},
                {"cropDropRate", 0.4},
                {"wildCropDropRate", 0.2},
            },
            Type = EnumTraitType.Positive
        },
        new Trait
        {
            Code = "gardener",  
            Attributes = new Dictionary<string, double>(),
            Type = EnumTraitType.Positive
        },
        new Trait
        {
            Code = "healthy",  
            Attributes = new Dictionary<string, double>() {
                {"maxhealthExtraPoints", 3}
            },
            Type = EnumTraitType.Positive
        },
    };
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof (CharacterSystem), "loadCharacterClasses")]
    public static void ShowCharacterClasses(CharacterSystem __instance)
    {
        foreach (var trait in traits.Where(trait => !__instance.traits.Contains(trait)))
        {
            __instance.traits.Add(trait);
            __instance.TraitsByCode[trait.Code] = trait;
        }
        
        foreach (var characterClass in CharacterClasses.Where(characterClass => !__instance.characterClasses.Contains(characterClass)))
        {
            __instance.characterClasses.Add(characterClass);
            __instance.characterClassesByCode[characterClass.Code] = characterClass;
        }
    }
}