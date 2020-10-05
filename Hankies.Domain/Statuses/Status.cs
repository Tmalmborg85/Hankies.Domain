using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class Status<T> : IStatus<T>
    {
        public IList<string> Errors { get; private set; }

        public bool ResponseIsRequired { get; private set; }

        public T ResponseObject { get; private set; }

        public string ErrorMessage => GenerateErrorMessage();

        public Status(bool responseIsRequired = false)
        {
            ResponseIsRequired = responseIsRequired;
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            if (!Errors.Contains(errorMessage))
            {
                Errors.Add(errorMessage);
            }
        }

        public void AddException(Exception exception)
        {
            var error = string.IsNullOrWhiteSpace(exception.Message) ?
                nameof(exception): exception.Message;

            AddError(error);
        }

        /// <remarks>
        /// If there are no errors and a response object is provided when
        /// required then the status is successfull
        /// </remarks>
        public bool IsSuccess()
        {
            if (Errors.Count > 0)
                return false;

            if (ResponseIsRequired)
            {
                this.AddError("Missing required response object of type " +
                    nameof(T));
                return ResponseObject != null;
            }

            return true;  
        }

        public void RespondWithObject(T responseObject)
        {
            ResponseObject = responseObject;
        }

        /// <summary>
        /// Concats all erros to one message. 
        /// </summary>
        /// <returns></returns>
        private string GenerateErrorMessage()
        {
            try
            {
                if (Errors.Count == 0)
                    return string.Empty;

                return string.Join(',', Errors);
            }
            catch 
            {
                return "Error generating error messages.";
            }
        }

    }
}
