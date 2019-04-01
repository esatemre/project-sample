namespace TheProject.Core.ErrorHandlers
{
    using System;

    public class ErrorHandler : IErrorHandler
    {
        public string GetMessage(ErrorMessages message)
        {
            switch (message)
            {
                case ErrorMessages.EntityNull:
                    return "The entity passed is null {0}. Additional information: {1}";

                case ErrorMessages.ModelValidation:
                    return "The request data is not correct. Additional information: {0}";

                case ErrorMessages.InputRowColumnCountIsNotExpected:
                    return "The count of columns is not correct. Data: {0}";

                case ErrorMessages.UnexpectedError:
                    return "Unexpected error occured. Additional information: {0} Data: {1}";

                case ErrorMessages.WrongFormat:
                    return "The input is in wrong format. Wrong Formatted Field: {0} Data: {1}";

                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }

        }
    }

    public enum ErrorMessages
    {
        EntityNull = 1,
        ModelValidation = 2,
        InputRowColumnCountIsNotExpected = 3,
        UnexpectedError = 4,
        WrongFormat = 5
    }

    public interface IErrorHandler
    {
        string GetMessage(ErrorMessages message);
    }
}
