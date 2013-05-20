using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQTestMessage
{
    using System.Reflection;

    public class ReflectionHelper  
    {
        public List<Type> GetMessageList(string sAssemblyFileName)
        {
            var assem = Assembly.LoadFrom(sAssemblyFileName);
            List<Type> types = assem.GetTypes().Where(t => t.IsClass && !t.IsAbstract).OrderBy(x => x.FullName).ToList();

            return types;
        }

        public List<string> GetMessageProperties(Type type)
        {
            PropertyInfo[] list = type.GetProperties();
            return (from item in list select item.Name).ToList();
        }
    }
}
