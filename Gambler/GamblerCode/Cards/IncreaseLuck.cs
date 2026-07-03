using Gambler.GamblerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Gambler.GamblerCode.Cards;

public class IncreaseLuck () : GamblerCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            IEnumerable<DynamicVar> canonicalVars = [new PowerVar<StrengthPower>(5M)];
            return canonicalVars;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IncreaseLuck cardSource = this;
        NPowerUpVfx.CreateNormal(cardSource.Owner.Creature);
        LuckPower strengthPower = await PowerCmd.Apply<LuckPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["StrengthPower"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }
}