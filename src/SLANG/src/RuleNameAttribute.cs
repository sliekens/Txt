﻿namespace SLANG
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class RuleNameAttribute : Attribute
    {
        private readonly string ruleName;

        public RuleNameAttribute(string ruleName)
        {
            this.ruleName = ruleName;
        }

        public string RuleName
        {
            get
            {
                return this.ruleName;
            }
        }
    }
}