namespace Com.Brother.Sdk.Lmprinter.Setting
{

  public partial class MWPrintSettings
  {

    public global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.Halftone? GetHalftone()
    {
      return Halftone;
    }

    public global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.PrintQuality? GetPrintQuality()
    {
      return PrintQuality;
    }

    public global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.ScaleMode? GetScaleMode()
    {
      return ScaleMode;
    }

    public void SetHalftone(global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.Halftone? p0)
    {
      Halftone = p0;
    }

    public void SetPrintQuality(global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.PrintQuality? p0)
    {
      PrintQuality = p0;
    }

    public void SetScaleMode(global::Com.Brother.Sdk.Lmprinter.Setting.IPrintImageSettings.ScaleMode? p0)
    {
      ScaleMode = p0;
    }
  }
}
