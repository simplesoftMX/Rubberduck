﻿using System.Collections.Generic;
using Rubberduck.Inspections.Abstract;
using Rubberduck.Parsing;
using Rubberduck.Parsing.Inspections.Abstract;
using Rubberduck.Parsing.Symbols;
using Rubberduck.VBEditor;

namespace Rubberduck.Inspections.Results
{
    public class DeclarationInspectionResult : InspectionResultBase
    {
        public DeclarationInspectionResult(
            IInspection inspection, 
            string description, 
            Declaration target, 
            QualifiedContext context = null,
            ICollection<string> disabledQuickFixes = null) 
            : base(inspection,
                 description,
                 context == null ? target.QualifiedName.QualifiedModuleName : context.ModuleName,
                 context == null ? target.Context : context.Context,
                 target,
                 target.QualifiedSelection,
                 GetQualifiedMemberName(target),
                 disabledQuickFixes)
        {}
        
        private static QualifiedMemberName? GetQualifiedMemberName(Declaration target)
        {
            if (string.IsNullOrEmpty(target?.QualifiedName.QualifiedModuleName.ComponentName))
            {
                return null;
            }

            return target.DeclarationType.HasFlag(DeclarationType.Member)
                ? target.QualifiedName
                : GetQualifiedMemberName(target.ParentDeclaration);
        }

        public override bool ChangesInvalidateResult(ICollection<QualifiedModuleName> modifiedModules)
        {
            return modifiedModules.Contains(Target.QualifiedModuleName)
                   || base.ChangesInvalidateResult(modifiedModules);
        }
    }

    public class DeclarationInspectionResult<TProperties> : DeclarationInspectionResult
    {
        private readonly TProperties _properties;

        public DeclarationInspectionResult(
            IInspection inspection, 
            string description, 
            Declaration target,
            TProperties properties, 
            QualifiedContext context = null,
            ICollection<string> disabledQuickFixes = null) :
            base(
                inspection,
                description,
                target,
                context,
                disabledQuickFixes)
        {
            _properties = properties;
        }

        public override T Properties<T>()
        {
            if (_properties is T properties)
            {
                return properties;
            }

            return base.Properties<T>();
        }
    }
}
