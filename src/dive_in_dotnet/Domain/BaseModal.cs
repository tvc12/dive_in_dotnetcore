using System;
namespace CatBasicExample.Domain
{
    public abstract class BaseModal
    {
        protected string id;

        protected DateTime createdAt;

        protected DateTime updatedAt;

        protected string createdBy;

        protected string updatedBy;
    }
}
