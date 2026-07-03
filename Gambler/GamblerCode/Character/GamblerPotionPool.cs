using BaseLib.Abstracts;
using Gambler.GamblerCode.Extensions;
using Godot;

namespace Gambler.GamblerCode.Character;

public class GamblerPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => Gambler.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}