using System;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.HelperClasses
{
    /// <summary>
    /// Enforces rules related to handkerchiefs an avatar has said "no" to. 
    /// </summary>
    public static class HardNoHankyEnforcer
    {
        public static bool IHaveAnyOfThierHardNoHankies(Avatar me
            , Avatar them)
        {
            return them.DidHardNoAnyOfTheseHandkerchiefs(me.Handkerchiefs);
        }

        public static bool TheyHaveAnyOfMyHardNoHankies(Avatar me
             , Avatar them)
        {
            return me.DidHardNoAnyOfTheseHandkerchiefs(them.Handkerchiefs);
        }

        public static bool WeHaveAnyOfEachothersHardNoHankies(Avatar me
            , Avatar them)
        {
            return IHaveAnyOfThierHardNoHankies(me, them)
                || TheyHaveAnyOfMyHardNoHankies(me, them);
        }
    }
}
