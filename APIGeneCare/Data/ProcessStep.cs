using System;
using System.Collections.Generic;

namespace APIGeneCare.Data;

public partial class ProcessStep
{
    public int StepId { get; set; }

    public string? StepName { get; set; }

    public virtual ICollection<TestProcess> TestProcesses { get; set; } = new List<TestProcess>();
}
