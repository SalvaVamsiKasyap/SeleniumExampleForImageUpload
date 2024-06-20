using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class SeleniumExampleForImageUpload
{
    public static void Main(string[] args)
    {
        // Initialize the Chrome WebDriver
        IWebDriver driver = new ChromeDriver();

        try
        {
            // Navigate to the first website
            driver.Navigate().GoToUrl("https://www.google.co.in/");

            // Take a screenshot
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"screenshot_{timestamp}.png";
            screenshot.SaveAsFile($"ScreenShots/{fileName}");

            // Navigate to the second website
            driver.Navigate().GoToUrl("https://west-wind.com/wconnect/wcscripts/FileUpload.wwd");

            //Uploading a file
            String filePath = $"C:\\Windows\\System32\\CSharp\\ScreenShots\\{fileName}"; 
            IWebElement fileInput = driver.FindElement(By.Id("ajaxUpload"));
            fileInput.SendKeys(filePath);
            
            // Validating the image upload by inspecting the image on the website
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement imageElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[id='ImageList'] img")));

        }
        catch (NoSuchElementException e)
        {
            // Handle case where element is not found
            Console.WriteLine("Error: Element not found! " + e.Message);
        }
        catch (WebDriverException e)
        {
            // Handle general WebDriver exceptions (e.g., network issues)
            Console.WriteLine("Error: WebDriver encountered an issue! " + e.Message);
        }
        catch (Exception e)
        {
            // Catch unexpected exceptions (use sparingly)
            Console.WriteLine("An unexpected error occurred! " + e.Message);
        }

        finally
        {
            // Close the browser
            driver.Quit();
        }
    }
}
