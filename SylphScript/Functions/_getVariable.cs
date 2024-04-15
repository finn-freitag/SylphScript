using SylphScript.Helper;
using SylphScript.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _getVariable : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get { return type; } set { } }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_getVariable";

        public ArgResPermutation Parameters
        {
            get
            {
                return ArgResPermutation.Build().Add(type);
            }
        }

        public ReferenceName ReferenceObject { get; set; }

        public Modifiers Modifiers => Modifiers.None;

        private string VariableName = "";
        private ReferenceName type = "";
        private List<(object, TokenType)> tokens = null;

        private _getVariable() { }

        public _getVariable(string VariableName, ReferenceName type)
        {
            this.VariableName = VariableName;
            this.type = type;
        }

        public _getVariable(List<(object, TokenType)> tokens, ReferenceName lastTypeName)
        {
            type = lastTypeName;
            this.tokens = tokens;
        }

        public IFunction GetNewInstance()
        {
            return new _getVariable()
            {
                VariableName = VariableName,
                type = type,
                tokens = tokens,
            };
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (VariableName != "" && tokens == null)
                return variableHolder.GetVariable(VariableName);
            if (VariableName == "" && tokens != null)
            {
                VariableHolder currentVHolder = variableHolder;
                ObjectHolder currentValue = null;
                string lastReferenceObj = "";
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].Item2 == TokenType.Variable)
                    {
                        currentValue = currentVHolder.GetVariable((string)tokens[i].Item1);
                        lastReferenceObj = (string)tokens[i].Item1;
                    }
                    if (tokens[i].Item2 == TokenType.Function)
                    {
                        IFunction func = ((IFunction)tokens[i].Item1);
                        func.ReferenceObject = lastReferenceObj;
                        currentValue = func.GetResult(currentVHolder);
                        if (func.AssignedReturnType == "null")
                            continue;
                        IType t = TypeRegistry.FindType(currentValue.TypeFullName);
                        if (t is CustomType)
                            currentVHolder = currentValue.SubHolder;
                        else
                            currentVHolder = t.ConvertToVHolder(currentValue.Object);
                        if (currentVHolder == null)
                            currentVHolder = new VariableHolder();
                    }
                }
                return currentValue;
            }
            throw new SylphException("Internal error!");
        }
    }

    public enum TokenType
    {
        Function,
        Variable
    }
}
