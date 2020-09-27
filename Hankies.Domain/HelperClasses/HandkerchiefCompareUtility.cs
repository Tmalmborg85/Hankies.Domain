using System;
using System.Collections.Generic;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.HelperClasses
{
    public static class HandkerchiefCompareUtility
    {
        public static bool HandkerchiefSetsHaveOneOrMoreMatches(
            this HashSet<Handkerchief> handkerchiefs
            , HashSet<Handkerchief> other)
        {
            handkerchiefs.IntersectWith()
        }
    }
}
