using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzakdolgozatErtekeloApi.Templates
{
    public class EvaluationCriterion
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ZeroPointText { get; set; } = string.Empty;

        public string OneTwoPointText { get; set; } = string.Empty;

        public string ThreeFourPointText { get; set; } = string.Empty;

        public string FivePointText { get; set; } = string.Empty;

        
    }
}
