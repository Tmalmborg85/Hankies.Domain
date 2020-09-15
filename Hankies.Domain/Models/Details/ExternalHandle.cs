using System;
using System.Text.RegularExpressions;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class ExternalHandle : IExternalHandle
    {
        // A private contructor for ORM mappers like EF Core and serializers
        private ExternalHandle() { }

        public ExternalHandle(string handle, Uri link,
            TrustedPlatforms platform)
        {
            var validHandleStatus = ValidateHandle(handle);
            if (!validHandleStatus.IsSuccess())
            {
                // Cant procede. Get & throw errors that caused failure. 
                throw new ArgumentOutOfRangeException(handle, handle
                    , validHandleStatus.ErrorMessage);
            }

            Handle = handle;
            Link = link;
            Platform = platform;
        }

        public string Handle { get; private set; }

        public Uri Link { get; private set; }

        public TrustedPlatforms Platform { get; private set; }

        public int HandleMinLength => 6;

        public int HandleMaxLength => 50;

        public string HandleAllowedCharacters => "^(a-zA-Z0-9_)";

        public IStatus<string> ValidateHandle(string handle)
        {
            var validationStatus = new Status<string>();

            // Check for reserved words.
            Regex reservedWordFilter = new Regex(ReserverdWords.Pattern());
            if (reservedWordFilter.IsMatch(handle))
            {
                validationStatus.AddError(handle + " matches a reserved word");
            }

            if (handle.Length < HandleMinLength)
                validationStatus.AddError("Handle is to short.");

            if (handle.Length > HandleMaxLength)
                validationStatus.AddError("Handle to long.");

            Regex allowedCharactersFilter = new Regex(HandleAllowedCharacters);
            if (!allowedCharactersFilter.IsMatch(handle))
            {
                validationStatus.AddError("Handle contains invalid characters");
            }

            return validationStatus;
        }
    }
}
