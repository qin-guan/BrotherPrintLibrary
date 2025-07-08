# `BrotherPrintLibrary`

This library provides bindings to
the [Brother Mobile SDK](https://support.brother.com/g/s/es/dev/en/mobilesdk/index.html?c=eu_ot&lang=en&navi=offall&comple=on&redirect=on).

Two variations of the library are published for the two suported major versions.

- `BrotherPrintLibraryV3`
- `BrotherPrintLibraryV4`

Minimal changes are made to the library, and code samples from Brother should _generally_ work without any issues.

## Get started

On Android, you need to grant these permissions (could be narrower, but I've never tested it out~):

`Platforms/Android/MainApplication.cs`

```csharp
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothAdmin)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothScan)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessWifiState)]
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
```

Code example for `BrotherPrintLibraryV3`, to print out basic text and QR code is provided below.

```csharp
var printer = new Printer();
printer.SetBluetooth(BluetoothAdapter.DefaultAdapter);

var scaleFactor = 4;
var baseTextSize = 150;
var basePadding = 20;
var baseQrSize = 256;

printer.SetPrinterInfo(new PrinterInfo
{
    WorkPath = Android.App.Application.Context.CacheDir?.ToString(),
    PrinterModel = PrinterInfo.Model.PtP710bt,
    MacAddress = "<SCAN_TO_RETRIEVE>",
    LocalName = "<SCAN_TO_RETRIEVE>",
    LabelNameIndex = LabelInfo.LabelColor.Red!.Id,
    IsAutoCut = true,
    IsLabelEndCut = true,
    IsHalfCut = false,
    IsSpecialTape = false,
    IsCutAtEnd = true,
    IsCutMark = true,
});

var qr = new ZXing.Android.BarcodeWriter
{
    Format = BarcodeFormat.QR_CODE,
    Options = new ZXing.Common.EncodingOptions
    {
        Margin = 2,
        Width = baseQrSize * scaleFactor,
        Height = baseQrSize * scaleFactor
    }
};

var qrBitmap = qr.WriteAsBitmap("https://www.example.com");

var textPaint = new Paint
{
    Color = Color.Black,
    SubpixelText = true,
    AntiAlias = true,
    TextSize = baseTextSize * scaleFactor,
};

var textToDraw = "https://example.com";
var textBounds = new Android.Graphics.Rect();
textPaint.GetTextBounds(textToDraw, 0, textToDraw.Length, textBounds);

int padding = 20 * scaleFactor;

var landscapeBitmap = Bitmap.CreateBitmap(
    qrBitmap.Width + textBounds.Width() + padding * 2,
    qrBitmap.Height,
    Bitmap.Config.Argb8888
);

var landscapeCanvas = new Canvas(landscapeBitmap);
landscapeCanvas.DrawColor(Color.White);
landscapeCanvas.DrawBitmap(qrBitmap, 0, 0, null);

float textY = (landscapeBitmap.Height + textBounds.Height()) / 2.0f;
landscapeCanvas.DrawText(textToDraw, qrBitmap.Width + padding, textY, textPaint);

var matrix = new Android.Graphics.Matrix();
matrix.PostRotate(90);

var finalBitmap = Bitmap.CreateBitmap(
    landscapeBitmap,
    0,
    0,
    landscapeBitmap.Width,
    landscapeBitmap.Height,
    matrix,
    true
);

var output = printer.PrintImage(finalBitmap);
```

## Copyright

Copyright 2025 Brother Mobile Solutions
