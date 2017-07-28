using System;
using System.ComponentModel.DataAnnotations;

namespace MRM.ViewModel
{
    public enum CampaignType
    {

        //[Display(Name = "None selected.")]
        //None_selected,
        [Display(Name= "BG Led")]
        BG_Led,
        GEPS
    }

    public enum InheritStatus
    {
        Draft,
        Active,
        Complete
    }

    public enum Status
    {        
        Draft,
        Complete
    }
}
