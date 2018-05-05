using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    public class MinimumAgeCheck : ValidationAttribute
    {
        private readonly int _min;
        private readonly string _defaultErrorMessage = "";

        public MinimumAgeCheck(int min, string defaultErrorMessage)
            : base(defaultErrorMessage)
        {
            _min = min;
            _defaultErrorMessage = defaultErrorMessage.Replace("{0}", _min.ToString());
        }
    }
}
