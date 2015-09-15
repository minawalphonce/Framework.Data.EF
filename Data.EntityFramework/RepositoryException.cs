using System;
using System.Data.Entity.Validation;
using System.Text;

namespace Framework.Data.EF
{
    public class RepositoryException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="innerException">The error message.</param>
        public RepositoryException(DbEntityValidationException innerException)
            : base("", innerException)
        {

        }

        public override string Message
        {
            get
            {
                return TranslateException();
            }
        }

        private string TranslateException()
        {
            StringBuilder strbld = new StringBuilder();
            foreach (var eve in ((DbEntityValidationException)this.InnerException).EntityValidationErrors)
            {
                strbld.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    strbld.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                }
            }
            return strbld.ToString();
        }


        #endregion Constructors
    }
}