using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ui
{
    public static class PetOwnerConstants
    {
        public static class ErrorMessages
        {
            public static class Owner
            {
                public const string DataNotFound = "Owner not found";
                public const string DataAlreadyExists = "Owner already exist";
            }
            public static class PetOwner
            {
                public const string DataNotFound = "PetOwner not found";
                public const string DataAlreadyExists = "PetOwner already exist";
            }
            public static class Pet
            {
                public const string DataNotFound = "Pet not found";
                public const string DataAlreadyExists = "Pet already exist";
            }
        }
    }
}