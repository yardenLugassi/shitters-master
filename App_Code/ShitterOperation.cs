using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShitterOperation
/// </summary>
public class ShitterOperation : ISimulationEntity
{
    public int Id { get; set; }
    public string ShiterNumber { get; set; }
    public string PaperType { get; set; }
    public int Amount { get; set; }
    public int PartialAmount { get; set; }
    public double PartialAmountPercent { get; set; }
    public string CoverType { get; set; }
    public DateTime OpDate { get; set; }
    public string Customer { get; set; }
    public bool SetupReq { get; set; }


}