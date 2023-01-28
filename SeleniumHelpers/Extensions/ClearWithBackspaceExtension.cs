using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelpers.Extensions
{
    public static class ClearWithBackspaceExtension
    {
        public static void ClearWithBackspace(this IWebElement element)
        {
            var length = element.GetAttribute("value")?.Length ?? 0;

            if (length != 0)
            {
                for (var i = 0; i < length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
            else
            {
                // if GetAttribute("value") returns null, this will be executed.
                // element.Clear() will not remove text from input types
                element.SendKeys(Keys.Control + "a");
                element.SendKeys(Keys.Backspace);
            }
        }
    }
}
