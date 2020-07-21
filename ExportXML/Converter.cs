using System;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ExportXML
{
    public class Converter
    {
        public static string XMLConvertToCSV(string XMLFile, string outputFilename, bool saveToCSV)
        {
            XslTransform transformXSL = new XslTransform();
            transformXSL.Load("Cars.xslt");
            XPathDocument xPathDocument = new XPathDocument(XMLFile);

            if (saveToCSV)
            {
                StreamWriter writer = new StreamWriter(outputFilename);
                transformXSL.Transform(xPathDocument, null, writer, null);

                writer.Flush();
                writer.Close();
            }

            StringWriter stringWriter = new StringWriter();
            transformXSL.Transform(xPathDocument, null, stringWriter, null);
            stringWriter.Flush();
            stringWriter.Close();
            return stringWriter.ToString();
        }
    }
}
