using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Commands
{
    public interface ICompositeCommandBuilder
    {
        ICompositeCommandBuilder DefineCommand(ICommandDefinition commandDefinition);
        ICompositeCommandBuilder DefineCatchAllCommand(ICommandDefinition commandDefinition);
    }
}
