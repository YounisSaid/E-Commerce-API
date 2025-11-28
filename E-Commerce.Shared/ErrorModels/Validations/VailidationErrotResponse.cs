using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.ErrorModels.Validations
{
    public class ValidationErrorsResponse
    {
        public int StatusCode { get; set; } = 400;
        public string ErrorMessage { get; set; } = "One or more validation errors occurred.";
        public IEnumerable<ValidationErrors> Errors { get; set; }
    }

    public class ValidationErrors
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
