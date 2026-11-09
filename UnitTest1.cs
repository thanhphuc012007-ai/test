using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTest1;

public class Tests
{
    IWebDriver driver = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();

        driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(5);

        driver.Navigate().GoToUrl("https://www.google.com");
    }

    [TearDown]
    public void Cleanup()
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose(); // Giải phóng tài nguyên để khắc phục lỗi NUnit1032
        }
    }

    // TC01: Kiểm tra tiêu đề Google
    [Test]
    public void Test01_CheckGoogleTitle()
    {
        Assert.That(driver.Title, Does.Contain("Google"));
    }

    // TC02: Kiểm tra ô tìm kiếm
    [Test]
    public void Test02_CheckSearchBox()
    {
        IWebElement searchBox = driver.FindElement(By.Name("q"));

        Assert.That(searchBox.Displayed, Is.True);
    }

    // TC03, TC04, TC05: Kiểm tra nhập từ khóa
    [TestCase("Selenium")]
    [TestCase("NUnit")]
    [TestCase("FPT Polytechnic")]
    public void Test03_SearchKeyword(string keyword)
    {
        IWebElement searchBox = driver.FindElement(By.Name("q"));

        searchBox.SendKeys(keyword);

        Assert.That(
            searchBox.GetAttribute("value"),
            Is.EqualTo(keyword)
        );
    }
}