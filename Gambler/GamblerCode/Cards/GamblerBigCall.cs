using BaseLib.Utils;
using Gambler.GamblerCode.Character;
using Gambler.GamblerCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Gambler.GamblerCode.Cards;

[Pool(typeof(GamblerCardPool))]
public class GamblerBigCall () : GamblerCard(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[5]
    {
        new DamageVar(6M, ValueProp.Move),
        new RepeatVar(6),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedVar("Power").WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) =>
        {
            ICombatState combatState = card.CombatState!;
            return (combatState != null ? combatState.Allies.Where<Creature>((Func<Creature, bool>) (c => c.IsAlive)).Sum<Creature>((Func<Creature, int>) (c => c.GetPowerAmount<GamblerLuckPower>())) : 0);
        }))
    };
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        GamblerBigCall card = this;
        
        var rng = new RandomNumberGenerator();
        var my_random_number = rng.RandfRange(0, 100);
            
        int value = 1;
        if(cardPlay.Card.Owner.Creature.GetPower<GamblerLuckPower>()!.Amount > my_random_number) value = 2;

        for (int i = 0; i < card.DynamicVars.Repeat.IntValue; ++i)
        {
            AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(card.CurrentTarget).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
            if (cardPlay.Card.Owner.Creature.GetPower<GamblerLuckPower>()!.Amount < my_random_number) break;
        }
    }

    protected override void OnUpgrade() => this.DynamicVars.Repeat.UpgradeValueBy(1M);
}