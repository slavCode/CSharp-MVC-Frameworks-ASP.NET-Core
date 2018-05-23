namespace CameraBazaar.Data.Models.Enums
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Flags]
    public enum LightMetering
    {
        Evaluative = 1,
        Spot = 2,
        [Display(Name = "Center Weighted")]
        CenterWeighted = 4
    }
}
