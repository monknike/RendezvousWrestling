using RendezvousWrestling.FightSystem.Achievements;
using RendezvousWrestling.FightSystem.Features;
using System.Threading.Tasks;

public class RWFight : BaseFight<RendezVousWrestling, RWAchievement, RWActionFactory, RWActiveAction, RWFeature, RWFeatureFactory, RWFight, RWFighterState, RWFighterStats, RWModifier, RWUser, RWFeatureParameter>
{ 

    public RWFight() : base()
    {
    }

    public override void save()
    {

    }

    public override void nextTurn()
    {
        foreach (var fighter in this.Fighters)
        {
            fighter.triggerMods(TriggerMoment.Any, Trigger.TurnChange);
            if (!fighter.isInHold())
            {
                fighter.healFP(1);
            }
            if (fighter.focus < fighter.minFocus())
            {
                fighter.consecutiveTurnsWithoutFocus++;
            }
            else
            {
                fighter.consecutiveTurnsWithoutFocus = 0;
            }
        }
        base.nextTurn();
    }

    public override void punishPlayerOnForfeit(RWFighterState fighter)
    {
        this.message.addHit(string.Format(Messages.forfeitItemApply, fighter.getStylizedName(), fighter.maxBondageItemsOnSelf().ToString()));
        for (var i = 0; i < fighter.maxBondageItemsOnSelf(); i++)
        {
            //fighter.modifiers.Add(RWModifierFactory.getModifier(ModifierType.Bondage, this, fighter, null)); //TODO
        }
        this.message.addHit(string.Format(Messages.forfeitTooManyItems, fighter.getStylizedName()));
        fighter.triggerPermanentOutsideRing();
    }


}