﻿

namespace TaskMgr.Domain
{
    public static class Codes
    {
        public static string EmailInUse => "email_in_use";
        public static string InvalidCredentials => "invalid_credentials";
        public static string InvalidCurrentPassword => "invalid_current_password";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string RefreshTokenNotFound => "refresh_token_not_found";
        public static string RefreshTokenAlreadyRevoked => "refresh_token_already_revoked";
        public static string UserNotFound => "user_not_found";
        public static string NotFound => "task_not_found";
    }
}
