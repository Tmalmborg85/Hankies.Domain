using System;
using System.Text.RegularExpressions;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Abstractions.Validators;

namespace Hankies.Domain.Models.Details
{
    public class Handle : IHandle
    {
        /// <summary>
        /// Private contructor for seralization
        /// </summary>
        private Handle(){}

        public Handle(string handle)
        {
            var handleStatus = ValidateHandle(handle);
            if (!handleStatus.IsSuccess())
                throw new ArgumentException(handle, handleStatus.ErrorMessage);

            HandleText = handle;
        }

        #region Rules
        public int HandleMinLength => 6;

        public int HandleMaxLength => 50;

        public string HandleAllowedCharacters => "^(a-zA-Z0-9_)";
        #endregion

        public string HandleText { get; }

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

            validationStatus.RespondWith(handle);

            return validationStatus;
        }
    }
}
