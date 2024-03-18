using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Common.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxFileSize;
    public MaxFileSizeAttribute(long maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }


    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return string.Concat("حداکثر سایز فایل", " ", _maxFileSize, " ", "بایت می باشد");
    }
}
