namespace Siska.Core
{
    using System.Collections.Generic;

    public class ErrorType
    {
        private Dictionary<string, string> ErrorList { get; set; }

        public bool IsInitialized { get; set; }

        public ErrorType()
        {
            IsInitialized = false;
        }

        public void Initialize()
        {
            ErrorList = new Dictionary<string, string>();

            ErrorList.Add(ErrorCode.SESSIONSERVICE_LAST_LOGGEDON_USER_DOESNT_EXIST, "Last logged on user doesn't exist");
            ErrorList.Add(ErrorCode.SESSIONSERVICE_SESSION_DOESNT_EXIST, "Session doesn't exist");
            ErrorList.Add(ErrorCode.SESSIONSERVICE_WRONG_USERNAME_PASSWORD, "Username or password is wrong!");

            IsInitialized = true;
        }

        public string ErrorText(string errorCode)
        {
            string error = string.Empty;

            error = ErrorList[errorCode];

            return error;
        }
    }
}
