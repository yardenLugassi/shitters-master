using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

/// <summary>
/// Summary description for ShitterOperationExcel
/// </summary>
/// 
[DelimitedRecord(",")]  // comma separated values
[IgnoreFirst(1)]        // first line is assumed to be the header
[IgnoreEmptyLines]      // ignore empty lines
public class ShitterOperationExcel
{

    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string ShiterNumber;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string PaperType;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public int Amount;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string CoverType;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string OpDate;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string Customer;
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    public string SetupReq;

}