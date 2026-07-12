using PdfSharp.Fonts;
using System;
using System.IO;

public class WindowsFontResolver : IFontResolver
{
    public byte[] GetFont(string faceName)
    {
        string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        string fontFile = Path.Combine(fontsFolder, faceName);
        return File.ReadAllBytes(fontFile);
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string fileName;

        if (isBold && isItalic)
            fileName = "arialbi.ttf";
        else if (isBold)
            fileName = "arialbd.ttf";
        else if (isItalic)
            fileName = "ariali.ttf";
        else
            fileName = "arial.ttf";

        return new FontResolverInfo(fileName);
    }

}