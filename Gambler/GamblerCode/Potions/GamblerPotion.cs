using BaseLib.Abstracts;
using BaseLib.Utils;
using Gambler.GamblerCode.Character;

namespace Gambler.GamblerCode.Potions;

[Pool(typeof(GamblerPotionPool))]
public abstract class GamblerPotion : CustomPotionModel;