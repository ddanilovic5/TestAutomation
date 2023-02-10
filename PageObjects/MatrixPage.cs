using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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
        public MatrixPage()
        {
            navigationBar = new NavigationBar();
        }

        public NavigationBar navigationBar;

        private const string _url = "/matrices";
        private By MatrixTable => By.ClassName("[class='page-table']");
        private By SuccessMessageLocator => By.Id("swal2-title");
        private By DeleteMatrixButtonLocator => By.CssSelector(".delete-item");
        private By DeleteWarningLocator => By.CssSelector(".swal2-warning");

        private IWebElement SearchInput => Driver.Instance.FindElement(By.CssSelector(".page-table-search input"));
        private IWebElement CreateNewMatrixButton => Driver.Instance.FindElement(By.CssSelector(".btn-icon-plus"));
        private IWebElement NameInput => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='Name']"));
        private IWebElement Manufacturer => Driver.Instance.FindElement(By.TagName("select"));
        private IWebElement SaveNewMatrixButton => Driver.Instance.FindElement(By.XPath("//*[text()=' Create ']"));
        private IWebElement MatrixDropdown => Driver.Instance.FindElement(By.CssSelector("[class='nav-item dropdown']"));
        private IWebElement DeleteMatrixButton => Driver.Instance.FindElement(DeleteMatrixButtonLocator);
        private IWebElement DeleteWarningPopup => Driver.Instance.FindElement(DeleteWarningLocator);
        private IWebElement ConfirmDeletionButton => Driver.Instance.FindElement(By.CssSelector(".swal2-confirm"));
        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);

        public void ClickOnCreateNewMatrixButton()
        {
            CreateNewMatrixButton.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".modal-content")).Count != 0);
        }

        public void TypeMatrixName(string name)
        {
            NameInput.SendKeys(name);
        }

        public void SelectManufacturer(string manufacturer)
        {
            SelectElement dropdown = new SelectElement(Manufacturer);
            dropdown.SelectByText(manufacturer, true);
        }

        public void SaveNewMatrixClick()
        {
            SaveNewMatrixButton.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(SuccessMessageLocator).Count != 0);
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(MatrixTable).Count != 0);
        }

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

            Driver.Wait(20, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".page-title"))
                                                            .FirstOrDefault(x => x.Text == matrixName) != null);
                                                      
        }

        public void DeleteMatrix()
        {
            MatrixDropdown.Click();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(DeleteMatrixButtonLocator).Count != 0);
            DeleteMatrixButton.Click();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(DeleteWarningLocator).Count != 0);
            ConfirmDeletionButton.Click();

            Driver.Wait(3, () => Driver.Instance.FindElement(By.Id("swal2-title")).Text.Trim() == "Successfully deleted");
        }
    }
}
