﻿
using System.Collections.Generic;

namespace Application
{
    public class ChangePasswordResult: IResult
    {
        public bool Status { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public bool Success { get; set; }
    }
}
