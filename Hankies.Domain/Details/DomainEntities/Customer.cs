using System;
using System.Collections.Generic;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class Customer : ICustomer
    {
        // A private contructor for ORM mappers like EF Core and serializers
        private Customer() { }

        //public Customer(Guid chatId, Guid voterId,
        //    IEnumerable<ICustomer> blockedCustomers)
        //{
        //}

        //public Customer(Guid chatId, Guid voterId,
        //    IEnumerable<ICustomer> blockedCustomers, IEnumerable<IPhoto> photos,
        //    IEnumerable<IAvatar> avatars)
        //{
        //}

        public Guid ChatID { get; private set; }
        public Guid VoterID { get; private set; }

        public IEnumerable<IPhoto> Photos { get; private set; }
        public List<IAvatar> Avatars { get; private set; }
        public List<ICustomer> BlockedCustomers { get; private set; }

        public Status<ICustomer> AddAvatar(IAvatar avatar)
        {
            var status = new Status<ICustomer>();

            try
            {
                if (Avatars == null)
                    Avatars = new List<IAvatar>();

                if (!Avatars.Contains(avatar))
                {
                    Avatars.Add(avatar);
                }
                else
                {
                    status.AddError("Avatar is already in list");
                }
            }
            catch (Exception ex)
            {
                status.AddException(ex);
            }

            status.RespondWith(this);

            return status;
        }

        public IStatus<ICustomer> AddPhoto(IPhoto photo)
        {
            throw new NotImplementedException();
        }

        public Status<ICustomer> BlockACustomer(ICustomer customerToBlock)
        {
            var status = new Status<ICustomer>();

            if (BlockedCustomers == null)
                BlockedCustomers = new List<ICustomer>();

            if (!BlockedCustomers.Contains(customerToBlock))
            {
                BlockedCustomers.Add(customerToBlock);
            }
            else
            {
                status.AddError("Customer already blocked.");
            }

            status.RespondWith(this);

            return status;
        }

        public void DownVote(IVoteable<object> votableThing)
        {
            throw new NotImplementedException();
        }

        public IStatus<IEnumerable<IPhoto>> NSFWPhotosOnly()
        {
            throw new NotImplementedException();
        }

        public void RemoveAvatar(IAvatar avatar)
        {
            throw new NotImplementedException();
        }

        public void RemovePhoto(IPhoto photo)
        {
            throw new NotImplementedException();
        }

        public IStatus<IEnumerable<string>> SelfDescriptions()
        {
            throw new NotImplementedException();
        }

        public IStatus<IEnumerable<IPhoto>> SFWPhotosOnly()
        {
            throw new NotImplementedException();
        }

        public void UpVote(IVoteable<object> voteableThing)
        {
            throw new NotImplementedException();
        }

        IStatus<ICustomer> ICustomer.AddAvatar(IAvatar avatar)
        {
            throw new NotImplementedException();
        }

        IStatus<ICustomer> ICustomer.BlockACustomer(ICustomer customerToBlock)
        {
            throw new NotImplementedException();
        }
    }
}
