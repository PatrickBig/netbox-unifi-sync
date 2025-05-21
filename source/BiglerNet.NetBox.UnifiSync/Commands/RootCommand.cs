using DotMake.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Commands;

[CliCommand(Description = "Root command")]
public class RootCommand
{
    public void Run(CliContext context)
    {
        context.ShowHelp();
    }
}
