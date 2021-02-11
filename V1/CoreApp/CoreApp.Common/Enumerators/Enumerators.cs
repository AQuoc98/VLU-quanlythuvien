using System;

namespace CoreApp.Common.Enums
{
    public enum LoginResult
    {
        Succeeded = 1,
        Failure = 2
    }
    public enum ResultStatus
    {
        Success = 1,
        Error = 2,
        ValidateFail = 3,
        CredentialTypeInvalid = 4,
        CreadentialInvalid = 5,
        AccountNeedVerify = 6,
        AccountNeedFillInformations = 7
    }
}
