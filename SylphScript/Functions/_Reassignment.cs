using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _Reassignment : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_reassignment";

        public ArgResPermutation Parameters
        {
            get
            {
                return ArgResPermutation.Build().Add(value.AssignedReturnType);
            }
        }

        public ReferenceName ReferenceObject { get; set; }

        public Modifiers Modifiers => Modifiers.None;

        string variableName = "";
        List<(object, TokenType)> tokens;
        IFunction value = null;
        bool asReference = false;
        bool keepRefs = false;
        bool Readonly = false;

        public _Reassignment(string variableName, IFunction value, bool asReference, bool keepRefs, bool Readonly)
        {
            this.variableName = variableName;
            this.value = value;
            this.asReference = asReference;
            this.keepRefs = keepRefs;
            this.Readonly = Readonly;
        }

        public _Reassignment(List<(object, TokenType)> tokens, IFunction value, bool asReference, bool keepRefs, bool Readonly)
        {
            this.tokens = tokens;
            this.value = value;
            this.asReference = asReference;
            this.keepRefs = keepRefs;
            this.Readonly = Readonly;
        }

        public IFunction GetNewInstance()
        {
            return new _Reassignment(variableName, value, asReference, keepRefs, Readonly);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (variableName != "" && tokens == null)
            {
                var varContent = value.GetResult(variableHolder);
                if (asReference)
                {
                    variableHolder.SetVariable(variableName, varContent, keepRefs, Readonly);
                    return varContent;
                }
                else
                {
                    ObjectHolder clone = varContent.Clone();
                    variableHolder.SetVariable(variableName, clone, keepRefs, Readonly);
                    return clone;
                }
            }
            if (variableName == "" && tokens != null)
            {
                VariableHolder currentVHolder = variableHolder;
                ObjectHolder currentValue = null;
                string lastReferenceObj = "";
                string lastVarName = "";
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].Item2 == TokenType.Variable)
                    {
                        lastVarName = (string)tokens[i].Item1;
                        currentValue = currentVHolder.GetVariable(lastVarName);
                        lastReferenceObj = (string)tokens[i].Item1;
                    }
                    if (tokens[i].Item2 == TokenType.Function)
                    {
                        IFunction func = ((IFunction)tokens[i].Item1);
                        func.ReferenceObject = lastReferenceObj;
                        currentValue = func.GetResult(currentVHolder);
                        if (func.AssignedReturnType == "null")
                            continue;
                        currentVHolder = TypeRegistry.FindType(currentValue.TypeFullName).ConvertToVHolder(currentValue.Object);
                        if (currentVHolder == null)
                            currentVHolder = new VariableHolder();
                    }
                }
                var varContent = value.GetResult(variableHolder);
                if (asReference)
                {
                    currentVHolder.SetVariable(lastVarName, varContent, keepRefs, Readonly);
                    return varContent;
                }
                else
                {
                    ObjectHolder clone = varContent.Clone();
                    currentVHolder.SetVariable(lastVarName, clone, keepRefs, Readonly);
                    return clone;
                }
            }
            throw new InvalidOperationException();
        }
    }
}
