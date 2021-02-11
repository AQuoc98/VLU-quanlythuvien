using CoreApp.Common.Constants;
using CoreApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace CoreApp.Common.Helpers
{
    public static class ObjectBuilder
    {
        #region Utilities
        /// <summary>
        /// Create Property for new Object
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }

        /// <summary>
        /// Get Type Builder
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dynamicModule"></param>
        /// <returns></returns>
        private static TypeBuilder GetTypeBuilder(string assemblyName, string dynamicModule)
        {
            var typeSignature = assemblyName;
            var an = new AssemblyName(typeSignature);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(dynamicModule);
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }

        /// <summary>
        /// Get Field Descriptions
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static IList<FieldDescriptor> GetFieldDescriptors(IList<DataColumn> columns)
        {
            var listOfFields = new List<FieldDescriptor>();
            foreach (var column in columns)
            {
                switch (column.DataTypeCode)
                {
                    case DataConstants.DataTypes.Checkbox:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(bool)));
                        break;
                    case DataConstants.DataTypes.Number:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(int?)));
                        break;
                    case DataConstants.DataTypes.Currency:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(decimal?)));
                        break;
                    case DataConstants.DataTypes.Date:
                    case DataConstants.DataTypes.DateTime:
                    case DataConstants.DataTypes.Time:
                    case DataConstants.DataTypes.DateRange:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(DateTime?)));
                        break;
                    case DataConstants.DataTypes.Email:
                    case DataConstants.DataTypes.Html:
                    case DataConstants.DataTypes.Image:
                    case DataConstants.DataTypes.Link:
                    case DataConstants.DataTypes.MultiSelect:
                    case DataConstants.DataTypes.Select:
                    case DataConstants.DataTypes.Text:
                    case DataConstants.DataTypes.MultiText:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(string)));
                        break;
                    case DataConstants.DataTypes.UniqueIdentifier:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(Guid)));
                        break;
                    default:
                        listOfFields.Add(new FieldDescriptor(column.SqlName, typeof(string)));
                        break;
                }
            }
            return listOfFields;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Complie Type Info
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dynamicModule"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static TypeInfo CompileResultTypeInfo(string assemblyName, string dynamicModule, IList<DataColumn> columns)
        {
            TypeBuilder tb = GetTypeBuilder(assemblyName, dynamicModule);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            var listOfFields = GetFieldDescriptors(columns);
            foreach (var field in listOfFields)
                CreateProperty(tb, field.FieldName, field.FieldType);

            TypeInfo objectTypeInfo = tb.CreateTypeInfo();
            return objectTypeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dynamicModule"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static TypeInfo CompileResultTypeInfo(string assemblyName, string dynamicModule, IList<string> properties)
        {
            TypeBuilder tb = GetTypeBuilder(assemblyName, dynamicModule);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (var prop in properties)
                CreateProperty(tb, prop, typeof(string));

            TypeInfo objectTypeInfo = tb.CreateTypeInfo();
            return objectTypeInfo;
        }

        /// <summary>
        /// Create New Object At Runtime
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dynamicModule"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static object CreateNewObject(string assemblyName, string dynamicModule, IList<DataColumn> columns)
        {
            var newTypeInfo = CompileResultTypeInfo(assemblyName, dynamicModule, columns);
            var newType = newTypeInfo.AsType();
            var newObject = Activator.CreateInstance(newType);

            return newObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dynamicModule"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static object CreateNewObject(string assemblyName, string dynamicModule, IList<string> properties)
        {
            var newTypeInfo = CompileResultTypeInfo(assemblyName, dynamicModule, properties);
            var newType = newTypeInfo.AsType();
            var newObject = Activator.CreateInstance(newType);

            return newObject;
        }

        /// <summary>
        /// Create Object
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static object CreateNewObject(TypeInfo typeInfo)
        {
            var newType = typeInfo.AsType();
            var newObject = Activator.CreateInstance(newType);

            return newObject;
        }

        /// <summary>
        /// Set value for object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="propName"></param>
        public static void SetValueForObject(object obj, object value, string propName)
        {
            var type = obj.GetType();
            var prop = type.GetProperty(propName);
            if (prop != null)
                prop.SetValue(obj, value);
        }

        /// <summary>
        /// Get prop value of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static T GetValueOfObject<T>(object obj, string propName)
        {
            var type = obj.GetType();
            var prop = type.GetProperty(propName);
            if (prop != null)
                return (T)prop.GetValue(obj);
            return default(T);
        }
        #endregion
    }
}
