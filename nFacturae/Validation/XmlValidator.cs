﻿#region License

//===================================================================================
//Copyright 2011 HexaSystems Corporation
//===================================================================================
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//http://www.apache.org/licenses/LICENSE-2.0
//===================================================================================
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
//===================================================================================

#endregion

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace nFacturae.Validation
{
    /// <summary>
    /// Static class used to validate an XML file.
    /// </summary>
    public static class XmlValidator
    {
        // Validation Error Count
        private static int _errorsCount;

        // Validation Error Message
        private static string _errorMessage = string.Empty;

        private static void validationHandler(object sender, ValidationEventArgs args)
        {
            _errorMessage = _errorMessage + args.Message + "\r\n";
            _errorsCount++;
        }

        public static void Validate(byte[] xmlDoc, XmlSchemaSet schemas)
        {
            _errorsCount = 0;
            _errorMessage = string.Empty;

            // Declare local objects
            XmlTextReader _XMLReader = null;
            XmlReaderSettings _settings = null;
            XmlReader _reader = null;

            // Create your fragment reader
            _XMLReader = new XmlTextReader(new MemoryStream(xmlDoc));

            // Add the schema to your reader settings
            _settings = new XmlReaderSettings();
            _settings.Schemas.Add(schemas);

            // Add validation event handler
            _settings.ValidationType = ValidationType.Schema;
            _settings.ValidationEventHandler += new ValidationEventHandler(validationHandler);

            // Create your reader with the validation
            _reader = XmlReader.Create(_XMLReader, _settings);

            // Validate XML data
            while (_reader.Read()) ;
                _reader.Close();

            // Raise exception, if XML validation fails
            if (_errorsCount > 0)
            {
                throw new XmlException(_errorMessage);
            }
        }

        /// <summary>
        /// Create a precompiled XMLSchemaSet.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="schemas"></param>
        /// <returns></returns>
        public static XmlSchemaSet CreateXmlSchemaSet(string schemaName, Dictionary<string, byte[]> schemas)
        {
            //Create and compile XmlSchemaSet
            using (XmlTextReader _XSDReader = new XmlTextReader(new MemoryStream(schemas[schemaName])))
            {
                XmlSchemaSet _schemaSet = new XmlSchemaSet();
                _schemaSet.XmlResolver = new SchemaResolver(schemas);
                _schemaSet.Add(null, _XSDReader);
                _schemaSet.Compile();
                return _schemaSet;
            }
        }

    }
}
