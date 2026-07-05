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
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Gambler.GamblerCode.Cards;

[Pool(typeof(GamblerCardPool))]
public class GamblerPlayingSafe() : GamblerCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[2]
    {
        new BlockVar(3M, ValueProp.Move),
        new DynamicVar("Power", 5)
    };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        GamblerPlayingSafe card = this;
        
        Decimal num = await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars.Block, cardPlay);
        
        NPowerUpVfx.CreateNormal(card.Owner.Creature);
        GamblerLuckPower? strengthPower = await PowerCmd.Apply<GamblerLuckPower>(choiceContext, card.Owner.Creature, card.DynamicVars["Power"].BaseValue, card.Owner.Creature, (CardModel) card);
    }

    protected override void OnUpgrade() => this.DynamicVars.Repeat.UpgradeValueBy(1M);
}