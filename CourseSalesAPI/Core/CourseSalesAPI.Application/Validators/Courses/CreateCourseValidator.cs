using CourseSalesAPI.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Validators.Courses
{
    //TODO: view model nesnesi al parametre olarak !!
    public class CreateCourseValidator:AbstractValidator<Course>
    {

        public CreateCourseValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Ad kısmı boş bırakılmaz.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Lütfen kurs adını 5 ile 150 karakter arasında giriniz. ");
        }


    }
}
