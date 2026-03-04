using System.Globalization;
using ChessExample;
using Xunit;

namespace App.Tests;

public class CheckerBoardPositionTests
{
    [Theory]
    [InlineData(1, 9)]
    [InlineData(9, 1)]
    public void CreatePostion_NotAllowedData_ThrowsArgumentOutOfRangeException(byte x, byte y)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new CheckerBoardPosition(x, y));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(5, 4)]
    public void CreatePosition_AllowedData_Success(byte x, byte y)
    {
        CheckerBoardPosition pos =  new CheckerBoardPosition(x, y);
        
        Assert.Equal(x, pos.X);
        Assert.Equal(y, pos.Y);
    }
    
    [Theory]
    [InlineData(1, 'A')]
    [InlineData(2, 'B')]
    [InlineData(3, 'C')]
    public void XLetter_ReturnsCorrectLetter(byte x, char letter)
    {
        CheckerBoardPosition pos =  new CheckerBoardPosition(x, 5);
        
        Assert.Equal(letter, pos.XLetter);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 5)]
    public void ToString_ReturnsCorrectChessPosition(byte x, byte y)
    {
        CheckerBoardPosition pos =  new CheckerBoardPosition(x, y);
        
        Assert.Equal($"{pos.XLetter}{y}", pos.ToString());
    }

    [Theory]
    [InlineData("B9")]
    [InlineData("I8")]
    [InlineData("A0")]
    [InlineData("BA")]
    [InlineData("BABA")]
    [InlineData("7B")]
    public void Parse_NotExistedPosition_ThrowsFormatException(string s)
    {
        Assert.Throws<FormatException>(() => CheckerBoardPosition.Parse(s, CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData("B7")]
    [InlineData("H8")]
    [InlineData("A1")]
    [InlineData("C3")]
    public void Parse_ExistedPosition_ParsedCorrect(string s)
    {
        CheckerBoardPosition pos = CheckerBoardPosition.Parse(s, CultureInfo.InvariantCulture);
        
        Assert.Equal(s, pos.ToString());
    }

    [Theory]
    [InlineData("B9")]
    [InlineData("N8")]
    [InlineData("ALT+F4")]
    [InlineData("BA")]
    [InlineData("BABA")]
    [InlineData("77B")]
    [InlineData("7A")]
    public void TryParse_NotExistedPosition_ReturnFalse(string s)
    {
        Assert.False(CheckerBoardPosition.TryParse(s, CultureInfo.InvariantCulture, out _));
    }

    [Theory]
    [InlineData("B5")]
    [InlineData("G8")]
    [InlineData("F1")]
    [InlineData("C8")]
    public void TryParse_ExistedPosition_ReturnsTrue(string s)
    {
        Assert.True(CheckerBoardPosition.TryParse(s, CultureInfo.InvariantCulture, out _));
    }
}