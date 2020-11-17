

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Stealer
{
    public class Spy
    {
        public string CollectGettersAndSetters(string className)
        {
            var classType = Type.GetType(className);
            var classMethods = classType.GetMethods(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic);
           

            StringBuilder sb = new StringBuilder();

            foreach (var item in classMethods.Where(x => x.Name.StartsWith("get")))
            {
                sb.AppendLine($"{item.Name} will return {item.ReturnType}");
            }

            foreach (var item in classMethods.Where(x => x.Name.StartsWith("set")))
            {
                sb.AppendLine($"{item.Name} will set field of " +
                    $"{item.GetParameters().First().ParameterType}");
            }

            return sb.ToString().Trim();
        }
            public string RevealPrivateMethods( string className)
        {
            var classType = Type.GetType(className);
            var classPrivateMethods = classType.GetMethods(
                BindingFlags.Instance|
                BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"All private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");

            foreach (var item in classPrivateMethods)
            {
                sb.AppendLine(item.Name);
            }
            return sb.ToString().Trim();
        }
        public string AnalizeAcessModifiers(string investigatedClass)
        {
            var classType = Type.GetType(investigatedClass);
            var classFields = classType.GetFields(BindingFlags.Instance|BindingFlags.Public|BindingFlags.Static);
            var classPublicMethods = classType.GetMethods(BindingFlags.Instance|BindingFlags.Public);
     var classNonPublicMethods = classType.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();
            foreach (var item in classFields)
            {
                sb.AppendLine($"{item.Name} must be private!");
            }
            foreach (var item in classPublicMethods.Where(x=> x.Name.StartsWith("set")))
            {
                sb.AppendLine($"{item.Name} must be private");
            }

            foreach (var item in classNonPublicMethods.Where(x=>x.Name.StartsWith("get")))
            {
                sb.AppendLine($"{item.Name} must be public");
            }

            return sb.ToString().Trim();
        }
        public string StealFieldInfo(string className,params string[] fields)
        {
           

            var classType = Type.GetType(className);

            var classFields = classType.GetFields(BindingFlags.Instance|BindingFlags.Static|
                BindingFlags.Public|BindingFlags.NonPublic);
            StringBuilder sb = new StringBuilder();

            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            sb.AppendLine($"Class under investigation: {className}");
                       
            foreach (var item in classFields.Where(x=>fields.Contains(x.Name)))
            {
                sb.AppendLine($"{item.Name} = {item.GetValue(classInstance)}");
            }
            return sb.ToString().Trim();
        }

       
    }
}
