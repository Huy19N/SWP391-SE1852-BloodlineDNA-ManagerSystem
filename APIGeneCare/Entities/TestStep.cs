using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class TestStep
{
    public int StepId { get; set; }

    public string? StepName { get; set; }

    public virtual ICollection<TestProcess> TestProcesses { get; set; } = new List<TestProcess>();
}
