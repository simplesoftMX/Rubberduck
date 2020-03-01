﻿using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Rubberduck.VBEditor;

namespace Rubberduck.Parsing.Inspections.Abstract
{
    public interface IInspectionListener: IParseTreeListener
    {
        IReadOnlyList<QualifiedContext<ParserRuleContext>> Contexts();
        IReadOnlyList<QualifiedContext<ParserRuleContext>> Contexts(QualifiedModuleName module);
        void ClearContexts();
        void ClearContexts(QualifiedModuleName module);
        QualifiedModuleName CurrentModuleName { get; set; }
    }
}