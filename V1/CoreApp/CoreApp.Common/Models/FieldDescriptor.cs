using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Models
{
    public class FieldDescriptor
    {
        public FieldDescriptor(string fieldName, Type fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }
        public string FieldName { get; }
        public Type FieldType { get; }
    }
}
