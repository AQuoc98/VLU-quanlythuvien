using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Constants
{
    public static class DataConstants
    {
        public static class DataTypes
        {
            public const string Html = "HTML";
            public const string Checkbox = "CHECKBOX";
            public const string Number = "NUMBER";
            public const string UniqueIdentifier = "UNIQUEIDENTIFIER";
            public const string Select = "SELECT";
            public const string Email = "EMAIL";
            public const string Date = "DATE";
            public const string MultiSelect = "MULTISELECT";
            public const string Currency = "CURRENCY";
            public const string Text = "TEXT";
            public const string MultiText = "MULTITEXT";
            public const string Link = "LINK";
            public const string Image = "IMAGE";
            public const string DateTime = "DATETIME";
            public const string Time = "TIME";
            public const string DateRange = "DATERANGE";
        }

        public static class Modules
        {
            public const string User = "USER";
        }

        public static class SystemModules
        {
            public const string NinoSchool = "NinoSchool";
            public const string NinoBus = "NinoBus";
            public const string KCMC = "KCMC";
            public const string Both = "Both";
        }

        public static class Permissions
        {
            public const string View = "VIEW";
            public const string Add = "ADD";
            public const string Edit = "EDIT";
            public const string Delete = "DELETE";
        }

        public static class EnumCode
        {
            public const string RELATIONSHIP = "RELATIONSHIP";
            public const string SEX = "SEX";
            public const string TEMPLATE_PROBLEM_BUS = "TEMPLATE_PROBLEM_BUS";
        }

        public static class ResourceType
        {
            public const string Child = "Child";
            public const string Class = "Class";
            public const string CarriageWay = "CarriageWay";

            public const string News = "News";
            public const string Album = "Album";
            public const string SchoolFees = "SchoolFees";
            public const string Invite = "Invite";
            public const string SchoolFeesPaid = "SchoolFeesPaid";
            public const string CheckInOut = "CheckInOut";
            public const string CheckInOutBus = "CheckInOutBus";
            public const string Vacation = "Vacation";
            public const string MenuFood = "MenuFood";
            public const string Problem = "Problem";
            public const string Comment = "Comment";
            public const string Conversation = "Conversation";
            public const string DrugRemind = "DrugRemind";
        }
        public static class ActionType
        {
            public const string PermissionVacation = "REQ_PERMISSION";
            public const string Otp = "OTP";
            public const string Password = "PASSWORD";
            public const string Default = "DEFAULT";
        }

        public static class Sex
        {
            public static Guid Male = new Guid("686b303d-d81c-476a-a702-a4ffab5c2b1a");
            public static Guid Female = new Guid("20bca445-ca81-4c98-bf11-e68bac64a6ef");
        }

        public static class StatusChildInClass
        {
            public static Guid Enable = new Guid("C9215B27-150B-4D53-9710-001627CD768F");
            public static Guid Disable = new Guid("71365878-D884-45E5-8E2E-DEFBA87B971E");
        }

        public static class Relationship
        {
            public static Guid Father = new Guid("169a1974-fd21-4988-a5f3-0692034298dd");
            public static Guid Mom = new Guid("e9c74b6a-0c7d-4af2-9530-0b5f054723b5");
        }

        public static class Role
        {
            public static Guid Teacher = new Guid("8f0cb0da-0283-48bc-b101-c5b2e1d0092b");
            public static Guid Driver = new Guid("20bf31da-7d73-446f-9bf7-f7214f7f941e");
            public static Guid BabyCare = new Guid("17948782-addc-401a-83c7-664549c452eb");
        }
        public static class RoleName
        {
            public static string Teacher = "Teacher";
            public static string Driver = "Driver";
            public static string BabyCare = "BabyCare";
        }

        public static class RoleType
        {
            public static string School = "SCHOOL";
            public static string Bus = "BUS";
        }

        public static class Date
        {
            public static DateTime DateRelease = new DateTime(2019, 10, 17);
        }

        public static class Status
        {
            public const string BusTripOff = "BUS.STATUS.OFF";
            public const string BusTripGoing = "BUS.STATUS.ON_GOING";
            public const string BusTripFinish = "BUS.STATUS.FINISH";
        }
        public static class App
        {
            public const string SCHOOL = "SCHOOL";
            public const string PARENT = "PARENT";
        }
        public static class ImageRoute
        {
            public const string AvatarChildDefault = "images/avatar-default.png";
            public const string BackgroundChildDefault = "images/background-default.png";
            public const string IconCreateBusTrip = "images/icon-create-bus-trip.png";
        }
        public static class ServiceCode
        {
            public const string DinnerService = "DINNER_SERVICE";
            public const string StudyOnDayOff = "STUDY_ON_DAY_OFF";
        }

        public static class ResourceTypeFee
        {
            public const string FeeSchool = "FeeSchool";
            public const string FeeLate = "FeeLate";
            public const string FeeService = "FeeService";
        }

        public static class FeeType
        {
            public static Guid FeeSchoolOptional = new Guid("CB626779-2DBA-4D2E-8585-0670FB1B466F");
            public static Guid FeeSchoolRequired = new Guid("CA60D855-0408-4B98-A299-17151986025C");
            public static Guid FeeService = new Guid("13ACD369-7500-4442-83FB-E8D91E23DBA7");
            public static Guid FeeLate = new Guid("BCDDD58C-717C-4E10-8CC7-EAE52ABA4F3F");
        }

        public static class FeedBackType
        {
            public const string Code = "TYPE_FEEDBACK";
            public const string TypeAppCode = "APP_FEEDBACK";
            public const string TypeSchoolCode = "SCHOOL_FEEDBACK";
        }

        public static class ConversationType
        {
            public const string Code = "TYPE_CONVERSATION";
            public const string ParentToSchool = "PARENT_TO_SCHOOL";
            public const string ParentToTeacher = "PARENT_TO_TEACHER";
            public const string SchoolToParent = "SCHOOL_TO_PARENT";
            public const string TeacherToParent = "TEACHER_TO_PARENT";
        }

        public static class CommentType
        {
            public const string Code = "TYPE_COMMENT";
            //public static Guid STUDY_COMMENT = new Guid("1f9db41a-a60a-40ca-8643-24d556476df6");
            public static Guid SLEEP_COMMENT = new Guid("4851b1d2-bca2-4e26-a556-3c9d317de78e");
            //public static Guid NOTE_COMMENT = new Guid("14a9646d-a5e9-4630-8394-597108724bc8");
            public static Guid EAT_COMMENT = new Guid("8ce1418f-b90c-4c56-865e-6d1fc1fbdb1a");
            public static Guid NORMAL_COMMENT = new Guid("b2574976-1218-49c1-b62f-be14405677df");

        }
    }
}
