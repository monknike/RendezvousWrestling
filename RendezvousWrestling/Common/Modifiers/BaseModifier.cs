using RendezvousWrestling.Common.DataContext;
using System;
using System.Collections.Generic;

public abstract class BaseModifier<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType> : BaseEntity
    where TActionFactory : IActionFactory<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFeature : BaseFeature<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFeatureFactory : IFeatureFactory<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TUser : BaseUser<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFight : BaseFight<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFighterState : BaseFighterState<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TActiveAction : BaseActiveAction<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where OptionalParameterType : BaseFeatureParameter<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TAchievement : BaseAchievement<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TModifier : BaseModifier<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFighterStats : BaseFighterStats<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFightingGame : BaseFightingGame<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
{
    public string idModifier { get; set; }
    public int tier { get; set; }

    public int type { get; set; }
    public string name { get; set; }

    public bool areDamageMultipliers { get; set; } = false;
    public int diceRoll { get; set; }
    public int escapeRoll { get; set; }
    public int uses { get; set; }
    public Trigger triggeringEvent { get; set; }
    public TriggerMoment timeToTrigger { get; set; }
    public List<string> idParentActions { get; set; }

    public TFight fight { get; set; }
    public TFighterState applier { get; set; }
    public TFighterState receiver { get; set; }

    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime deletedAt { get; set; }

    public BaseModifier()
    {

    }

    public BaseModifier(string name, TFight fight, TFighterState receive, TFighterState applier, int tier, int uses, TriggerMoment timeToTrigger, Trigger triggeringEvent, List<string> parentActionIds = null)
    {
        this.idModifier = Guid.NewGuid().ToString();
        this.receiver = receiver;
        this.applier = applier;
        this.fight = fight;
        this.tier = tier;
        this.name = name;
        this.uses = uses;
        this.triggeringEvent = triggeringEvent;
        this.timeToTrigger = timeToTrigger;
        this.idParentActions = parentActionIds;
        this.initialize();
    }

    public void initialize()
    {
        this.areDamageMultipliers = false;
        this.diceRoll = 0;
    }

    public bool isOver()
    {
        return (this.uses <= 0); //note: removed "|| this.receiver.isTechnicallyOut()" in latest patch. may break things.
    }

    public void remove()
    {
        var indexModReceiver = this.receiver.modifiers.FindIndex(x => x.idModifier == this.idModifier);
        if (indexModReceiver != -1)
        {
            this.receiver.modifiers.RemoveAt(indexModReceiver);
        }

        if (this.applier != null)
        {
            var indexModApplier = this.applier.modifiers.FindIndex(x => x.idModifier == this.idModifier);
            if (indexModApplier != -1)
            {
                this.applier.modifiers.RemoveAt(indexModApplier);
            }
        }


        foreach (var mod in this.receiver.modifiers)
        {
            if (mod.idParentActions != null)
            {
                if (mod.idParentActions.Count == 1 && mod.idParentActions[0] == this.idModifier)
                {
                    mod.remove();
                }
                else if (mod.idParentActions.IndexOf(this.idModifier) != -1)
                {
                    mod.idParentActions.RemoveAt(mod.idParentActions.IndexOf(this.idModifier));
                }
            }
        }

        if (this.applier != null)
        {
            foreach (var mod in this.applier.modifiers)
            {
                if (mod.idParentActions != null)
                {
                    if (mod.idParentActions.Count == 1 && mod.idParentActions[0] == this.idModifier)
                    {
                        mod.remove();
                    }
                    else if (mod.idParentActions.IndexOf(this.idModifier) != -1)
                    {
                        mod.idParentActions.RemoveAt(mod.idParentActions.IndexOf(this.idModifier));
                    }
                }
            }
        }

    }

    public string trigger(TriggerMoment moment, Trigger triggeringEvent, dynamic objFightAction = null)
    {
        string messageAboutModifier = "";

        if (Utils.willTriggerForEvent(this.timeToTrigger, moment, this.triggeringEvent, triggeringEvent))
        {
            this.uses--;
            messageAboutModifier = "${this.receiver.getStylizedName()} is affected by the ${this.name}, ";
            if (!objFightAction)
            {
                messageAboutModifier += this.applyModifierOnReceiver(moment, triggeringEvent);
            }
            else
            {
                messageAboutModifier += this.applyModifierOnAction(moment, triggeringEvent, objFightAction);
            }

            if (this.isOver())
            {
                foreach (var fighter in this.fight.fighters)
                {
                    fighter.removeMod(this.idModifier);
                }
                messageAboutModifier += " and it is now expired.";
            }
            else
            {
                messageAboutModifier += " still effective for ${this.uses} more turns.";
            }

            this.fight.message.addSpecial(messageAboutModifier);
        }

        return messageAboutModifier;
    }

    public abstract string applyModifierOnReceiver(TriggerMoment moment, Trigger triggeringEvent);
    public abstract string applyModifierOnAction(TriggerMoment moment, Trigger triggeringEvent, dynamic objFightAction);
    public abstract bool isAHold();

}