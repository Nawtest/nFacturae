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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace nFacturae
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //var signer = new nFacturae.Signer();
            //signer.Sign(@"..\..\Samples\sample_31.xml", "signed31.xml", "certificate.pfx", "password");

            //signer.Sign(@"..\..\Samples\sample_32.xml", "signed32.xml", "certificate.pfx", "password");

            //var inv = nFacturae.Facturae31.Facturae.FromFile(@"..\..\Samples\sample_31.xml");

            //var inv2 = nFacturae.Facturae32.Facturae.FromFile(@"..\..\Samples\sample_32.xml");

            var fe32 = new Facturae32.Facturae()
                .AddLegalParty(true, Facturae32.PersonTypeCodeType.J, Facturae32.ResidenceTypeCodeType.E, "79065414H",
                    "HexaSystems Corporation", "HexaSystems Address", "28019", "Madrid", "Madrid", Facturae32.CountryType.ESP)
                .AddIndividualParty(false, Facturae32.ResidenceTypeCodeType.E, "3422357Y", "Carlos",
                    "Mendible", "Carlos Address", "28019", "Madrid", "Madrid", Facturae32.CountryType.ESP)
                .AddInvoice((i) => 
                    {
                        i.HeatherAndIssueData("231418", Facturae32.InvoiceDocumentTypeType.FA,
                            Facturae32.InvoiceClassType.OO, DateTime.Now, Facturae32.CurrencyCodeType.EUR, Facturae32.LanguageCodeType.es)
                        .AddLine(l => l.Item("Item Description", 1, 2000).AddTax(Facturae32.TaxTypeCodeType.Item01, 16, 2000))  
                        .AddLine(l => l.Item("Item Description 2", 1, 100).AddTax(Facturae32.TaxTypeCodeType.Item01, 16, 100));  
                    });

            var fe32Text = fe32.ToString();
        }
    }
}
