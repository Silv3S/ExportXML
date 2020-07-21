using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class CsvDataAttribute : DataAttribute
{
    private readonly string fileName;
    public CsvDataAttribute(string fileName)
    {
        this.fileName = fileName;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var pars = testMethod.GetParameters();
        var parameterTypes = pars.Select(par => par.ParameterType).ToArray();
        using (var csvFile = new StreamReader(fileName))
        {
            csvFile.ReadLine();
            string line;
            int i = 1;
            
            while ((line = csvFile.ReadLine()) != null)
            {
                line += "," + i.ToString();
                var row = line.Split(',');
                i++;
                yield return ConvertParameters((object[])row, parameterTypes);
            }
        }
    }

    private static object[] ConvertParameters(IReadOnlyList<object> values, IReadOnlyList<Type> parameterTypes)
    {
        var result = new object[parameterTypes.Count];
        for (var idx = 0; idx < parameterTypes.Count; idx++)
        {
            result[idx] = ConvertParameter(values[idx], parameterTypes[idx]);
        }

        return result;
    }

    private static object ConvertParameter(object parameter, Type parameterType)
    {
        return parameterType == typeof(int) ? Convert.ToInt32(parameter) : parameter;
    }
}