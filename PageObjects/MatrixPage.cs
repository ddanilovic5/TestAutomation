using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    public class MatrixPage
    {
        private const string _url = "/matrices";
        private IWebElement SearchInput => Driver.Instance.FindElement(By.CssSelector(".page-table-search input"));
        private IWebElement TableMatrixName => Driver.Instance.FindElement(By.ClassName("table-matrix-name"));
        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);

        public void SearchForMatrix(string matrixName)
        {
            SearchInput.Click();
            SearchInput.Clear();
            SearchInput.SendKeys(matrixName);
            SearchInput.SendKeys(Keys.Enter);
        }

        public void OpenMatrixDetails(string matrixName)
        {
            SearchForMatrix(matrixName);

            ReadOnlyCollection<IWebElement> allMatrices = Driver.Instance.FindElements(By.ClassName("table-matrix-name"));
            IWebElement matrix = allMatrices.FirstOrDefault(x => x.Text == matrixName);

            if (matrix != null)
            {
                Actions action = new Actions(Driver.Instance);
                action.DoubleClick(matrix).Perform();
            }

            Driver.Wait(20, () => Driver.Instance.FindElementsNoWait(By.ClassName("ag-header-group-text"))
                                                            .FirstOrDefault(x => x.Text == "Product Offer Info") != null);
                                                      
        }
    }
}
