using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.Modals
{
    public class FilterModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal";
        private const string ApplyButtonId = "modal__btn-apply";        
        private const string FilterIdPrefix = "filter-field__";
        private const string DimensionFilterIdPrefix = "filter-field__dimension-";
        private const string HierarchyDimensionFilterIdPrefix = "filter -field__hierarchical-construct-";
        private const string NoContentTextId = "filter-definition__no-content-text";
        private readonly By FilterLocator = By.XPath($".//*[contains(@data-auto-id,'{FilterIdPrefix}')]");
        private readonly By DimensionFilterLocator = By.XPath($".//*[contains(@data-auto-id,'{DimensionFilterIdPrefix}')]");
        private const string BoxFilterTypeClassName = "box__selection";
        private static readonly By BoxFilterLocator = By.XPath($".//*[contains(@class,'{BoxFilterTypeClassName}')]");
        private Dictionary<string, ItemSelect> _filterDictionary;
        private Dictionary<string, ItemSelect> _dimensionFilterDictionary;
        private Dictionary<string, ItemSelect> _hierarchyDimensionFilterDictionary;
        private const string ResetButtonId = "filter__reset";

        public FilterModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            _filterDictionary = FilterDictionary;
            _dimensionFilterDictionary = FilterDictionary.Where(kv => kv.Key.Contains(DimensionFilterIdPrefix)).ToDictionary(kv => kv.Key, kv => kv.Value);
            _hierarchyDimensionFilterDictionary = FilterDictionary.Where(kv => kv.Key.Contains(HierarchyDimensionFilterIdPrefix)).ToDictionary(kv => kv.Key, kv => kv.Value);
            Assert.IsTrue(IsModalDisplayed());
        }
        
        private IWebElement ApplyButton => _webDriver.SafeFindElementByDataAutoId(ApplyButtonId);

        private IReadOnlyCollection<IWebElement> Filters => _webDriver.FindElements(FilterLocator);

        private IWebElement ResetButton => _webDriver.SafeFindClickableElementByDataAutoId(ResetButtonId);

        private Dictionary<string, ItemSelect> FilterDictionary
        {
            get
            {
                var filterDictionary = new Dictionary<string, ItemSelect>();

                foreach (var filter in Filters)
                {
                    ItemSelect itemSelect;
                    var isBoxFilter = filter.FindElements(BoxFilterLocator).Any();
                    if (isBoxFilter)
                    {
                        itemSelect = new BoxItemSelect(_webDriver, filter);
                    }
                    else
                    {
                        itemSelect = new ListItemSelect(_webDriver, filter);
                    }

                    filterDictionary.Add(filter.GetAttribute("data-auto-id"), itemSelect);
                }

                return filterDictionary;
            }
        }

        private IWebElement NoContentText => _webDriver.SafeFindElementByDataAutoId(NoContentTextId);
        
        public string GetNoContentText() => NoContentText.Text;

        public bool IsApplyEnabled() => ApplyButton.Enabled;

        public PageBody Apply()
        {
            Assert.IsTrue(ApplyButton.Enabled);
            IWebElement modalWindow = ModalWindow;
            ApplyButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(modalWindow));
            return new PageBody(_webDriver);
        }

        public PageBody Cancel()
        {
            ClickCancelButton();            
            return new PageBody(_webDriver);
        }

        public FilterModal ResetToDefaults()
        {
            ResetButton.Click();
            return this;
        }

        public List<string> GetAllFilters() => _filterDictionary.Select(i => i.Key.ToString()).ToList();

        public ItemSelect GetFilter(string filterName) => _filterDictionary[filterName];

        public List<string> GetAllDimensionFilters() => _dimensionFilterDictionary.Select(i => i.Key.ToString()).ToList();

        public List<string> GetAllHierarchyDimensionFilters() => _dimensionFilterDictionary.Select(i => i.Key.ToString()).ToList();

        public ItemSelect GetDimensionFilter(string filterName) => _dimensionFilterDictionary[filterName];

        public class ItemSelect
        {
            protected IWebDriver _webDriver;
            protected IWebElement _parentElement;
            protected const string ItemSelectLabelClassName = "item-select__label";
            protected readonly By ItemSelectLabelLocator = By.XPath($".//*[contains(@class,'{ItemSelectLabelClassName}')]");
            protected const string ItemAllPlaceholderId = "item-All";
            protected const string ItemIdPrefix = "item-option-";
            protected readonly By ItemIdPrefixLocator = By.XPath($".//*[contains(@data-auto-id,'{ItemIdPrefix}')]");
            protected const string IsSelectedClassName = "is-selected";
            protected Dictionary<string, string> FilterOptionNameIdLookup;

            public ItemSelect(IWebDriver webDriver, IWebElement parentElement)
            {
                _webDriver = webDriver;
                _parentElement = parentElement;
            }
            private IWebElement ItemSelectLabel => _parentElement.FindElements(ItemSelectLabelLocator).FirstOrDefault();

            public string Name => ItemSelectLabel.Text;

            public virtual string GetSelectedFilterOption()
            {
                throw new NotImplementedException();
            }

            public virtual List<string> GetAllFilterOptions()
            {
                throw new NotImplementedException();
            }

            public virtual void ClearFilterOptions()
            {
                throw new NotImplementedException();
            }

            public virtual void SelectFilterOption(string optionName)
            {
                throw new NotImplementedException();
            }

            internal void BuildFilterOptionIdLookup()
            {
                FilterOptionNameIdLookup = _parentElement?.FindElements(ItemIdPrefixLocator).ToDictionary(el => el.Text, el => el.GetAttribute("data-auto-id"));
            }
        }

        private class ListItemSelect : ItemSelect
        {
            public ListItemSelect(IWebDriver webDriver, IWebElement parentElement) : base(webDriver, parentElement)
            {
                
            }

            public override string GetSelectedFilterOption()
            {
                var selectedOption = _parentElement?.FindElements(new ByAll(ItemIdPrefixLocator, By.ClassName(IsSelectedClassName))).FirstOrDefault()?.Text;
                return selectedOption;
            }

            public override List<string> GetAllFilterOptions()
            {
                BuildFilterOptionIdLookup();
                return FilterOptionNameIdLookup?.Keys.ToList();
            }

            public override void ClearFilterOptions()
            {
                Assert.IsTrue(GetAllFilterOptions().Any(o => o == "All"));
                SelectFilterOption("All");
                Assert.AreEqual("All", GetSelectedFilterOption());
            }

            public override void SelectFilterOption(string optionName)
            {
                string optionId = FilterOptionNameIdLookup[optionName];
                By optionItemLocator = By.XPath($".//*[@data-auto-id='{optionId}']");
                var option = _parentElement?.FindElements(optionItemLocator).FirstOrDefault();
                Assert.IsNotNull(option);
                option.Click();
                Assert.AreEqual(option.Text, GetSelectedFilterOption());
            }
        }

        private class BoxItemSelect : ItemSelect
        {
            private const string BoxClearButtonId = "item-select__clear";
            private const string BoxSelectionId = "box__selection";
            private const string BoxCloseButtonId = "box-list-close";
            private const string BoxSelectionPlaceholderClassName = "box__selection__placeholder";
            private const string BoxSelectionSelectedId = "box__selection__selected";
            private const string BoxSelectionListId = "item-select__box__list";
            private const string BoxSelectionUnderlayClassName = "box__list-underlay";
            private readonly By BoxSelectionSelectedLocator = By.XPath($".//*[@data-auto-id='{BoxSelectionSelectedId}']");

            public BoxItemSelect(IWebDriver webDriver, IWebElement parentElement) : base(webDriver, parentElement)
            {
                
            }

            public override string GetSelectedFilterOption()
            {
                var selectedOption = _parentElement?.FindElements(BoxSelectionSelectedLocator).FirstOrDefault()?.Text;
                return selectedOption;
            }
            public override List<string> GetAllFilterOptions()
            {
                OpenList();
                BuildFilterOptionIdLookup();
                CloseList();
                return FilterOptionNameIdLookup.Keys.ToList();
            }

            public override void ClearFilterOptions()
            {
                if (IsClearable())
                {
                    _parentElement?.FindElement(By.XPath($".//*[@data-auto-id='{BoxClearButtonId}']")).Click();
                }
                Assert.AreEqual("All", GetSelectedFilterOption());
            }

            public override void SelectFilterOption(string optionName)
            {
                OpenList();
                string optionId = FilterOptionNameIdLookup[optionName];
                By optionItemLocator = By.XPath($".//*[@data-auto-id='{optionId}']");
                var option = _parentElement?.FindElements(optionItemLocator).FirstOrDefault();
                Assert.IsNotNull(option);
                string optionText = option.Text;
                option.Click();
                Assert.AreEqual(optionText, GetSelectedFilterOption());
            }

            private bool IsClearable()
            {
                var clearElement = _parentElement.FindElements(By.XPath($".//*[@data-auto-id='{BoxClearButtonId}']")).FirstOrDefault();

                return clearElement?.Displayed ?? false;
            }

            private bool IsListVisible()
            {
                var listElement = _parentElement.FindElements(By.XPath($".//*[@data-auto-id='{BoxSelectionListId}']")).FirstOrDefault();

                return listElement?.Displayed ?? false;
            }

            private void OpenList()
            {
                _parentElement?.FindElements(By.XPath($".//*[@data-auto-id='{BoxSelectionId}']")).FirstOrDefault()?.Click();
                _webDriver.WaitUntil(driver => IsListVisible());
            }

            private void CloseList()
            {
                _parentElement?.FindElements(By.XPath($".//*[@data-auto-id='{BoxCloseButtonId}']")).FirstOrDefault()?.Click();
                _webDriver.WaitUntil(driver => !IsListVisible());
            }
        }
    }
}
