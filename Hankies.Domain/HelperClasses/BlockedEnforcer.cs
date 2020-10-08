using System;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.HelperClasses
{
    /// <summary>
    /// Maintins methods for checking blocked status between customers, avatars, ect. 
    /// </summary>
    public static class BlockedEnforcer
    {
        /// <summary>
        /// The inverse of CustomersHaveBlockedEachother. 
        /// </summary>
        /// <param name="A">Customer A</param>
        /// <param name="B">Customer B</param>
        /// <returns></returns>
        public static bool CustomersHaveNotBlockedEachother(Customer A, Customer B)
        {
            return !CustomersHaveBlockedEachother(A, B);
        }

        /// <summary>
        /// Checks if either customer has blocked the other. The order of the
        /// customers is not important. 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool CustomersHaveBlockedEachother(Customer A, Customer B)
        {
            if (A.HasBlocked(B))
                return true;

            if (B.HasBlocked(A))
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the avatar owning customers have blocked eachother.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool AvatarCreatersHaveBlockedEachother(Avatar A, Avatar B)
        {
            return CustomersHaveBlockedEachother(A.Customer, B.Customer);
        }

        /// <summary>
        /// Checks to make sure the owning avatars have NOT blocked eachother.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool AvatarCreatersHaveNotBlockedEachother(Avatar A, Avatar B)
        {
            return CustomersHaveNotBlockedEachother(A.Customer, B.Customer);
        }
    }
}
