using System;
using System.Security.Cryptography;
using System.Text;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public abstract class BaseFlag
    {
        public BaseFlag()
        {
        }

        /// <summary>
        /// Each flag has a unique ID based on its unique description.
        /// </summary>
        public Guid ID
        {
            get
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Description));
                    return new Guid(hash);
                }
            }
        }

        public abstract string Description { get; }
    }
}
