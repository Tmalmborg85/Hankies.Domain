﻿using System;
using System.Collections.Generic;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class Avatar : IAvatar
    {

        public ICustomer Owner => throw new NotImplementedException();

        public IEnumerable<IAvatarCruiseSession> Sessions => throw new NotImplementedException();

        public IHandle Handle => throw new NotImplementedException();

        public bool Blindfolded => throw new NotImplementedException();

        public bool Hooded => throw new NotImplementedException();

        public IPhoto ImpressionPhoto => throw new NotImplementedException();

        public string ImpressionDescription => throw new NotImplementedException();

        public IEnumerable<IPhoto> ExposingPhotos => throw new NotImplementedException();

        public IEnumerable<IHandkerchief> Handkerchiefs => throw new NotImplementedException();

        public bool CanISeeThem(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public bool CanTheySeeMe(IAvatar they)
        {
            throw new NotImplementedException();
        }

        public void CruiseAnAvatar(IAvatar cruisee)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset CurrentExperation()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> DoffBlindfoldFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> DoffHoodFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public void EndCruiseSession()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatarCruiseSession> ExtendCurrentSession(ITimeExtension timeExtension)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAvatar> InteractableAvatars()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> ReDonBlindfoldFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> ReDonHoodFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> SearchForCruisableAvatars()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> SendMessage(IChatMessage message)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatarCruiseSession> StartNewCruiseSession(ICruiseCoordinates coordinates, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public void WasCruisedBy(IAvatar cruisee)
        {
            throw new NotImplementedException();
        }
    }
}
