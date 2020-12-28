using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Shared
{
    public static class ExceptionHelper
    {

        public static Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendLine($"Type: {result.Entity.GetType().Name} was part of the problem. {GetValidationErrors(result.GetValidationResult().ValidationErrors)}");
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            return new Exception(builder.ToString(), dbu);
        }

        public static string GetValidationErrors(IEnumerable<DbValidationError> validationErrors)
        {
            if (!validationErrors.Any()) return string.Empty;

            var entityError = validationErrors.Select(x => x.ErrorMessage);
            var getFullMessage = string.Join("; ", entityError);
            return "errors are: " + getFullMessage;
        }

        public static void GetAllInnerExceptionMessage(Exception ex, ref List<string> errList)
        {
            if (ex.InnerException != null)
            {
                errList.Add(ex.InnerException.Message);
                if (ex.InnerException.InnerException != null)
                    GetAllInnerExceptionMessage(ex.InnerException, ref errList);
            }
        }
    }
}
