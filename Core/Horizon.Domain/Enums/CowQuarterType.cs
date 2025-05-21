using System.ComponentModel.DataAnnotations;

namespace Horizon.Domain.Enums;
public enum CowQuarterType
{

    [Display(Name = "زند شمال")]
    FrontLeft = 1,

    [Display(Name = "فخده شمال")]
    HindLeft = 2,

    [Display(Name = "زند يمين")]
    FrontRight = 3,

    [Display(Name = "فخده يمين")]
    HindRight = 4
}
