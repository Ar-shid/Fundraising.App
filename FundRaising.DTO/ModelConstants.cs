namespace CaseManager.Common
{
    public static class ModelConstants
    {
        public const int TextMaxLength = 4000;
        public const int NotesMaxLength = TextMaxLength;
        public const int DescriptionMaxLength = 2000;
        public const int CollectionMaxCount = 50;
        public const int UserIdMaxLength = 450;
        public const int RoleIdMaxLength = 450;
        public const int EmailAddressMaxLength = 320;
        public const int HangfireJobIdMaxLength = 50;
        public const int CommunicationBodyMaxLength = 2000000000;
        public const int CommunicationMaxLength = 4000;

        public static class ApiConstants
        {
            public const int ApiNameMaxLength = 256;
            public const int DomainMaxLength = 128;
            public const int IPAddressMaxLength = 56;
        }

        public static class UserConstants
        {
            public const int UserNameMaxLength = 256;
            public const int PasswordMaxLength = 256;
            public const int ClassificationMaxLength = 40;
            public const int NameMaxlength = 60;
            public const int InitialsMaxLength = 4;
            public const int InitialsMinLength = 2;
            public const int SsnMaxLength = 100;
            public const int PasswordMinLength = 6;
            public const int PasswordMinAuth0Length = 8;
            public const int JobTitleMaxLength = 100;
            public const int FirstNameMaxLength = 35;
            public const int MiddleNameMaxLength = 35;
            public const int LastNameMaxLength = 35;
            public const int MaxRoles = 1;
            public const int ImageUrlMaxLength = 1500000;
            public const int MaxUserLicense = 20;
        }

        public static class PermissionConstants
        {
            public const int DescriptionMaxLength = 100;
        }

    }
}
