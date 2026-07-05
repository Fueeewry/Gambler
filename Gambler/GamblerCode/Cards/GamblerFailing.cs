using Gambler.GamblerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Gambler.GamblerCode.Cards;

public class GamblerFailing () : GamblerCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[2]
    {
        new DynamicVar("Power", -15),
        new EnergyVar(2)
    };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        GamblerFailing cardSource = this;
        NPowerUpVfx.CreateNormal(cardSource.Owner.Creature);
        GamblerLuckPower? strengthPower = await PowerCmd.Apply<GamblerLuckPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["Power"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }
}